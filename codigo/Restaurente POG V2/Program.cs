using Restaurante_POG_V2;
using System;
using System.Collections.Generic;

class Program
{
    private static Estabelecimento estabelecimento;

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
                    estabelecimento = new Restaurante();
                    GerenciarEstabelecimento();
                    break;
                case "2":
                    estabelecimento = new Cafe();
                    GerenciarEstabelecimento();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void GerenciarEstabelecimento()
    {
        while (true)
        {
            estabelecimento.ExibirMenu();
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
                    ExibirCardapio();
                    break;
                case "5":
                    ExibirCardapio();
                    AdicionarItemAoPedido();
                    break;
                case "6":
                    if (estabelecimento is Restaurante restaurante)
                    {
                        restaurante.ExibirStatusMesas();
                    }
                    else
                    {
                        Console.Clear();
                        return;
                    }
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

    private static void CadastrarCliente()
    {
        try
        {
            Console.Write("Nome do Cliente: ");
            string nomeCliente = Console.ReadLine();
            Cliente cliente = estabelecimento.CadastrarCliente(nomeCliente);
            Console.WriteLine($"Cliente '{nomeCliente}' cadastrado com sucesso!");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Erro ao cadastrar cliente: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao cadastrar cliente: {ex.Message}");
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
            if (int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Cliente cliente = estabelecimento.BuscarCliente(idCliente);
                estabelecimento.CriarRequisicao(cliente);
            }
            else
            {
                Console.WriteLine("ID de cliente inválido.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Erro ao criar requisição: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Erro ao criar requisição: {ex.Message}");
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
                estabelecimento.FinalizarRequisicao(idRequisicao);
            }
            else
            {
                Console.WriteLine("ID de requisição inválido.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
        }
    }

    private static void ExibirCardapio()
    {
        try
        {
            estabelecimento.ExibirCardapio();
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
                        estabelecimento.AdicionarItemAoPedido(idRequisicao, numeroItem, quantidade);
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
        catch (FormatException ex)
        {
            Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
        }
    }
}