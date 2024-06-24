using Restaurente_POG_V2;
using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public class Cafe
    {
        private CardapioCafe cardapioCafe;
        private Dictionary<int, Requisicao> requisicoes;
        private int requisicaoId = 1;

        public Cafe()
        {
            cardapioCafe = new CardapioCafe();
            requisicoes = new Dictionary<int, Requisicao>();
            cardapioCafe.InicializarCardapio();
        }

        public void ExibirCardapio()
        {
            cardapioCafe.ExibirCardapio();
        }

        public void AdicionarItemAoPedido(int requisicaoId, int numeroItem, int quantidade)
        {
            if (requisicoes.ContainsKey(requisicaoId))
            {
                Requisicao requisicao = requisicoes[requisicaoId];
                ItemCardapio item = cardapioCafe.BuscarItemPorNumero(numeroItem);
                if (item != null)
                {
                    requisicao.AdicionarItemAoPedido(item, quantidade);
                    Console.WriteLine($"Adicionado {quantidade}x {item.Nome} ao pedido.");
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

        public void ComecarAtendimento(Cliente cliente)
        {
            Requisicao requisicao = new Requisicao(cliente, 1);
            requisicoes[requisicaoId++] = requisicao;
            Console.WriteLine($"Atendimento iniciado para {cliente.Nome}. ID da requisição: {requisicaoId - 1}");
        }

        public void FinalizarRequisicao(int requisicaoId)
        {
            if (requisicoes.ContainsKey(requisicaoId))
            {
                Requisicao requisicao = requisicoes[requisicaoId];
                requisicao.TerminarAtendimento();
                requisicao.ExibirConta();
                requisicoes.Remove(requisicaoId);
            }
            else
            {
                Console.WriteLine("Requisição não encontrada.");
            }
        }
    }
}