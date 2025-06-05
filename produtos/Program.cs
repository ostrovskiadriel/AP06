using System;

class Program
{
    static void Main()
    {
        var repositorio = new ProdutoJsonRepository();

        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Notebook Dell",
            Descricao = "Notebook com 16GB RAM e SSD 512GB",
            Preco = 4500.00m,
            Estoque = 10
        };

        repositorio.Adicionar(produto);
        Console.WriteLine("Produto adicionado!");

        Console.WriteLine("\nLista de Produtos:");
        foreach (var p in repositorio.ObterTodos())
        {
            Console.WriteLine($"{p.Nome} - {p.Descricao} - R$ {p.Preco} - Estoque: {p.Estoque}");
        }

        var produtoEncontrado = repositorio.ObterPorId(produto.Id);
        if (produtoEncontrado != null)
        {
            Console.WriteLine($"\nProduto encontrado: {produtoEncontrado.Nome}");
        }

        produto.Preco = 4200.00m;
        repositorio.Atualizar(produto);
        Console.WriteLine("\nProduto atualizado!");

        repositorio.Remover(produto.Id);
        Console.WriteLine("\nProduto removido!");
    }
}
