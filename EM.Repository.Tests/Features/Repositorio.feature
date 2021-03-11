#language: pt-br
Funcionalidade: Repositorio aluno

@Adicionar
Esquema do Cenario: Adicionar aluno na Lista
	Cenario: Adicionar aluno 
		Dado O nome é <Nome>
		E Matricula é <Matricula>
		E Nascimento <Nascimento>
		E Sexo <Sexo>
		E Cpf <Cpf>
		Quando Adicionar dados na lista
		Entao vejo que aluno foi inserido com sucesso

		Exemplos: 
		| Matricula | Nome  | Nascimento | Sexo      | Cpf         |
		| 123456    | Paulo | 29/05/2000 | Masculino |             |
		| 12345678  | Paty  | 08/04/1998 | Feminino  | 13561268048 |

@Remover
Esquema do Cenario: Remover alunos
	Cenario: Remover Aluno
	Dado Que a Matricula é <Matricula>
	Quando Remover aluno da lista
	Entao vejo que aluno foi removido com sucesso

		Exemplos: 
		| Matricula |
		| 123456    |
		| 12345678  |

@Alterar
Esquema do Cenario: Alterar aluno na lista
	Cenario: Alterar aluno
	Dado que a Matricula é <Matricula>
	E novos dados são Nome <Nome> Nascimento <Nascimento> Sexo <Sexo> Cpf <Cpf>
	Quando alterar aluno
	Entao vejo alteraçoes

		Exemplos: 
		| Matricula | Nome                              | Nascimento | Sexo | Cpf         |
		| 43201857  | guilherme augusto gustavo da mata | 29/05/2000 | 0    | 56309533037 |
		| 745118505 | sophie gabrielly evelyn fernande  | 13/01/1960 | 0    | 87414307948 |
		| 24450275  | oliver antonio sales              | 07/12/1957 | 0    | 26778543094 |

@Listar
Cenario: Listar alunos
	Quando mostrar todos
	Entao liste todos alunos

@BuscaPorNome
Esquema do Cenario: Busca por parte do nome na lista
	Cenario: Buscar por qualquer parte do nome
	Dado que busco por <ParteDoNome>
	Quando buscar por parte do nome
	Entao vejo resultado da busca

		Exemplos: 
		| ParteDoNome | Matricula | Nome              | Nascimento | Sexo | Cpf         |
		| aulo        | 548808454 | marcos paulo rios | 29/05/2000 | 0    | 70536598100 |

@BuscaPorMatricula							
Esquema do Cenario: Busco por numero de matricula do aluno
	Cenario: Buscar aluno pela matricula
	Dado Que a matricula é <MatriculaBusca>
	Quando Buscar pela matricula
	Entao vejo aluno retornado

		Exemplos: 
		| MatriculaBusca |
		| 898169553      |
		| 211185682      |