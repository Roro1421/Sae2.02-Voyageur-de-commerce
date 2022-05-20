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
    /// Gère l'algorithme croissant
    /// </summary>
    public class AlgorithmeCroissant : Algorithme
    {
        public override string Nom => "Tournée croissante";

        /// <summary>
        /// Exécute l'algorithme croissant sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            //On lance la stopwatch
            Stopwatch sw = Stopwatch.StartNew();

            //On lance les calculs de FloydWarshall
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            //Boucle trouvant l'ordre de la tournée
            foreach (Lieu lieu in listeLieux)
            {
                Tournee.Add(lieu);
                sw.Stop();
                this.NotifyPropertyChanged("Tournee");
                sw.Start(); 

            }
            this.TempsExecution = sw.ElapsedMilliseconds;
        }
    }
}
