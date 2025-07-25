using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximaTechProductAPI.Infrastructure.Database
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            var initialConnectionString = _connectionString.Replace("Database=MaximaTechDB", "Database=mysql");

            var conexaoInicial = new MySqlConnection(initialConnectionString);
            conexaoInicial.Open();

            ExecuteScriptFile(conexaoInicial, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database/Schema/01_create_database.sql"));

            var conexao = new MySqlConnection(_connectionString);
            conexao.Open();

            var existeTabelas = conexao.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = 'Departamento';"
            );

            if (existeTabelas == 0)
            {
                ExecuteScriptFile(conexao, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database/Schema/02_create_tables.sql"));
                ExecuteScriptFile(conexao, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database/Seed/03_seed_departamentos.sql"));

                //Console.WriteLine("Banco de dados inicializado com sucesso.");
            }
        }

        private void ExecuteScriptFile(IDbConnection connection, string filePath)
        {
            try
            {
                var script = File.ReadAllText(filePath);
                connection.Execute(script);
                Console.WriteLine($"Script executado com sucesso: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar script {Path.GetFileName(filePath)}: {ex.Message}");
                throw;
            }
        }
    }
}
