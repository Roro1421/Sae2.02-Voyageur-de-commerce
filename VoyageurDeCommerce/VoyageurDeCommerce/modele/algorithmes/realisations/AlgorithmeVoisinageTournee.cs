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
            
        }
    }
}
