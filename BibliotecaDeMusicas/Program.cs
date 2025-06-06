using System;
using System.Linq;

namespace BibliotecaDeMusicas
{
    public class Program
    {
        private static readonly GenericJsonRepository<Musica> repositorioDeMusicas = new GenericJsonRepository<Musica>();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Biblioteca de Músicas Pessoais ---");
                Console.WriteLine("1. Adicionar Nova Música");
                Console.WriteLine("2. Listar Todas as Músicas");
                Console.WriteLine("3. Buscar Música por ID");
                Console.WriteLine("4. Remover Música");
                Console.WriteLine("5. Sair");
                Console.Write("\nDigite sua opção: ");

                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        AdicionarMusica();
                        break;
                    case "2":
                        ListarMusicas();
                        break;
                    case "3":
                        BuscarMusicaPorId();
                        break;
                    case "4":
                        RemoverMusica();
                        break;
                    case "5":
                        Console.WriteLine("Até logo!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void AdicionarMusica()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Nova Música ---");

            var novaMusica = new Musica();

            Console.Write("Título: ");
            novaMusica.Titulo = Console.ReadLine();

            Console.Write("Artista: ");
            novaMusica.Artista = Console.ReadLine();

            Console.Write("Álbum: ");
            novaMusica.Album = Console.ReadLine();

            int segundos = 0;
            while (true)
            {
                Console.Write("Duração (em segundos): ");
                if (int.TryParse(Console.ReadLine(), out segundos) && segundos > 0)
                {
                    novaMusica.Duracao = TimeSpan.FromSeconds(segundos);
                    break;
                }
                Console.WriteLine("Valor inválido. Por favor, digite um número inteiro positivo.");
            }

            repositorioDeMusicas.Adicionar(novaMusica);
            Console.WriteLine("\nMúsica adicionada com sucesso!");
        }

        private static void ListarMusicas()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Músicas na Biblioteca ---");

            var todasAsMusicas = repositorioDeMusicas.ObterTodos();

            if (!todasAsMusicas.Any())
            {
                Console.WriteLine("Nenhuma música cadastrada.");
                return;
            }

            foreach (var musica in todasAsMusicas)
            {
                Console.WriteLine($"ID: {musica.Id}");
                Console.WriteLine($"   {musica}");
                Console.WriteLine("-------------------------------------------------");
            }
        }

        private static void BuscarMusicaPorId()
        {
            Console.Clear();
            Console.WriteLine("--- Buscar Música por ID ---");

            Console.Write("Digite o ID da música: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid idParaBuscar))
            {
                Console.WriteLine("ID em formato inválido.");
                return;
            }

            var musicaEncontrada = repositorioDeMusicas.ObterPorId(idParaBuscar);

            if (musicaEncontrada != null)
            {
                Console.WriteLine("\nMúsica encontrada:");
                Console.WriteLine(musicaEncontrada);
            }
            else
            {
                Console.WriteLine("\nNenhuma música encontrada com este ID.");
            }
        }

        private static void RemoverMusica()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Música ---");
            ListarMusicas(); 
            Console.Write("\nDigite o ID da música que deseja remover: ");

            if (!Guid.TryParse(Console.ReadLine(), out Guid idParaRemover))
            {
                Console.WriteLine("ID em formato inválido.");
                return;
            }

            var musica = repositorioDeMusicas.ObterPorId(idParaRemover);
            if (musica == null)
            {
                Console.WriteLine("Nenhuma música encontrada com este ID.");
                return;
            }

            Console.WriteLine($"\nVocê tem certeza que deseja remover '{musica.Titulo}'? (s/n)");
            string confirmacao = Console.ReadLine().ToLower();

            if (confirmacao == "s")
            {
                bool foiRemovida = repositorioDeMusicas.Remover(idParaRemover);
                if (foiRemovida)
                {
                    Console.WriteLine("Música removida com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro ao tentar remover a música.");
                }
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
    }
}