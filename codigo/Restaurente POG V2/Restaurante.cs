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

        InicializarMesas();

        cardapio.InicializarCardapio();
    }

    private void InicializarMesas()
    {
        for (int i = 0; i < 4; i++)
        {
            mesas.Add(new Mesa(i + 1, 4));
        }

        for (int i = 4; i < 8; i++)
        {
            mesas.Add(new Mesa(i + 1, 6));
        }

        for (int i = 8; i < 10; i++)
        {
            mesas.Add(new Mesa(i + 1, 8));
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

    public void ExibirCardapio()
    {
        cardapio.ExibirCardapio();
    }

    public void AdicionarItemAoPedido(int requisicaoId, int numeroItem, int quantidade)
    {
        if (requisicoes.ContainsKey(requisicaoId))
        {
            Requisicao requisicao = requisicoes[requisicaoId];
            ItemCardapio item = cardapio.BuscarItemPorNumero(numeroItem);
            if (item != null)
            {
                requisicao.AdicionarItemAoPedido(item, quantidade);
                Console.WriteLine($"Adicionado {quantidade}x {item.Nome} ao pedido da mesa {requisicao.Mesa.Id}.");
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

    public int ObterRequisicaoIdPorMesa(int mesaId)
    {
        foreach (var requisicao in requisicoes)
        {
            if (requisicao.Value.Mesa.Id == mesaId)
            {
                return requisicao.Key;
            }
        }
        return -1;
    }

    public void ExibirPedido(int requisicaoId)
    {
        if (requisicoes.ContainsKey(requisicaoId))
        {
            Requisicao requisicao = requisicoes[requisicaoId];
            requisicao.Pedido.ExibirPedido();
        }
        else
        {
            Console.WriteLine("Requisição não encontrada.");
        }
    }
}
