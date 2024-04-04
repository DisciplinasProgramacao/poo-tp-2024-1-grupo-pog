public class Requisicao
{
    public Cliente Cliente { get; set; }
    public int QtdPessoas { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public Mesa Mesa { get; set; }

    public void IniciarAtendimento()
    {
        if (Mesa.VerificarDisponibilidade(QtdPessoas))
        {
            Mesa.Ocupada = true;
        }
        else
        {
            throw new InvalidOperationException("Mesa não disponível para a quantidade de pessoas.");
        }
    }

    public TimeSpan TerminarAtendimento()
    {
        if (DataSaida.HasValue)
        {
            return DataSaida.Value.Subtract(DataEntrada);
        }
        else
        {
            throw new InvalidOperationException("Atendimento não foi concluído.");
        }
    }
}
