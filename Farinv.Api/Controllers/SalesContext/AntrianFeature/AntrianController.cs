using Farinv.Application.SalesContext.AntrianFeature.UsesCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace Farinv.Api.Controllers.SalesContext.AntrianFeature;

[Route("api/[controller]")]
[ApiController]
public class AntrianController : ControllerBase
{
    private readonly IMediator _mediator;

    public AntrianController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAntrian(QueAddAntrianCmd cmd)
    {
        await _mediator.Send(cmd);
        return Ok(new JSendOk("Done"));
    }
}
