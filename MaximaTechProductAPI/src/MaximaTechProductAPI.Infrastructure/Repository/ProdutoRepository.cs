using Dapper;
using MaximaTechProductAPI.Core.Entities;
using MaximaTechProductAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _connection;

        public ProdutoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Adicionar(Produto produto)
        {
            var departamentoExiste = await _connection.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM departamento WHERE Id = @Id AND Status = 1);",
                new { Id = produto.DepartamentoId }
            );
            
            if (!departamentoExiste)
                throw new Exception("Departamento informado não existe ou está inativo.");
            
            var sql = @"
                INSERT INTO produto (Id, Codigo, Descricao, DepartamentoId, Preco, Status)
                VALUES (@Id, @Codigo, @Descricao, @DepartamentoId, @Preco, @Status);";

            await _connection.ExecuteAsync(sql, new
            {
                produto.Id,
                produto.Codigo,
                produto.Descricao,
                produto.DepartamentoId,
                produto.Preco,
                produto.Status
            });
        }

        public async Task Atualizar(Produto produto)
        {
            var sql = @"
                UPDATE produto
                SET Codigo = @Codigo,
                    Descricao = @Descricao,
                    DepartamentoId = @DepartamentoId,
                    Preco = @Preco,
                    Status = @Status
                WHERE Id = @Id;";

            await _connection.ExecuteAsync(sql, new
            {
                produto.Id,
                produto.Codigo,
                produto.Descricao,
                produto.DepartamentoId,
                produto.Preco,
                produto.Status
            });
        }

        public async Task Inativar(Guid id)
        {
            var sql = "UPDATE produto SET Status = 0 WHERE Id = @Id;";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Produto?> obter(Guid id)
        {
            var sql = "SELECT * FROM produto WHERE Id = @Id;";
            return await _connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
        }

        public async Task<IEnumerable<dynamic>> obterTodos()
        {
            var sql = @"SELECT p.Id, p.Codigo, p.Descricao, d.Descricao AS Departamento, p.Preco
                FROM produto p
                INNER JOIN departamento d ON p.DepartamentoId = d.Id
                WHERE p.Status = 1";
            
            return await _connection.QueryAsync(sql);
        }
    }
}

