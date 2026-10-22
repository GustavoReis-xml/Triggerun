# Passagem — LAMSTER-004

- Responsável: Codex;07/10/2026. Objetivo: secundário de inspiração em8m,CD6s e falas1007.
- Estado: implementado/testado/instalado em07/10 22:58UTC (19:58 BRT); teste Unity pendente.
- Cliente real anterior: usuário relata Lamster ficou legal após LAMSTER-003; novo secundário ainda não testado.
- Dados nativos: buff1007 tipo6,valor25,duração5000ms; buffMe só toca fala se soundbuff=1. Recursos existentes6 classes: Jipusai,Jvjishou,LeiZhenZi,Terminator,Valkyries,XingJiLieRen.
- Alteração: secundário mantém cura360, inspira aliados e próprio Lamster1007(+25%/5s),250 dano base a inimigos em8m;CD6000ms/launchershootspeed10; filtros layer0 para ambos lados.
- Self buff: SB nativo do secundário aplica1007 mesmo sozinho/sem alvos; healAlly permite alvo self apenas2 comcura0 e cooldown. SDG de aliados concede buff mesmo com vida cheia, cura só vida faltante; inimigos seguem dano normal e canStrikeEnemy2 respeita8m/CD6s.
- Bônus ofensivo: SDG aplica1,25 ao atacante com1007 ativo; não amplifica o próprio pulso secundário250. Proteções/debuffs do alvo continuam válidos. Outros caminhos de dano fora do SDG não foram ampliados.
- Áudio: helper TriggerunBuffVoices.TryLoad intercepta somente1007; nomes normalizados/arquivos baxi existentes, sem download para heróis sem gravação. Corrige callback assíncrono original que passava0 em vez de buffId, impedindo cache/reprodução subsequentes.
- Patch cliente:1 método CPlayerSound.LoadBuffSound;14.780 métodos comparados. Mantém filtro material Lamster,HUD,HIT,MODEL.
- Arquivos:src/{socketServer,lamster}.js,XMLs baxi/zh weapon/buff/heroskillinfo,manifesto26100704,CombatHud.cs,BuffVoices.cs,tools/patch-buff-voices.ps1 e testes.
- Testes:101/101 servidor locais;6 XMLs+manifesto parseados;helper compilado contra bibliotecas reais;6 verificações simuladas de voz (cache/download/semasset/idioma/buffId). Bônus25% e expiração testados em ataque de outro herói.
- Candidatos:scratch/lamster-rally/{Assembly-CSharp,TriggerunCombatHud}.dll;patch-result.json;deploy/{bundle.tar.gz,base.sha256,new.sha256,install.sh}.
- Hash Assembly candidato900A03FC5FE55D21208705750EA66539F9F58655D70370945FFC2C246149EAD4; base0373341552C5D8E1497E34494630733F7587704665EACBEE2B87F5EA75F5BDA6.
- Backup local:backups/lamster-rally-20261007-before/; cliente e runtime anteriores preservados.
- Oracle: instalada após usuário fechar clientes e gatezero;101/101 testes;backup /home/ubuntu/backups/lamster-rally-20261007T225833Z/{runtime,logs}.tar.gz;PM2 online PID594337/restart187;HTTP200/porta7777 confirmados após inicialização.
- Cliente: duasDLLs instaladas com jogo fechado/hashbase conferido. Helper SHA256 A2E0E903F4E64B8400C4CB486FE4CF5E8D25F289BB0B6B9C0A3D996A7D4D2756. Próximo: reabrir e validar cenários Unity abaixo.
- Teste Unity: CD6s; selfbuff sozinho; aliado ferido/cheio/em8m; inimigo250; fora8m e paredes; buffs/falas disponíveis; eliminação por pulso; potência+25% expira5s.
