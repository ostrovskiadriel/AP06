class Program
{
    static void Main()
    {
        IPacienteRepository repo = new PacienteJsonRepository();

        repo.Adicionar(new Paciente
        {
            Id = Guid.NewGuid(),
            NomeCompleto = "Alice",
            DataNascimento = new DateTime(1990, 5, 1),
            ContatoEmergencia = "123456"
        });

        repo.Adicionar(new Paciente
        {
            Id = Guid.NewGuid(),
            NomeCompleto = "Bruno",
            DataNascimento = new DateTime(2005, 8, 15),
            ContatoEmergencia = "987654"
        });

        Console.WriteLine("Todos os pacientes:");
        foreach (var p in repo.ObterTodos())
            Console.WriteLine($"{p.NomeCompleto} - Nasc: {p.DataNascimento:dd/MM/yyyy}");

        Console.WriteLine("\nPacientes com idade entre 18 e 35:");
        var filtrados = repo.ObterPorFaixaEtaria(18, 35);
        foreach (var p in filtrados)
            Console.WriteLine($"{p.NomeCompleto} ({p.DataNascimento:dd/MM/yyyy})");
    }
}
