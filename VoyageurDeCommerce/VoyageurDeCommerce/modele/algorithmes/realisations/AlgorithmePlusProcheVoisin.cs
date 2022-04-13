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
    /// Gère l'algorithme du plus proche voisin
    /// </summary>
    public class AlgorithmePlusProcheVoisin : Algorithme
    {
        public override string Nom => "Plus proche voisin";

        /// <summary>
        /// Exécute l'algorithme du plus proche voisin sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //On lance la StopWatch
            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //On initialise la liste des lieux non visités en copiant la liste de tout les lieux
            List<Lieu> lieuxNonVisites = new List<Lieu>(listeLieux);

            //On initialise le lieu actuel au premier lieu non visité (l'usine)
            Lieu lieuActuel = lieuxNonVisites[0];

            //Boucle tant qu'il reste des lieux à visiter
            while (lieuxNonVisites.Count > 0)
            {
                //On initilise le voisin le plus proche à null
                Lieu voisinProche = null;

                //On initialise la distance entre le voisin connu le plus proche et le lieu actuel à 0
                int distance = 0;

                ///Boucle foreach qui parcours chaque lieu non visité 
                foreach (Lieu l in lieuxNonVisites)
                {
                    //voisinProche proche prend la valeur du lieu parcouru si sa distance avec la case actuelle est inférieur à celle du précédent voisin proche (ou si voisin proche est null)
                    if (FloydWarshall.Distance(lieuActuel, l) < distance || voisinProche == null)
                    {
                        voisinProche = l;
                        distance = FloydWarshall.Distance(lieuActuel, l);
                    }
                }

                //On ajoute le voisin le plus proche à la tournée
                Tournee.Add(voisinProche);

                //On arrête la StopWatch le temps de faire la capture de la tournée
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();

                //On enlève le voisinProche de la liste des lieux non visités
                lieuxNonVisites.Remove(voisinProche);

                //Le lieu actuel est désormais le voisin le plus proche
                lieuActuel = voisinProche;
            }
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
