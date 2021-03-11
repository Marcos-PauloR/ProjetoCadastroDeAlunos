using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace EM.Repository.Tests.Features
{
    [Binding]
    public class RemoverAluno
    {
        private readonly RepositorioAluno _repositorio = new RepositorioAluno();
        private readonly Domain.Aluno _aluno = new Domain.Aluno();
        private int matriculaDeletada;

        [Given(@"Que a Matricula é (.*)")]
        public void DadoQueAMatriculaE(int matricula)
        {
            matriculaDeletada = matricula;
            _aluno.Matricula = matricula;
        }

        [When(@"Remover aluno da lista")]
        public void QuandoRemoverAlunoDaLista()
        {
            _repositorio.Remove(_aluno);
        }

        [Then(@"vejo que aluno foi removido com sucesso")]
        public void EntaoVejoQueAlunoFoiRemovidoComSucesso()
        {
            IEnumerable<Domain.Aluno> alunos = _repositorio.GetAll();

            Assert.IsFalse(alunos.Contains(_repositorio.Get(x => x.Matricula == matriculaDeletada).FirstOrDefault()));
        }
    }
}