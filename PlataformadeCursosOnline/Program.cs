var repo = new CursoOnlineJsonRepository();
var servico = new CatalogoCursosService(repo);

var curso1 = new CursoOnline
{
    Id = Guid.NewGuid(),
    Titulo = "Programação Orientada a Objetos em C#",
    Instrutor = "Prof. Silva",
    Categoria = "Desenvolvimento",
    Duracao = TimeSpan.FromHours(12)
};

var curso2 = new CursoOnline
{
    Id = Guid.NewGuid(),
    Titulo = "Programação Orientada a Objetos em C#", // título duplicado
    Instrutor = "Prof. João",
    Categoria = "Desenvolvimento",
    Duracao = TimeSpan.FromHours(10)
};

servico.RegistrarCurso(curso1); // deve registrar
servico.RegistrarCurso(curso2); // deve exibir mensagem de duplicata

Console.WriteLine("\nCatálogo de cursos:");
servico.ListarCursos();