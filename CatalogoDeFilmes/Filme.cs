using System;

namespace CatalogoDeFilmes
{
    public class Filme : IEntidade
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Diretor { get; set; }
        public int AnoLancamento { get; set; }
        public string Genero { get; set; }

        public Filme()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"'{Titulo}' ({AnoLancamento}) | Diretor: {Diretor} | Gênero: {Genero}";
        }
    }
}