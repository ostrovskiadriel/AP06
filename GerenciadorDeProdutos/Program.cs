using System;
using System.Linq;

namespace GerenciadorDeProdutos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gerenciador = new GerenciadorDeProdutos();

            Console.WriteLine("--- Gerenciador de Catálogo de Produtos ---");

            Console.WriteLine("\nAdicionando um novo produto...");
            var novoProduto = new Produto
            {
                Nome = "Teclado Mecânico",
                Descricao = "Teclado com switches blue e RGB.",
                Preco = 350.00m,
                Estoque = 30
            };
            gerenciador.Adicionar(novoProduto);
            Console.WriteLine($"Produto '{novoProduto.Nome}' adicionado com sucesso!");

            Console.WriteLine("\n--- Lista de Produtos Atuais ---");
            var todosOsProdutos = gerenciador.ObterTodos();
            if (todosOsProdutos.Any())
            {
                foreach (var p in todosOsProdutos)
                {
                    Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: R$ {p.Preco:F2}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            
            Console.WriteLine("\n--- Fim da Execução ---");
        }
    }
}