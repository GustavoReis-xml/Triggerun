# Passagem de trabalho — SKIN-022

- Responsável: Codex; instalação 13/10/2026 00:41 UTC (12/10 BRT).
- Objetivo: FPS exclusivo da Celina Executiva8 com mãos sem luvas, mangas vermelhas, punhos brancos e relógio esquerdo.
- Estado: implementação validada e instalada; teste no cliente real pendente.
- Base: FPS nativo mulan_fps_sz2 (skin3); originais preservados.
- Alteração: malha nativa das mãos, mangas com volume, remoção de pulseiras antigas, punhos e relógio modelados. Apenas Executiva8 recebe skinname1=xingjilieren_fps_red.
- Validação automatizada: 203 objetos comparados; 40 bones/bindposes preservados; 926 vértices dos dedos exatamente originais; pesos/índices válidos. Arma e controlador originais.
- Arquivos: public/baxi/herounlock.xml, zh/herounlock.xml, files_dir.xml e novo HeroAnimator/xingjilieren_fps_red.mo. Sem alteração de DLL.
- Oracle: instalada com CRC26101202/5556 entradas; catálogo validado; reinício autorizado com 1 conexão; PM2 online PID698981.
- Verificação após instalação: 6 downloads com hashes iguais à candidata, manifesto e TCP7777 OK; cópia local sincronizada após conferir hashes de base.
- Cliente real: não testado; não confundir inspeção de bundle com animação validada em Unity.
- Limites visuais: tecido vermelho simples com relevo geométrico; relógio simplificado; unhas originais preservadas.
- Backups: backups/celina022-before; Oracle /home/ubuntu/backups/celina-fps-skin022-20261013T004154Z/runtime.tar.gz.
- Evidências: candidate-assets/celina-red-fps/{report.json,validation.json,preview.png}; scratch/celina-red-fps-release/http-validation.json.
- Próximo passo: reabrir o jogo, equipar Executiva e verificar mira, disparo, recarga e alinhamento dos punhos.
- Reserva liberada; diagnóstico SKIN-020 e correção VALK-001 não alterados.
