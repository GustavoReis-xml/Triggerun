using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Microsoft.Win32;

namespace TriggerunLauncher
{
    public class LauncherConfig
    {
        public string Username { get; set; } = "Jogador";
        public string ServerIp { get; set; } = "127.0.0.1";
        public int WebPort { get; set; } = 80;
        public int GamePort { get; set; } = 7777;
        public string Language { get; set; } = "baxi";
        public bool CloseOnLaunch { get; set; } = true;
        public string CustomExePath { get; set; } = "";
    }

    public partial class MainWindow : Window
    {
        private readonly string _configFilePath;
        private LauncherConfig _config = new();
        private System.Windows.Threading.DispatcherTimer? _pingTimer;

        public MainWindow()
        {
            InitializeComponent();
            _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launcher_config.json");
            
            LoadConfig();
            InitPingTimer();
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    string json = File.ReadAllText(_configFilePath);
                    _config = JsonSerializer.Deserialize<LauncherConfig>(json) ?? new LauncherConfig();
                }
            }
            catch
            {
                _config = new LauncherConfig();
            }

            TxtUsername.Text = string.IsNullOrWhiteSpace(_config.Username) ? "Jogador" : _config.Username;
            TxtServerIp.Text = string.IsNullOrWhiteSpace(_config.ServerIp) ? "127.0.0.1" : _config.ServerIp;
            ChkCloseOnLaunch.IsChecked = _config.CloseOnLaunch;

