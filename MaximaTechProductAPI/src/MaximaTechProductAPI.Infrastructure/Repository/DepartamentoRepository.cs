using Dapper;
using MaximaTechProductAPI.Core.Entities;
using MaximaTechProductAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Infrastructure.Repository
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly IDbConnection _connection;
        public DepartamentoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Adicionar(Departamento departamento)
        {
            var sql = @"
                INSERT INTO Departamento (Id, Codigo, Descricao, Status)
                VALUES (@Id, @Codigo, @Descricao, @Status)";

            await _connection.ExecuteAsync(sql, departamento);
        }

        public async Task Atualizar(Departamento departamento)
        {
            var sql = @"
                UPDATE Departamento SET Codigo = @Codigo, Descricao = @Descricao, Status = @Status 
                WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, departamento);
        }

        public async Task Inativar(Guid id)
        {
            var sql = "UPDATE FROM Departamento SET status = 0 WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<Departamento?> obter(Guid id)
        {
            var sql = "SELECT * FROM Departamento WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Departamento>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Departamento>> obterTodos()
        {
            var sql = "SELECT * FROM Departamento WHERE status = 1";
            return await _connection.QueryAsync<Departamento>(sql);
        }
    }
}
