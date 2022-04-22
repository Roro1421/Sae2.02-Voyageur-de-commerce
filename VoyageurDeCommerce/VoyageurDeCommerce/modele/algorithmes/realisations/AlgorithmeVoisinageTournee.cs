using System.Collections.Generic;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Gère l'algorithme du voisinage d'une tournée
    /// </summary>
    public class AlgorithmeVoisinageTournee : Algorithme
    {
        public override string Nom => "Voisinage d'une tournée";

        /// <summary>
        /// Exécute l'algorithme du voisinage d'une tournée sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            int distanceTotalMin = Distance(listeLieux);

            List<Lieu> voisinage = new List<Lieu>(listeLieux);
            List<Lieu> meilleurVoisinage = new List<Lieu>(listeLieux);

            for (int i = 0; i < listeLieux.Count; i++)
            {
                Swap(voisinage, i, (i + 1) % voisinage.Count);

                int distanceTotal = Distance(voisinage);

                if (distanceTotal < distanceTotalMin)
                {
                    distanceTotalMin = distanceTotal;
                    meilleurVoisinage = voisinage;
                }
                voisinage = listeLieux;
            }

            foreach (Lieu l in meilleurVoisinage)
            {
                Tournee.Add(l);
            }
            this.NotifyPropertyChanged("Tournee");

        }

        public void Swap(List<Lieu> listeLieu, int indexA, int indexB) 
        {
            Lieu tmp = listeLieu[indexA];
            listeLieu[indexA] = listeLieu[indexB];
            listeLieu[indexB] = tmp; 
        }

        public int Distance(List<Lieu> ListeLieux)
        {

                int resultat = 0;
                for (int i = 0; i < ListeLieux.Count; i++)
                {
                    resultat += FloydWarshall.Distance(ListeLieux[i], ListeLieux[(i + 1) % ListeLieux.Count]);
                }
                return resultat;

        }

    }
}
