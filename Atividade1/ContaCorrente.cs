public class ContaCorrente : ContaBancaria
{
    // Calcula automaticamente: 3x o saldo se for positivo, ou 0 se for negativo
    public decimal Limite => Saldo > 0 ? Saldo * 3 : 0;

    public ContaCorrente(string numero, decimal saldoInicial = 0) : base(numero, saldoInicial)
    {
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0) return false;

        // Pode usar o saldo disponível + o limite atual
        if (valor <= Saldo + Limite)
        {
            Saldo -= valor;
            Console.WriteLine($"Saque de {valor:C} realizado na Conta Corrente {Numero}.");
            return true;
        }

        Console.WriteLine($"Saldo e limite insuficientes na Conta Corrente {Numero}.");
        return false;
    }
}
