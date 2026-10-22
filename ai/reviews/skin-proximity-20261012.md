# Passagem — SKIN-020

- Responsável: Codex; objetivo: Hércules/Rider estáveis ao aproximar jogadores.
- Estado: investigação; diagnóstico local instalado, correção ainda não determinada.
- Cliente real: usuário relata corpo inteiro empurrado, com saltos; Scarlet não apresenta o problema. Não confirmou se depende de heróis iguais.
- Confirmado: referências dos ossos das cinco malhas Hércules e dos LODs Rider pertencem ao próprio prefab; não apontam para outro personagem. Auditoria em scratch/skin-proximity/audit.py e *-hierarchy.json.
- Diferença: Hércules ampliou paletas para44bones; Rider para64; Scarlet manteve62. Não estabelece causalidade.
- Limites de renderização foram gravados no espaço da malha nas três skins; Rider também substituiu limites999 de efeitos. Defeito separado possível, sem evidência de que produza empurrão físico.
- Física original dos bundles foi preservada pelas adaptações; TBCharacterThirdIt.Move alterna velocidade e correção para moveTarget, compatível com saltos se houver deslocamento físico, ainda hipótese.
- Implementação: SkinProximity.cs identifica as3skins, registra control/modelLocal/Bip001/velocidade/alvo remoto e contatos entre jogadores a cada segundo quando próximos (<3m). Não altera transforms, rigidbodies, colisores, animação ou rede; adiciona apenas observadores.
- Helper CombatHud:29métodos existentes comparados; só Ensure mudou para iniciar o observador. Compilação contra DLLs efetivamente instaladas OK.
- Instalação: cliente fechado; cópia/hashes verificados, backup backups/skin020-before; scratch/skin-proximity/installation.json. Assembly-CSharp/Valk e Oracle intactos.
- Teste automatizado: auditoria hierarquia/bindposes e comparação IL concluídas; não simula física Unity nem confirma causa.
- Próximo passo: usuário reproduzir aproximação por~10s em dois clientes; ler output_log antes de novas sessões sobrescreverem. Comparar [TRIG SkinNear]/[TRIG SkinContact] e posição real/alvo/ossos para escolher correção.
- Não declarar bug resolvido; nenhuma correção de física foi instalada nesta etapa.
