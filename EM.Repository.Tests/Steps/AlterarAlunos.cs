using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Steps
{
    [Binding]
    public class AlterarDadosDaTabela
    {
        private readonly RepositorioAluno _repositorio = new RepositorioAluno();
        private readonly Domain.Aluno _aluno = new Domain.Aluno();

        [Given(@"que a Matricula é (.*)")]
        public void DadoQueAMatriculaE(int matricula)
        {
            _aluno.Matricula = matricula;
        }

        [Given(@"novos dados são Nome(.*) Nascimento(.*) Sexo (.*) Cpf (.*)")]
        public void DadoNovosDadosSaoNomeNascimentoSexoCpf(string nome, DateTime nascimento, Domain.EnumeradorSexo sexo, string cpf)
        {
            _aluno.Nome = nome;
            _aluno.Nascimento = nascimento;
            _aluno.Sexo = sexo;
            _aluno.CPF = cpf;
        }

        [When(@"alterar aluno")]
        public void QuandoAlterarAluno()
        {
            _repositorio.Update(_aluno);
        }

        [Then(@"vejo alteraçoes")]
        public void EntaoVejoAlteracoes()
        {
            var alunoAlterado = _repositorio.GetByMatricula(_aluno.Matricula);
            Assert.IsNotNull(alunoAlterado);
            Assert.AreEqual(_aluno.Matricula, alunoAlterado.Matricula);
        }
    }
}