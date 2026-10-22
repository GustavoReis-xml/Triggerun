# Passagem de trabalho — SKIN-011

- Responsável: Codex,08/10/2026; inspeção validada, adaptação ainda não implementada.
- Objetivo: verificar GLB Scarlet recebido e viabilidade de adaptação à Celina sem alterar execução.
- Fonte: C:/Users/Gustavo/Downloads/scarlet_proxy__free_sci-fi_charchter_riged.glb;9062576bytes. Original somente lido.
- Evidência: scratch/scarlet-inspection/{report.json,gltf.json,texture-0.png..texture-6.png}; scripts inspect_glb.py e compare_rig.py.
- GLB2 íntegro, sem Draco; três malhas com UVs/normais/tangentes/pesos: corpo14616v/23686tri, cabelo2878v/3509tri, katana724v/1356tri. Total28551tri; sem katana27195tri.
- Um skin93joints; pesos4influências somam aproximadamente1 em todas as malhas. Articulações de coluna/pescoço/braços/dedos/pernas presentes; bones extras para rosto/cabelo/twist.
- Sete PNG embutidos1024x1024 (não4K como anúncio); dois materiais PBR especular/gloss com normais/emissão. Materiais doubleSided, sem alphaMode explícito; verificar aparência do cabelo antes de converter.
- Uma animação Idle_Main,275canais. Não corresponde às animações de combate do jogo.
- Metadados: autorMoonMoon, fonteSketchfab e licençaCC-BY-SA4.0 preservados noGLB; página anunciava condições divergentes. Não interpretar download como licença irrestrita.
- Comparação: Celina alvo62bones; não pode usar skin93joints diretamente. Mapear anatomia/twist/dedos, obter repouso pelas inverseBindMatrices e converter para62bones mantendo animações originais.
- Transformações: coordenadas locais da malha e transforms de nodes produzem escalas/eixos diferentes dos bones em cena; não ajustar por AABB de POSITION bruto nem aplicar nodeWorld sem skinning. Usar inverseBindMatrices e pose de repouso para normalização.
- Decisão: candidato viável para protótipoCelina, sem garantia visual/animação. Omitir katana no primeiro teste; preservar arma e gameplayCelina. Converter materiais ao shader suportado; aparênciaPBR exata não garantida.
- Testes: parser cabeçalho/tamanhoGLB, acessores/pesos/texturas/transformações lidos com sucesso. Adaptação/posesUnity não testadas.
- Cliente real: não testado. Oracle: não alterada, permaneceSKIN007.
- Próximo: construir candidato separado com rigCelina, prévias e poses sintéticas; não substituir default ou instalar antes da revisão visual.
- Reserva de inspeção liberada.
