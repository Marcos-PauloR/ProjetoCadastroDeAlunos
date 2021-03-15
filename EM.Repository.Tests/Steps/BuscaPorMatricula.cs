using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Steps
{
    [Binding]
    public class BuscaPorMatricula
    {
        private RepositorioAluno _repositorioAluno = new RepositorioAluno();
        private Domain.Aluno _aluno = new Domain.Aluno();

        [Given(@"Que a matricula é (.*)")]
        public void DadoQueAMatriculaE(int matricula)
        {
            _aluno.Matricula = matricula;
        }

        [When(@"Buscar pela matricula")]
        public void QuandoBuscarPelaMatricula()
        {
            _repositorioAluno.GetByMatricula(_aluno.Matricula);
        }

        [Then(@"vejo aluno retornado")]
        public void EntaoVejoAlunoRetornado()
        {
            var matriculaBuscada = _repositorioAluno.GetByMatricula(_aluno.Matricula);
            Assert.IsNotNull(matriculaBuscada);
            Assert.AreEqual(_aluno.Matricula, matriculaBuscada.Matricula);
        }
    }
}