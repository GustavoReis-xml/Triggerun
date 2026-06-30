const ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_`abcdefghijklmnopqrstuvwxyz";

function decodeHex(hexStr) {
    if (!hexStr) return "";
    let str = "";
    for (let i = 0; i < hexStr.length; i += 2) {
        str += String.fromCharCode(parseInt(hexStr.substr(i, 2), 16));
    }
    return str;
}

function getPlayerNameFromNgol(texto) {
    if (!texto) return null;
    const parts = texto.split(",");
    if (parts.length >= 5) {
        let namePart = parts[4].trim();
        // Remove single quotes, double quotes, parentheses, newlines
        namePart = namePart.replace(/['"\(\)\r\n]/g, "");
        
        // Se parecer hexadecimal (apenas caracteres 0-9 a-f A-F), decodificamos
        if (/^[0-9a-fA-F]+$/.test(namePart) && namePart.length % 2 === 0) {
            try {
                const dec = decodeHex(namePart);
                if (dec) {
                    return dec;
                }
            } catch(e) {}
        }
        return namePart;
    }
    return null;
}

// Test cases
const lobbyText1 = "NGOL'HallGroup','HallUser','1','1',Jogador_1";
const lobbyText2 = "NGOL'HallGroup','HallUser','1','1',Jogador_2\n";
const sceneText1 = "NGOL'SceneGroup','PlayerUser','1','1',4a6f6761646f725f31\n"; // Jogador_1 in hex
const sceneText2 = "NGOL'SceneGroup','PlayerUser','1','1',4a6f6761646f725f32"; // Jogador_2 in hex

console.log("Lobby 1:", getPlayerNameFromNgol(lobbyText1)); // Should be Jogador_1
console.log("Lobby 2:", getPlayerNameFromNgol(lobbyText2)); // Should be Jogador_2
console.log("Scene 1:", getPlayerNameFromNgol(sceneText1)); // Should be Jogador_1
console.log("Scene 2:", getPlayerNameFromNgol(sceneText2)); // Should be Jogador_2
