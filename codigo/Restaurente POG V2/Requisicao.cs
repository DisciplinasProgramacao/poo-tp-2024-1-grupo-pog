using System;

public class Requisicao
{
    public Cliente Cliente { get; private set; }
    public int QtdePessoas { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public DateTime? DataSaida { get; private set; }
    public Mesa Mesa { get; set; }

    public Requisicao(Cliente cliente, int qtdePessoas)
    {
        Cliente = cliente;
        QtdePessoas = qtdePessoas;
        DataEntrada = DateTime.Now;
    }

    public void TerminarAtendimento()
    {
        DataSaida = DateTime.Now;
    }
}