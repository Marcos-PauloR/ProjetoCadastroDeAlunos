using EM.Domain;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioAluno : RepositorioAbstrato<Aluno>
    {
        public override void Add(Aluno aluno)
        {
            using (var conexao = BancoDeDados.connection())
            {
                var comando = conexao.CreateCommand();
                comando.CommandText = @"INSERT INTO ALUNO (MATRICULA, NOME, SEXO, NASCIMENTO, CPF)
                                      VALUES (@Matricula,@Nome, @Sexo,@Nascimento,@CPF)";

                comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
                comando.Parameters.AddWithValue("@NOME", aluno.Nome);
                comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);
                comando.Parameters.AddWithValue("@Nascimento", aluno.Nascimento);
                comando.Parameters.AddWithValue("@CPF", aluno.CPF);

                comando.ExecuteNonQuery();
            }
        }

        public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }

        public override IEnumerable<Aluno> GetAll()
        {
            List<Aluno> alunos = new List<Aluno>();
            using (var conexao = BancoDeDados.connection())
            {
                var comando = conexao.CreateCommand();
                comando.CommandText = "SELECT MATRICULA, NOME, NASCIMENTO, SEXO, CPF FROM ALUNO";

                var leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Aluno aluno = new Aluno
                    {
                        Matricula = leitor.GetInt32(leitor.GetOrdinal("MATRICULA")),
                        Nome = leitor.GetString(leitor.GetOrdinal("NOME")),
                        Sexo = (EnumeradorSexo)leitor.GetInt32(leitor.GetOrdinal("SEXO")),
                        Nascimento = leitor.GetDateTime(leitor.GetOrdinal("NASCIMENTO")),
                        CPF = leitor.GetString(leitor.GetOrdinal("CPF"))
                    };

                    alunos.Add(aluno);
                }
            }
            return alunos.OrderBy(x=>x.Nome);
        }

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome)
        {
            return Get(x => x.Nome.Contains(parteDoNome));
        }

        public Aluno GetByMatricula(int matricula)
        {
            return Get(x => x.Matricula == matricula).FirstOrDefault();
        }

        public override void Remove(Aluno aluno)
        {
            using (var conexao = BancoDeDados.connection())
            {
                var comando = conexao.CreateCommand();
                comando.CommandText = "DELETE FROM ALUNO WHERE MATRICULA=@MATRICULA";
                comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);

                comando.ExecuteNonQuery();
            }
        }

        public override void Update(Aluno aluno)
        {
            using (var conexao = BancoDeDados.connection())
            {
                var comando = conexao.CreateCommand();
                comando.CommandText = @"UPDATE ALUNO SET MATRICULA=@MATRICULA, NOME=@NOME, SEXO=@SEXO, NASCIMENTO=@NASCIMENTO, CPF=@CPF
                                        WHERE MATRICULA=@MATRICULA";
                comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
                comando.Parameters.AddWithValue("@NOME", aluno.Nome);
                comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);
                comando.Parameters.AddWithValue("@Nascimento", aluno.Nascimento);
                comando.Parameters.AddWithValue("@CPF", aluno.CPF);
                comando.ExecuteNonQuery();
            }
        }

        public static class BancoDeDados
        {
            private const string linhaDeConexao = @"User=SYSDBA;
                                                    Password=masterkey;
                                                    Database=C:\\Users\\Usuario\\Desktop\\ProjetoEstagio.FDB;
                                                    DataSource=localhost;
                                                    Port=3050;
                                                    Dialect=3;
                                                    Charset=NONE;
                                                    Role=;
                                                    Connection lifetime=15;
                                                    Pooling=true;
                                                    MinPoolSize=0;
                                                    MaxPoolSize=50;
                                                    Packet Size=8192;
                                                    ServerType = 0; ";

            public static FbConnection connection()
            {
                var connection = new FbConnection(linhaDeConexao);
                connection.Open();
                return connection;
            }
        }
    }
}