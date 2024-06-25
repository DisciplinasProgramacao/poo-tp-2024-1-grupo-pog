using Restaurante_POG_V2;
using Restaurente_POG_V2;
using System;
using System.Collections.Generic;
using System.Data;

class Program
{
    private static Restaurante restaurante = new Restaurante();
    private static Cafe cafe = new Cafe();
    private static Dictionary<int, Cliente> clientes = new Dictionary<int, Cliente>();
    private static int clienteId = 1;
    private static int requisicaoId = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Escolha o estabelecimento:");
            Console.WriteLine("1. Restaurante");
            Console.WriteLine("2. Café");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    GerenciarRestaurante();
                    break;
                case "2":
                    GerenciarCafe();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void GerenciarRestaurante()
    {
        while (true)
        {
            MostrarMenuRestaurante();
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
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void GerenciarCafe()
    {
        while (true)
        {
            MostrarMenuCafe();
            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    CadastrarCliente();
                    break;
                case "2":
                    ComecarAtendimento();
                    break;
                case "3":
                    FinalizarRequisicaoCafe();
                    break;
                case "4":
                    ExibirCardapioCafe();
                    break;
                case "5":
                    AdicionarItemAoPedidoCafe();
                    break;
                case "6":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void MostrarMenuRestaurante()
    {
        Console.WriteLine("\n--- Sistema de Gerenciamento do Restaurante ---");
        Console.WriteLine("1. Cadastrar Cliente");
        Console.WriteLine("2. Criar Requisição");
        Console.WriteLine("3. Finalizar Requisição");
        Console.WriteLine("4. Exibir Status das Mesas");
        Console.WriteLine("5. Exibir Cardápio");
        Console.WriteLine("6. Adicionar Item ao Pedido");
        Console.WriteLine("7. Voltar");
        Console.Write("Escolha uma opção: ");
    }

    private static void MostrarMenuCafe()
    {
        Console.WriteLine("\n--- Sistema de Gerenciamento do Café ---");
        Console.WriteLine("1. Cadastrar Cliente");
        Console.WriteLine("2. Começar Atendimento");
        Console.WriteLine("3. Finalizar Requisição");
        Console.WriteLine("4. Exibir Cardápio");
        Console.WriteLine("5. Adicionar Item ao Pedido");
        Console.WriteLine("6. Voltar");
        Console.Write("Escolha uma opção: ");
    }

    // Métodos do Restaurante
    private static void CadastrarCliente()
    {
        try
        {
            Console.Write("Nome do Cliente: ");
            string nomeCliente = Console.ReadLine();
            Cliente cliente = new Cliente(nomeCliente);
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
                Console.Write("Quantidade de Pessoas: ");
                if (int.TryParse(Console.ReadLine(), out int qtdePessoas))
                {
                    if (qtdePessoas > 8)
                    {
                        Console.WriteLine("Quantidade de pessoas excede a capacidade máxima das mesas.");
                        return;
                    }

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
                restaurante.FinalizarRequisicao(idRequisicao);
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
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
        }
    }

    // Métodos do Café
    private static void ComecarAtendimento()
    {
        try
        {
            Console.Write("ID do Cliente: ");
            if (int.TryParse(Console.ReadLine(), out int idCliente) && clientes.ContainsKey(idCliente))
            {
                cafe.ComecarAtendimento(clientes[idCliente]);
            }
            else
            {
                Console.WriteLine("ID de cliente inválido.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao começar atendimento: {ex.Message}");
        }
    }

    private static void FinalizarRequisicaoCafe()
    {
        try
        {
            Console.Write("ID da Requisição a ser finalizada: ");
            if (int.TryParse(Console.ReadLine(), out int idRequisicao))
            {
                cafe.FinalizarRequisicao(idRequisicao);
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

    private static void ExibirCardapioCafe()
    {
        try
        {
            cafe.ExibirCardapio();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao exibir cardápio: {ex.Message}");
        }
    }

    private static void AdicionarItemAoPedidoCafe()
    {
        try
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
                        cafe.AdicionarItemAoPedido(idRequisicao, numeroItem, quantidade);
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
}
