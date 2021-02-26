using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;


namespace TesteApiSul
{
    class Program
    {
        private static IElevadorService _elevadorService;

        private static void Main()
        {
            // o arquivo foi configurado na solução para sempre copiar para a pasta de saída.
            _elevadorService = new ElevadorService(@"input.json");
            
            var ids = _elevadorService.AndarMenosUtilizado();
            string currentMsg = "Andares menos utilizados: " + string.Join(", ", ids);
            Console.WriteLine(currentMsg);

            Console.WriteLine("=========================================");

            List<char> elevsMaiorFreq = _elevadorService.ElevadorMaisFrequentado();
            List<char> turnosMaiorFreq = _elevadorService.PeriodoMaiorFluxoElevadorMaisFrequentado();

            currentMsg = "Elevadores mais frequentados: \n";

            for (int i = 0; i < elevsMaiorFreq.Count; i++)            
                currentMsg += $"{elevsMaiorFreq[i]} - turno de maior fluxo: {turnosMaiorFreq[i]}\n";            

            Console.WriteLine(currentMsg);

            Console.WriteLine("=========================================");

            List<char> elevsMenorFreq = _elevadorService.ElevadorMenosFrequentado();
            List<char> turnosMenorFreq = _elevadorService.PeriodoMenorFluxoElevadorMenosFrequentado();

            currentMsg = "Elevadores menos frequentados: \n";

            for (int i = 0; i < elevsMenorFreq.Count; i++)            
                currentMsg += $"{elevsMenorFreq[i]} - turno de menor fluxo: {turnosMenorFreq[i]}\n";            

            Console.WriteLine(currentMsg);

            Console.WriteLine("=========================================");

            var turnos = _elevadorService.PeriodoMaiorUtilizacaoConjuntoElevadores();
            currentMsg = "Período(s) de maior utilização conjunta dos elevadores: " + new string(turnos.ToArray());
            Console.WriteLine(currentMsg);

            Console.WriteLine("=========================================");

            Console.WriteLine($"Percentual de uso do elevador A: {_elevadorService.PercentualDeUsoElevadorA()}%");
            Console.WriteLine($"Percentual de uso do elevador B: {_elevadorService.PercentualDeUsoElevadorB()}%");
            Console.WriteLine($"Percentual de uso do elevador C: {_elevadorService.PercentualDeUsoElevadorC()}%");
            Console.WriteLine($"Percentual de uso do elevador D: {_elevadorService.PercentualDeUsoElevadorD()}%");
            Console.WriteLine($"Percentual de uso do elevador E: {_elevadorService.PercentualDeUsoElevadorE()}%");
        }
    }
}