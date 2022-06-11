using System;
using System.Collections.Generic;

namespace GeneGraph
{
    /// <summary>
    /// Permet d'écrire dans la console les lignes pour créer un graph bipartie avec n sommets, interprétable par le parceur du voyageur de commerce
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //nombre de sommets 
            int nb = 100;
            //nombre random
            var rand = new Random();
            //numéro du magasin 
            int num = 0;
            //abcisse du magasin 
            int x = 0;

            //création des magasins
            for (int i = 0; i < nb/2; i++)
            {
                Console.WriteLine("MAGASIN" + " " + num + " " + x + " " + "0");
                num++;
                Console.WriteLine("MAGASIN" + " " + num + " " + x + " " + "5");
                num++;
                x++; 

            } 

            //création des routes  
            for (int i = 0; i < nb; i += 2)
            {
                for (int y = 1; y <= nb; y += 2)
                {
                    Console.Write("ROUTE " + i + " ");
                    Console.WriteLine(y + " " + rand.Next(1, nb));
                }
            }
        }
    }
}
