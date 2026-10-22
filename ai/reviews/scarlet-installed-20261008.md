# Passagem de trabalho — SKIN-014

- Responsável: Codex;08/10/2026, instalação23:30UTC(20:30BRT). Estado: implementada/instalada; testeUnity pendente.
- Objetivo: usuário aprovouSKIN013 e autorizou prosseguir; disponibilizar Scarlet como skin separadaCelina.
- Cadastro: Celina1017,skin7 Scarlet Proxy gratuita,sem bônus; íconecelina_scarlet,bundleTPSxingjilieren_tps_scarlet. Clássica/Aurora e seleção de arma preservadas.
- FPS: usa mãos nativas xingjilieren_fps e controller original. Não há adaptação do modeloScarlet em primeira pessoa; escolha deliberada para primeiro teste funcional.
- Bundle: CAB e AssetBundleName próprios; assets internos originais mantidos conforme loader. Comparação com candidata013 confirma somenteAssetBundle alterado; rig/malha/texturas idênticos à prévia aprovada.
- Arquivos alterados: public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml. Novos: downloads/cdn_cache/res/1.1.1/StreamingAssets/HeroAnimator/xingjilieren_tps_scarlet.mo eIcon/herounlock/skin/celina_scarlet.txt.
- Manifesto:5549entradas,novos recursosCRC26100802. Catálogosbaxi/zhCRC26100802. Nenhum código servidor/Assembly alterado.
- Backups: localbackups/scarlet-skin014-before(3arquivos); remoto/home/ubuntu/backups/scarlet-skin014-20261008T233046Z/runtime.tar.gz. Novosarquivos previamente inexistentes.
- Testes: verify.py confirma identidade exclusiva/externals/catalogos semanticamente preservados/IDs/refs/manifesto; initializeDatabase isolado com catálogo preparado confirma preço0. ComparaçãoXML inicial falhou por whitespace; corrigida para comparação semântica, sem relaxar atributos/conteúdo.
- Instalação: hashes remotosbase conferidos antes; staged/new hashes conferidos antes/depois da cópia; catálogo remoto inicializado e preço0/mapeamentoScarlet confirmados. Zero conexões7777 antes do reinício; PM2triggerun reiniciado/onlinePID619127.
- Rede: seisURLs(bundles/ícone/catálogo em1.1.1 e1.3.5) retornam bytes exatos; manifestoHTTP tambémhashigual;TCP7777 acessível. HashesTPSclássica/Aurora remotos iguais aos locais, intactos.
- Cliente real: não testado; usuário deve fechar/reabrir ambosclientes, selecionarCelina→Scarlet Proxy e conferir menu/partida/corrida/mira/disparo/habilidades/respawn e outro jogador. Poses simuladas013 não equivalem aUnity.
- Limites: cabelo segue cabeça, sem dinâmica; shader toon semPBR completo; alcance mãos alinhado emrepouso, pegada/animações reaispendentes.
- Evidências: scratch/scarlet-release/{release.json,files.json,base.sha256,new.sha256,http-validation.json,stage,install.sh}; candidata013 e créditos preservados. ChaveSSH temporária removida.
- Reversão: restaurar trêsarquivosbackup remoto e reiniciar após verificar conexões; remover apenas doisnovosScarlet se necessário. Não restaurar sobre alterações posteriores sem comparação.
- Próximo: feedback do testeUnity; investigar animação/arma/clipping pelo log se necessário. Reserva liberada.
