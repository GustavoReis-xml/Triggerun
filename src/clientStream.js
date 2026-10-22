// BaseSocket.SendMessageStr terminates commands with CRLF. TCP may split a
// command or coalesce commands, including the plaintext NGOL / crypto switch.
function consume(socket, data, onMessage) {
    if (!socket.incomingBytes) socket.incomingBytes = [];
    for (const byte of data) {
        const value = socket.isEncrypted ? (((byte ^ 173) - 37 + 256) % 256) ^ 227 : byte;
        if (value === 10) {
            const message = Buffer.from(socket.incomingBytes).toString('utf8').replace(/\r$/, '');
            socket.incomingBytes = [];
            if (message) onMessage(message);
        } else {
            socket.incomingBytes.push(value);
        }
    }
}
module.exports = { consume };
