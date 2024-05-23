using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;

public class Restaurante
{
    private List<Mesa> mesas;
    private Queue<Requisicao> filaEspera;
    private const int TOTAL_MESAS = 10;

    public Restaurante()
    {
        mesas = new List<Mesa>(TOTAL_MESAS);
        filaEspera = new Queue<Requisicao>();

        Random rand = new Random();
        for (int i = 0; i < TOTAL_MESAS; i++)
        {
            int capacidade = rand.Next(2, 6);
            mesas.Add(new Mesa(i + 1, capacidade));
        }
    }

    public Cliente CadastrarCliente(string nome)
    {
        return new Cliente(nome);
    }

    public Requisicao CriarRequisicao(Cliente cliente, int qtdePessoas)
    {
        Requisicao requisicao = new Requisicao(cliente, qtdePessoas);
        AlocarMesa(requisicao);
        return requisicao;
    }

    private void AlocarMesa(Requisicao requisicao)
    {
        Mesa mesa = LocalizarMesa(requisicao.QtdePessoas);
        if (mesa != null)
        {
            mesa.Alocar();
            requisicao.Mesa = mesa;
        }
        else
        {
            filaEspera.Enqueue(requisicao);
        }
    }

    private Mesa LocalizarMesa(int qtdePessoas)
    {
        return mesas.FirstOrDefault(m => m.VerificarDisponibilidade(qtdePessoas));
    }

    public void DesalocarMesa(Mesa mesa)
    {
        mesa.Desalocar();
        if (filaEspera.Count > 0)
        {
            Requisicao proximaRequisicao = filaEspera.Dequeue();
            AlocarMesa(proximaRequisicao);
        }
    }

    public void FinalizarRequisicao(Requisicao requisicao)
    {
        requisicao.TerminarAtendimento();
        DesalocarMesa(requisicao.Mesa);
    }

    public Mesa LocalizarMesaPorId(int id)
    {
        return mesas.FirstOrDefault(m => m.Id == id);
    }

    public List<Mesa> ObterMesas()
    {
        return mesas;
    }
}
