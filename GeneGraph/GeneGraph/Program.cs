using System;
using System.Collections.Generic;

namespace GeneGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            //nombre de sommets 
            int nb = 50;
            //copie du nombre de sommet -1 pour créer les routes (50 sommets numérotés de 0 à 49)
            int nbcopie = nb - 1;
            //nombre random
            var rand = new Random();
            //distance initial du centre du cercle
            int distanceFromCenter = 15; 

            //création des magasins 
            for (int i = 0; i < nb; i++)
            {
                //La première ligne correspond à l'usine qui est au numéro 0
                if (i == 0)
                {
                    Console.Write("USINE " + i + " "); 
                }

                //le reste sont les magasins et sont nuémroté de manière croissante
                else
                {
                    Console.Write("MAGASIN " + i + " "); 
                }

                //partie trigonométire
                double angle = (360 / nb) * i;
                double angleRadian = angle * Math.PI / 180;

                //x et y correspondent aux coordonnées d'un sommet sur le cercle trigonométrique 
                double x = Math.Cos(angleRadian) * distanceFromCenter;
                double y = Math.Sin(angleRadian) * distanceFromCenter;

                //écriture des magasins
                Console.WriteLine(Math.Round(x) + " " + Math.Round(y));

            }

            //création des route, on relie chaque sommet entre eux 
            for (int i = 0; i < nb; i++) 
            {
                //sommet correspond à l'arrivée, on ne relie pas un sommet à lui même
                //j'utilise sommet pour passer en revue chaque sommet qui n'est pas encore relié au sommet actuel
                int sommet = nbcopie -1;
                for (int y = 0; y < nbcopie; y++)
                {
                    Console.WriteLine("ROUTE " + nbcopie + " " + sommet + " " + rand.Next(1, nb)); //la pondération des routes est aléatoire (compris entre 1 et le nombres de sommets)
                    sommet--; 
                }
                nbcopie--; 
            }

        }

       
        

        
    }
}
