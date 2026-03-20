using BibliotecaApi.UseCases.Usuario.DTO;

namespace BibliotecaApi.Tests.Fixtures
{
    public class UsuarioFixture
    {
        public string NomeValido => "Usuário Teste";
        public string CpfFormatadoValido => "529.982.247-25";
        public string CpfNormalizadoValido => "52998224725";
        public string EmailValido => "usuario@email.com";

        public CadastrarUsuarioInputDTO CriarInputValido()
        {
            return new CadastrarUsuarioInputDTO
            {
                Nome = NomeValido,
                CPF = CpfFormatadoValido,
                Email = EmailValido
            };
        }

        public CadastrarUsuarioInputDTO CriarInputComCpf(string cpf)
        {
            return new CadastrarUsuarioInputDTO
            {
                Nome = NomeValido,
                CPF = cpf,
                Email = EmailValido
            };
        }
    }
}