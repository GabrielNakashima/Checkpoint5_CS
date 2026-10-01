using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using AppProdutos.Models;
using AppProdutos.Repositories;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"[TESTE DE CONEXÃO]: Tentando conectar em -> {connectionString}");
        var repository = new ProdutoRepository(connectionString);

        int opcao = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE CADASTRO DE PRODUTOS ===");
            Console.WriteLine("1. Inserir produto");
            Console.WriteLine("2. Listar produtos");
            Console.WriteLine("3. Buscar produto por ID");
            Console.WriteLine("4. Atualizar produto");
            Console.WriteLine("5. Excluir produto");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida! Pressione ENTER para continuar.");
                Console.ReadLine();
                continue;
            }

            try
            {
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--- Inserir Novo Produto ---");

                        var novoProduto = new Produto(); 

                        Console.Write("Nome: ");
                        novoProduto.Nome = Console.ReadLine();
                        Console.Write("Preço: ");
                        novoProduto.Preco = decimal.Parse(Console.ReadLine());
                        Console.Write("Estoque: ");
                        novoProduto.Estoque = int.Parse(Console.ReadLine());
                        Console.Write("Categoria: ");
                        novoProduto.Categoria = Console.ReadLine();

                        repository.Inserir(novoProduto);
                        Console.WriteLine("\nProduto inserido com sucesso!");
                        break;

                    case 2:
                    Console.Clear();
                    Console.WriteLine("--- Lista de Produtos ---");
                    var produtos = repository.Listar();
                    foreach (var p in produtos)
                    {
                        Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: R$ {p.Preco:F2} | Estoque: {p.Estoque} | Categoria: {p.Categoria}");
                    }
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("--- Buscar Produto por ID ---");
                    Console.Write("Digite o ID: ");
                    int buscaId = int.Parse(Console.ReadLine());
                    var encontrado = repository.BuscarPorId(buscaId);

                    if (encontrado != null)
                    {
                        Console.WriteLine($"\nID: {encontrado.Id}\nNome: {encontrado.Nome}\nPreço: R$ {encontrado.Preco:F2}\nEstoque: {encontrado.Estoque}\nCategoria: {encontrado.Categoria}");
                    }
                    else
                    {
                        Console.WriteLine("\nProduto não encontrado.");
                    }
                    break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("--- Atualizar Produto ---");
                        Console.Write("Digite o ID do produto a atualizar: ");
                        int atualizaId = int.Parse(Console.ReadLine());

                        var prodExistente = repository.BuscarPorId(atualizaId);
                        if (prodExistente == null)
                        {
                            Console.WriteLine("\nProduto não encontrado.");
                            break;
                        }

                        var produtoAtualizado = new Produto();
                        produtoAtualizado.Id = atualizaId;

                        Console.Write($"Novo Nome [{prodExistente.Nome}]: ");
                        produtoAtualizado.Nome = Console.ReadLine();
                        Console.Write($"Novo Preço [{prodExistente.Preco}]: ");
                        produtoAtualizado.Preco = decimal.Parse(Console.ReadLine());
                        Console.Write($"Novo Estoque [{prodExistente.Estoque}]: ");
                        produtoAtualizado.Estoque = int.Parse(Console.ReadLine());
                        Console.Write($"Nova Categoria [{prodExistente.Categoria}]: ");
                        produtoAtualizado.Categoria = Console.ReadLine();

                        repository.Atualizar(produtoAtualizado);
                        Console.WriteLine("\nProduto atualizado com sucesso!");
                        break;

                    case 5:
                    Console.Clear();
                    Console.WriteLine("--- Excluir Produto ---");
                    Console.Write("Digite o ID do produto a excluir: ");
                    int excluiId = int.Parse(Console.ReadLine());
                    repository.Excluir(excluiId);
                    Console.WriteLine("\nOperação de exclusão realizada.");
                    break;

                case 6:
                    Console.WriteLine("Saindo do sistema...");
                    break;

                default:
                    Console.WriteLine("Opção desconhecida.");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO]: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DETALHE DO ERRO]: {ex.InnerException.Message}");
                }
            }

            if (opcao != 6)
            {
                Console.WriteLine("\nPressione ENTER para voltar ao menu.");
                Console.ReadLine();
            }

        } while (opcao != 6);
    }
}