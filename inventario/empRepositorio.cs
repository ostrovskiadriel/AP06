public class InMemoryEquipamentoTIRepository : IEquipamentoTIRepository
{
    private readonly List<EquipamentoTI> _lista = new();

    public void Adicionar(EquipamentoTI entidade)
    {
        _lista.Add(entidade);
    }

    public EquipamentoTI? ObterPorId(Guid id)
    {
        return _lista.FirstOrDefault(e => e.Id == id);
    }

    public List<EquipamentoTI> ObterTodos()
    {
        return _lista.ToList(); 
    }

    public void Atualizar(EquipamentoTI entidade)
    {
        var index = _lista.FindIndex(e => e.Id == entidade.Id);
        if (index != -1)
            _lista[index] = entidade;
    }

    public bool Remover(Guid id)
    {
        return _lista.RemoveAll(e => e.Id == id) > 0;
    }
}
