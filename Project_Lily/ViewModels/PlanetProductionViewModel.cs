using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Project_Lily.ViewModels
{
    public class PlanetProductionViewModel : ObservableObject
    {
        public ObservableCollection<ProductionViewModel> PlanetViewModels { get; set; }

        public PlanetProductionViewModel()
        {
            PlanetViewModels = new ObservableCollection<ProductionViewModel>
        {
            new PlanetViewModel1(),
            new PlanetViewModel2(),
            new PlanetViewModel3(),
            new PlanetViewModel4()
        };
        }
    }
}