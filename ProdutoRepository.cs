using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AppProdutos.Models;
using System.IO;

namespace AppProdutos.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;
        private readonly string _logPath = "log.txt";

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private void RegistrarLog(string mensagem)
        {
            try
            {
                string logMensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}\n";
                File.AppendAllText(_logPath, logMensagem);
            }
            catch { /* Evita que falha de log quebre a aplicação */ }
        }

        public void Inserir(Produto produto)
        {
            string query = "INSERT INTO Produtos (Nome, Preco, Estoque, Categoria) VALUES (@Nome, @Preco, @Estoque, @Categoria)";

            try
            {
                using (var conexao = new SqliteConnection(_connectionString))
                {
                    conexao.Open();
                    using (var comando = new SqliteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Nome", produto.Nome);
                        comando.Parameters.AddWithValue("@Preco", produto.Preco);
                        comando.Parameters.AddWithValue("@Estoque", produto.Estoque);
                        comando.Parameters.AddWithValue("@Categoria", produto.Categoria);

                        comando.ExecuteNonQuery();
                        RegistrarLog($"Produto inserido com sucesso: {produto.Nome}");
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarLog($"Erro ao inserir produto: {ex.Message}");
                throw new Exception("Erro ao acessar o banco de dados durante a inserção.", ex);
            }
        }

        public List<Produto> Listar()
        {
            var produtos = new List<Produto>();
            string query = "SELECT Id, Nome, Preco, Estoque, Categoria FROM Produtos";

            try
            {
                using (var conexao = new SqliteConnection(_connectionString))
                {
                    conexao.Open();
                    using (var comando = new SqliteCommand(query, conexao))
                    {
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                produtos.Add(new Produto
                                {
                                    Id = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Preco = reader.GetDecimal(2),
                                    Estoque = reader.GetInt32(3),
                                    Categoria = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
                RegistrarLog("Listagem de produtos realizada com sucesso.");
            }
            catch (Exception ex)
            {
                RegistrarLog($"Erro ao listar produtos: {ex.Message}");
                throw new Exception("Erro ao listar os produtos.", ex);
            }

            return produtos;
        }

        public Produto BuscarPorId(int id)
        {
            string query = "SELECT Id, Nome, Preco, Estoque, Categoria FROM Produtos WHERE Id = @Id";

            try
            {
                using (var conexao = new SqliteConnection(_connectionString))
                {
                    conexao.Open();
                    using (var comando = new SqliteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Id", id);

                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Produto
                                {
                                    Id = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Preco = reader.GetDecimal(2),
                                    Estoque = reader.GetInt32(3),
                                    Categoria = reader.GetString(4)
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                RegistrarLog($"Erro ao buscar produto por ID {id}: {ex.Message}");
                throw new Exception("Erro ao buscar o produto.", ex);
            }
        }

        public void Atualizar(Produto produto)
        {
            string query = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, Estoque = @Estoque, Categoria = @Categoria WHERE Id = @Id";

            try
            {
                using (var conexao = new SqliteConnection(_connectionString))
                {
                    conexao.Open();
                    using (var comando = new SqliteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Id", produto.Id);
                        comando.Parameters.AddWithValue("@Nome", produto.Nome);
                        comando.Parameters.AddWithValue("@Preco", produto.Preco);
                        comando.Parameters.AddWithValue("@Estoque", produto.Estoque);
                        comando.Parameters.AddWithValue("@Categoria", produto.Categoria);

                        int linhasAfetadas = comando.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                            RegistrarLog($"Produto ID {produto.Id} atualizado com sucesso.");
                        else
                            RegistrarLog($"Tentativa de atualizar produto ID {produto.Id} que não existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarLog($"Erro ao atualizar produto ID {produto.Id}: {ex.Message}");
                throw new Exception("Erro ao atualizar o produto.", ex);
            }
        }

        public void Excluir(int id)
        {
            string query = "DELETE FROM Produtos WHERE Id = @Id";

            try
            {
                using (var conexao = new SqliteConnection(_connectionString))
                {
                    conexao.Open();
                    using (var comando = new SqliteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Id", id);
                        int linhasAfetadas = comando.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            RegistrarLog($"Produto ID {id} excluído com sucesso.");
                        else
                            RegistrarLog($"Tentativa de excluir produto ID {id} que não existe.");
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarLog($"Erro ao excluir produto ID {id}: {ex.Message}");
                throw new Exception("Erro ao excluir o produto.", ex);
            }
        }
    }
}