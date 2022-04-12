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
    class AlgorithmeCroissant : Algorithme
    {
        public override string Nom => "Tournée croissante"; 

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch sw = Stopwatch.StartNew();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

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
