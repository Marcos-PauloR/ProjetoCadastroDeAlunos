using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EM.Domain.Testes.Steps
{
    [Binding]
    public class Equals
    {
        Aluno _aluno = new Aluno();
        Aluno _aluno2 = new Aluno();
        [Given(@"Que o aluno tem Nome (.*) Matricula (.*) Cpf (.*) Nascimento (.*) Sexo (.*)")]
        public void DadoQueOAlunoTemNomeMatriculaCpfNascimentoSexo(string nome, int matricula, string Cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            _aluno.Nome = nome;
            _aluno.Matricula = matricula;
            _aluno.CPF = Cpf;
            _aluno.Nascimento = nascimento;
            _aluno.Sexo = sexo;
        }


        [Given(@"e o outro aluno tem Nome (.*) Matricula (.*) Cpf (.*) Nascimento (.*) Sexo (.*)")]
        public void DadoEOOutroAlunoTemNomeMatriculaCpfNascimentoSexo(string nome, int matricula, string Cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            _aluno2.Nome = nome;
            _aluno2.Matricula = matricula;
            _aluno2.CPF = Cpf;
            _aluno2.Nascimento = nascimento;
            _aluno2.Sexo = sexo;
        }


        [When(@"Ver igualdade")]
        public void QuandoVerIgualdade()
        {
            _aluno.Equals(_aluno2);
        }
        
        [Then(@"alunos devem ser diferentes")]
        public void EntaoAlunosDevemSerDiferentes()
        {
            Assert.NotNull(_aluno);
            Assert.NotNull(_aluno2);
            Assert.AreNotEqual(_aluno, _aluno2);
        }
    }
}
