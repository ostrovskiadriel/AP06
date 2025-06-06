class Program
{
    static void Main()
    {
        IEquipamentoTIRepository repo = new InMemoryEquipamentoTIRepository();

        repo.Adicionar(new EquipamentoTI
        {
            Id = Guid.NewGuid(),
            NomeEquipamento = "Notebook Dell G15",
            TipoEquipamento = "Notebook",
            NumeroSerie = "ABC12345",
            DataAquisicao = new DateTime(2023, 6, 1)
        });

        repo.Adicionar(new EquipamentoTI
        {
            Id = Guid.NewGuid(),
            NomeEquipamento = "Monitor LG UltraWide",
            TipoEquipamento = "Monitor",
            NumeroSerie = "XYZ67890",
            DataAquisicao = new DateTime(2022, 12, 15)
        });

        // Listar
        Console.WriteLine("Equipamentos cadastrados:");
        foreach (var e in repo.ObterTodos())
        {
            Console.WriteLine($"- {e.NomeEquipamento} ({e.TipoEquipamento}) - Série: {e.NumeroSerie} - Aquis.: {e.DataAquisicao:dd/MM/yyyy}");
        }
    }
}
