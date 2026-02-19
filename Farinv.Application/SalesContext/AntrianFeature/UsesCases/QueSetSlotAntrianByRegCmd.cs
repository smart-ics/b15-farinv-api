using Farinv.Domain.SalesContext.AntrianFeature;
using MediatR;

namespace Farinv.Application.SalesContext.AntrianFeature.UsesCases;

public record QueSetSlotAntrianByRegCmd(int NoAntrian, string RegId, int ServicePoint, string JamMulai)
    : IRequest<AntrianSetSlotByRegResponse>, IRegKey;

public record AntrianSetSlotByRegResponse(int NoAntrian);

public class AntrianCreateByRegHandler : IRequestHandler<QueSetSlotAntrianByRegCmd, AntrianSetSlotByRegResponse>
{
    private readonly IAntrianRepo _antrianRepo;

    public AntrianCreateByRegHandler(IAntrianRepo antrianRepo)
    {
        _antrianRepo = antrianRepo;
    }

    public Task<AntrianSetSlotByRegResponse> Handle(QueSetSlotAntrianByRegCmd request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}