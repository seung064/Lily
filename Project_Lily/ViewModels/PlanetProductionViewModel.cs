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


    /*
    public class PlanetProductionViewModel : ObservableObject
    {
        public PlanetViewModel1 Planet1 { get; } = new PlanetViewModel1();
        public PlanetViewModel2 Planet2 { get; } = new PlanetViewModel2();
        public PlanetViewModel3 Planet3 { get; } = new PlanetViewModel3();
        public PlanetViewModel4 Planet4 { get; } = new PlanetViewModel4();

        public PlanetProductionViewMode()
        {
            Planet1 = new ProductionViewModel();  
            Planet2 = new FoodProductionViewModel();   
            Planet3 = new ChemicalProductionViewModel(); 
            Planet4 = new ChemicalProductionViewModel(); 
        }

    }
    
}
public class IntegratedPlanetManagerViewModel : ObservableObject
{
    public ObservableCollection<ProductionViewModel> PlanetViewModels { get; set; }

    public IntegratedPlanetManagerViewModel()
    {
        PlanetViewModels = new ObservableCollection<ProductionViewModel>
        {
            new PlanetViewModel1(),
            new PlanetViewModel2(),
            new PlanetViewModel3(),
            new PlanetViewModel4()
        };
    }*/
}