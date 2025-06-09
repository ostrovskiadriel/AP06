public class CursoOnlineJsonRepository : GenericJsonRepository<CursoOnline>, ICursoOnlineRepository
{
    public CursoOnlineJsonRepository() : base("cursos.json") { }

    public bool ExisteTitulo(string titulo)
    {
        return ObterTodos().Any(c => c.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
    }
}