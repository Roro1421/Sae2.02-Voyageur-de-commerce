using System;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.parseur
{
    /// <summary>Monteur de lieu </summary>
    public class MonteurLieu
    {
        /// <summary>
        /// Crée un lieu à partir du tableau de string obtenu en lisant le fichier 
        /// </summary>
        /// <param name="morceaux">Les 4 morceaux de la ligne correspondant à la ligne</param>
        /// <returns>Le lieu créé</returns>
        public static Lieu Creer(String[] morceaux)
        {
            Lieu resultat = null; 
            switch (morceaux[0])
            {
                case "MAGASIN":
                    resultat = new Lieu(TypeLieu.MAGASIN, morceaux[1], Int32.Parse(morceaux[2]), Int32.Parse(morceaux[3])); 
                    break;
                case "USINE":
                    resultat = new Lieu(TypeLieu.USINE, morceaux[1], Int32.Parse(morceaux[2]), Int32.Parse(morceaux[3]));
                    break;
            }
            return resultat;
        }
    }
}
