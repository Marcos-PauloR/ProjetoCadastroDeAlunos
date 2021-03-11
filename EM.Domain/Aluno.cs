using System;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        public int Matricula { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime Nascimento { get; set; }

        public EnumeradorSexo Sexo { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Aluno aluno &&
                   Matricula == aluno.Matricula;
        }

        public override int GetHashCode() => Matricula;

        public override string ToString()
        {
            return $"Aluno: {Nome}, Matricula: {Matricula}, Cpf: {CPF}, Nascimento: {Nascimento}, Sexo: {Sexo}";
        }
    }
}