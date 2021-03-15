using NUnit.Framework;
using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Steps
{
    [Binding]
    public class InserirDadosNaTabelaSteps
    {
        private RepositorioAluno _repositorio = new RepositorioAluno();
        private readonly Domain.Aluno _aluno = new Domain.Aluno();

        [Given(@"O nome é (.*)")]
        public void DadoONome(string nome)
        {
            _aluno.Nome = nome;
        }

        [Given(@"Matricula é (.*)")]
        public void DadoMatriculaE(int matricula)
        {
            _aluno.Matricula = matricula;
        }

        [Given(@"Nascimento (.*)")]
        public void DadoNascimento(string data)
        {
            _aluno.Nascimento = Convert.ToDateTime(data);
        }

        [Given(@"Sexo (.*)")]
        public void DadoSexo(Domain.EnumeradorSexo sexo)
        {
            _aluno.Sexo = sexo;
        }

        [Given(@"Cpf (.*)")]
        public void DadoCpf(string cpf)
        {
            _aluno.CPF = cpf;
        }

        [When(@"Adicionar dados na lista")]
        public void QuandoAdicionarDadosNaLista()
        {
            _repositorio.Add(_aluno);
        }

        [Then(@"vejo que aluno foi inserido com sucesso")]
        public void EntaoVejoQueAlunoFoiInseridoComSucesso()
        {
            var alunoInserido = _repositorio.GetByMatricula(_aluno.Matricula);
            Assert.IsNotNull(alunoInserido);
            Assert.AreEqual(_aluno.Matricula, alunoInserido.Matricula);
        }
    }
}