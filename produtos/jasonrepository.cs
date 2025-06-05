using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class ProdutoJsonRepository : IProdutoRepository
{
    private const string CaminhoArquivo = "produtos.json";
    private List<Produto> produtos;

    public ProdutoJsonRepository()
    {
        produtos = CarregarDoArquivo();
    }

    public void Adicionar(Produto produto)
    {
        produtos.Add(produto);
        SalvarNoArquivo();
    }

    public Produto? ObterPorId(Guid id)
    {
        return produtos.FirstOrDefault(p => p.Id == id);
    }

    public List<Produto> ObterTodos()
    {
        return new List<Produto>(produtos);
    }

    public void Atualizar(Produto produto)
    {
        var existente = ObterPorId(produto.Id);
        if (existente != null)
        {
            existente.Nome = produto.Nome;
            existente.Descricao = produto.Descricao;
            existente.Preco = produto.Preco;
            existente.Estoque = produto.Estoque;
            SalvarNoArquivo();
        }
    }

    public bool Remover(Guid id)
    {
        var produto = ObterPorId(id);
        if (produto != null)
        {
            produtos.Remove(produto);
            SalvarNoArquivo();
            return true;
        }
        return false;
    }

    

    private List<Produto> CarregarDoArquivo()
    {
        if (!File.Exists(CaminhoArquivo))
            return new List<Produto>();

        var json = File.ReadAllText(CaminhoArquivo);
        return JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
    }

    private void SalvarNoArquivo()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(produtos, options);
        File.WriteAllText(CaminhoArquivo, json);
    }
}
