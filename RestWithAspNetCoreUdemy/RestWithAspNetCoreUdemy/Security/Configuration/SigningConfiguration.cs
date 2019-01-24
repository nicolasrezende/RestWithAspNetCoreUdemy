using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RestWithAspNetCoreUdemy.Security.Configuration
{
    public class SigningConfiguration
    {
        /// <summary>
        /// Chave de segurança que será usada para criar e validar o Token
        /// </summary>
        public SecurityKey Key { get; set; }
        /// <summary>
        /// Responsável por armazenar a Chave de Segurança e o tipo de algoritmo que será usado no Token
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        public SigningConfiguration()
        {
            //RSACryptoServiceProvider = classe responsavel por criptografar usando algoritmo RSA
            //2048 = criara uma criptografia do tamanho de 2048 bits
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                //RsaSecurityKey = classe responsavel por gerar uma chave de segurança usando o altoritmo RSA
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            //Informações que iram na área SIGNATURE do Token
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
