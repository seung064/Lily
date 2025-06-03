using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Lily.Models;
using Project_Lily.ViewModels;

public class Planet1ProductionViewModel : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Gold.png", ProductionName = "진토금", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTimer = TimeSpan.FromSeconds(20), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Gold.png", ProductionName = "황금덩어리", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(90), ProductionTimer = TimeSpan.FromSeconds(30), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Gold.png", ProductionName = "금괴", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(120), ProductionTimer = TimeSpan.FromSeconds(45), Quantity = 0 });
    }
}

// 행성2: 암철석 계열  
public class Planet2ProductionViewModel : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Iron.png", ProductionName = "암철석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(45), ProductionTimer = TimeSpan.FromSeconds(15), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Iron.png", ProductionName = "철괴", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTimer = TimeSpan.FromSeconds(25), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Iron.png", ProductionName = "강철", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(90), ProductionTimer = TimeSpan.FromSeconds(40), Quantity = 0 });
    }
}

// 행성3: 석기정 계열
public class Planet3ProductionViewModel : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Stone.png", ProductionName = "석기정", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(90), ProductionTimer = TimeSpan.FromSeconds(25), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Stone.png", ProductionName = "대리석", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(120), ProductionTimer = TimeSpan.FromSeconds(35), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Stone.png", ProductionName = "화강암", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(150), ProductionTimer = TimeSpan.FromSeconds(50), Quantity = 0 });
    }
}

// 행성4: 약초 계열
public class Planet4ProductionViewModel : ProductionViewModel
{
    protected override void InitializeItems()
    {
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Herb.png", ProductionName = "약초", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(30), ProductionTimer = TimeSpan.FromSeconds(8), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Herb.png", ProductionName = "고급약초", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(45), ProductionTimer = TimeSpan.FromSeconds(12), Quantity = 0 });
        ProductionItems.Add(new ProductionItem { ProductionImagePath = "/Assets/Herb.png", ProductionName = "영약초", ProductionProgress = 30, ExpirationTime = TimeSpan.FromSeconds(60), ProductionTimer = TimeSpan.FromSeconds(18), Quantity = 0 });
    }
}

