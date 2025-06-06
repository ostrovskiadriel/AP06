using System;

// Define o namespace para organizar o código
namespace GerenciadorDeProdutos
{
    // Esta classe representa os dados de um produto.
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        public Produto()
        {
            Id = Guid.NewGuid();
        }
    }
}