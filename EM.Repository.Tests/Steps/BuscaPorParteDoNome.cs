using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Steps
{
    [Binding]
    public class BuscaPorParteDoNome
    {
        private RepositorioAluno _repositorioAluno = new RepositorioAluno();
        private string busca;

        [Given(@"que busco por (.*)")]
        public void DadoQueBuscoPor(string parteDoNome)
        {
            busca = parteDoNome;
        }

        [When(@"buscar por parte do nome")]
        public void QuandoBuscarPorParteDoNome()
        {
            _repositorioAluno.GetByContendoNoNome(busca);
        }

        [Then(@"vejo resultado da busca")]
        public void EntaoVejoResultadoDaBusca()
        {
            var parteDoNome = _repositorioAluno.GetByContendoNoNome(busca);
            Assert.IsNotNull(parteDoNome);
            Assert.AreEqual(parteDoNome, _repositorioAluno.GetByContendoNoNome(busca));
        }
    }
}