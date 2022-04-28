namespace ApiEmpresas.Services.Authentication
{
    /// <summary>
    /// Classe para cepturar os parametros de geração do token
    /// definidos no arquivo /appsettings.json
    /// </summary>
    public class TokenSettings
    {
        /// <summary>
        /// Capturar a chave anti-falsificação
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Capturar o tempo de expiração
        /// </summary>
        public int ExpirationInHours { get; set; }
    }
}




