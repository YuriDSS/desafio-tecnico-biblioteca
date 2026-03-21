using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Data;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using BibliotecaApi.UseCases.Livro.DTO;
using Dapper;

namespace BibliotecaApi.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DbSession _session;

        public LivroRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<int> Cadastrar(LivroEntity livro)
        {
            var sql = @"
                INSERT INTO Livros (titulo, autor, isbn, disponivel)
                VALUES (@Titulo, @Autor, @ISBN, 1);
                SELECT last_insert_rowid();
            ";

            var id = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                livro.Titulo,
                livro.Autor,
                livro.ISBN
            });

            return id;
        }

        public async Task MarcarComoIndisponivel(int idLivro)
        {
            var sql = "UPDATE Livros SET disponivel = 0 WHERE id = @IdLivro";
            await _session.Connection.ExecuteAsync(sql, new { IdLivro = idLivro });
        }

        public async Task MarcarComoDisponivel(int idLivro)
        {
            var sql = "UPDATE Livros SET disponivel = 1 WHERE id = @IdLivro";
            await _session.Connection.ExecuteAsync(sql, new { IdLivro = idLivro });
        }

        public async Task<List<ListarLivrosOutputDTO>> ListarAsync()
        {
            string sql = @"
                         SELECT
                             id AS Id,
                             titulo AS Titulo,
                             autor AS Autor,
                             isbn AS ISBN
                         FROM Livros
                     ";

            IEnumerable<ListarLivrosOutputDTO> livros = await _session.Connection.QueryAsync<ListarLivrosOutputDTO>(sql);

            return livros.ToList();
        }
    }
}