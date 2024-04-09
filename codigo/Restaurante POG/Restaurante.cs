class Program
{
    static void Main(string[] args)
    {
        //instância da classe Restaurante
        Restaurante restaurante = new Restaurante();

        //lista para armazenar os clientes
        List<Cliente> clientes = new List<Cliente>();

        // var controle
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1. Cadastrar cliente");
            Console.WriteLine("2. Atender cliente");
            Console.WriteLine("3. Sair");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)

            {
               // Opção 1: Cadastrar cliente
                case 1:

                    Console.WriteLine("Digite o nome do cliente:");
                    string nomeCliente = Console.ReadLine();

                    //instância da classe Cliente com o nome digitado
                    Cliente novoCliente = new Cliente(nomeCliente);

                    // adc à lista de clientes
                    clientes.Add(novoCliente);

                    // confirmação do cadastro
                    Console.WriteLine($"Cliente {nomeCliente} cadastrado com sucesso!");
                    break;

            // Opção 2: Atender cliente
                case 2:

                    // verifica se há clientes cadastrados
                    if (clientes.Count == 0)
                    {
                        Console.WriteLine("Não há clientes cadastrados.");
                        break;
                    }

                    // exibe os clientes cadastrados e solicita a escolha um
                    Console.WriteLine("Escolha um cliente:");
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {clientes[i].Nome}");
                    }
                    int indiceCliente = int.Parse(Console.ReadLine()) - 1;

                    Console.WriteLine("Digite a quantidade de pessoas:");
                    int qtdPessoas = int.Parse(Console.ReadLine());

                    // puxa o cliente selecionado e cria uma requisição com a quantidade de pessoas
                    Cliente clienteSelecionado = clientes[indiceCliente];
                    Requisicao novaRequisicao = new Requisicao(clienteSelecionado, qtdPessoas);

                    restaurante.AlocarMesa(novaRequisicao);

                    // verifica se o cliente foi alocado ou entrou na fila de espera
                    if (novaRequisicao.Mesa != null)
                    {
                        Console.WriteLine($"Cliente {clienteSelecionado.Nome} alocado na mesa {novaRequisicao.Mesa.Id}.");
                    }
                    else
                    {
                        Console.WriteLine($"Cliente {clienteSelecionado.Nome} entrou na fila de espera.");
                    }
                    break;
            // Opção 3: Sair do programa
                case 3:
                    continuar = false;
                    break;

                default:
                    // Opção inválida
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

}
public class Restaurante
{
    private List<Mesa> mesas;
    private Queue<Requisicao> filaEspera;
    private const int TOTAL_MESAS = 10;

    public Restaurante()
    {
        mesas = new List<Mesa>(TOTAL_MESAS);
        filaEspera = new Queue<Requisicao>();

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
        }
        else
        {
            filaEspera.Enqueue(requisicao);
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
