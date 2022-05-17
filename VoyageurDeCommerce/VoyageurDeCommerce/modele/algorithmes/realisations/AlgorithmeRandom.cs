using System;
using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgorithmeRandom : Algorithme
    {
        public AlgorithmeRandom()
        {
        }

        public override string Nom => "Random";

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = Stopwatch.StartNew();

            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            List<Lieu> lieuVisité = new List<Lieu>(listeLieux);
            Random rand = new Random();
            int index;
            for (int i = 0; i < listeLieux.Count; i++)
            {
                index = rand.Next(0, lieuVisité.Count);
                Tournee.Add(lieuVisité[index]);
                lieuVisité.RemoveAt(index);

                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();

            }
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
