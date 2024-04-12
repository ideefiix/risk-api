using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using risk_api.Auth;
using risk_api.Controllers.Requests;
using risk_api.DAL.Processes;

namespace risk_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TerritoryController : ControllerBase
{
    private readonly TerritoryProcess _territoryProcess;

    public TerritoryController(TerritoryProcess territoryProcess)
    {
        _territoryProcess = territoryProcess;
    }

    [HttpGet]
    public ActionResult GetAllTerritories()
    {
        var territories = _territoryProcess.GetAllTerritories();
        return Ok(territories);
    }

    [HttpPost("attack/{territoryId}")]
    public ActionResult AttackTerritory([FromBody] AttackTerritoryRequest request)
    {
        //Auth
        var authenticated = AuthValidator.UserHasId(HttpContext.Request, request.AttackingPlayerId.ToString());
        if (!authenticated) return Unauthorized();
        
        var dto = _territoryProcess.AttackTerritory(request);
        return Ok(dto);
    }
    
    [HttpPost("reinforce/{territoryId}")]
    public ActionResult ReinforceTerritory([FromBody] ReinforceTerritoryRequest request)
    {
        //Auth
        var authenticated = AuthValidator.UserHasId(HttpContext.Request, request.ReinforcingPlayerId.ToString());
        if (!authenticated) return Unauthorized();
        
        var dto = _territoryProcess.ReinforceTerritory(request);
        return Ok(dto);
    }
}