using ApiEmpresas.Infra.Data.Entities;
using ApiEmpresas.Infra.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ApiEmpresas.Infra.Data.Repositories
{
    /// <summary>
    /// Classe de repositorio para operações de Empresa
    /// </summary>
    public class EmpresaRepository : IEmpresaRepository
    {
        //atributo
        private readonly string _connectionString;

        //construtor para injeção de dependência
        public EmpresaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Empresa entity)
        {
            var query = @"
                    INSERT INTO EMPRESA(
                        IDEMPRESA,
                        NOMEFANTASIA,
                        RAZAOSOCIAL,
                        CNPJ,
                        DATAINCLUSAO,
                        DATAALTERACAO)
                    VALUES(
                        NEWID(),
                        @NomeFantasia,
                        @RazaoSocial,
                        @Cnpj,
                        GETDATE(),
                        GETDATE())
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Create(Empresa empresa, List<Funcionario> funcionarios)
        {
            #region Queries para gravação de empresa e de funcionário

            var queryEmpresa = @"
                    INSERT INTO EMPRESA(
                        IDEMPRESA,
                        NOMEFANTASIA,
                        RAZAOSOCIAL,
                        CNPJ,
                        DATAINCLUSAO,
                        DATAALTERACAO)
                    VALUES(
                        @IdEmpresa,
                        @NomeFantasia,
                        @RazaoSocial,
                        @Cnpj,
                        GETDATE(),
                        GETDATE())
                    ";

            var queryFuncionario = @"
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

            #endregion

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    //cadastrar a empresa..
                    connection.Execute(queryEmpresa, empresa);

                    //cadastrar os funcionários..
                    foreach (var item in funcionarios)
                        connection.Execute(queryFuncionario, item);

                    transaction.Complete(); //COMMIT!
                }
            }
        }

        public void Update(Empresa entity)
        {
            var query = @"
                    UPDATE EMPRESA 
                    SET
                        NOMEFANTASIA = @NomeFantasia,
                        RAZAOSOCIAL = @RazaoSocial,
                        CNPJ = @CNPJ,
                        ATIVO = @Ativo,
                        DATAALTERACAO = GETDATE()
                    WHERE
                        IDEMPRESA = @IdEmpresa                            
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Empresa entity)
        {
            var query = @"
                    UPDATE EMPRESA 
                    SET
                        ATIVO = 0,
                        DATAALTERACAO = GETDATE()
                    WHERE
                        IDEMPRESA = @IdEmpresa                            
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Empresa> GetAll()
        {
            var query = @"
                    SELECT * FROM EMPRESA
                    WHERE ATIVO = 1
                    ORDER BY NOMEFANTASIA
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Empresa>(query)
                    .ToList();
            }
        }

        public Empresa GetById(Guid id)
        {
            var query = @"
                    SELECT * FROM EMPRESA
                    WHERE IDEMPRESA = @id
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Empresa>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public Empresa GetByRazaoSocial(string razaoSocial)
        {
            var query = @"
                    SELECT * FROM EMPRESA
                    WHERE RAZAOSOCIAL = @razaoSocial
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Empresa>(query, new { razaoSocial })
                    .FirstOrDefault();
            }
        }

        public Empresa GetByCnpj(string cnpj)
        {
            var query = @"
                    SELECT * FROM EMPRESA
                    WHERE CNPJ = @cnpj
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Empresa>(query, new { cnpj })
                    .FirstOrDefault();
            }
        }
    }
}



