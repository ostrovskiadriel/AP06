using System;
using System.Linq;

namespace CatalogoDeFilmes
{
    public class Program
    {
        private static readonly IFilmeRepository repositorio = new FilmeJsonRepository();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Catálogo de Filmes ---");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Todos os Filmes");
                Console.WriteLine("3. Atualizar Filme");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("5. Buscar Filmes por Gênero");
                Console.WriteLine("6. Sair");
                Console.Write("\nEscolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": AdicionarFilme(); break;
                    case "2": ListarTodos(); break;
                    case "3": AtualizarFilme(); break;
                    case "4": RemoverFilme(); break;
                    case "5": BuscarPorGenero(); break;
                    case "6": Console.WriteLine("Saindo..."); return;
                    default: Console.WriteLine("Opção inválida."); break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void AdicionarFilme()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Filme ---");
            
            var filme = new Filme();

            Console.Write("Título: ");
            filme.Titulo = Console.ReadLine();

            Console.Write("Diretor: ");
            filme.Diretor = Console.ReadLine();

            Console.Write("Gênero: ");
            filme.Genero = Console.ReadLine();
            
            int ano;
            do
            {
                Console.Write("Ano de Lançamento: ");
            } while (!int.TryParse(Console.ReadLine(), out ano));
            filme.AnoLancamento = ano;

            repositorio.Adicionar(filme);
            Console.WriteLine("\nFilme adicionado com sucesso!");
        }

        private static void ListarTodos()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Todos os Filmes ---");
            var filmes = repositorio.ObterTodos();
            
            if (!filmes.Any())
            {
                Console.WriteLine("Nenhum filme cadastrado.");
                return;
            }

            foreach (var filme in filmes)
            {
                Console.WriteLine($"ID: {filme.Id}");
                Console.WriteLine($"   {filme}\n");
            }
        }

        private static void AtualizarFilme()
        {
            Console.Clear();
            Console.WriteLine("--- Atualizar Filme ---");
            Console.Write("Digite o ID do filme para atualizar: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var filme = repositorio.ObterPorId(id);
            if (filme == null)
            {
                Console.WriteLine("Filme não encontrado.");
                return;
            }

            Console.WriteLine($"\nEditando: '{filme.Titulo}'. Deixe em branco para não alterar.");

            Console.Write($"Título atual: {filme.Titulo}\nNovo Título: ");
            string novoTitulo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoTitulo)) filme.Titulo = novoTitulo;

            Console.Write($"Diretor atual: {filme.Diretor}\nNovo Diretor: ");
            string novoDiretor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoDiretor)) filme.Diretor = novoDiretor;
            
            Console.Write($"Gênero atual: {filme.Genero}\nNovo Gênero: ");
            string novoGenero = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoGenero)) filme.Genero = novoGenero;

            Console.Write($"Ano atual: {filme.AnoLancamento}\nNovo Ano: ");
            string novoAnoStr = Console.ReadLine();
            if (int.TryParse(novoAnoStr, out int novoAno)) filme.AnoLancamento = novoAno;

            repositorio.Atualizar(filme);
            Console.WriteLine("\nFilme atualizado com sucesso!");
        }

        private static void RemoverFilme()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Filme ---");
            Console.Write("Digite o ID do filme para remover: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var filme = repositorio.ObterPorId(id);
            if (filme == null)
            {
                Console.WriteLine("Filme não encontrado.");
                return;
            }
            
            Console.WriteLine($"\nFilme encontrado: {filme}");
            Console.Write("Tem certeza que deseja remover este filme? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                repositorio.Remover(id);
                Console.WriteLine("Filme removido com sucesso.");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }

        private static void BuscarPorGenero()
        {
            Console.Clear();
            Console.WriteLine("--- Buscar Filmes por Gênero ---");
            Console.Write("Digite o gênero: ");
            string genero = Console.ReadLine();

            var filmes = repositorio.ObterPorGenero(genero);

            Console.WriteLine($"\nResultados para '{genero}':");
            if (!filmes.Any())
            {
                Console.WriteLine("Nenhum filme encontrado com este gênero.");
                return;
            }

            foreach (var filme in filmes)
            {
                Console.WriteLine(filme);
            }
        }
    }
}