using BibliotecaApi.Domain.Shared;

namespace BibliotecaApi.Domain.Entities
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public void Cadastrar(string nome, string cpf, string email)
        {
            string cpfNormalizado = CpfHelper.Normalizar(cpf);

            bool nomeInvalido = string.IsNullOrWhiteSpace(nome);
            bool cpfInvalido = string.IsNullOrWhiteSpace(cpfNormalizado) || cpfNormalizado.Length != 11;
            bool emailInvalido = string.IsNullOrWhiteSpace(email);

            if (nomeInvalido)
            {
                throw new ArgumentException("Nome é obrigatório.");
            }

            if (cpfInvalido)
            {
                throw new ArgumentException("CPF deve conter 11 dígitos numéricos.");
            }

            if (emailInvalido)
            {
                throw new ArgumentException("Email é obrigatório.");
            }

            Nome = nome.Trim();
            CPF = cpfNormalizado;
            Email = email.Trim();
        }
    }
}