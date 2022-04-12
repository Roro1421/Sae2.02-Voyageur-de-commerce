using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.parseur
{
    /// <summary>Parseur de fichier de graphe</summary>
    public class Parseur
    {
        private string adresseFichier;

        /// <summary>Propriétés nécessaires</summary>
        private Dictionary<string, Lieu> listeLieux;
        public Dictionary<string, Lieu> ListeLieux => listeLieux;
        private List<Route> listeRoutes;
        public List<Route> ListeRoutes => listeRoutes;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="nomDuFichier">Nom du fichier à parser</param>
        public Parseur(String nomDuFichier)
        {
            this.listeLieux = new Dictionary<string, Lieu>();
            this.listeRoutes = new List<Route>();
            this.adresseFichier = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\ressources\" + nomDuFichier;
        }

        /// <summary>
        /// Parsage du fichier
        /// </summary>
        public void Parser()
        {
            using (StreamReader stream = new StreamReader(this.adresseFichier))
            {
                string ligne;
                //boucle sur la lecture de chaque ligne
                while ((ligne = stream.ReadLine()) != null)
                {
                    //analyser la ligne
                    string[] morceaux = ligne.Split(' ');

                    if (morceaux[0] == "ROUTE")
                    {
                        Route route = MonteurRoute.Creer(morceaux, listeLieux);
                        listeRoutes.Add(route);
                    }

                    else
                    {
                        Lieu lieu = MonteurLieu.Creer(morceaux);
                        listeLieux.Add(morceaux[1], lieu);
                    }
                }
            }

            /*
            //graphe à la main du tp03
            this.listeLieux.Add("1", new Lieu(TypeLieu.USINE, "USINE", 0, 0));
            this.listeLieux.Add("2", new Lieu(TypeLieu.MAGASIN, "MAGASIN", 2, 0));
            this.listeLieux.Add("3", new Lieu(TypeLieu.MAGASIN, "MAGASIN", -2, 2));
            this.listeLieux.Add("4", new Lieu(TypeLieu.MAGASIN, "MAGASIN", 4, 2));
            this.listeLieux.Add("5", new Lieu(TypeLieu.MAGASIN, "MAGASIN", 1, 4));

            this.listeRoutes.Add(new Route(ListeLieux["1"], ListeLieux["2"], 2));
            this.listeRoutes.Add(new Route(ListeLieux["1"], ListeLieux["3"], 3));
            this.listeRoutes.Add(new Route(ListeLieux["1"], ListeLieux["5"], 6));
            this.listeRoutes.Add(new Route(ListeLieux["2"], ListeLieux["4"], 1));
            this.listeRoutes.Add(new Route(ListeLieux["2"], ListeLieux["5"], 3));
            this.listeRoutes.Add(new Route(ListeLieux["3"], ListeLieux["5"], 4));
            this.listeRoutes.Add(new Route(ListeLieux["4"], ListeLieux["5"], 1));
            */

        }
    }
}
