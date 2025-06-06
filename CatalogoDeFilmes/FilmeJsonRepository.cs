using System;
using System.Collections.Generic;
using System.Linq;

namespace CatalogoDeFilmes
{
    public class FilmeJsonRepository : GenericJsonRepository<Filme>, IFilmeRepository
    {
        public IEnumerable<Filme> ObterPorGenero(string genero)
        {
            var todosOsFilmes = ObterTodos();
            return todosOsFilmes.Where(filme => filme.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase));
        }
    }
}