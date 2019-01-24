namespace RestWithAspNetCoreUdemy.Security.Configuration
{
    public class TokenConfiguration
    {
        /// <summary>
        /// Indica quem emitiu o Token, no caso, a aplicação
        /// iss: https://tools.ietf.org/html/rfc7519#section-4.1.1
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// aud: Indica quem será os destinatários do Token
        /// https://tools.ietf.org/html/rfc7519#section-4.1.3
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Indica o tempo em que o Token será valido a partir da sua criação
        /// </summary>
        public int Seconds { get; set; }
    }
}
