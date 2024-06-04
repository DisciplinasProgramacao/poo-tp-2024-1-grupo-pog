public class Requisicao
{
    public Cliente Cliente { get; private set; }
    public int QtdePessoas { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public DateTime? DataSaida { get; private set; }
    public Mesa Mesa { get; set; }
    public Pedido Pedido { get; private set; }

    public Requisicao(Cliente cliente, int qtdePessoas)
    {
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
        Console.WriteLine($"Conta para {Cliente.Nome}:");
        Pedido.ExibirPedido();
    }
}