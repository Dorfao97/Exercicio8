public abstract class ContaBancaria
{
    public string Numero { get; set; }
    public decimal Saldo { get; protected set; }

    public ContaBancaria(string numero, decimal saldoInicial = 0)
    {
        Numero = numero;
        Saldo = saldoInicial;
    }

    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de depósito inválido.");
            return;
        }

        Saldo += valor;
        Console.WriteLine($"Depósito de {valor:C} realizado na conta {Numero}.");
    }

    // Método abstrato: cada tipo de conta define sua própria regra de saque
    public abstract bool Sacar(decimal valor);

    // Transferir reutiliza a lógica de Sacar da própria conta e Depositar na conta destino
    public virtual bool Transferir(decimal valor, ContaBancaria contaDestino)
    {
        if (Sacar(valor))
        {
            contaDestino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor:C} para a conta {contaDestino.Numero} concluída.");
            return true;
        }

        Console.WriteLine("Transferência não realizada por falta de saldo.");
        return false;
    }

    // Pagar também reutiliza a regra de saque/débito da conta
    public virtual bool Pagar(decimal valor)
    {
        if (Sacar(valor))
        {
            Console.WriteLine($"Pagamento de {valor:C} realizado com sucesso.");
            return true;
        }

        Console.WriteLine("Pagamento não realizado por falta de saldo.");
        return false;
    }
}
