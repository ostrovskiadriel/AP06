using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GenericJsonRepository<T> : IRepository<T> where T : IEntidade
{
    protected readonly string _caminhoArquivo;

    public GenericJsonRepository(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
    }

    protected List<T> Carregar()
    {
        if (!File.Exists(_caminhoArquivo))
            return new List<T>();

        var json = File.ReadAllText(_caminhoArquivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    protected void Salvar(List<T> lista)
    {
        var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_caminhoArquivo, json);
    }

    public void Adicionar(T entidade)
    {
        var lista = Carregar();
        lista.Add(entidade);
        Salvar(lista);
    }

    public T? ObterPorId(Guid id)
    {
        return Carregar().FirstOrDefault(e => e.Id == id);
    }

    public List<T> ObterTodos()
    {
        return Carregar();
    }

    public void Atualizar(T entidade)
    {
        var lista = Carregar();
        var index = lista.FindIndex(e => e.Id == entidade.Id);
        if (index >= 0)
        {
            lista[index] = entidade;
            Salvar(lista);
        }
    }

    public bool Remover(Guid id)
    {
        var lista = Carregar();
        var entidade = lista.FirstOrDefault(e => e.Id == id);
        if (entidade != null)
        {
            lista.Remove(entidade);
            Salvar(lista);
            return true;
        }
        return false;
    }
}