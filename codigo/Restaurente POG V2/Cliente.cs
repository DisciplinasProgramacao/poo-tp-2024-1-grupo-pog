using System;

public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; }

    public Cliente(int id, string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome do cliente não pode ser nulo ou vazio.", nameof(nome));
        }
        Id = id;
        Nome = nome;
    }
}
