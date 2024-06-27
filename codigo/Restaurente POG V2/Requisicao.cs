using Restaurante_POG_V2;
using Restaurente_POG_V2;
using System;

public class Requisicao
{
    public int Id { get; set; }
    public Cliente Cliente { get; private set; }
    public int QtdePessoas { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public DateTime DataSaida { get; private set; }
    public Mesa Mesa { get; set; }
    public Pedido Pedido { get; private set; }

    public Requisicao(int id, Cliente cliente, int qtdePessoas)
    {
        Id = id;
        Cliente = cliente;
        QtdePessoas = qtdePessoas;
        DataEntrada = DateTime.Now;
        Pedido = new Pedido();
    }

    public void AdicionarItemAoPedido(ItemCardapio item, int quantidade)
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
