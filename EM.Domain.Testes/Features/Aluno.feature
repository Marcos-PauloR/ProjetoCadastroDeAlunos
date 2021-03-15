#language: pt-Br
Funcionalidade: Aluno 

@mytag
Esquema do Cenario: Alunos iguais
	Cenario: verificar aluno igual
		Dado Que o aluno tem Nome <Nome> Matricula <Matricula> Cpf <Cpf> Nascimento <Nascimento> Sexo <Sexo>
		E e o outro aluno tem Nome <Nome2> Matricula <Matricula2> Cpf <Cpf2> Nascimento <Nascimento2> Sexo <Sexo2>
		Quando Ver igualdade
		Entao alunos devem ser diferentes
			
			Exemplos: 
			| Nome  | Nome2  | Matricula | Matricula2 | Cpf | Cpf2 | Nascimento | Nascimento2 | Sexo | Sexo2 |
			| Pedro | Paulo  | 1234      | 123        |     |      | 29/05/2000 | 26/07/1998  | 0    | 0     |
			| juca  | Paulin | 4321      | 321        |     |      | 25/07/1997 | 15/06/2003  | 0    | 0     |


Esquema do Cenario: Ver Hashcode
	Cenario: Hashcode aluno
	Dado aluno Nome <Nome> Matricula <Matricula> Cpf <Cpf> Nascimento <Nascimento> Sexo <Sexo>
	Quando ver hashcode
	Entao hashcode deve ser igual matricula

		Exemplos: 
		| Nome  | Matricula | Cpf | Nascimento | Sexo | Hascode |
		| Pedro | 1234      |     | 29/05/2000 | 0    | 1234    |
		| Juca  | 4321      |     | 25/07/1997 | 0    | 4321    |

Cenario: ToString do Aluno
	Dado o aluno Nome <Nome> Matricula <Matricula> Cpf <Cpf> Nascimento <Nascimento> Sexo <Sexo>
	Quando ver ToString
	Entao vizualizar saida
		Exemplos: 
		| Nome  | Matricula | Cpf | Nascimento | Sexo |
		| Pedro | 1234      |     | 29/05/2000 | 0    |
		| Juca  | 4321      |     | 25/07/1997 | 0    |
