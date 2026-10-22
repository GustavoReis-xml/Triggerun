const scope = require('./sceneScope');
const {encodeInt} = require('./utils');

function createModelSwapper(state, catalog) {
    const keys = new Map(catalog.map(e => [e.key, e]));
    return function swap(input) {
        let id = Number(input.visualHeroId || 0);
        if (input.entityKey) {
            const entry = keys.get(input.entityKey);
            if (!entry || entry.kind !== 'robot') throw Error('Selecione uma definição de bot do catálogo.');
            id = -entry.id;
        } else if (!Number.isInteger(id) || id < 0 || (id !== 0 && !keys.has('hero:' + id))) {
            throw Error('Modelo de herói inválido. Para monstros use o seletor de bots.');
        }
        const player = state.activeSockets.find(p => String(p.playerId) === String(input.playerId) && scope.canReceive(p,p));
        if (!player || player.isDead) throw Error('Selecione um jogador vivo que terminou de carregar a partida.');
        player.visualHeroId = id || null;
        const packet = `e${encodeInt(player.playerIndex || Number(player.playerId))}:NVM,((${encodeInt(id)}))\n`;
        state.activeSockets.forEach(peer => { if (scope.canReceive(player,peer)) peer.write(packet); });
        return {ok:true, message:id ? 'Aparência aplicada. Arma e controles continuam sendo do herói original.' : 'Aparência original restaurada.'};
    };
}
module.exports = {createModelSwapper};
