﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VoyageurDeCommerce.vuemodele
{
    /// <summary> Classe abstraite pour les objets observables.</summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <summary> Evénement de modification d'une property </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary> Méthode à appler pour avertir d'une modification </summary>
        /// <param name="propertyName">Nom de la property modifiée (authomatiquement déterminé si appelé directement dans le setter une property) </param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
