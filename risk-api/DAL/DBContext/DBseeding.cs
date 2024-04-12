using risk_api.Models;

namespace risk_api.DAL.DBContext;

public static class DBseeding
{
    public static void Initialize(DatabaseContext context)
    {
        if(context.Territories.Any()) return; //Database already seeded
        
        //Territories
        var territories = new List<Territory>();
        territories.Add(new Territory
        {
            Id = "gl",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "is",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "pt",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ma",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "es",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "tn",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "dz",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "be",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "it",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "by",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "pl",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "jo",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "gr",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "tm",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "kz",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "fi",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "de",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "se",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        territories.Add(new Territory
        {
            Id = "ua",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "il",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "sa",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "iq",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "az",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ir",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ge",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "sy",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "am",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "cy",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        territories.Add(new Territory
        {
            Id = "ie",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "gb",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ch",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "at",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "cz",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "sk",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "hu",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "lt",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "lv",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "md",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        territories.Add(new Territory
        {
            Id = "ro",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "bg",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "al",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ee",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "lb",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ad",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "sm",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "mc",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "lu",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "fr",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        territories.Add(new Territory
        {
            Id = "li",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "nl",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ba",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "si",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "mk",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "hr",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "dk",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "ru",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "mt",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "me",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        territories.Add(new Territory
        {
            Id = "sr",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "tr",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
        });
        territories.Add(new Territory
        {
            Id = "no",
            Color = "#C6C6C6",
            ControlledBy = null,
            TimeConquered = null,
            Troops = 100
            
        });
        
        context.Territories.AddRange(territories);
        context.SaveChanges();

        context.Buildings.Add(new Building
        {
            Id = 1,
            Name = "Bank",
            Cost = 100,
            Type = BuildingType.Cash,
            Income = 10
        });
        context.Buildings.Add(new Building
        {
            Id = 2,
            Name = "Barrack",
            Cost = 100,
            Type = BuildingType.Troop,
            Income = 10
        });
        context.SaveChanges();
    }
}