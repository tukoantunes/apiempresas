using ApiEmpresas.Infra.Data.Entities;
using ApiEmpresas.Infra.Data.Interfaces;
using ApiEmpresas.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresas.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        //atributos
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionariosController(IEmpresaRepository empresaRepository, IFuncionarioRepository funcionarioRepository)
        {
            _empresaRepository = empresaRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpPost]
        public IActionResult Post(FuncionarioPostRequest request)
        {
            try
            {
                #region Não permitir o cadastro de funcionários com o mesmo cpf ou matricula

                if (_funcionarioRepository.GetByCpf(request.Cpf) != null)
                    return StatusCode(422, new { mensagem = "O CPF informado já encontra-se cadastrado." });

                if (_funcionarioRepository.GetByMatricula(request.Matricula) != null)
                    return StatusCode(422, new { mensagem = "A Matrícula informada já encontra-se cadastrada." });

                #endregion

                #region Verificar se a empresa informada não existe na base de dados

                if (_empresaRepository.GetById(request.IdEmpresa) == null)
                    return StatusCode(422, new { mensagem = "A Empresa informada não foi encontrada." });

                #endregion

                var funcionario = new Funcionario
                {
                    Nome = request.Nome,
                    Cpf = request.Cpf,
                    Matricula = request.Matricula,
                    DataAdmissao = request.DataAdmissao,
                    IdEmpresa = request.IdEmpresa
                };

                _funcionarioRepository.Create(funcionario);

                return StatusCode(201, new { mensagem = "Funcionário cadastrado com sucesso." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(FuncionarioPutRequest request)
        {
            try
            {
                #region Verificar se o id informado existe no banco de dados

                if (_funcionarioRepository.GetById(request.IdFuncionario) == null)
                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhum funcionário cadastrado." });

                #endregion

                #region Não permitir o cadastro de funcionários com o mesmo cpf ou matricula

                var funcionarioByCpf = _funcionarioRepository.GetByCpf(request.Cpf);
                if (funcionarioByCpf != null && funcionarioByCpf.IdFuncionario != request.IdFuncionario)
                    return StatusCode(422, new { mensagem = "O CPF informado já encontra-se cadastrado para outro funcionário." });

                var funcionarioByMatricula = _funcionarioRepository.GetByMatricula(request.Matricula);
                if (funcionarioByMatricula != null && funcionarioByMatricula.IdFuncionario != request.IdFuncionario)
                    return StatusCode(422, new { mensagem = "A Matrícula informada já encontra-se cadastrada para outro funcionário." });

                #endregion

                #region Verificar se a empresa informada não existe na base de dados

                if (_empresaRepository.GetById(request.IdEmpresa) == null)
                    return StatusCode(422, new { mensagem = "A Empresa informada não foi encontrada." });

                #endregion

                var funcionario = new Funcionario
                {
                    IdFuncionario = request.IdFuncionario,
                    Nome = request.Nome,
                    Cpf = request.Cpf,
                    Matricula = request.Matricula,
                    DataAdmissao = request.DataAdmissao,
                    IdEmpresa = request.IdEmpresa,
                    Ativo = request.Ativo
                };

                _funcionarioRepository.Update(funcionario);

                return StatusCode(200, new { mensagem = "Funcionário atualizado com sucesso." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                #region Verificar se o id informado existe no banco de dados

                var funcionario = _funcionarioRepository.GetById(id);
                if (funcionario == null)
                    return StatusCode(422, new { mensagem = "O ID informado não corresponde a nenhum funcionário cadastrado." });

                #endregion

                _funcionarioRepository.Delete(funcionario);

                return StatusCode(200, new { mensagem = "Funcionário excluído com sucesso." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var funcionarios = _funcionarioRepository.GetAll();

                if (funcionarios.Count > 0)
                    return StatusCode(200, funcionarios);
                else
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var funcionario = _funcionarioRepository.GetById(id);

                if (funcionario != null)
                    return StatusCode(200, funcionario);
                else
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}



