using BibliotecaApi.Domain.Entities;
using BibliotecaApi.Infrastructure.Data;
using BibliotecaApi.Infrastructure.Repositories.Interfaces;
using Dapper;

namespace BibliotecaApi.Infrastructure.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly DbSession _session;

        public EmprestimoRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<int> Cadastrar(EmprestimoEntity emprestimo)
        {
            string sql = @"
                INSERT INTO Emprestimos
                (id_usuario, id_livro, data_emprestimo, data_prevista_devolucao, data_devolucao, valor, multa, total)
                VALUES
                (@IdUsuario, @IdLivro, @DataEmprestimo, @DataPrevistaDevolucao, @DataDevolucao, @Valor, @Multa, @Total);
                SELECT last_insert_rowid();
            ";

            int id = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                emprestimo.IdUsuario,
                emprestimo.IdLivro,
                emprestimo.DataEmprestimo,
                emprestimo.DataPrevistaDevolucao,
                emprestimo.DataDevolucao,
                emprestimo.Valor,
                emprestimo.Multa,
                emprestimo.Total
            });

            return id;
        }

        public async Task Atualizar(EmprestimoEntity emprestimo)
        {
            string sql = @"
                UPDATE Emprestimos
                SET data_devolucao = @DataDevolucao,
                    multa = @Multa,
                    total = @Total
                WHERE id = @Id
            ";

            await _session.Connection.ExecuteAsync(sql, new
            {
                emprestimo.Id,
                emprestimo.DataDevolucao,
                emprestimo.Multa,
                emprestimo.Total
            });
        }

        public async Task<EmprestimoEntity?> ObterPorIdAsync(int idEmprestimo)
        {
            string sql = @"
                SELECT 
                    id AS Id,
                    id_usuario AS IdUsuario,
                    id_livro AS IdLivro,
                    data_emprestimo AS DataEmprestimo,
                    data_prevista_devolucao AS DataPrevistaDevolucao,
                    data_devolucao AS DataDevolucao,
                    valor AS Valor,
                    multa AS Multa,
                    total AS Total
                FROM Emprestimos
                WHERE id = @IdEmprestimo
            ";

            return await _session.Connection.QueryFirstOrDefaultAsync<EmprestimoEntity>(
                sql,
                new { IdEmprestimo = idEmprestimo });
        }

        public async Task<bool> LivroEstaEmprestadoAsync(int idLivro)
        {
            string sql = @"
                        SELECT COUNT(1)
                        FROM Emprestimos
                        WHERE id_livro = @IdLivro
                          AND data_devolucao IS NULL
                        ";

            int quantidade = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                IdLivro = idLivro
            });

            return quantidade > 0;
        }

        public async Task<bool> UsuarioPossuiEmprestimoEmAtrasoAsync(int idUsuario)
        {
            string sql = @"
                         SELECT COUNT(1)
                         FROM Emprestimos
                         WHERE id_usuario = @IdUsuario
                           AND data_devolucao IS NULL
                           AND data_prevista_devolucao < @DataAtual
                         ";

            int quantidade = await _session.Connection.ExecuteScalarAsync<int>(sql, new
            {
                IdUsuario = idUsuario,
                DataAtual = DateTime.Now
            });

            return quantidade > 0;
        }
    }
}