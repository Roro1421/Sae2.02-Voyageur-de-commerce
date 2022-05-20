using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Gère l'algorithme du voisinage d'une tournée
    /// </summary>
    public class AlgorithmeVoisinageTourneeLoin : Algorithme
    {
        public override string Nom => "Voisinage d'une tournée (insertion loin)";

        /// <summary>
        /// Exécute l'algorithme du voisinage d'une tournée sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //On lance la stopwatch
            Stopwatch sw = Stopwatch.StartNew();

            //On initialise l'algorithme d'insertion loin sur lequel on va se baser
            AlgorithmeInsertionLoin algoUtilisé = new AlgorithmeInsertionLoin();
            List<Lieu> tournée = algoUtilisé.Tournée(listeLieux, listeRoute);

            //Initialisation de la distance minimale
            int distanceTotalMin = Distance(tournée);

            //Initialisation des voisinages
            List<Lieu> voisinage = new List<Lieu>(tournée);
            List<Lieu> voisinagePrecedent = new List<Lieu>(tournée);
            List<Lieu> meilleurVoisinagetrouve = new List<Lieu>(tournée);

            //Initialisation compteur pour le while
            int compteur = 0;

            do
            {
                for (int i = 0; i < tournée.Count; i++)
                {
                    Swap(voisinage, i, (i + 1) % voisinage.Count);

                    if (Distance(voisinage) < distanceTotalMin)
                    {
                        voisinagePrecedent = meilleurVoisinagetrouve;
                        distanceTotalMin = Distance(voisinage);
                        meilleurVoisinagetrouve = voisinage;
                    }
                }
                compteur++;
            } while (Distance(meilleurVoisinagetrouve) >= Distance(voisinagePrecedent) && compteur<1000);

            foreach (Lieu l in meilleurVoisinagetrouve)
            {
                Tournee.Add(l);
                //Capture de la tournée
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }

            this.TempsExecution = sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Echange deux lieux dans une liste donnée
        /// </summary>
        /// <param name="listeLieu">Liste de Lieu concernée</param>
        /// <param name="indexA">Premier Lieu à échanger</param>
        /// <param name="indexB">Deuxième Lieu à échanger</param>
        public void Swap(List<Lieu> listeLieu, int indexA, int indexB)
        {
            Lieu tmp = listeLieu[indexA];
            listeLieu[indexA] = listeLieu[indexB];
            listeLieu[indexB] = tmp;
        }

        /// <summary>
        /// Renvoie la distance 
        /// </summary>
        /// <param name="ListeLieux">Liste des lieux du graphe</param>
        /// <returns>Entier de la distance la plus </returns>
        public int Distance(List<Lieu> ListeLieux)
        {
            int resultat = 0;
            Lieu lPrécédent = ListeLieux[0];
            foreach (Lieu l in ListeLieux)
            {
                resultat += FloydWarshall.Distance(lPrécédent, l);
                lPrécédent = l;
            }
            resultat += FloydWarshall.Distance(lPrécédent, ListeLieux[0]);
            return resultat;
        }
    }
}
