using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations  
{
    /// <summary>
    /// Gère l'algorithme d'insertion loin
    /// </summary>
    public class AlgorithmeInsertionLoin : AlgorithmeInsertion
    {

        public override string Nom => "Insertion loin";

        /// <summary>
        /// Exécute l'algorithme d'insertion loin sur un graphe
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

            //Initialise la distance la plus longue
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

            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();

            while (lieuxNonVisites.Count != 0)
            {
                int positionInsertion = 0;
                Lieu plusLoin = lieuxNonVisites[0];
                int mindistance = 0;
                foreach (Lieu L in lieuxNonVisites)
                {
                    if (distanceTournee(lieuxVisites, L,out positionInsertion) > mindistance)
                    {
                        plusLoin = L;
                        mindistance = distanceTournee(lieuxVisites, plusLoin, out positionInsertion);
                    }
                }
                mindistance = distanceTournee(lieuxVisites, plusLoin, out positionInsertion);
                Tournee.insert(positionInsertion, plusLoin);
                lieuxVisites.Insert(positionInsertion, plusLoin);
                lieuxNonVisites.Remove(plusLoin);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();
            }
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
        public List<Lieu> Tournée(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Initialisation de la variable
            Lieu lieuPlusEloigne1 = listeLieux[0];
            Lieu lieuPlusEloigne2 = listeLieux[0];

            //Initialisation les listes
            List<Lieu> lieuxVisites = new List<Lieu>();
            List<Lieu> lieuxNonVisites = new List<Lieu>(listeLieux);

            //Initialise la distance la plus longue
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

            //On retire les deux lieux les plus éloignés à la liste des lieux non visités 
            lieuxNonVisites.Remove(lieuPlusEloigne1);
            lieuxNonVisites.Remove(lieuPlusEloigne2);

            while (lieuxNonVisites.Count != 0)
            {
                int positionInsertion = 0;
                Lieu plusLoin = lieuxNonVisites[0];
                int mindistance = 0;
                foreach (Lieu L in lieuxNonVisites)
                {
                    if (distanceTournee(lieuxVisites, L, out positionInsertion) > mindistance)
                    {
                        plusLoin = L;
                        mindistance = distanceTournee(lieuxVisites, plusLoin, out positionInsertion);
                    }
                }
                mindistance = distanceTournee(lieuxVisites, plusLoin, out positionInsertion);
                lieuxVisites.Insert(positionInsertion, plusLoin);
                lieuxNonVisites.Remove(plusLoin);
            }
            return lieuxVisites;
        }
    }
}

