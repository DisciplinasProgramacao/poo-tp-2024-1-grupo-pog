using Restaurente_POG_V2;
using System;

public abstract class ItemCardapioCafe : ItemCardapio
{
    protected ItemCardapioCafe(string nome, double preco)
        : base(nome, preco)
    {
    }
}