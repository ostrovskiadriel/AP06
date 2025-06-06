using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GerenciadorDeProdutos
{
    public class GerenciadorDeProdutos
    {
        private readonly string _caminhoArquivo = "produtos.json";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // Método privado para ler todos os produtos do arquivo.
        private List<Produto> LerProdutosDoArquivo()
        {
            if (!File.Exists(_caminhoArquivo))
            {
                return new List<Produto>();
            }

            string json = File.ReadAllText(_caminhoArquivo);
            if (string.IsNullOrEmpty(json))
            {
                return new List<Produto>();
            }

            return JsonSerializer.Deserialize<List<Produto>>(json, _jsonOptions);
        }

        // Método privado para escrever a lista de produtos no arquivo.
        private void EscreverProdutosNoArquivo(List<Produto> produtos)
        {
            string json = JsonSerializer.Serialize(produtos, _jsonOptions);
            File.WriteAllText(_caminhoArquivo, json);
        }

        // --- MÉTODOS PÚBLICOS PARA MANIPULAR OS PRODUTOS ---

        public void Adicionar(Produto produto)
        {
            var produtos = LerProdutosDoArquivo();
            produtos.Add(produto);
            EscreverProdutosNoArquivo(produtos);
        }

        public List<Produto> ObterTodos()
        {
            return LerProdutosDoArquivo();
        }

        public Produto ObterPorId(Guid id)
        {
            var produtos = LerProdutosDoArquivo();
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public void Atualizar(Produto produtoAtualizado)
        {
            var produtos = LerProdutosDoArquivo();
            var produtoExistente = produtos.FirstOrDefault(p => p.Id == produtoAtualizado.Id);

            if (produtoExistente != null)
            {
                produtoExistente.Nome = produtoAtualizado.Nome;
                produtoExistente.Descricao = produtoAtualizado.Descricao;
                produtoExistente.Preco = produtoAtualizado.Preco;
                produtoExistente.Estoque = produtoAtualizado.Estoque;
                EscreverProdutosNoArquivo(produtos);
            }
        }

        public bool Remover(Guid id)
        {
            var produtos = LerProdutosDoArquivo();
            var produtoParaRemover = produtos.FirstOrDefault(p => p.Id == id);

            if (produtoParaRemover != null)
            {
                produtos.Remove(produtoParaRemover);
                EscreverProdutosNoArquivo(produtos);
                return true;
            }

            return false;
        }
    }
}