using Restaurente_POG_V2;
using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public class Cafe : Estabelecimento
    {
        public Cafe(ICardapio cardapioPersonalizado = null) : base(cardapioPersonalizado) { }

        public override void CriarRequisicao(Cliente cliente)
        {
            Requisicao requisicao = new Requisicao(cliente, 1);
            requisicoes[requisicaoId++] = requisicao;
            Console.WriteLine($"Atendimento iniciado para {cliente.Nome}. ID da requisição: {requisicaoId - 1}");
        }

        public override void ExibirCardapio()
        {
            cardapio.ExibirCardapio();
        }

        public override void AdicionarItemAoPedido(int requisicaoId, int numeroItem, int quantidade)
        {
            try
            {
                ExibirCardapio();
                if (requisicoes.ContainsKey(requisicaoId))
                {
                    Requisicao requisicao = requisicoes[requisicaoId];
                    ItemCardapio item = cardapio.BuscarItemPorNumero(numeroItem);
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Erro ao buscar requisição: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
            }
        }

        public override void FinalizarRequisicao(int requisicaoId)
        {
            try
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao finalizar requisição: {ex.Message}");
            }
        }

        public override void ExibirMenu()
        {
            Console.WriteLine("\n--- CAFETERIA POG ---");
            Console.WriteLine("1. Cadastrar Cliente");
            Console.WriteLine("2. Começar Atendimento");
            Console.WriteLine("3. Finalizar Requisição");
            Console.WriteLine("4. Exibir Cardápio");
            Console.WriteLine("5. Adicionar Item ao Pedido");
            Console.WriteLine("6. Voltar");
            Console.Write("Escolha uma opção: ");
        }
    }
}