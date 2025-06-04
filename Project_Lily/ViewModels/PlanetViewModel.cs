using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Project_Lily.Models;
using Project_Lily.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;

// Theranos
public class PlanetViewModel1 : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "암철석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "진토금", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Theranos.png", ProductionName = "석기정", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
    }
}

// Aquarius 
public class PlanetViewModel2 : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "심해석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "청류정수", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Aquarius.png", ProductionName = "바다수정", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
    }
}

// Silphy
public class PlanetViewModel3 : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Silphy.png", ProductionName = "풍정석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Silphy.png", ProductionName = "회오리 플라스크", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Silphy.png", ProductionName = "공명결정", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
    }
}

// Infarna
public class PlanetViewModel4 : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Inferna.png", ProductionName = "화염저왕", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(5), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Inferna.png", ProductionName = "용석탄", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(10), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Inferna.png", ProductionName = "발화진주", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTime = TimeSpan.FromSeconds(15), Quantity = 0 });
    }
}

public class PlanetProductionViewModel : ObservableObject
{
    public ObservableCollection<ProductionViewModel> PlanetProductionViewModels { get; set; }


    public PlanetProductionViewModel()
    {
        PlanetProductionViewModels = new ObservableCollection<ProductionViewModel>
        {
            new PlanetViewModel1(),
            new PlanetViewModel2(),
            new PlanetViewModel3(),
            new PlanetViewModel4()
        };
    }
}
