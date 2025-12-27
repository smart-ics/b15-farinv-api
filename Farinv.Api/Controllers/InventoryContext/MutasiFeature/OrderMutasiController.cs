using Farinv.Application.InventoryContext.MutasiFeature.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace Farinv.Api.Controllers.InventoryContext.MutasiFeature;

[Route("api/[controller]")]
[ApiController]
public class OrderMutasiController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderMutasiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("AddItem")]
    public async Task<IActionResult> AddItem(OrderMutasiAddItemCommand cmd)
    {
        await _mediator.Send(cmd);
        return Ok(new JSendOk("Done"));
    }
}
