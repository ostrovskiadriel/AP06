using System;
using System.Collections.Generic;

namespace CatalogoDeFilmes
{
    // Contrato genérico para operações CRUD.
    public interface IRepository<T> where T : IEntidade
    {
        void Adicionar(T entidade);
        T ObterPorId(Guid id);
        IEnumerable<T> ObterTodos();
        void Atualizar(T entidade);
        bool Remover(Guid id);
    }
}