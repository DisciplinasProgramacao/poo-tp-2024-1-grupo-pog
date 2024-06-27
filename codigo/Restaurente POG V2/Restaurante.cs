using Restaurente_POG_V2;
using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public class Restaurante : Estabelecimento
    {
        private List<Mesa> mesas;
        private Queue<Requisicao> filaEspera;

        public Restaurante(ICardapio cardapioPersonalizado = null) : base(cardapioPersonalizado)
        {
            mesas = new List<Mesa>();
            filaEspera = new Queue<Requisicao>();
            InicializarMesas();
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

        public override void CriarRequisicao(Cliente cliente)
        {
            Console.Write("Quantidade de Pessoas: ");
            if (int.TryParse(Console.ReadLine(), out int qtdePessoas))
            {
                if (qtdePessoas > 8)
                {
                    Console.WriteLine("Quantidade de pessoas excede a capacidade máxima das mesas.");
                    return;
                }

                Requisicao requisicao = new Requisicao(cliente, qtdePessoas);
                AlocarMesa(requisicao);
            }
            else
            {
                Console.WriteLine("Quantidade de pessoas inválida.");
            }
        }


        private void AlocarMesa(Requisicao requisicao)
        {
            Mesa mesa = LocalizarMesa(requisicao.QtdePessoas);
            if (mesa != null)
            {
                mesa.Alocar();
                requisicao.Mesa = mesa;
                requisicoes.Add(requisicaoId++, requisicao);
            }
            else
            {
                filaEspera.Enqueue(requisicao);
                Console.WriteLine($"{requisicao.Cliente.Nome} foi adicionado à fila de espera.");
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
                    Mesa mesa = requisicao.Mesa;
                    requisicoes.Remove(requisicaoId);

                    if (filaEspera.Count > 0)
                    {
                        Requisicao proximaRequisicao = filaEspera.Dequeue();
                        proximaRequisicao.Mesa = mesa;
                        mesa.Alocar();
                        requisicoes.Add(this.requisicaoId++, proximaRequisicao);
                        Console.WriteLine($"Cliente {proximaRequisicao.Cliente.Nome} foi movido da fila para a mesa {mesa.Id}.");
                        ExibirPedido(proximaRequisicao.Id);
                    }
                    else
                    {
                        mesa.Desalocar();
                    }
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


        public override void ExibirStatusMesas()
        {
            try
            {
                Console.WriteLine("Status das Mesas:");
                foreach (var mesa in mesas)
                {
                    Console.WriteLine($"Mesa {mesa.Id}: {(mesa.Ocupada ? "Ocupada" : "Disponível")}");
                    if (mesa.Ocupada)
                    {
                        int requisicaoId = ObterRequisicaoIdPorMesa(mesa.Id);
                        if (requisicaoId != -1)
                        {
                            ExibirPedido(requisicaoId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir status das mesas: {ex.Message}");
            }
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exibir pedido: {ex.Message}");
            }
        }

        public override void ExibirMenu()
        {
            Console.WriteLine("\n--- RESTAURANTE POG ---");
            Console.WriteLine("1. Cadastrar Cliente");
            Console.WriteLine("2. Criar Requisição");
            Console.WriteLine("3. Finalizar Requisição");
            Console.WriteLine("4. Exibir Cardápio");
            Console.WriteLine("5. Adicionar Item ao Pedido");
            Console.WriteLine("6. Exibir Status das Mesas");
            Console.WriteLine("7. Voltar");
            Console.Write("Escolha uma opção: ");
        }
    }
}
