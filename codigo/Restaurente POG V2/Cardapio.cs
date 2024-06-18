using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurente_POG_V2
{
    public class Cardapio
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

        public void ExibirCardapio()
        {
            Console.WriteLine("Cardápio:");
            foreach (var item in itens)
            {
                string descricao = item is Comida ? ((Comida)item).Descricao : ((Bebida)item).Tipo;
                Console.WriteLine($"{item.Nome} - {descricao} - {item.Preco:C}");
            }
        }

        public void InicializarCardapio()
        {
            AdicionarItem(new Comida("Hamburguer", 12.50, "Hamburguer com queijo e bacon"));
            AdicionarItem(new Comida("Pizza", 25.00, "Pizza de calabresa com queijo"));
            AdicionarItem(new Comida("Salada", 10.00, "Salada mista com molho especial"));
            AdicionarItem(new Comida("Sopa", 8.00, "Sopa de legumes"));
            AdicionarItem(new Bebida("Coca-Cola", 5.00, "Refrigerante"));
            AdicionarItem(new Bebida("Água", 3.00, "Água mineral"));
            AdicionarItem(new Bebida("Suco de Laranja", 6.00, "Suco natural de laranja"));
            AdicionarItem(new Bebida("Cerveja", 7.00, "Cerveja gelada"));
        }
    }
}
