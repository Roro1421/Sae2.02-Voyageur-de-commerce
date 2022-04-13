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

            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //On initialise les listes
            List<Lieu> lieuxVisites = new List<Lieu>();
            List<Lieu> lieuxNonVisites = new List<Lieu>(listeLieux);

            Lieu usine = lieuxNonVisites[0];

            //On commence la tournée avec l'usine
            Tournee.Add(usine);
            sw.Stop();
            this.NotifyPropertyChanged("Tournee");
            sw.Start();
            

            //On ajoute l'usine à la liste des lieux visités
            lieuxVisites.Add(usine);

            //On enlève l'usine de la lsite des lieux non visités
            lieuxNonVisites.Remove(usine);

            while (lieuxNonVisites.Count > 0)
            {
                Lieu voisinProche = lieuxNonVisites[0];
                Lieu dernier = lieuxVisites[lieuxVisites.Count - 1];
                foreach (Lieu l in lieuxNonVisites)
                {
                    if (FloydWarshall.Distance(dernier, l) < FloydWarshall.Distance(dernier, voisinProche))
                    {
                        voisinProche = l;
                    }
                }

                //On ajoute le voisin le plus proche à la tournée
                Tournee.Add(voisinProche);

                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start();

                //On ajoute l'usine à la liste des lieux visités
                lieuxVisites.Add(voisinProche);

                //On enlève l'usine de la liste des lieux non visités
                lieuxNonVisites.Remove(voisinProche);
            }
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
