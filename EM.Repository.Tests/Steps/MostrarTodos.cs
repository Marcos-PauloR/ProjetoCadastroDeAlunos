using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Steps
{
    [Binding]
    public class MostrarTodos
    {
        private readonly RepositorioAluno _repositorioAluno = new RepositorioAluno();

        [When(@"mostrar todos")]
        public void QuandoMostrarTodos()
        {
            _repositorioAluno.GetAll();
        }

        [Then(@"liste todos alunos")]
        public void EntaoListeTodosAlunos()
        {
            var alunos = _repositorioAluno.GetAll();
            Assert.IsNotNull(alunos);
            Assert.AreEqual(_repositorioAluno.GetAll(), alunos);
        }
    }
}