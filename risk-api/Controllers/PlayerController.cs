using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using risk_api.Auth;
using risk_api.Controllers.Requests;
using risk_api.DAL.Processes;
using risk_api.DAL.Processes.DTO;

namespace risk_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerProcess _playerProcess;

    public PlayerController(PlayerProcess playerProcess)
    {
        _playerProcess = playerProcess;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<GetAllPlayersDto>> GetAllPlayers()
    {
        return Ok(_playerProcess.GetAllPlayers());
    }
    
    [HttpGet("{userId}")]
    public ActionResult<GetPlayerDto> GetSinglePlayer([FromRoute] Guid userId)
    {
        //Auth
        var authenticated = AuthValidator.UserHasId(HttpContext.Request, userId.ToString());
        if (!authenticated) return Unauthorized();
        
        return Ok(_playerProcess.GetSinglePlayer(userId));
    }
    
    [AllowAnonymous]
    [HttpPost]
    public ActionResult<CreatePlayerDto> CreatePlayer([FromBody] CreatePlayerRequest request)
    {
        var dto = _playerProcess.CreatePlayer(request);
        return Created("not used", dto);
    }
    
    [HttpPut("color/{userId}")]
    public ActionResult<GetPlayerDto> UpdateColor([FromRoute] Guid userId)
    {
        //Auth
        var authenticated = AuthValidator.UserHasId(HttpContext.Request, userId.ToString());
        if (!authenticated) return Unauthorized();
        
        return Ok(_playerProcess.ChangeColor(userId));
    }
}