using Restaurente_POG_V2;
using System;

namespace Restaurante_POG_V2
{
    public abstract class ItemCardapioCafe : ItemCardapio
    {
        protected ItemCardapioCafe(string nome, double preco)
            : base(nome, preco)
        {
        }
    }

    public class ComidaCafe : ItemCardapioCafe
    {
        public string Descricao { get; private set; }

        public ComidaCafe(string nome, double preco, string descricao)
            : base(nome, preco)
        {
            Descricao = descricao;
        }
    }

    public class BebidaCafe : ItemCardapioCafe
    {
        public string Tipo { get; private set; }

        public BebidaCafe(string nome, double preco, string tipo)
            : base(nome, preco)
        {
            Tipo = tipo;
        }
    }
}