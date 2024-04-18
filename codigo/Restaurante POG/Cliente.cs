using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cliente
{
    public string Nome { get; private set; }

    public Cliente(string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome do cliente não pode ser nulo ou vazio.", nameof(nome));
        }
        Nome = nome;
    }
}
