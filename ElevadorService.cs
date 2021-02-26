using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TesteApiSul
{
    public class ElevadorService : IElevadorService
    {
        public List<Viagem> Viagens { get; set; }

        public ElevadorService(string filePath)
        {
            // carrega o input em uma lista
            // utilizo aqui o Newtonsoft.Json para fazer o parse do arquivo json
            using StreamReader r = new StreamReader(filePath);
            string fileContent = r.ReadToEnd();
            Viagens = JsonConvert.DeserializeObject<List<Viagem>>(fileContent);
        }

        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary>
        /// Retorna lista de andares empatados em menos chamadas, ordenado pelo index.
        public List<int> AndarMenosUtilizado()
        {
            var grupos = Viagens.GroupBy(x => x.Andar);
            grupos = grupos.OrderBy(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<int> result = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return result;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
        /// Retorna lista dos elevadores empatados em mais chamadas, ordenados pela letra de identificação.
        public List<char> ElevadorMaisFrequentado()
        {
            var grupos = Viagens.GroupBy(x => x.Elevador);
            grupos = grupos.OrderByDescending(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> result = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return result;
        }

        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
        /// Retorna lista dos turnos mais frequentados pelos elevadores, na mesma ordem em que os elevadores foram recebidos do método ElevadorMaisFrequentado.
        public List<char> PeriodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<char> elevMaisFreq = ElevadorMaisFrequentado();
            List<char> result = new List<char>();

            foreach(char elev in elevMaisFreq)
            {
                var viagens = Viagens.Where(x => x.Elevador == elev).ToList();
                var turnos = viagens.GroupBy(x => x.Turno).OrderByDescending(x => x.Count());
                result.Add(turnos.FirstOrDefault().Key);
            }

            return result;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        /// Retorna lista dos elevadores empatados em menos chamadas, ordenados pela letra de identificação.
        public List<char> ElevadorMenosFrequentado()
        {
            var grupos = Viagens.GroupBy(x => x.Elevador);
            grupos = grupos.OrderBy(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> result = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return result;
        }

        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
        /// Retorna lista dos turnos menos frequentados pelos elevadores, na mesma ordem em que os elevadores foram recebidos do método ElevadorMenosFrequentado.
        public List<char> PeriodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> elevMenosFreq = ElevadorMenosFrequentado();
            List<char> result = new List<char>();

            foreach(char elev in elevMenosFreq)
            {
                var viagens = Viagens.Where(x => x.Elevador == elev).ToList();
                var turnos = viagens.GroupBy(x => x.Turno).OrderBy(x => x.Count());
                result.Add(turnos.FirstOrDefault().Key);
            }

            return result;
        }

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        /// Retorna lista com os turnos empatados em maior utilização. 
        public List<char> PeriodoMaiorUtilizacaoConjuntoElevadores()
        {
            var grupos = Viagens.GroupBy(x => x.Turno);
            grupos = grupos.OrderByDescending(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> result = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return result;
        }


        /// <summary> Retorna um float (duas casas decimais) contendo o percentual de uso do elevador x (identificador recebido como parâmetro)
        /// em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorX(char x)
        {
            var grupos = Viagens.GroupBy(x => x.Elevador);
            int totalElevador = grupos.FirstOrDefault(g => g.Key.CompareTo(x) == 0).Count();
            float result = (float)totalElevador*100 / (float)Viagens.Count;
            return (float)Math.Round(result * 100f) / 100f; ;
        }


        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorA()        {            return PercentualDeUsoElevadorX('A');        }
        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorB()        {            return PercentualDeUsoElevadorX('B');        }
        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorC()        {            return PercentualDeUsoElevadorX('C');        }
        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorD()        {            return PercentualDeUsoElevadorX('D');        }
        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
        public float PercentualDeUsoElevadorE()        {            return PercentualDeUsoElevadorX('E');        }
    }
}