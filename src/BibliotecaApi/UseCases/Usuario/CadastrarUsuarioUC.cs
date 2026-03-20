using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Domain.Shared;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Usuario.DTO;

namespace BibliotecaApi.UseCases.Usuario
{
    public class CadastrarUsuarioUC
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuarioUC(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> Executar(CadastrarUsuarioInputDTO input)
        {
            string cpfNormalizado = CpfHelper.Normalizar(input.CPF);

            bool cpfJaCadastrado = await _usuarioRepository.ExisteCpfAsync(cpfNormalizado);

            if (cpfJaCadastrado)
            {
                throw new ArgumentException("Usuário com este CPF já está cadastrado.");
            }

            UsuarioEntity usuario = new();
            usuario.Cadastrar(input.Nome, cpfNormalizado, input.Email);

            int idUsuario = await _usuarioRepository.Cadastrar(usuario);

            return idUsuario;
        }
    }
}