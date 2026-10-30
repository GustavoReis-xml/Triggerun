# Cosméticos de armas — WEAPON-002

`WeaponCosmetics.cs` gera `TriggerunWeaponCosmetics.dll` (.NET3.5, UnityEngine e Assembly-CSharp instalados). O patch `tools/patch-weapon-cosmetics.ps1` modifica somente CHeroWeapon.SetHeroWeapon/LoadWeaponMatOk. Preserva todas as DLLs auxiliares existentes. Instalar Assembly e helper juntos com TRIG fechado; nunca usar Assembly puro para atualizar cosméticos.

## Novo cadastro

Arma e roupa são independentes: `<weapon id>` dentro de herounlock.xml. Nenhuma alteração de combate é necessária. Atribuir lowmat_1st/highmat_1st/commat_3rd ao novo bundle; cada perfil pode usar bundle próprio. O primeiro asset retornado pelo bundle deve ser Material, como exige o carregador original. Incluir TextAsset com caminho terminando `/tr_weapon_cosmetic.xml`:

```xml
<weaponCosmetic version="1" hero="1016" id="5">
  <meshes>
    <mesh source="nome_original_da_mesh" asset="assets/10_weapon.mesh"/>
    <companion path="caminho/relativo/da/camada" source="nome_original_da_mesh"/>
  </meshes>
  <effects>
    <effect path="caminho/relativo/ao/CHeroWeapon" color="1,0.035,0.085"/>
  </effects>
</weaponCosmetic>
```

Mapear TODAS as malhas nativas de WeaponTransform, incluindo LOD/FPS/loja e variantes de roupa; conservar pivôs/escalas locais de cada renderer. MeshFilter é a rota validada estruturalmente nesta tarefa. SkinnedMeshRenderer exige paleta de bindposes compatível; igualdade de contagem não prova equivalência dos ossos, validar antes de cadastrar. Cada mesh deve ter uma submesh. Bundles sem metadata continuam usando o material nativo. XML inválido/mapeamento incompleto restaura geometria/material nativos.

WEAPON-004 acrescenta `meshes/companion`: renderers auxiliares que não constam em WeaponTransform, como holograma da loja/FPS e contornos de aliado/visibilidade. O path é explícito e relativo ao CHeroWeapon. O source deve corresponder à malha nativa e existir no mapa; usa a mesma malha de destino sem mudar o material nem a ativação do renderer auxiliar. Caminhos ausentes são permitidos para descrever vários prefabs; caminho presente com mesh errada gera fallback. Duplicatas são deduplicadas porrenderer. Na troca/disable, restaura os originais. Não fazer buscas globais por nomes de mesh nem desativar contornos usados pelo jogo. Requer helper004; pacote002 ignora companions.

## Efeitos e ciclo de vida

Paths são relativos à instância, sem buscas globais. Só listar efeitos da arma: nunca efeitos de corpo/skills. O helper preserva alpha, intensidade, modo/tempos dos gradientes e ativação nativa; muda RGB de partículas/trails e cópias privadas dos materiais encontrados. Texturas coloridas podem exigir adaptação própria. O movimento/ativação continuam nativos; não adiciona emissão luminosa ao shader da lâmina.

Troca/disable/destroy restaura as referências originais e descarta cópias privadas. Cada pedido recebe revisão monotônica; callbacks de downloads anteriores são rejeitados, inclusive A→B→A. A ocultação síncrona da loja tem uma exceção de disable por pedido. Não substituir o customParam opaco por string: fResourceManager preserva esse objeto por callback.

## Validação e reversão

Antes de comprimir um albedo opaco em DXT5, converter explicitamente para RGBA com alpha255. Na WEAPON-003, a mesma imagem RGB2048 gerou alpha0 nesta combinação de importador/compressor; um teste de cor uniforme não reproduziu. Reabrir o bundle final e decodificar TODOS os mips para verificar alpha, além dos valores RGB. O shader nativo `Custom/ttcq` usa `TransparentCutout`/`_Cutoff`: uma textura carregada com alpha0 desaparece sem erro de download. Regressão reproduzível: `scratch/weapon063/fix-and-verify.py`. Material/meshes/efeitos/DLLs permaneceram iguais; a revisão063 corrige só os blocos de alpha.

WEAPON-004 corrige também o contrato dos mapas: `Custom/ttcq` usa `_NR` packed-DXT5nm (normalXY em alpha/verde), `_M` como máscara de brilho/reflexo e `_RF` cubemap. Nulificar essas referências não equivale a desativar os recursos: os defaults podem saturar o resultado. Eclipse usa normal neutraRGBA128/128/255/128, máscara24/24/24/255 e cubemap nativo com `_RFpowe=0.35`; atlas/alpha/geometria permanecem idênticos003. Construção/validação `scratch/weapon064/fix-assets.py`, contrato observado também em `scratch/weapon064/fragment.asm`. Para outra arma, conferir seu shader e suas texturas antes de reaproveitar valores.

Fixture `tests/WeaponCosmeticsFixture.cs` compila a mesma fonte com APIs Unity simuladas; não comprova aparência no jogo. Evidências da primeira arma em `scratch/weapon062/`: validation dos assets, helper-validation e patch-validation. Testar no Unity: loja, FPS/TPS, baixa/alta qualidade, trocar arma/roupa, reuso, dois clientes e efeitos após troca. Eclipse tem 15.620 triângulos em todos os perfis; não há LOD otimizado ainda.

Para reverter WEAPON-002: fechar TRIG e restaurar Assembly de `backups/weapon062-before/Assembly-CSharp.dll`. Helper novo pode permanecer sem uso. Restaurar os três arquivos de cadastro/manifesto do backup remoto para retirar arma5; conservar skins e auxiliares anteriores. Pacote atual: `candidate-assets/valk-eclipse/weapon/client-update-weapon064.zip`, com o mesmo Assembly002 e helper004; manter os auxiliares anteriores. Pacote062 é histórico e não acompanha as camadas auxiliares.
