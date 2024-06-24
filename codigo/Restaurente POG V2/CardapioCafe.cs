using Restaurente_POG_V2;
using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public class CardapioCafe : ICardapio
    {
        private List<ItemCardapioCafe> itens;

        public CardapioCafe()
        {
            itens = new List<ItemCardapioCafe>();
        }

        public void AdicionarItem(ItemCardapioCafe item)
        {
            itens.Add(item);
        }

        public ItemCardapio BuscarItem(string nome)
        {
            foreach (var item in itens)
            {
                if (item.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        public ItemCardapio BuscarItemPorNumero(int numero)
        {
            if (numero > 0 && numero <= itens.Count)
            {
                return itens[numero - 1];
            }
            return null;
        }

        public void ExibirCardapio()
        {
            Console.WriteLine("Cardápio do Café:");
            for (int i = 0; i < itens.Count; i++)
            {
                var item = itens[i];
                string descricao = item is ComidaCafe ? ((ComidaCafe)item).Descricao : ((BebidaCafe)item).Tipo;
                Console.WriteLine($"{i + 1}. {item.Nome} - {descricao} - {item.Preco:C}");
            }
        }

        public void InicializarCardapio()
        {
            AdicionarItem(new ComidaCafe("Não de queijo", 5.00, "Delicioso lanche sem queijo"));
            AdicionarItem(new ComidaCafe("Bolinha de cogumelo", 7.00, "Bolinha crocante de cogumelo"));
            AdicionarItem(new ComidaCafe("Rissole de palmito", 7.00, "Rissole recheado com palmito"));
            AdicionarItem(new ComidaCafe("Coxinha de carne de jaca", 8.00, "Coxinha feita com carne de jaca"));
            AdicionarItem(new ComidaCafe("Fatia de queijo de caju", 9.00, "Fatia de queijo feito de caju"));
            AdicionarItem(new ComidaCafe("Biscoito amanteigado", 3.00, "Biscoito vegano amanteigado"));
            AdicionarItem(new ComidaCafe("Cheesecake de frutas vermelhas", 15.00, "Cheesecake vegano de frutas vermelhas"));

            AdicionarItem(new BebidaCafe("Água", 3.00, "Água mineral"));
            AdicionarItem(new BebidaCafe("Copo de suco", 7.00, "Suco natural de frutas"));
            AdicionarItem(new BebidaCafe("Café espresso orgânico", 6.00, "Café espresso orgânico"));
        }
    }
}