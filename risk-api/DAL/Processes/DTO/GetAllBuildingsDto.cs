using risk_api.Models;

namespace risk_api.DAL.Processes.DTO;

public class GetAllBuildingsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public BuildingType Type { get; set; }
    public int Income { get; set; }
}