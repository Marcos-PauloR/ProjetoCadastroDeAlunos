using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EM.Domain.Testes.Steps
{
    [Binding]
    public class AlunoSteps
    {
        Aluno _aluno = new Aluno();
        String saidaAluno;

        [Given(@"o aluno Nome (.*) Matricula (.*) Cpf (.*) Nascimento (.*) Sexo (.*)")]
        public void DadoOAlunoNomeMatriculaCpfNascimentoSexo(string nome, int matricula, string Cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            _aluno.Nome = nome;
            _aluno.Matricula = matricula;
            _aluno.CPF = Cpf;
            _aluno.Nascimento = nascimento;
            _aluno.Sexo = sexo;
        }

        [When(@"ver ToString")]
        public void QuandoVerToString()
        {
            saidaAluno = _aluno.ToString();
        }
        
        [Then(@"vizualizar saida")]
        public void EntaoVizualizarSaida()
        {
            Assert.IsNotNull(saidaAluno);
            Assert.AreEqual(saidaAluno, _aluno.ToString());
        }
    }
}
