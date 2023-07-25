using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSortRegistro_3_
{
    internal class Program
    {
        public static int comparacoes;
        public static int trocas; 
        static void Main(string[] args)
        {
            //int N = 10;
            //int N = 100;
            //int N = 500;
            int N = 1000;
            Registro[] registros = new Registro[N];

            // Preencher o vetor com registros aleatórios
            TimeSpan tempoDecorrido = MedirTempoDecorrido(() =>
            {
                // Preencher o vetor com registros aleatórios
                PreencherRegistros(registros);

                Console.WriteLine("Vetor original:");
                ImprimirVetor(registros);

                QuicksortIterativo(registros);

                Console.WriteLine("Vetor ordenado:");
                ImprimirVetor(registros);

                Console.WriteLine("O número de trocas realizadas: " + trocas + " trocas");
                Console.WriteLine("O número de comparações realizadas são: " + comparacoes + " comparações");
            });

            Console.WriteLine("Tempo de execução: " + tempoDecorrido.TotalMilliseconds.ToString("F2") + " milessegundos");
            Console.ReadKey();
        }
        public static TimeSpan MedirTempoDecorrido(Action acao)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            acao.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void QuicksortIterativo(Registro[] vetor)
        {
            if (vetor == null || vetor.Length <= 1)
                return;

            var pilha = new Stack<int[]>();

            int inicio = 0;
            int fim = vetor.Length - 1;

            pilha.Push(new[] { inicio, fim });

            while (pilha.Count > 0)
            {
                var indices = pilha.Pop();
                inicio = indices[0];
                fim = indices[1];

                if (inicio < fim)
                {
                    int pivo = Particionar(vetor, inicio, fim);

                    pilha.Push(new[] { inicio, pivo - 1 });
                    pilha.Push(new[] { pivo + 1, fim });
                }
            }
        }

        static int Particionar(Registro[] vetor, int inicio, int fim)
        {
            Registro pivo = vetor[fim];
            int i = inicio - 1;

            for (int j = inicio; j < fim; j++)
            {
                comparacoes++;
                if (vetor[j].Chave < pivo.Chave)
                {
                    trocas++;
                    i++;
                    TrocarRegistros(vetor, i, j);
                }
            }

            TrocarRegistros(vetor, i + 1, fim);

            return i + 1;
        }

        static void TrocarRegistros(Registro[] vetor, int indice1, int indice2)
        {
            Registro temp = vetor[indice1];
            vetor[indice1] = vetor[indice2];
            vetor[indice2] = temp;
        }

        static void PreencherRegistros(Registro[] vetor)
        {
            Random random = new Random();

            for (int i = 0; i < vetor.Length; i++)
            {
                Registro registro = new Registro();
                registro.Chave = random.Next(100);
                registro.Cadeias = new string[10];
                for (int j = 0; j < registro.Cadeias.Length; j++)
                {
                    registro.Cadeias[j] = GerarCadeiaAleatoria(200);
                }
                registro.Booleano = random.NextDouble() > 0.5;
                registro.NumerosReais = new double[4];
                for (int j = 0; j < registro.NumerosReais.Length; j++)
                {
                    registro.NumerosReais[j] = random.NextDouble();
                }
                vetor[i] = registro;
            }
        }

        static string GerarCadeiaAleatoria(int tamanho)
        {
            Random random = new Random();
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            char[] cadeia = new char[tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                cadeia[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }

            return new string(cadeia);
        }

        static void ImprimirVetor(Registro[] vetor)
        {
            foreach (var registro in vetor)
            {
                Console.WriteLine($"Chave: {registro.Chave}, Booleano: {registro.Booleano}");
                Console.WriteLine("Cadeias:");
                foreach (var cadeia in registro.Cadeias)
                {
                    Console.WriteLine(cadeia);
                }
                Console.WriteLine("Números Reais:");
                foreach (var numero in registro.NumerosReais)
                {
                    Console.WriteLine(numero);
                }
                Console.WriteLine();
            }
        }
    }
}
