using System.Text.RegularExpressions;

namespace BibliotecaApi.Domain.Shared
{
    public static class CpfHelper
    {
        public static string Normalizar(string cpf)
        {
            string cpfNormalizado = Regex.Replace(cpf ?? string.Empty, @"\D", string.Empty);

            return cpfNormalizado;
        }
    }
}