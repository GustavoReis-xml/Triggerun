# Espada Eclipse — referência Tripo

Use front.png como referência da arma inteira e isolada. Os símbolos vermelhos são entalhes superficiais; não devem virar correntes, argolas vazadas ou peças soltas.
Corpo rígido, frente e verso simétricos; cabo cilíndrico para duas mãos e guarda sólida. Preserve espessura da lâmina e biséis; exporte GLB com textura.
O encaixe final usará a empunhadura/pivô e o comprimento da espada nativa da Valk, aproximadamente1,54m, em versões para primeira e terceira pessoa.

A arte foi criada com image_gen integrado; prompt completo em front-prompt.txt. É uma referência ilustrada, não uma planta dimensional calibrada.

## Integração planejada

Cosmético independente de arma da Valk, não vinculado à skin corporal. O jogo já transmite weaponId separado de skinId.
O código original dos cosméticos dourado/cristal troca materiais, sem trocar a geometria. Para o novo formato será necessário um ajuste específico do cliente, ativado somente nessa nova arma, com restauração da malha original ao equipar os outros acabamentos.
Nenhum cadastro, DLL ou arquivo da Oracle foi alterado nesta etapa. Próximo passo: receber GLB, conferir cabo/guarda/proporção e construir a integração.
