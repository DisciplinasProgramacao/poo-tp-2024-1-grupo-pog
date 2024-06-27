using Restaurante_POG_V2;
using Restaurente_POG_V2;
using System;

public class Requisicao
{
    private static int proximoId = 1;
    public int Id { get; private set; }
    public Cliente Cliente { get; private set; }
    public int QtdePessoas { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public DateTime? DataSaida { get; private set; }
    public Mesa Mesa { get; set; }
    public Pedido Pedido { get; private set; }

    public Requisicao(Cliente cliente, int qtdePessoas)
    {
        Id = proximoId++;
        Cliente = cliente;
        QtdePessoas = qtdePessoas;
        DataEntrada = DateTime.Now;
        Pedido = new Pedido();
    }

    public void AdicionarItemAoPedido(ItemCardapio item, int quantidade)
    {
        Pedido.AdicionarItem(item, quantidade);
    }

    public void AdicionarItemAoPedido(ItemCardapioCafe item, int quantidade)
    {
        Pedido.AdicionarItem(item, quantidade);
    }

    public void TerminarAtendimento()
    {
        DataSaida = DateTime.Now;
    }

    public void ExibirConta()
    {
        double total = Pedido.CalcularTotal();
        double taxaServico = total * 0.10;
        double totalComTaxa = total + taxaServico;
        double valorPorPessoa = totalComTaxa / QtdePessoas;

        Console.WriteLine($"Conta para {Cliente.Nome}:");
        Pedido.ExibirPedido();
        Console.WriteLine($"Taxa de Serviço (10%): {taxaServico:C}");
        Console.WriteLine($"Total com Taxa de Serviço: {totalComTaxa:C}");
        Console.WriteLine($"Valor por Pessoa: {valorPorPessoa:C}");
    }
}
