﻿using Restaurante_POG_V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurente_POG_V2
{
    public class Pedido
    {
        private Dictionary<ItemCardapio, int> itens;
        private Dictionary<ItemCardapioCafe, int> itensCafe;
        public double Total { get; private set; }

        public Pedido()
        {
            itens = new Dictionary<ItemCardapio, int>();
            itensCafe = new Dictionary<ItemCardapioCafe, int>();
            Total = 0;
        }

        public void AdicionarItem(ItemCardapio item, int quantidade)
        {
            if (itens.ContainsKey(item))
            {
                itens[item] += quantidade;
            }
            else
            {
                itens[item] = quantidade;
            }
            Total += item.Preco * quantidade;
        }

        public void AdicionarItem(ItemCardapioCafe item, int quantidade)
        {
            if (itensCafe.ContainsKey(item))
            {
                itensCafe[item] += quantidade;
            }
            else
            {
                itensCafe[item] = quantidade;
            }
            Total += item.Preco * quantidade;
        }

        public double CalcularTotal()
        {
            return Total;
        }

        public void ExibirPedido()
        {
            foreach (var item in itens)
            {
                Console.WriteLine($"{item.Key.Nome} - Quantidade: {item.Value} - Preço Total: {item.Key.Preco * item.Value}");
            }
            foreach (var item in itensCafe)
            {
                Console.WriteLine($"{item.Key.Nome} - Quantidade: {item.Value} - Preço Total: {item.Key.Preco * item.Value}");
            }
            Console.WriteLine($"Total: {CalcularTotal():C}");
        }
    }
}
