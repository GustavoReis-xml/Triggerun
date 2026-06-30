// ============================================================================
// SERVIDOR TRIGGER RUN - PONTO DE ENTRADA PRINCIPAL
// ============================================================================
// Para garantir que todos os novos patches de customização (skins, armas, emotes,
// poses, sprays, runas/genes), correções de bugs de interface, e o gerenciamento 
// modularizado funcionem corretamente, este arquivo agora redireciona a execução 
// para o diretório unificado `./src/index.js`.
//
// Para realizar quaisquer alterações na lógica do servidor, edite os arquivos 
// correspondentes dentro da pasta `./src/`.
// ============================================================================

require('./src/index.js');
