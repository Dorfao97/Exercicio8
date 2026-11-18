public class ContaSalario : ContaBancaria
{
    public ContaSalario(string numero, decimal saldoInicial = 0) : base(numero, saldoInicial)
    {
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0) return false;

        if (valor <= Saldo)
        {
            Saldo -= valor;
            Console.WriteLine($"Operação de {valor:C} realizada na Conta Salário {Numero}.");
            return true;
        }

        Console.WriteLine($"Saldo insuficiente na Conta Salário {Numero}.");
        return false;
    }
}
