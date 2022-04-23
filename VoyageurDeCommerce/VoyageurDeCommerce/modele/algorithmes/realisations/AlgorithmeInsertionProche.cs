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
            //On lance la stopwatch
            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Initialisation de la variable
            Lieu lieuPlusEloigne1 = listeLieux[0];
            Lieu lieuPlusEloigne2 = listeLieux[0];

            //Initialisation les listes
            List<Lieu> lieuxVisites = new List<Lieu>();
            List<Lieu> lieuxNonVisites = new List<Lieu>(listeLieux);

            //Initialisation de la distance la plus longue
            int max = 0;
            foreach (Lieu lieu1 in listeLieux)
            {
                foreach (Lieu lieu2 in listeLieux)
                {
                    if (FloydWarshall.Distance(lieu1, lieu2) > max)
                    {
                        lieuPlusEloigne1 = lieu1;
                        lieuPlusEloigne2 = lieu2;
                        max = FloydWarshall.Distance(lieuPlusEloigne1, lieuPlusEloigne2);
                    }
                }
            }

            //On ajoute les deux lieux les plus éloignés à la liste des lieux visités
            lieuxVisites.Add(lieuPlusEloigne1);
            lieuxVisites.Add(lieuPlusEloigne2);

            //On ajoute les deux lieux les plus éloignés à la tournée
            Tournee.Add(lieuPlusEloigne1);
            Tournee.Add(lieuPlusEloigne2);

            //On retire les deux lieux les plus éloignés à la liste des lieux non visités
            lieuxNonVisites.Remove(lieuPlusEloigne1);
            lieuxNonVisites.Remove(lieuPlusEloigne2);


            //Capture de la tournée
            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

            //Boucle trouvant l'ordre de la tournée
            while (lieuxNonVisites.Count != 0)
            {
                Lieu plusProche = lieuxNonVisites[0];
                foreach (Lieu L in lieuxNonVisites)
                {
                    if (distanceTournee(lieuxVisites, L) < distanceTournee(lieuxVisites, plusProche))
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
            //Retourne la durée de la stopwatch
            this.TempsExecution = sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Retourne la distance entre un lieu et un couple de lieux
        /// </summary>
        /// <param name="A">Lieu hors couple</param>
        /// <param name="B">Premier lieu du couple</param>
        /// <param name="C">Deuxième lieu du couple</param>
        /// <returns>Entier de la distance entre le lieu et le couple de lieux</returns>
        public int distanceCouple(Lieu A, Lieu B, Lieu C)
        {
            int distance = 0;

            //Calcul de la distance du lieu A au couple de lieux B,C
            distance = FloydWarshall.Distance(B, A) + FloydWarshall.Distance(A, C) - FloydWarshall.Distance(B, C);
            return distance;
        }

        /// <summary>
        /// Retourne la distance entre un lieu et une tournée
        /// </summary>
        /// <param name="T">Liste de Lieu représentant la tournée</param>
        /// <param name="A">Lieu hors tournée</param>
        /// <returns></returns>
        public int distanceTournee(List<Lieu> T, Lieu A)
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
