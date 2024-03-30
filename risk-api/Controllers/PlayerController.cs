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

    [HttpGet("{Id}")]
    public ActionResult<GetPlayerDto> GetSinglePlayer([FromRoute] Guid Id)
    {
        return Ok(_playerProcess.GetSinglePlayer(Id));
    }

    [HttpPost]
    public ActionResult<CreatePlayerDto> CreatePlayer([FromBody] CreatePlayerRequest request)
    {
        var dto = _playerProcess.CreatePlayer(request);
        return Created("not used", dto);
    }
}