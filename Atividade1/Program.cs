class Program
{
    static void Main()
    {
        // 3. Instanciando as três contas com números e saldos iniciais
        ContaCorrente cc = new ContaCorrente("1001-CC", 1000m);
        ContaPoupanca cp = new ContaPoupanca("2002-CP", 500m);
        ContaSalario cs = new ContaSalario("3003-CS", 1200m);

        // Começa com a Conta Corrente selecionada por padrão
        ContaBancaria contaAtual = cc;
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("         SISTEMA BANCÁRIO C#             ");
            Console.WriteLine("=========================================");
            Console.WriteLine($"Conta Selecionada: [{contaAtual.GetType().Name}] Nº {contaAtual.Numero}");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Selecionar conta bancária");
            Console.WriteLine("2. Consultar saldo");
            Console.WriteLine("3. Realizar depósito");
            Console.WriteLine("4. Realizar saque");
            Console.WriteLine("5. Transferir entre contas");
            Console.WriteLine("6. Efetuar pagamento");
            Console.WriteLine("7. Exibir informações completas das contas");
            Console.WriteLine("8. Encerrar o programa");
            Console.WriteLine("=========================================");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    contaAtual = SelecionarConta(cc, cp, cs, contaAtual);
                    break;

                case "2":
                    Console.WriteLine($"Saldo atual da conta {contaAtual.Numero}: {contaAtual.Saldo:C}");
                    if (contaAtual is ContaCorrente corrente)
                    {
                        Console.WriteLine($"Limite adicional disponível: {corrente.Limite:C}");
                        Console.WriteLine($"Total disponível para uso: {(corrente.Saldo + corrente.Limite):C}");
                    }
                    break;

                case "3":
                    decimal valorDeposito = LerValor("Digite o valor para depósito: R$ ");
                    contaAtual.Depositar(valorDeposito);
                    break;

                case "4":
                    decimal valorSaque = LerValor("Digite o valor para saque: R$ ");
                    contaAtual.Sacar(valorSaque);
                    break;

                case "5":
                    RealizarTransferencia(contaAtual, cc, cp, cs);
                    break;

                case "6":
                    decimal valorPagamento = LerValor("Digite o valor do pagamento: R$ ");
                    contaAtual.Pagar(valorPagamento);
                    break;

                case "7":
                    ExibirInformacoesCompletas(cc, cp, cs);
                    break;

                case "8":
                    executando = false;
                    Console.WriteLine("Encerrando o sistema bancário. Até logo!");
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }

            if (executando)
            {
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }
    }

    // Método auxiliar para escolher qual conta gerenciar
    static ContaBancaria SelecionarConta(ContaCorrente cc, ContaPoupanca cp, ContaSalario cs, ContaBancaria atual)
    {
        Console.WriteLine("Selecione a conta desejada:");
        Console.WriteLine($"1 - Conta Corrente ({cc.Numero})");
        Console.WriteLine($"2 - Conta Poupança ({cp.Numero})");
        Console.WriteLine($"3 - Conta Salário  ({cs.Numero})");
        Console.Write("Opção: ");

        string escolha = Console.ReadLine();
        switch (escolha)
        {
            case "1":
                Console.WriteLine("Conta Corrente selecionada!");
                return cc;
            case "2":
                Console.WriteLine("Conta Poupança selecionada!");
                return cp;
            case "3":
                Console.WriteLine("Conta Salário selecionada!");
                return cs;
            default:
                Console.WriteLine("Opção inválida. Mantendo a conta atual.");
                return atual;
        }
    }

    // Método auxiliar para escolher a conta de destino da transferência
    static void RealizarTransferencia(ContaBancaria origem, ContaCorrente cc, ContaPoupanca cp, ContaSalario cs)
    {
        Console.WriteLine($"Transferindo a partir de: {origem.Numero} ({origem.GetType().Name})");
        Console.WriteLine("Escolha a conta de destino:");
        Console.WriteLine($"1 - Conta Corrente ({cc.Numero})");
        Console.WriteLine($"2 - Conta Poupança ({cp.Numero})");
        Console.WriteLine($"3 - Conta Salário  ({cs.Numero})");
        Console.Write("Destino: ");

        string escolha = Console.ReadLine();
        ContaBancaria destino = escolha switch
        {
            "1" => cc,
            "2" => cp,
            "3" => cs,
            _ => null
        };

        if (destino == null)
        {
            Console.WriteLine("Conta de destino inválida.");
            return;
        }

        if (destino == origem)
        {
            Console.WriteLine("Não é possível transferir para a mesma conta de origem.");
            return;
        }

        decimal valor = LerValor("Digite o valor da transferência: R$ ");
        origem.Transferir(valor, destino);
    }

    // Método auxiliar para exibir o relatório completo da opção 7
    static void ExibirInformacoesCompletas(ContaCorrente cc, ContaPoupanca cp, ContaSalario cs)
    {
        Console.WriteLine("=== INFORMAÇÕES COMPLETAS DAS CONTAS ===\n");

        Console.WriteLine($"[Conta Corrente] Nº: {cc.Numero}");
        Console.WriteLine($"Saldo: {cc.Saldo:C} | Limite: {cc.Limite:C} | Total Disponível: {(cc.Saldo + cc.Limite):C}");
        Console.WriteLine("Regra: Limite automático de 3x o saldo positivo.\n");

        Console.WriteLine($"[Conta Poupança] Nº: {cp.Numero}");
        Console.WriteLine($"Saldo: {cp.Saldo:C}");
        Console.WriteLine("Regra: Taxa adicional de 5% em operações de saque.\n");

        Console.WriteLine($"[Conta Salário]  Nº: {cs.Numero}");
        Console.WriteLine($"Saldo: {cs.Saldo:C}");
        Console.WriteLine("Regra: Sem limite adicional; operações limitadas ao saldo.");
    }

    // Método auxiliar para evitar erro caso o usuário digite letras em vez de números
    static decimal LerValor(string mensagem)
    {
        Console.Write(mensagem);
        if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor > 0)
        {
            return valor;
        }

        Console.WriteLine("Valor inválido! Considerando R$ 0,00.");
        return 0;
    }
}
