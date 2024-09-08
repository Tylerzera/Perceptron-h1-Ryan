using System;

class Perceptron
{
    // Atributos
    double[] pesos;
    double bias;
    double taxaAprendizado;
    Random random;

    // Construtor
    public Perceptron(int numAtributos, double taxaAprendizado)
    {
        this.pesos = new double[numAtributos];
        this.bias = 0;
        this.taxaAprendizado = taxaAprendizado;
        this.random = new Random();

        // Inicializa pesos aleatórios
        for (int i = 0; i < pesos.Length; i++)
        {
            pesos[i] = random.NextDouble() * 2 - 1; // Pesos aleatórios entre -1 e 1
        }
        bias = random.NextDouble() * 2 - 1;
    }

    // Função de ativação (degrau)
    private int Ativar(double soma)
    {
        return soma >= 0 ? 1 : 0; // 1 para maçã, 0 para laranja
    }

    // Função de previsão
    public int Prever(double[] entrada)
    {
        double soma = bias;
        for (int i = 0; i < entrada.Length; i++)
        {
            soma += pesos[i] * entrada[i];
        }
        return Ativar(soma);
    }

    // Função de treino
    public void Treinar(double[][] entradas, int[] resultadosEsperados, int iteracoes)
    {
        for (int it = 0; it < iteracoes; it++)
        {
            for (int i = 0; i < entradas.Length; i++)
            {
                int resultadoPrevisto = Prever(entradas[i]);
                int erro = resultadosEsperados[i] - resultadoPrevisto;

                // Atualizar os pesos e bias
                for (int j = 0; j < pesos.Length; j++)
                {
                    pesos[j] += taxaAprendizado * erro * entradas[i][j];
                }
                bias += taxaAprendizado * erro;
            }
            Console.WriteLine($"Iteração {it + 1}: Pesos = [{string.Join(", ", pesos)}], Bias = {bias}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Dados de treinamento (Peso, pH) e rótulos (1 = maçã, 0 = laranja)
        double[][] entradas = new double[][]
        {
            new double[] { 113, 6.8 },
            new double[] { 122, 4.7 },
            new double[] { 107, 5.2 },
            new double[] { 98, 3.6 },
            new double[] { 115, 2.9 },
            new double[] { 120, 4.2 }
        };
        int[] rotulos = { 1, 0, 1, 1, 0, 0 }; // Maçãs e Laranjas

        // Inicializando o perceptron
        Perceptron perceptron = new Perceptron(2, 0.1); // 2 atributos (Peso e pH), taxa de aprendizado 0.1

        // Treinamento
        perceptron.Treinar(entradas, rotulos, 10); // Treinando por 10 iterações

        // Testando com novos exemplos
        double[][] novosCasos = new double[][]
        {
            new double[] { 110, 6.5 },
            new double[] { 125, 4.3 },
            new double[] { 105, 5.8 },
            new double[] { 100, 3.1 },
            new double[] { 115, 4.9 }
        };

        Console.WriteLine("\nClassificação dos novos casos:");
        for (int i = 0; i < novosCasos.Length; i++)
        {
            int resultado = perceptron.Prever(novosCasos[i]);
            string fruta = resultado == 1 ? "Maçã" : "Laranja";
            Console.WriteLine($"Caso {i + 1}: Peso = {novosCasos[i][0]}, pH = {novosCasos[i][1]} => {fruta}");
        }
    }
}
