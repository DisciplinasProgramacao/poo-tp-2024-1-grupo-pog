using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using Restaurente_POG_V2;

public class Restaurante
{
    private List<Mesa> mesas;
    private Queue<Requisicao> filaEspera;
    private Cardapio cardapio;
    private Dictionary<int, Requisicao> requisicoes;
    private const int TOTAL_MESAS = 10;

    public Restaurante()
    {
        mesas = new List<Mesa>(TOTAL_MESAS);
        filaEspera = new Queue<Requisicao>();
        cardapio = new Cardapio();
        requisicoes = new Dictionary<int, Requisicao>();

        Random rand = new Random();
        for (int i = 0; i < TOTAL_MESAS; i++)
        {
            int capacidade = rand.Next(2, 6);
            mesas.Add(new Mesa(i + 1, capacidade));
        }

        cardapio.InicializarCardapio();
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

    public void ExibirCardapio()
    {
        cardapio.ExibirCardapio();
    }

    public void AdicionarItemAoPedido(int requisicaoId, string nomeItem, int quantidade)
    {
        if (requisicoes.ContainsKey(requisicaoId))
        {
            Requisicao requisicao = requisicoes[requisicaoId];
            ItemCardapio item = cardapio.BuscarItem(nomeItem);
            if (item != null)
            {
                requisicao.AdicionarItemAoPedido(item, quantidade);
            }
            else
            {
                Console.WriteLine("Item não encontrado no cardápio.");
            }
        }
        else
        {
            Console.WriteLine("Requisição não encontrada.");
        }
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
        foreach (var mesa in mesas)
        {
            if (mesa.VerificarDisponibilidade(qtdePessoas))
            {
                return mesa;
            }
        }
        return null;
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

    public void FinalizarRequisicao(int requisicaoId)
    {
        if (requisicoes.ContainsKey(requisicaoId))
        {
            Requisicao requisicao = requisicoes[requisicaoId];
            requisicao.TerminarAtendimento();
            requisicao.ExibirConta();
            DesalocarMesa(requisicao.Mesa);
            requisicoes.Remove(requisicaoId);
        }
    }

    public Mesa LocalizarMesaPorId(int id)
    {
        foreach (var mesa in mesas)
        {
            if (mesa.Id == id)
            {
                return mesa;
            }
        }
        return null;
    }

    public List<Mesa> ObterMesas()
    {
        return mesas;
    }

    public void AdicionarRequisicao(int id, Requisicao requisicao)
    {
        requisicoes[id] = requisicao;
    }

    public void RemoverRequisicao(int id)
    {
        requisicoes.Remove(id);
    }
}
