using System.Collections.Generic;
using System.Diagnostics;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Gère l'algorithme d'insertion proche
    /// </summary>
    public abstract class AlgorithmeInsertion : Algorithme
    {
        /// <summary>
        /// Exécute l'algorithme d'insertion proche sur un graphe
        /// </summary>
        /// <param name="listeLieux">Liste de tous les lieux du graphe concerné</param>
        /// <param name="listeRoute">Liste de toutes les routes du graphe concerné</param>
        public abstract override void Executer(List<Lieu> listeLieux, List<Route> listeRoute);

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
        public int distanceTournee(List<Lieu> T, Lieu A, out int positionInsertion)
        {
            int distance = distanceCouple(A, T[0], T[1]);
            positionInsertion = 1;

            for (int i = 0; i < T.Count; i++)
            {
                if (distanceCouple(A, T[i], T[(i + 1) % T.Count]) <= distance)
                {
                    distance = distanceCouple(A, T[i], T[(i + 1)%T.Count]);
                    positionInsertion = i + 1;
                }
            }
            return distance;
        }
    }
}
