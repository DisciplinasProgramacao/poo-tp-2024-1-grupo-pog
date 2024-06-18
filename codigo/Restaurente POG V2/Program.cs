using System;
using System.Collections.Generic;

class Program
{
    private static Restaurante restaurante = new Restaurante();
    private static Dictionary<int, Cliente> clientes = new Dictionary<int, Cliente>();
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
        Console.Write("Nome do Cliente: ");
        string nomeCliente = Console.ReadLine();
        Cliente cliente = restaurante.CadastrarCliente(nomeCliente);
        clientes.Add(clienteId++, cliente);
        Console.WriteLine($"Cliente '{nomeCliente}' cadastrado com sucesso!");
    }

    private static void CriarRequisicao()
    {
        Console.Write("ID do Cliente: ");
        if (int.TryParse(Console.ReadLine(), out int idCliente) && clientes.ContainsKey(idCliente))
        {
            Console.Write("Quantidade de Pessoas: ");
            if (int.TryParse(Console.ReadLine(), out int qtdePessoas))
            {
                Requisicao requisicao = restaurante.CriarRequisicao(clientes[idCliente], qtdePessoas);
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

    private static void FinalizarRequisicao()
    {
        Console.Write("ID da Requisição a ser finalizada: ");
        if (int.TryParse(Console.ReadLine(), out int idRequisicao))
        {
            restaurante.FinalizarRequisicao(idRequisicao);
        }
        else
        {
            Console.WriteLine("ID de requisição inválido.");
        }
    }

    private static void ExibirStatusMesas()
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

    private static void ExibirCardapio()
    {
        restaurante.ExibirCardapio();
    }

    private static void AdicionarItemAoPedido()
    {
        Console.Write("ID da Requisição: ");
        if (int.TryParse(Console.ReadLine(), out int idRequisicao))
        {
            Console.Write("Número do Item: ");
            if (int.TryParse(Console.ReadLine(), out int numeroItem))
            {
                Console.Write("Quantidade: ");
                if (int.TryParse(Console.ReadLine(), out int quantidade))
                {
                    restaurante.AdicionarItemAoPedido(idRequisicao, numeroItem, quantidade);
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

}