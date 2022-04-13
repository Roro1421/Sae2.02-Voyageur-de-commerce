using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Gère l'algorithme d'insertion proche
    /// </summary>
    public class AlgorithmeInsertionProche : Algorithme
    {
        private int positionInsertion;
        public override string Nom => "Insertion proche";

        /// <summary>
        /// Exécute l'algorithme d'insertion proche sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //initialisation de la variable
            Lieu lieuPlusEloigne1 = listeLieux[0];
            Lieu lieuPlusEloigne2 = listeLieux[0];

            //On initialise les listes
            List<Lieu> lieuxVisites = new List<Lieu>();
            List<Lieu> lieuxNonVisites = new List<Lieu>(listeLieux);

            //initialise la distance la plus longue
            for (int i = 0; i < listeLieux.Count; i++)
            {
                for (int j = 0; j < listeLieux.Count; j++)
                {
                    if (FloydWarshall.Distance(listeLieux[i], listeLieux[j]) > FloydWarshall.Distance(lieuPlusEloigne1, lieuPlusEloigne2))
                    {
                        lieuPlusEloigne1 = listeLieux[i];
                        lieuPlusEloigne2 = listeLieux[j];
                    }
                }
            }

            //on ajoute le deux lieux les plus eloigne a la liste des lieux visite
            lieuxVisites.Add(lieuPlusEloigne1);
            lieuxVisites.Add(lieuPlusEloigne2);

            //on ajoute le deux lieux les plus eloigne a la tourne
            Tournee.Add(lieuPlusEloigne1);
            Tournee.Add(lieuPlusEloigne2);

            //on retire le deux lieux les plus eloigne a la liste des lieux non visite
            lieuxNonVisites.Remove(lieuPlusEloigne1);
            lieuxNonVisites.Remove(lieuPlusEloigne2);

            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

            while (lieuxNonVisites.Count != 0)
            {
                Lieu plusProche = lieuxNonVisites[0];
                foreach (Lieu L in lieuxNonVisites)
                {
                    if (distanceTourne(lieuxVisites, L) < distanceTourne(lieuxVisites, plusProche))
                    {
                        plusProche = L;
                    }
                }
                Tournee.insert(positionInsertion,plusProche);
                lieuxVisites.Insert(positionInsertion, plusProche);
                lieuxNonVisites.Remove(plusProche);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }
            sw.Stop();
            this.TempsExecution = sw.ElapsedMilliseconds;
        }

        //renvoi la distance entre un point et un couple de point
        public int distanceCouple(Lieu A, Lieu B, Lieu C)
        {
            int distance = 0;

            //calcul de la distance du point A au couple de point B,C
            distance = FloydWarshall.Distance(B, A) + FloydWarshall.Distance(A, C) - FloydWarshall.Distance(B, C);
            return distance;
        }

        //retourne la distance entre un point et une tournee
        public int distanceTourne(List<Lieu> T, Lieu A)
        {
            int distance = distanceCouple(A, T[0], T[1]);
            this.positionInsertion = 1;

            for (int i = 0; i < T.Count; i++)
            {
                if (distanceCouple(A, T[i], T[(i + 1) % T.Count]) <= distance)
                {
                    distance = distanceCouple(A, T[i], T[(i + 1)%T.Count]);
                    this.positionInsertion = i + 1;
                }
            }
            return distance;
        }

    }
}
