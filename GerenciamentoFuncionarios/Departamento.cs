using System;

namespace GerenciamentoFuncionarios
{
    public class Departamento : IEntidade
    {
        public Guid Id { get; set; }
        public string NomeDepartamento { get; set; }
        public string Sigla { get; set; }

        public Departamento()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"[{Sigla}] {NomeDepartamento}";
        }
    }
}