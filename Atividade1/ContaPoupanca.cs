public class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(string numero, decimal saldoInicial = 0) : base(numero, saldoInicial)
    {
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0) return false;

        decimal taxa = valor * 0.05m;
        decimal totalDesconto = valor + taxa;

        if (totalDesconto <= Saldo)
        {
            Saldo -= totalDesconto;
            Console.WriteLine($"Saque de {valor:C} (Taxa: {taxa:C}) realizado na Poupança {Numero}.");
            return true;
        }

        Console.WriteLine($"Saldo insuficiente na Poupança {Numero} para cobrir saque e taxa.");
        return false;
    }

    // Sobrescrevemos Transferir e Pagar para que não cobrem a taxa de 5% exclusiva do saque
    public override bool Transferir(decimal valor, ContaBancaria contaDestino)
    {
        if (valor > 0 && valor <= Saldo)
        {
            Saldo -= valor;
            contaDestino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor:C} realizada da Poupança {Numero}.");
            return true;
        }

        Console.WriteLine("Saldo insuficiente para transferência.");
        return false;
    }

    public override bool Pagar(decimal valor)
    {
        if (valor > 0 && valor <= Saldo)
        {
            Saldo -= valor;
            Console.WriteLine($"Pagamento de {valor:C} realizado na Poupança {Numero}.");
            return true;
        }

        Console.WriteLine("Saldo insuficiente para pagamento.");
        return false;
    }
}
