using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurente_POG_V2
{
    public abstract class ItemCardapio
    {
        public string Nome { get; private set; }
        public double Preco { get; private set; }

        protected ItemCardapio(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    public class Comida : ItemCardapio
    {
        public string Descricao { get; private set; }

        public Comida(string nome, double preco, string descricao)
            : base(nome, preco)
        {
            Descricao = descricao;
        }
    }

    public class Bebida : ItemCardapio
    {
        public string Tipo { get; private set; }

        public Bebida(string nome, double preco, string tipo)
            : base(nome, preco)
        {
            Tipo = tipo;
        }
    }
}
