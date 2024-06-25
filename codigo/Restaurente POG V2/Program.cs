using Restaurante_POG_V2;
using System;
using System.Collections.Generic;

class Program
{
    private static Restaurante restaurante = new Restaurante();
    private static Dictionary<int, Cliente> clientes = new Dictionary<int, Cliente>();
    private static Dictionary<int, Requisicao> requisicoes = new Dictionary<int, Requisicao>();
    private static int clienteId = 1;
    private static int requisicaoId = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            MostrarMenu();
            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    CadastrarCliente();
                    break;
                case "2":
                    CriarRequisicao();
                    break;
                case "3":
                    FinalizarRequisicao();
                    break;
                case "4":
                    ExibirStatusMesas();
                    break;
                case "5":
                    ExibirCardapio();
                    break;
                case "6":
                    AdicionarItemAoPedido();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void MostrarMenu()
    {
        Console.WriteLine("\n--- Sistema de Gerenciamento de Restaurante ---");
        Console.WriteLine("1. Cadastrar Cliente");
        Console.WriteLine("2. Criar Requisição");
        Console.WriteLine("3. Finalizar Requisição");
        Console.WriteLine("4. Exibir Status das Mesas");
        Console.WriteLine("5. Exibir Cardápio");
        Console.WriteLine("6. Adicionar Item ao Pedido");
        Console.WriteLine("7. Sair");
        Console.Write("Escolha uma opção: ");
    }

    private static void CadastrarCliente()
    {
        try
        {
            Console.Write("Nome do Cliente: ");
            string nomeCliente = Console.ReadLine();
            Cliente cliente = new Cliente(clienteId, nomeCliente);
            clientes.Add(clienteId++, cliente);
            Console.WriteLine($"Cliente '{nomeCliente}' cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar cliente: {ex.Message}");
        }
    }

    private static void CriarRequisicao()
    {
        try
        {
            Console.Write("ID do Cliente: ");
            if (int.TryParse(Console.ReadLine(), out int idCliente) && clientes.ContainsKey(idCliente))
            {
                if (ClienteJaPossuiRequisicaoAtiva(idCliente))
                {
                    Console.WriteLine($"O cliente '{clientes[idCliente].Nome}' já possui uma requisição ativa.");
                    return;
                }

                Console.Write("Quantidade de Pessoas: ");
                if (int.TryParse(Console.ReadLine(), out int qtdePessoas))
                {
                    Requisicao requisicao = new Requisicao(clientes[idCliente], qtdePessoas);
                    requisicoes.Add(requisicaoId, requisicao);
                    restaurante.AdicionarRequisicao(requisicaoId++, requisicao);
                    if (requisicao.Mesa != null)
                    {
                        Console.WriteLine($"Mesa {requisicao.Mesa.Id} alocada para {requisicao.Cliente.Nome}.");
                    }
                    else
                    {
                        Console.WriteLine($"Não há mesas disponíveis para {requisicao.Cliente.Nome}. Adicionado à fila de espera.");
                    }
                }
                else
                {
                    Console.WriteLine("Quantidade de pessoas inválida.");
                }
            }
            else
            {
                Console.WriteLine("ID de cliente inválido.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar requisição: {ex.Message}");
        }
    }

    private static void FinalizarRequisicao()
    {
        try
        {
            Console.Write("ID da Requisição a ser finalizada: ");
            if (int.TryParse(Console.ReadLine(), out int idRequisicao))
            {
                if (!requisicoes.ContainsKey(idRequisicao))
                {
                    Console.WriteLine("ID de requisição inválido.");
                    return;
                }

                restaurante.FinalizarRequisicao(idRequisicao);
                requisicoes.Remove(idRequisicao);
                Console.WriteLine("Requisição finalizada com sucesso.");
            }
            else
            {
                Console.WriteLine("ID de requisição inválido.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
        }
    }

    private static void ExibirStatusMesas()
    {
        try
        {
            Console.WriteLine("Status das Mesas:");
            foreach (var mesa in restaurante.ObterMesas())
            {
                Console.WriteLine($"Mesa {mesa.Id}: {(mesa.Ocupada ? "Ocupada" : "Disponível")}");
                if (mesa.Ocupada)
                {
                    int requisicaoId = restaurante.ObterRequisicaoIdPorMesa(mesa.Id);
                    if (requisicaoId != -1)
                    {
                        restaurante.ExibirPedido(requisicaoId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao exibir status das mesas: {ex.Message}");
        }
    }

    private static void ExibirCardapio()
    {
        try
        {
            restaurante.ExibirCardapio();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao exibir cardápio: {ex.Message}");
        }
    }

    private static void AdicionarItemAoPedido()
    {
        try
        {
            Console.Write("ID da Requisição: ");
            if (int.TryParse(Console.ReadLine(), out int idRequisicao))
            {
                if (!requisicoes.ContainsKey(idRequisicao))
                {
                    Console.WriteLine("ID de requisição inválido.");
                    return;
                }

                Console.Write("Número do Item: ");
                if (int.TryParse(Console.ReadLine(), out int numeroItem))
                {
                    Console.Write("Quantidade: ");
                    if (int.TryParse(Console.ReadLine(), out int quantidade))
                    {
                        restaurante.AdicionarItemAoPedido(idRequisicao, numeroItem, quantidade);
                        Console.WriteLine("Item adicionado ao pedido com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Quantidade inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Número do item inválido.");
                }
            }
            else
            {
                Console.WriteLine("ID de requisição inválido.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
        }
    }

    private static bool ClienteJaPossuiRequisicaoAtiva(int clienteId)
    {
        foreach (var requisicao in requisicoes.Values)
        {
            if (requisicao.Cliente.Id == clienteId)
            {
                return true;
            }
        }
        return false;
    }
}
