using ApiEmpresas.Infra.Data.Interfaces;
using ApiEmpresas.Services.Authentication;
using ApiEmpresas.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresas.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenCreator _tokenCreator;

        //injeção de dependência (inicialização)
        public LoginController(IUsuarioRepository usuarioRepository, TokenCreator tokenCreator)
        {
            _usuarioRepository = usuarioRepository;
            _tokenCreator = tokenCreator;
        }

        /// <summary>
        /// Método de serviço para autenticação dos usuários
        /// </summary>
        [HttpPost]
        public IActionResult Post(LoginPostRequest request)
        {
            try
            {
                //buscar o usuário no banco de dados..
                var usuario = _usuarioRepository.Get(request.Login, request.Senha);

                //verificar se o usuário não foi encontrado
                if (usuario == null)
                    //HTTP 401 - UNAUTHORIZED ou 403 - FORBIDDEN
                    return StatusCode(401, new { mensagem = "Acesso negado, login ou senha inválidos." });

                //retornar status de sucesso e gerar uma
                //chave de autorização para o usuário (ACCESS TOKEN)
                return StatusCode(200, new
                {
                    mensagem = "Usuário autenticado com sucesso.",
                    nome = usuario.Nome,
                    login = usuario.Login,
                    accessToken = _tokenCreator.GenerateToken(usuario.Login)
                });

            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}




