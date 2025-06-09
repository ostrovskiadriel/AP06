public class CatalogoCursosService
{
    private readonly ICursoOnlineRepository _repo;

    public CatalogoCursosService(ICursoOnlineRepository repo)
    {
        _repo = repo;
    }

    public bool RegistrarCurso(CursoOnline curso)
    {
        if (_repo.ExisteTitulo(curso.Titulo))
        {
            Console.WriteLine($"Curso com título \"{curso.Titulo}\" já está cadastrado.");
            return false;
        }

        _repo.Adicionar(curso);
        Console.WriteLine($"Curso \"{curso.Titulo}\" registrado com sucesso!");
        return true;
    }

    public void ListarCursos()
    {
        var cursos = _repo.ObterTodos();
        foreach (var c in cursos)
        {
            Console.WriteLine($"- {c.Titulo} ({c.Categoria}) | Instrutor: {c.Instrutor} | Duração: {c.Duracao.TotalHours}h");
        }
    }
}