using System;

public class Mesa
{
    public int Id { get; private set; }
    public int Capacidade { get; private set; }
    public bool Ocupada { get; private set; }

    public Mesa(int id, int capacidade)
    {
        Id = id;
        Capacidade = capacidade;
        Ocupada = false;
    }

    public bool VerificarDisponibilidade(int qtdePessoas)
    {
        return !Ocupada && qtdePessoas <= Capacidade;
    }

    public void Alocar()
    {
        if (!Ocupada)
        {
            Ocupada = true;
        }
        else
        {
            throw new InvalidOperationException("Mesa já está ocupada.");
        }
    }

    public void Desalocar()
    {
        Ocupada = false;
    }
}