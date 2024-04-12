using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using risk_api.Auth;
using risk_api.DAL.Processes;
using risk_api.DAL.Processes.DTO;

namespace risk_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BuildingController : ControllerBase
{
    private readonly BuildingProcess _buildingProcess;

    public BuildingController(BuildingProcess buildingProcess)
    {
        _buildingProcess = buildingProcess;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<GetAllBuildingsDto>> GetAllBuildings()
    {
        return Ok(_buildingProcess.GetAllBuildings());
    }
    
    [HttpPost("{buildingId}")]
    public ActionResult<PurchaseBuildingDto> PurchaseBuilding([FromRoute] int buildingId)
    {
        var userId = AuthValidator.GetUserIdFromToken(HttpContext.Request);
        var dto = _buildingProcess.PurchaseBuilding(buildingId, userId);
        return Ok(dto);
    }
}