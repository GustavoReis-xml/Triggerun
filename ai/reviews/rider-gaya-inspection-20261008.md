# Passagem — SKIN-018 inspeção Rider/Gaya

- Responsável: Codex, 08/10/2026. Estado: inspeção concluída.
- Fonte: Downloads/psp_-_fate_unlimited_codes_-_rider_2.glb,478600bytes.
- Metadados: pve771309/Sketchfab, licença declarada CC-BY-4.0; referência completa em scratch/rider-inspection/report.json.
- 40 primitivas,8982vértices/11026triângulos; cinco PNGs de16x32 até128x128.
- Nenhuma skin, nenhum atributo JOINTS/WEIGHTS, nenhuma animação: modelo estático, requer criação de pesos.
- Gaya1036: meidusha_tps.mo,14SkinnedMeshRenderers,276Transform distintos referenciados (inclui extras/cabelo).
- Bones anatômicos Bip001 disponíveis; nomes BoneNN repetidos exigem identificar Transform por ID/hierarquia, nunca somente nome.
- Decisão: viável como candidata independente; transferir pesos do corpo original e tratar cabelo separadamente. Aparência PSP de baixa resolução permanecerá perceptível.
- Evidências: scratch/rider-inspection/{gltf.json,report.json,comparison.json,texture-*.png}.
- Testes: leitura GLB/texturas/estrutura do bundle; sem protótipo, poses ou teste Unity.
- Oracle/cliente: intactos nesta tarefa.
- Próximo passo: preparar alinhamento visual e pesos; validar ombros/quadril/cabelo antes de cadastro.