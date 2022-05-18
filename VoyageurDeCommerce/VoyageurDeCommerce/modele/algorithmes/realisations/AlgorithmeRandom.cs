using System;
using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Gère l'algorithme aléatoire
    /// </summary>
    public class AlgorithmeRandom : Algorithme
    {
        public override string Nom => "Aléatoire";

        /// <summary>
        /// Exécute l'algorithme aléatoire sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //On lance la stopwatch
            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Initialisation de la liste
            List<Lieu> tourne = new List<Lieu>();

            //Appelle de la recherche locale par l'aléatoire
            tourne = repeateRandom(listeLieux,10000);

            //Ajoute tous les lieux à la Tournee
            foreach(Lieu l in tourne)
            {
                Tournee.Add(l);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }

            this.TempsExecution = sw.ElapsedMilliseconds;

        }

        /// <summary>
        /// Renvoie une tournée aléatoire
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="distance">Distance de la tournée</param>
        /// <returns>List de Lieux visités</returns>
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

        /// <summary>
        /// Repète l'algo random un nombre de fois donné en paramètre et sort la meilleur tournée
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="nbRepeate"></param>
        /// <returns>Meilleure tournée parmis toutes celles calculée</returns>
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
