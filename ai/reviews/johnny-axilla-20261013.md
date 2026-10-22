# Passagem de trabalho — SKIN-034

- Responsável / destinatário / horário: Codex / próxima IA / 13/10/2026 13:34:43UTC (10:34BRT).
- Objetivo: região posterior da axila/deltoide afinada ao segurar arma, screenshots recebidos. Estado: instalada, validação novaUnity pendente.
- Base: SKIN-033 confirmou queixo corrigido no cliente real; braço continua com deformação leve. Preservar cabeça/pesos aprovados.
- Arquivos: `scratch/johnny034/`, `candidate-assets/johnny-solar/shoulders/`, recursoTPSsolar emanifesto. Fotos copiadas em `scratch/johnny034/client-shoulder-close/wide.png`.
- Hipótese: interpolar linearmente posições daT-pose com posições já rotacionadas comprime a transição do ombro antes da própria animação de mira; a ligação posterior fica estreita/alongada.
- Mudança:1576 vértices/normais na região dos ombros, transição horizontal0,110–0,325 com rotação interpolada por eixo/ângulo e escala longitudinal gradual. Entrada/saída vertical suave1,320–1,620; sem alterar braços distais/mãos/cabeça.
- Normais: calculadas pelo Jacobiano do deformador com diferença central1e−5, respeitando mudança do ângulo ao longo da superfície. Determinante mínimo0,267>0 e menor valor singular0,260, sem inversão local.
- Limite medido: deslocamento máximo0,032682 unidades demesh (~3,3cm na escala do modelo). Todos os pesos/boneindices033 iguais, além deUV/triângulos/bindposes/world; cabeça/queixo/mãos/normais externos à região iguais. Textura/material/rig/física/IK/armas intactos.
- Teste específico:IKanalítico de dois segmentos, mãos convergindo à frente emtrês alturas; hierarquia completa preservada. Não equivale a decodificar a animação nativa do jogo.
- Resultado:520triângulos da região posterior avaliados; p95 da razão de áreas na pose central2,887→2,218, alta3,006→2,282, baixa2,493→2,131. Menos alongamento extremo nesta amostra; não interpretar área como medida exata de volume anatômico.
- Testes:37poses sintéticas (34anteriores+3grips), p99≤1,286/max≤2,826, jawrigid continua com erro<1e−6. EstruturaUnity/reabertura/pesos/preservações/Pythoncompile aprovados.
- Prévia: `rear-before-gripHigh.png`/`rear-gripHigh.png`, pose central efront/side/back em `candidate-assets/johnny-solar/shoulders/`; melhoria visual moderada, cabeça aprovada não alterada.
- Oracle: doisarquivos atualizados/CRC26101303/5559entradas, semreinício/PID709544 mantido; guarda de hashes,4HTTP1.1.1/1.3.5 eTCP7777 aprovados; locais sincronizados.
- Backups: local `backups/johnny034-before/`; remoto `/home/ubuntu/backups/johnny-solar-skin034-20261013T133443Z/runtime.tar.gz`.
- Cliente real: queixo033 confirmado bom; ombros034 ainda não testados. Próximo passo: reiniciar cliente e repetir pose de arma mostrada nas fotos, olhando axila por trás e durante mira/recarga.
- Base publicada para ajustes futuros: `candidate-assets/johnny-solar/shoulders/`; scripts e métricas em `scratch/johnny034/` evalidation.json/grip-validation.json. Reserva liberada; sembloqueio.
