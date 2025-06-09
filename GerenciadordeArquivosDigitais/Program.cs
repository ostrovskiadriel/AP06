using System;

class Program
{
    static void Main()
    {
        IArquivoDigitalRepository repositorio = new ArquivoDigitalJsonRepository();

        var novoArquivo = new ArquivoDigital
        {
            Id = Guid.NewGuid(),
            NomeArquivo = "documento.pdf",
            TipoArquivo = "pdf",
            TamanhoBytes = 1500000,
            DataUpload = DateTime.Now
        };

        repositorio.Adicionar(novoArquivo);

        Console.WriteLine("Arquivo adicionado!");

        var todos = repositorio.ObterTodos();
        foreach (var arquivo in todos)
        {
            Console.WriteLine($"{arquivo.NomeArquivo} ({arquivo.TipoArquivo}) - {arquivo.TamanhoBytes} bytes - {arquivo.DataUpload}");
        }
    }
}
