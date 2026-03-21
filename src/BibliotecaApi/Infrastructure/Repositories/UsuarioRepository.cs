using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Data;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using Dapper;

namespace BibliotecaApi.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbSession _session;

        public UsuarioRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<int> Cadastrar(UsuarioEntity usuario)
        {
            var sql = @"
                INSERT INTO Usuarios (nome, cpf, email)
                VALUES (@Nome, @CPF, @Email);
                SELECT last_insert_rowid();
            ";

            var id = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                usuario.Nome,
                usuario.CPF,
                usuario.Email
            });

            return id;
        }

        public async Task<bool> ExisteCpfAsync(string cpf)
        {
            var sql = @"
                SELECT COUNT(1)
                FROM Usuarios
                WHERE cpf = @Cpf
            ";

            int quantidadeUsuariosComCpf = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                Cpf = cpf
            });

            bool cpfJaCadastrado = quantidadeUsuariosComCpf > 0;

            return cpfJaCadastrado;
        }

        public async Task AtualizarPossuiAtrasoAtivoAsync(int idUsuario, bool possuiAtrasoAtivo)
        {
            string sql = @"
                         UPDATE Usuarios
                         SET possui_atraso_ativo = @PossuiAtrasoAtivo
                         WHERE id = @IdUsuario
                         ";

            await _session.Connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                PossuiAtrasoAtivo = possuiAtrasoAtivo ? 1 : 0
            });
        }
    }
}