            if (_config.Language == "zh")
                CmbLanguage.SelectedIndex = 1;
            else
                CmbLanguage.SelectedIndex = 0;
        }

        private void SaveConfig()
        {
            try
            {
                _config.Username = TxtUsername.Text.Trim();
                _config.ServerIp = TxtServerIp.Text.Trim();
                _config.CloseOnLaunch = ChkCloseOnLaunch.IsChecked ?? true;
                
                if (CmbLanguage.SelectedItem is ComboBoxItem item && item.Tag is string lang)
                {
                    _config.Language = lang;
                }

                string json = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_configFilePath, json);
            }
            catch
            {
                // Ignora falhas de escrita
            }
        }

        private void InitPingTimer()
        {
            _pingTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(4)
            };
            _pingTimer.Tick += async (s, e) => await CheckServerStatusAsync();
            _pingTimer.Start();

            Loaded += async (s, e) => await CheckServerStatusAsync();
        }

        private async Task CheckServerStatusAsync()
        {
            string rawIp = TxtServerIp.Text.Trim();
            if (string.IsNullOrWhiteSpace(rawIp)) return;

            string host = rawIp;
            int port = 7777;

            if (rawIp.Contains(':'))
            {
                var parts = rawIp.Split(':');
                host = parts[0];
                if (int.TryParse(parts[1], out int customPort))
                    port = customPort;
            }

            var sw = Stopwatch.StartNew();
            bool isOnline = false;

            try
            {
                using var tcpClient = new TcpClient();
                var connectTask = tcpClient.ConnectAsync(host, port);
                var timeoutTask = Task.Delay(1500);

                var completed = await Task.WhenAny(connectTask, timeoutTask);
                if (completed == connectTask && tcpClient.Connected)
                {
                    isOnline = true;
                    sw.Stop();
                }
            }
            catch
            {
                isOnline = false;
            }

            if (isOnline)
            {
                StatusDot.Fill = new SolidColorBrush(Color.FromRgb(0x3F, 0xB9, 0x50));
                StatusDot.Effect = new DropShadowEffect { Color = Color.FromRgb(0x3F, 0xB9, 0x50), BlurRadius = 10, Opacity = 0.9 };
                TxtServerStatus.Text = "ONLINE";
                TxtServerStatus.Foreground = new SolidColorBrush(Color.FromRgb(0x3F, 0xB9, 0x50));
                TxtServerLatency.Text = $"Ping: {sw.ElapsedMilliseconds} ms";
            }
            else
            {
                StatusDot.Fill = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                StatusDot.Effect = new DropShadowEffect { Color = Color.FromRgb(0xF8, 0x51, 0x49), BlurRadius = 8, Opacity = 0.8 };
                TxtServerStatus.Text = "OFFLINE";
                TxtServerStatus.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                TxtServerLatency.Text = "Não foi possível conectar";
            }
        }

        private string? FindGameExecutable()
        {
            if (!string.IsNullOrEmpty(_config.CustomExePath) && File.Exists(_config.CustomExePath))
            {
                return _config.CustomExePath;
            }

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] searchNames = new[] { "TRIG.exe", "Triggerun.exe", "Trigger.exe", "game.exe", "AutoUpdate.exe" };

            // 1. Procura na pasta atual
            foreach (var name in searchNames)
            {
                string fullPath = Path.Combine(baseDir, name);
                if (File.Exists(fullPath)) return fullPath;
            }

            // 2. Procura na subpasta GAME
            string subGameDir = Path.Combine(baseDir, "GAME");
            if (Directory.Exists(subGameDir))
            {
                foreach (var name in searchNames)
                {
                    string fullPath = Path.Combine(subGameDir, name);
                    if (File.Exists(fullPath)) return fullPath;
                }
            }

            // 3. Procura no diretório padrão C:\trig2\GAME
            string defaultDir = @"C:\trig2\GAME";
            if (Directory.Exists(defaultDir))
            {
                foreach (var name in searchNames)
                {
                    string fullPath = Path.Combine(defaultDir, name);
                    if (File.Exists(fullPath)) return fullPath;
                }
            }

            // 4. Procura na pasta pai
            string? parentDir = Directory.GetParent(baseDir)?.FullName;
            if (parentDir != null)
            {
                foreach (var name in searchNames)
                {
                    string fullPath = Path.Combine(parentDir, name);
                    if (File.Exists(fullPath)) return fullPath;
                }
            }

            return null;
        }

        private async void BtnClearCache_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xF0, 0xFF));
                TxtLaunchError.Text = "Limpando cache do jogo...";

                await Task.Run(() =>
                {
                    string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string localLow = Path.Combine(userProfile, "AppData", "LocalLow");

                    // 1. Pasta huoshengame (XMLs e dados)
                    string huoshenDir = Path.Combine(localLow, "huoshengame");
                    if (Directory.Exists(huoshenDir))
                    {
                        try { Directory.Delete(huoshenDir, true); } catch { }
                    }

                    // 2. Unity WebPlayer Cache (Shaders e AssetBundles)
                    string unityCacheDir = Path.Combine(localLow, "Unity", "WebPlayer", "Cache");
                    if (Directory.Exists(unityCacheDir))
                    {
                        try
                        {
                            var huoshenCaches = Directory.GetDirectories(unityCacheDir, "huoshengame_*");
                            foreach (var dir in huoshenCaches)
                            {
                                try { Directory.Delete(dir, true); } catch { }
                            }
                        }
                        catch { }
                    }

                    // 3. Game local GameCache
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string localGameCache = Path.Combine(baseDir, "GameCache");
                    if (Directory.Exists(localGameCache))
                    {
                        try { Directory.Delete(localGameCache, true); } catch { }
                    }

                    string defaultGameCache = @"C:\trig2\GAME\GameCache";
                    if (Directory.Exists(defaultGameCache))
                    {
                        try { Directory.Delete(defaultGameCache, true); } catch { }
                    }
                });

                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0x3F, 0xB9, 0x50));
                TxtLaunchError.Text = "✅ Cache limpo com sucesso! Os arquivos serão baixados zerados ao jogar.";
            }
            catch (Exception ex)
            {
                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                TxtLaunchError.Text = $"Erro ao limpar cache: {ex.Message}";
            }
        }

        private void BtnLaunchGame_Click(object sender, RoutedEventArgs e)
        {
            TxtLaunchError.Text = "";
            string username = TxtUsername.Text.Trim();
            string serverIp = TxtServerIp.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                TxtLaunchError.Text = "Por favor, digite um Nickname antes de iniciar.";
                return;
            }

            if (string.IsNullOrWhiteSpace(serverIp))
            {
                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                TxtLaunchError.Text = "Por favor, informe o IP do servidor.";
                return;
            }

            SaveConfig();

            string? exePath = FindGameExecutable();

            if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
            {
                var dialog = new OpenFileDialog
                {
                    Title = "Selecione o executável do Trigger Run (TRIG.exe ou Triggerun.exe)",
                    Filter = "Executável do Jogo (*.exe)|*.exe|Todos os Arquivos (*.*)|*.*",
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
                };

                if (dialog.ShowDialog() == true)
                {
                    exePath = dialog.FileName;
                    _config.CustomExePath = exePath;
                    SaveConfig();
                }
                else
                {
                    TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                    TxtLaunchError.Text = "Executável do jogo não encontrado.";
                    return;
                }
            }

            try
            {
                string lang = (_config.Language == "zh") ? "zh" : "baxi";
                int gamePort = 7777;

                string hostOnly = serverIp;
                if (serverIp.Contains(':'))
                {
                    var parts = serverIp.Split(':');
                    hostOnly = parts[0];
                    if (int.TryParse(parts[1], out int p)) gamePort = p;
                }

                string tcpAddr = $"{hostOnly}:{gamePort}";
                string httpUrl = $"http://{hostOnly}";

                // Montagem do protocolo WebParam do Trigger Run (34 argumentos exatos)
                string[] pArray = new string[34];
                pArray[0] = "1";
                pArray[1] = username;
                pArray[2] = "BR";
                pArray[3] = "123456";
                pArray[4] = hostOnly;
                pArray[5] = "steam";
                pArray[6] = tcpAddr;
                pArray[7] = gamePort.ToString();
                pArray[8] = "1";
                pArray[9] = "1.3.5";
                pArray[10] = "ongame";
                pArray[11] = httpUrl;
                pArray[12] = httpUrl;
                pArray[13] = lang;
                pArray[14] = "1";
                pArray[15] = "";
                pArray[16] = "1";
                pArray[17] = "";
                pArray[18] = "";
                pArray[19] = "";
                pArray[20] = "1";
                pArray[21] = "0";
                pArray[22] = "0";
                pArray[23] = "";
                pArray[24] = "";
                pArray[25] = "";
                pArray[26] = "";
                pArray[27] = "";
                pArray[28] = "";
                pArray[29] = "";
                pArray[30] = tcpAddr;
                pArray[31] = gamePort.ToString();
                pArray[32] = hostOnly;
                pArray[33] = "0";

                string launchArg = string.Join("|", pArray);

                var startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = $"\"{launchArg}\"",
                    WorkingDirectory = Path.GetDirectoryName(exePath) ?? AppDomain.CurrentDomain.BaseDirectory,
                    UseShellExecute = true
                };

                Process.Start(startInfo);

                if (ChkCloseOnLaunch.IsChecked == true)
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                TxtLaunchError.Foreground = new SolidColorBrush(Color.FromRgb(0xF8, 0x51, 0x49));
                TxtLaunchError.Text = $"Erro ao iniciar jogo: {ex.Message}";
            }
        }

        private async void BtnRefreshStatus_Click(object sender, RoutedEventArgs e)
        {
            TxtServerStatus.Text = "Testando...";
            await CheckServerStatusAsync();
        }

        private async void TxtServerIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(600);
            await CheckServerStatusAsync();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
            Application.Current.Shutdown();
        }
    }
}
