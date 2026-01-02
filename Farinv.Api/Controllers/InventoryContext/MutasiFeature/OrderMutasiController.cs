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
    [Route("addItem")]
    public async Task<IActionResult> AddItem(OrderMutasiAddItemCommand cmd)
    {
        await _mediator.Send(cmd);
        return Ok(new JSendOk("Done"));
    }

    [HttpPost]
    [Route("submit")]
    public async Task<IActionResult> Submit(OrderMutasiSubmitCommand cmd)
    {
        await _mediator.Send(cmd);
        return Ok(new JSendOk("Done"));
    }

    [HttpPost]
    [Route("approve")]
    public async Task<IActionResult> Approve(OrderMutasiApproveCommand cmd)
    {
        await _mediator.Send(cmd);
        return Ok(new JSendOk("Done"));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        var query = new OrderMutasiGetQuery(id);
        var response = await _mediator.Send(query);
        return Ok(new JSendOk(response));
    }

    [HttpGet]
    [Route("layananOrder/{id}/draft")]
    public async Task<IActionResult> GetOrderDraft(string id)
    {
        var query = new OrderMutasiDraftStateGetQuery(id);
        var response = await _mediator.Send(query);
        return Ok(new JSendOk(response));
    }
}
