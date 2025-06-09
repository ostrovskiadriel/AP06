public interface ICursoOnlineRepository : IRepository<CursoOnline>
{
    bool ExisteTitulo(string titulo);
}