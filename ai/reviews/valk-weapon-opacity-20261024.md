# Passagem de trabalho — WEAPON-003

- Responsável / horário: Codex; instalada25/10/2026 02:19:25UTC (24/10 23:19BRT).
- Objetivo: corrigir espada quase invisível na loja/FPS após instalação002;FX já apareciam nas capturas do usuário.
- Estado: causa reproduzida, correção instalada; novo teste visual Unity pendente.
- Base: bundle002 SHA6114A0B05174118695B02F58D56050BBAC0F13A0AC90854A6F3B6A615F655D30,manifesto19ED8E7CDDF9DB18571F2A0A4A91EE169535C08C44B5ED8EB5105AB2FABE4D98.
- Reserva: scratch/weapon063/, candidate-assets/valk-eclipse/weapon/opaque/,bundleEclipse/manifesto. Nenhuma DLL/servidorJS/catálogo alterados.
- Evidência: output_log copiado emscratch/weapon063/output_log.txt confirma download/cache de tr_weapon_valk_eclipse.mo?v=26102408 e carregamento dohelper;semexceçãoWeaponCosmetics.
- Causa confirmada: albedo importado emDXT5 tinhaalpha0 emtoda imagem. Shader nativo Custom/ttcq tem TransparentCutout/_Cutoff0,5;superfície descartada enquantoFXindependentes continuam.
- Reprodução: compressão da imagem RGB2048 real voltou aproduziralpha0;RGBA explícito gera255. Amostra pequena de coruniforme NÃO reproduziu,portanto não usar como substituto da textura real.
- Correção mínima: substituir apenas8bytes de alpha porblocoDXT5 (endpoints255/índices0) emtodos os10mips. BlocosRGB comprimidos eRGB decodificado idênticos,sem recompressão decores.
- Fonte atual: `candidate-assets/valk-eclipse/weapon/opaque/tr_weapon_valk_eclipse.mo`;002/adapted é histórico e contémalpha errado. Backup embackups/weapon063-before/.
- Testes: scratch/weapon063/fix-and-verify.py PASS;12objetos comparados, sóTexture2D900000000000006201 alterado;10mips2048→4 comalpha255;geometria,material,shader,XML,FX,atlasRGB intactos.
- Prevenção: scratch/weapon062/build-assets.py passaRGBA explícito e verificaalpha;contrato emclient-patches/WEAPON-COSMETICS.md exige reabrir e decodificarmips. Não reconstruí002/adapted nemreinstaleiDLLs.
- Oracle:2arquivos instalados comguardasSHA/backup antes alterações;CRC26102409/5566recursos,mesmoPID928695,semreinício/desconexão.
- Backup remoto: /home/ubuntu/backups/weapon-eclipse-063-20261025T021925Z/runtime.tar.gz.
- Publicação:4HTTP hashes (bundle+manifesto ×1.1.1/1.3.5) eTCP7777 aprovados;arquivoslocaissincronizados comguardas.
- Cliente real: usuário comprovou falha002 porcapturas. Revisão003ainda não vista noUnity;orientar fechar/abrirjogo para releituranovomanifesto/cache. DLLs/pacotecliente002 continuam válidos.
- Próximo passo: retestar arma5na loja/FPS/TPS e manterchecagemalphaem futuras importações. Não aumentar espera decache semverificarevidências.
- Reserva liberada.
