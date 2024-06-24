using Restaurente_POG_V2;

namespace Restaurante_POG_V2
{
    public interface ICardapio
    {
        ItemCardapio BuscarItem(string nome);
       ItemCardapio BuscarItemPorNumero(int numero);
        void ExibirCardapio();
        void InicializarCardapio();
    }
}
