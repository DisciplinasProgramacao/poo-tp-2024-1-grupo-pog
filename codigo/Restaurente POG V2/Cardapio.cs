using Restaurente_POG_V2;
using System;
using System.Collections.Generic;

namespace Restaurante_POG_V2
{
    public class Cardapio : ICardapio
    {
        private List<ItemCardapio> itens;

        public Cardapio()
        {
            itens = new List<ItemCardapio>();
        }

        public void AdicionarItem(ItemCardapio item)
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
            Console.WriteLine("Cardápio:");
            for (int i = 0; i < itens.Count; i++)
            {
                var item = itens[i];
                string descricao = item is Comida ? ((Comida)item).Descricao : ((Bebida)item).Tipo;
                Console.WriteLine($"{i + 1}. {item.Nome} - {descricao} - {item.Preco:C}");
            }
        }

        public void InicializarCardapio()
        {
            AdicionarItem(new Comida("Moqueca de Palmito", 32.00, "Deliciosa moqueca de palmito"));
            AdicionarItem(new Comida("Falafel Assado", 20.00, "Falafel assado crocante"));
            AdicionarItem(new Comida("Salada Primavera com Macarrão Konjac", 25.00, "Salada fresca com macarrão Konjac"));
            AdicionarItem(new Comida("Escondidinho de Inhame", 18.00, "Escondidinho de inhame com temperos"));
            AdicionarItem(new Comida("Strogonoff de Cogumelos", 35.00, "Strogonoff de cogumelos cremoso"));
            AdicionarItem(new Comida("Caçarola de legumes", 22.00, "Caçarola de legumes variados"));
            AdicionarItem(new Bebida("Água", 3.00, "Água mineral"));
            AdicionarItem(new Bebida("Copo de suco", 7.00, "Suco natural de frutas"));
            AdicionarItem(new Bebida("Refrigerante orgânico", 7.00, "Refrigerante orgânico"));
            AdicionarItem(new Bebida("Cerveja vegana", 9.00, "Cerveja vegana artesanal"));
            AdicionarItem(new Bebida("Taça de vinho vegano", 18.00, "Vinho vegano de alta qualidade"));
        }
    }
}