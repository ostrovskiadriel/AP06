using System.Text.Json;

public class GenericJsonRepository<T> : IRepository<T> where T : IEntidade
{
    protected readonly string caminhoArquivo;

    public GenericJsonRepository(string nomeArquivo)
    {
        caminhoArquivo = nomeArquivo;
        if (!File.Exists(caminhoArquivo))
            File.WriteAllText(caminhoArquivo, "[]");
    }

    public void Adicionar(T entidade)
    {
        var lista = ObterTodos();
        lista.Add(entidade);
        SalvarTodos(lista);
    }

    public T? ObterPorId(Guid id)
    {
        return ObterTodos().FirstOrDefault(e => e.Id == id);
    }

    public List<T> ObterTodos()
    {
        var json = File.ReadAllText(caminhoArquivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public void Atualizar(T entidade)
    {
        var lista = ObterTodos();
        var index = lista.FindIndex(e => e.Id == entidade.Id);
        if (index != -1)
        {
            lista[index] = entidade;
            SalvarTodos(lista);
        }
    }

    public bool Remover(Guid id)
    {
        var lista = ObterTodos();
        var removido = lista.RemoveAll(e => e.Id == id) > 0;
        if (removido)
            SalvarTodos(lista);
        return removido;
    }

    protected void SalvarTodos(List<T> lista)
    {
        var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }
}
