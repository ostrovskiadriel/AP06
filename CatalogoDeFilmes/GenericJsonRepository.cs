using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CatalogoDeFilmes
{
    // Implementa o CRUD genérico, lendo e escrevendo para um arquivo JSON.
    public class GenericJsonRepository<T> : IRepository<T> where T : IEntidade
    {
        private readonly string _caminhoArquivo;
        protected readonly JsonSerializerOptions _jsonOptions;

        public GenericJsonRepository()
        {
            string nomeArquivo = $"{typeof(T).Name.ToLower()}s.json";
            _caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        // Métodos de leitura e escrita agora são 'protected' para que classes filhas possam usá-los se necessário.
        protected List<T> LerDoArquivo()
        {
            if (!File.Exists(_caminhoArquivo)) return new List<T>();
            string json = File.ReadAllText(_caminhoArquivo);
            return string.IsNullOrWhiteSpace(json) ? new List<T>() : JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
        }

        protected void EscreverNoArquivo(List<T> entidades)
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

        public IEnumerable<T> ObterTodos()
        {
            return LerDoArquivo();
        }

        public T ObterPorId(Guid id)
        {
            return LerDoArquivo().FirstOrDefault(e => e.Id == id);
        }

        public bool Remover(Guid id)
        {
            var lista = LerDoArquivo();
            var item = lista.FirstOrDefault(e => e.Id == id);
            if (item != null)
            {
                lista.Remove(item);
                EscreverNoArquivo(lista);
                return true;
            }
            return false;
        }

        public void Atualizar(T entidade)
        {
            var lista = LerDoArquivo();
            int index = lista.FindIndex(e => e.Id == entidade.Id);
            if (index != -1)
            {
                lista[index] = entidade;
                EscreverNoArquivo(lista);
            }
        }
    }
}