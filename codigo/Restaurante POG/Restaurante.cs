public class Restaurante
{
    private List<Mesa> mesas;
    private Queue<Requisicao> filaEspera;
    private const int TOTAL_MESAS = 10;

    public Restaurante()
    {
        mesas = new List<Mesa>(TOTAL_MESAS);
        filaEspera = new Queue<Requisicao>();

        // Inicializa as mesas
        for (int i = 0; i < TOTAL_MESAS; i++)
        {
            mesas.Add(new Mesa(i + 1)); 
        }
    }

    public Mesa LocalizarMesa(Requisicao requisicao)
    {
        foreach (var mesa in mesas)
        {
            if (mesa.VerificarDisponibilidade(requisicao.QtdePessoas))
            {
                return mesa;
            }
        }
        return null; 
    }

    public void AlocarMesa(Requisicao requisicao)
    {
        Mesa mesa = LocalizarMesa(requisicao);
        if (mesa != null)
        {
            mesa.Alocar();
            requisicao.Mesa = mesa;
            Console.WriteLine($"Mesa {mesa.Id} alocada para {requisicao.Cliente.Nome}.");
        }
        else
        {
            filaEspera.Enqueue(requisicao);
            Console.WriteLine($"{requisicao.Cliente.Nome} foi adicionado à fila de espera.");
        }
    }

    public void DesalocarMesa(Mesa mesa)
    {
        mesa.Desalocar();
        Console.WriteLine($"Mesa {mesa.Id} desalocada.");

        if (filaEspera.Count > 0)
        {
            Requisicao proximaRequisicao = filaEspera.Dequeue();
            AlocarMesa(proximaRequisicao);
        }
    }
}
