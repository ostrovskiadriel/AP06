using System.Collections.Generic;

namespace CatalogoDeFilmes
{
    // Interface especializada que herda o CRUD de IRepository e adiciona métodos específicos para Filme.
    public interface IFilmeRepository : IRepository<Filme>
    {
        IEnumerable<Filme> ObterPorGenero(string genero);
    }
}