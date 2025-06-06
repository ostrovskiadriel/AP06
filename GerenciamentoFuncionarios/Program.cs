using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoFuncionarios
{
    public class Program
    {
        private static readonly GenericJsonRepository<Departamento> repoDepartamentos = new();
        private static readonly GenericJsonRepository<Funcionario> repoFuncionarios = new();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Sistema de Gerenciamento ---");
                Console.WriteLine("1. Gerenciar Departamentos");
                Console.WriteLine("2. Gerenciar Funcionários");
                Console.WriteLine("3. Sair");
                Console.Write("\nEscolha uma área para gerenciar: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        MenuDepartamentos();
                        break;
                    case "2":
                        MenuFuncionarios();
                        break;
                    case "3":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Pausar();
                        break;
                }
            }
        }

        private static void MenuDepartamentos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Gerenciar Departamentos ---");
                Console.WriteLine("1. Adicionar Departamento");
                Console.WriteLine("2. Listar Departamentos");
                Console.WriteLine("3. Atualizar Departamento");
                Console.WriteLine("4. Remover Departamento");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": AdicionarDepartamento(); break;
                    case "2": ListarDepartamentos(); break;
                    case "3": AtualizarDepartamento(); break;
                    case "4": RemoverDepartamento(); break;
                    case "5": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
                Pausar();
            }
        }

        private static void MenuFuncionarios()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Gerenciar Funcionários ---");
                Console.WriteLine("1. Adicionar Funcionário");
                Console.WriteLine("2. Listar Funcionários");
                Console.WriteLine("3. Atualizar Funcionário");
                Console.WriteLine("4. Remover Funcionário");
                Console.WriteLine("5. Voltar ao Menu Principal");
                Console.Write("\nEscolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": AdicionarFuncionario(); break;
                    case "2": ListarFuncionarios(); break;
                    case "3": AtualizarFuncionario(); break;
                    case "4": RemoverFuncionario(); break;
                    case "5": return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
                Pausar();
            }
        }

        // --- Funções de Departamento ---

        private static void AdicionarDepartamento()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Departamento ---");
            var depto = new Departamento();
            Console.Write("Nome do Departamento: ");
            depto.NomeDepartamento = Console.ReadLine();
            Console.Write("Sigla: ");
            depto.Sigla = Console.ReadLine();
            repoDepartamentos.Adicionar(depto);
            Console.WriteLine("\nDepartamento adicionado com sucesso!");
        }

        private static void ListarDepartamentos()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Departamentos ---");
            var deptos = repoDepartamentos.ObterTodos();
            if (!deptos.Any())
            {
                Console.WriteLine("Nenhum departamento cadastrado.");
                return;
            }
            foreach (var depto in deptos)
            {
                Console.WriteLine($"ID: {depto.Id} | {depto}");
            }
        }

        private static void AtualizarDepartamento()
        {
            ListarDepartamentos();
            Console.Write("\nDigite o ID do departamento para atualizar: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var depto = repoDepartamentos.ObterPorId(id);
            if (depto == null)
            {
                Console.WriteLine("Departamento não encontrado.");
                return;
            }
            
            Console.Write($"Novo Nome ({depto.NomeDepartamento}): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome)) depto.NomeDepartamento = novoNome;

            Console.Write($"Nova Sigla ({depto.Sigla}): ");
            string novaSigla = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaSigla)) depto.Sigla = novaSigla;

            repoDepartamentos.Atualizar(depto);
            Console.WriteLine("\nDepartamento atualizado!");
        }

        private static void RemoverDepartamento()
        {
            ListarDepartamentos();
            Console.Write("\nDigite o ID do departamento para remover: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            bool existeFuncionario = repoFuncionarios.ObterTodos().Any(f => f.DepartamentoId == id);
            if (existeFuncionario)
            {
                Console.WriteLine("\nErro: Não é possível remover este departamento, pois há funcionários associados a ele.");
                return;
            }

            var depto = repoDepartamentos.ObterPorId(id);
            if(depto == null) 
            {
                Console.WriteLine("Departamento não encontrado.");
                return;
            }
            
            Console.Write($"Tem certeza que deseja remover o departamento '{depto.NomeDepartamento}'? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                repoDepartamentos.Remover(id);
                Console.WriteLine("\nDepartamento removido.");
            }
            else
            {
                Console.WriteLine("\nOperação cancelada.");
            }
        }

        // --- Funções de Funcionário ---

        private static void AdicionarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Funcionário ---");
            
            Guid? deptoId = SelecionarDepartamento();
            if (deptoId == null) return;

            var func = new Funcionario();
            Console.Write("Nome Completo: ");
            func.NomeCompleto = Console.ReadLine();
            Console.Write("Cargo: ");
            func.Cargo = Console.ReadLine();
            func.DepartamentoId = deptoId.Value;

            repoFuncionarios.Adicionar(func);
            Console.WriteLine("\nFuncionário adicionado com sucesso!");
        }

        private static void ListarFuncionarios()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Funcionários ---");
            var funcs = repoFuncionarios.ObterTodos();
            if (!funcs.Any())
            {
                Console.WriteLine("Nenhum funcionário cadastrado.");
                return;
            }

            foreach (var func in funcs)
            {
                var depto = repoDepartamentos.ObterPorId(func.DepartamentoId);
                string nomeDepto = depto?.NomeDepartamento ?? "Departamento Desconhecido";
                Console.WriteLine($"ID: {func.Id} | {func.NomeCompleto} | Cargo: {func.Cargo} | Depto: {nomeDepto}");
            }
        }

        private static void AtualizarFuncionario()
        {
            ListarFuncionarios();
            Console.Write("\nDigite o ID do funcionário para atualizar: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var func = repoFuncionarios.ObterPorId(id);
            if (func == null)
            {
                Console.WriteLine("Funcionário não encontrado.");
                return;
            }

            Console.Write($"Novo Nome ({func.NomeCompleto}): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome)) func.NomeCompleto = novoNome;

            Console.Write($"Novo Cargo ({func.Cargo}): ");
            string novoCargo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoCargo)) func.Cargo = novoCargo;

            Console.WriteLine("\nSelecione o novo departamento (ou pressione Enter para manter o atual):");
            Guid? novoDeptoId = SelecionarDepartamento(podePular: true);
            if (novoDeptoId.HasValue) func.DepartamentoId = novoDeptoId.Value;
            
            repoFuncionarios.Atualizar(func);
            Console.WriteLine("\nFuncionário atualizado!");
        }

        private static void RemoverFuncionario()
        {
            ListarFuncionarios();
            Console.Write("\nDigite o ID do funcionário para remover: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var func = repoFuncionarios.ObterPorId(id);
            if(func == null) 
            {
                Console.WriteLine("Funcionário não encontrado.");
                return;
            }

            Console.Write($"Tem certeza que deseja remover '{func.NomeCompleto}'? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                repoFuncionarios.Remover(id);
                Console.WriteLine("\nFuncionário removido.");
            }
            else
            {
                Console.WriteLine("\nOperação cancelada.");
            }
        }

        // --- Funções Auxiliares ---
        
        private static Guid? SelecionarDepartamento(bool podePular = false)
        {
            var deptos = repoDepartamentos.ObterTodos().ToList();
            if (!deptos.Any())
            {
                Console.WriteLine("\nErro: Nenhum departamento cadastrado. Adicione um departamento primeiro.");
                return null;
            }

            Console.WriteLine("\nDepartamentos disponíveis:");
            for (int i = 0; i < deptos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {deptos[i]}");
            }

            while (true)
            {
                Console.Write("Escolha o número do departamento: ");
                string escolha = Console.ReadLine();

                if (podePular && string.IsNullOrWhiteSpace(escolha))
                {
                    return null;
                }

                if (int.TryParse(escolha, out int indice) && indice > 0 && indice <= deptos.Count)
                {
                    return deptos[indice - 1].Id;
                }
                
                Console.WriteLine("Seleção inválida. Tente novamente.");
            }
        }

        private static void Pausar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}