using ApiEmpresas.Infra.Data.Entities;
using ApiEmpresas.Infra.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Data.Repositories
{
    /// <summary>
    /// Classe de repositorio para operações de Funcionário
    /// </summary>
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo para armazenar a string de conexão do banco de dados
        private readonly string _connectionString;

        //construtor para injeção de dependencia
        public FuncionarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Funcionario entity)
        {
            var query = @"
                        INSERT INTO FUNCIONARIO(
                            IDFUNCIONARIO,
                            NOME,
                            CPF,
                            MATRICULA,
                            DATAADMISSAO,
                            IDEMPRESA,
                            DATAINCLUSAO,
                            DATAALTERACAO)
                        VALUES(
                            NEWID(),
                            @Nome,
                            @Cpf,
                            @Matricula,
                            @DataAdmissao,
                            @IdEmpresa,
                            GETDATE(),
                            GETDATE())
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Funcionario entity)
        {
            var query = @"
                        UPDATE FUNCIONARIO
                        SET                            
                            NOME = @Nome,
                            CPF = @Cpf,
                            MATRICULA = @Matricula,
                            DATAADMISSAO = @DataAdmissao,
                            IDEMPRESA = @IdEmpresa,
                            DATAALTERACAO = GETDATE(),
                            ATIVO = @Ativo
                        WHERE
                            IDFUNCIONARIO = @IdFuncionario
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Funcionario entity)
        {
            var query = @"
                        UPDATE FUNCIONARIO
                        SET    
                            ATIVO = 0
                        WHERE
                            IDFUNCIONARIO = @IdFuncionario
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Funcionario> GetAll()
        {
            var query = @"
                        SELECT * FROM FUNCIONARIO f
                        INNER JOIN EMPRESA e
                        ON f.IDEMPRESA = e.IDEMPRESA
                        WHERE f.ATIVO = 1
                        ORDER BY f.NOME
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query(query,
                    (Funcionario f, Empresa e) =>
                    {
                        f.Empresa = e;
                        return f;
                    },
                    splitOn: "IDEMPRESA")
                    .ToList();
            }
        }

        public Funcionario GetById(Guid id)
        {
            var query = @"
                        SELECT * FROM FUNCIONARIO f
                        INNER JOIN EMPRESA e
                        ON f.IDEMPRESA = e.IDEMPRESA
                        WHERE f.IDFUNCIONARIO = @id
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query(query,
                    (Funcionario f, Empresa e) =>
                    {
                        f.Empresa = e;
                        return f;
                    },
                    new { id },
                    splitOn: "IDEMPRESA")
                    .FirstOrDefault();
            }
        }

        public Funcionario GetByCpf(string cpf)
        {
            var query = @"
                        SELECT * FROM FUNCIONARIO f
                        INNER JOIN EMPRESA e
                        ON f.IDEMPRESA = e.IDEMPRESA
                        WHERE f.CPF = @cpf
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query(query,
                    (Funcionario f, Empresa e) =>
                    {
                        f.Empresa = e;
                        return f;
                    },
                    new { cpf },
                    splitOn: "IDEMPRESA")
                    .FirstOrDefault();
            }
        }

        public Funcionario GetByMatricula(string matricula)
        {
            var query = @"
                        SELECT * FROM FUNCIONARIO f
                        INNER JOIN EMPRESA e
                        ON f.IDEMPRESA = e.IDEMPRESA
                        WHERE f.MATRICULA = @matricula
                    ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query(query,
                    (Funcionario f, Empresa e) =>
                    {
                        f.Empresa = e;
                        return f;
                    },
                    new { matricula },
                    splitOn: "IDEMPRESA")
                    .FirstOrDefault();
            }
        }
    }
}



