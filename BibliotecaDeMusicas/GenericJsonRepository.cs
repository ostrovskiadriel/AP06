using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BibliotecaDeMusicas
{
    // Um repositório genérico que funciona com qualquer classe que implemente IEntidade.
    public class GenericJsonRepository<T> where T : IEntidade
    {
        private readonly string _caminhoArquivo;
        private readonly JsonSerializerOptions _jsonOptions;

        public GenericJsonRepository()
        {
            // Gera o nome do arquivo automaticamente com base no nome do tipo (ex: Musica -> musicas.json)
            string nomeArquivo = $"{typeof(T).Name.ToLower()}s.json";
            _caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        private List<T> LerDoArquivo()
        {
            if (!File.Exists(_caminhoArquivo))
            {
                return new List<T>();
            }
            string json = File.ReadAllText(_caminhoArquivo);
            return string.IsNullOrWhiteSpace(json) ? new List<T>() : JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
        }

        private void EscreverNoArquivo(List<T> entidades)
        {
            string json = JsonSerializer.Serialize(entidades, _jsonOptions);
            File.WriteAllText(_caminhoArquivo, json);
        }

        public void Adicionar(T entidade)
        {
            var lista = LerDoArquivo();
            lista.Add(entidade);
            EscreverNoArquivo(lista);
        }

        public List<T> ObterTodos()
        {
            return LerDoArquivo();
        }

        public T ObterPorId(Guid id)
        {
            var lista = LerDoArquivo();
            // A restrição "where T : IEntidade" garante que a propriedade "Id" sempre existirá.
            return lista.FirstOrDefault(entidade => entidade.Id == id);
        }

        public bool Remover(Guid id)
        {
            var lista = LerDoArquivo();
            var itemParaRemover = lista.FirstOrDefault(entidade => entidade.Id == id);
            if (itemParaRemover != null)
            {
                lista.Remove(itemParaRemover);
                EscreverNoArquivo(lista);
                return true;
            }
            return false;
        }
    }
}