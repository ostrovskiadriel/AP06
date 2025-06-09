using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GenericJsonRepository<T> : IRepository<T> where T : IEntidade
{
    private readonly string _arquivo;
    private List<T> _dados;

    public GenericJsonRepository(string nomeArquivo)
    {
        _arquivo = nomeArquivo;
        _dados = Carregar();
    }

    public void Adicionar(T entidade)
    {
        _dados.Add(entidade);
        Salvar();
    }

    public T? ObterPorId(Guid id)
    {
        return _dados.FirstOrDefault(e => e.Id == id);
    }

    public List<T> ObterTodos()
    {
        return new List<T>(_dados);
    }

    public void Atualizar(T entidade)
    {
        var index = _dados.FindIndex(e => e.Id == entidade.Id);
        if (index != -1)
        {
            _dados[index] = entidade;
            Salvar();
        }
    }

    public bool Remover(Guid id)
    {
        var entidade = ObterPorId(id);
        if (entidade != null)
        {
            _dados.Remove(entidade);
            Salvar();
            return true;
        }
        return false;
    }

    private List<T> Carregar()
    {
        if (!File.Exists(_arquivo)) return new List<T>();
        var json = File.ReadAllText(_arquivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(_dados, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_arquivo, json);
    }
}