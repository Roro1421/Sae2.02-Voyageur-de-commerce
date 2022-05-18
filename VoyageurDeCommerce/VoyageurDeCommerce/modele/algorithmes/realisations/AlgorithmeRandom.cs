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

            List<Lieu> tourne = new List<Lieu>();
            tourne = repeateRandom(listeLieux,1000);

            foreach(Lieu l in tourne)
            {
                Tournee.Add(l);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }



            //this.TempsExecution = sw.ElapsedMilliseconds;

        }

        public List<Lieu> random(List<Lieu> listeLieux, out int distance)
        {
            List<Lieu> lieuNonVisite = new List<Lieu>(listeLieux);
            List<Lieu> lieuVisite = new List<Lieu>();
            Random rand = new Random();
            distance = 0;
            int index;
            for (int i = 0; i < listeLieux.Count; i++)
            {
                index = rand.Next(0, lieuNonVisite.Count);
                lieuVisite.Add(lieuNonVisite[index]);
                if (i != 0)
                {
                    distance += FloydWarshall.Distance(lieuVisite[lieuVisite.Count - 1], lieuVisite[lieuVisite.Count - 2]);
                }
                lieuNonVisite.Remove(lieuNonVisite[index]);
            }
            distance += FloydWarshall.Distance(lieuVisite[lieuVisite.Count - 1], lieuVisite[0]);
            return lieuVisite;
        }

        public List<Lieu> repeateRandom(List<Lieu> listeLieux,int nbRepeate)
        {
            int distance;
            int minDistance = 100000;
            List<Lieu> tourne = new List<Lieu>();
            List<Lieu> meilleurTourne = new List<Lieu>();
            for (int i = 0; i < nbRepeate; i++)
            {
                tourne = random(listeLieux, out distance);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    meilleurTourne = tourne;
                }
            }
            return meilleurTourne;
        }
    }
}
