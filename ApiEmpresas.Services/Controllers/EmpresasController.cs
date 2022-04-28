using ApiEmpresas.Infra.Data.Entities;
using ApiEmpresas.Infra.Data.Interfaces;
using ApiEmpresas.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiEmpresas.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        //atributo
        private readonly IEmpresaRepository _empresaRepository;

        //construtor para injeção de dependência (inicialização)
        public EmpresasController(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        [HttpPost]
        public IActionResult Post(EmpresaPostRequest request)
        {
            try
            {
                #region Não permitir o cadastro de empresas com a mesma razão social ou cnpj

                if (_empresaRepository.GetByRazaoSocial(request.RazaoSocial) != null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "A razão social informada já está cadastrada para outra empresa." });

                if (_empresaRepository.GetByCnpj(request.Cnpj) != null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "O CNPJ informado já está cadastrado para outra empresa." });

                #endregion

                var empresa = new Empresa
                {
                    NomeFantasia = request.NomeFantasia,
                    RazaoSocial = request.RazaoSocial,
                    Cnpj = request.Cnpj,
                };

                //verificar se foram enviados funcionários nesta requisição
                if (request.Funcionarios != null && request.Funcionarios.Count > 0)
                {
                    //gerando o id da empresa
                    empresa.IdEmpresa = Guid.NewGuid();

                    //capturar os funcionários
                    var funcionarios = new List<Funcionario>();
                    foreach (var item in request.Funcionarios)
                    {
                        funcionarios.Add(new Funcionario
                        {
                            Nome = item.Nome,
                            Cpf = item.Cpf,
                            Matricula = item.Matricula,
                            DataAdmissao = item.DataAdmissao,
                            IdEmpresa = empresa.IdEmpresa
                        });
                    }

                    _empresaRepository.Create(empresa, funcionarios);

                    //HTTP 201 - CREATE (Sucesso!)
                    return StatusCode(201, new { mensagem = "Empresa e funcionários cadastrados com sucesso." });
                }
                else
                {
                    _empresaRepository.Create(empresa);

                    //HTTP 201 - CREATE (Sucesso!)
                    return StatusCode(201, new { mensagem = "Empresa cadastrada com sucesso." });
                }
            }
            catch (SqlException)
            {
                return StatusCode(422, new { mensagem = "Dados inválidos para a transação, por favor verifique." });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(EmpresaPutRequest request)
        {
            try
            {
                #region Verificar se o id informado existe no banco de dados

                if (_empresaRepository.GetById(request.IdEmpresa) == null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhuma empresa cadastrada." });

                #endregion

                #region Não permitir a alteração de empresa com a mesma razão social ou cnpj de outra empresa

                var empresaByRazaoSocial = _empresaRepository.GetByRazaoSocial(request.RazaoSocial);
                if (empresaByRazaoSocial != null && empresaByRazaoSocial.IdEmpresa != request.IdEmpresa)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "A razão social informada já está cadastrada para outra empresa." });

                var empresaByCnpj = _empresaRepository.GetByCnpj(request.Cnpj);
                if (empresaByCnpj != null && empresaByCnpj.IdEmpresa != request.IdEmpresa)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "O CNPJ informado já está cadastrado para outra empresa." });

                #endregion

                var empresa = new Empresa
                {
                    IdEmpresa = request.IdEmpresa,
                    NomeFantasia = request.NomeFantasia,
                    RazaoSocial = request.RazaoSocial,
                    Cnpj = request.Cnpj,
                    Ativo = request.Ativo
                };

                _empresaRepository.Update(empresa);

                //HTTP 200 - OK (Sucesso!)
                return StatusCode(200, new { mensagem = "Empresa atualizada com sucesso." });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                #region Verificar se o id informado existe no banco de dados

                var empresa = _empresaRepository.GetById(id);
                if (empresa == null)
                    //HTTP 422 - UNPROCESSABLE ENTITY
                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhuma empresa cadastrada." });

                #endregion

                _empresaRepository.Delete(empresa);

                //HTTP 200 - OK (Sucesso!)
                return StatusCode(200, new { mensagem = "Empresa inativada com sucesso." });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var empresas = _empresaRepository.GetAll();

                if (empresas.Count > 0)
                    //HTTP 200 - OK (Sucesso!)
                    return StatusCode(200, empresas);
                else
                    //HTTP 204 - NO CONTENT (Vazio)
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var empresa = _empresaRepository.GetById(id);

                if (empresa != null)
                    //HTTP 200 - OK (Sucesso!)
                    return StatusCode(200, empresa);
                else
                    //HTTP 204 - NO CONTENT (Vazio)
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}





