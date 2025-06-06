using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GerenciamentoFuncionarios
{
    public class GenericJsonRepository<T> where T : IEntidade
    {
        private readonly string _caminhoArquivo;
        private readonly JsonSerializerOptions _jsonOptions;

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

        public void Adicionar(T entidade)
        {
            var lista = ObterTodos().ToList();
            lista.Add(entidade);
            EscreverNoArquivo(lista);
        }

        public T? ObterPorId(Guid id)
        {
            return ObterTodos().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> ObterTodos()
        {
            if (!File.Exists(_caminhoArquivo)) return new List<T>();
            string json = File.ReadAllText(_caminhoArquivo);
            if (string.IsNullOrWhiteSpace(json)) return new List<T>();

            var lista = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);
            return lista ?? new List<T>();
        }

        public void Atualizar(T entidade)
        {
            var lista = ObterTodos().ToList();
            int index = lista.FindIndex(e => e.Id == entidade.Id);
            if (index != -1)
            {
                lista[index] = entidade;
                EscreverNoArquivo(lista);
            }
        }

        public bool Remover(Guid id)
        {
            var lista = ObterTodos().ToList();
            var itemParaRemover = lista.FirstOrDefault(e => e.Id == id);
            if (itemParaRemover != null)
            {
                lista.Remove(itemParaRemover);
                EscreverNoArquivo(lista);
                return true;
            }
            return false;
        }

        public void EscreverNoArquivo(List<T> lista)
        {
            string json = JsonSerializer.Serialize(lista, _jsonOptions);
            File.WriteAllText(_caminhoArquivo, json);
        }
    }
}