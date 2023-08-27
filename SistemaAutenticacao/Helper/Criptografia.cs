using System.Security.Cryptography;
using System.Text;

namespace SistemaAutenticacao.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(string valor)
        {
            var Hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);

            array = Hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            { 
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }

    }
}
