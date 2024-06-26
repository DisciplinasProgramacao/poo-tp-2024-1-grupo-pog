using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public abstract class Estabelecimento
    {
        protected ICardapio cardapio;
        protected Dictionary<int, Requisicao> requisicoes;
        protected Dictionary<int, Cliente> clientes;
        protected int requisicaoId;
        protected int clienteId;

        public Estabelecimento(ICardapio cardapioPersonalizado = null)
        {
            cardapio = cardapioPersonalizado ?? new Cardapio();
            requisicoes = new Dictionary<int, Requisicao>();
            clientes = new Dictionary<int, Cliente>();
            requisicaoId = 1;
            clienteId = 1;
            cardapio.InicializarCardapio();
        }

        public Cliente CadastrarCliente(string nome)
        {
            Cliente cliente = new Cliente(clienteId++, nome);
            clientes.Add(cliente.Id, cliente);
            return cliente;
        }

        public Cliente BuscarCliente(int id)
        {
            if (clientes.ContainsKey(id))
            {
                return clientes[id];
            }
            else
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
        }

        public abstract void ExibirCardapio();
        public abstract void AdicionarItemAoPedido(int requisicaoId, int numeroItem, int quantidade);
        public abstract void FinalizarRequisicao(int requisicaoId);
        public abstract void CriarRequisicao(Cliente cliente);
        public abstract void ExibirMenu();
        public virtual void ExibirStatusMesas() { }
    }
}
