using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EM.Domain.Testes.Steps
{
    [Binding]
    public class HashCode
    {
        Aluno _aluno = new Aluno();
        [Given(@"aluno Nome (.*) Matricula (.*) Cpf (.*) Nascimento (.*) Sexo (.*)")]
        public void DadoAlunoNomeMatriculaCpfNascimentoSexo(string nome, int matricula, string Cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            _aluno.Nome = nome;
            _aluno.Matricula = matricula;
            _aluno.CPF = Cpf;
            _aluno.Nascimento = nascimento;
            _aluno.Sexo = sexo;
        }
        
        [When(@"ver hashcode")]
        public void QuandoVerHashcode()
        {
            _aluno.GetHashCode();
        }
        
        [Then(@"hashcode deve ser igual matricula")]
        public void EntaoHashcodeDeveSerIgualMatricula()
        {
            var hash = _aluno.GetHashCode();
            Assert.IsNotNull(hash);
            Assert.AreEqual(_aluno.Matricula, hash);
        }
    }
}
