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
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly string _connectionString;

        //construtor para injeção de dependência (inicialização)
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Usuario entity)
        {
            var query = $@"
                INSERT INTO USUARIO(
                    IDUSUARIO,
                    NOME,
                    LOGIN,
                    SENHA,
                    DATAINCLUSAO)
                VALUES(
                    NEWID(),
                    @Nome,
                    @Login,
                    CONVERT(VARCHAR(32), HASHBYTES('MD5', '{entity.Senha}'),2),
                    GETDATE())
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Usuario entity)
        {
            var query = $@"
                UPDATE USUARIO
                SET
                    NOME = @Nome,
                    LOGIN = @Login,
                    SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', '{entity.Senha}'),2),
                WHERE
                    IDUSUARIO = @IdUsuario
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Usuario entity)
        {
            var query = @"
                DELETE FROM USUARIO
                WHERE IDUSUARIO = @IdUsuario
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"
                SELECT * FROM USUARIO
                ORDER BY LOGIN
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Usuario>(query)
                    .ToList();
            }
        }

        public Usuario GetById(Guid id)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE IDUSUARIO = @id
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Usuario>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public Usuario Get(string login, string senha)
        {
            var query = $@"
                SELECT * FROM USUARIO
                WHERE 
                    LOGIN = @login
                AND
                    SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', '{senha}'),2)
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Usuario>(query, new { login })
                    .FirstOrDefault();
            }
        }
    }
}


