using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.BrgContext.StandardFeature;

namespace Farinv.Infrastructure.BrgContext.BrgFeature;

public record BrgStandardFormulariumDto(
    string BrgId,
    string FormulariumId,
    string FormulariumName
)
{
    public static BrgStandardFormulariumDto FromModel(string brgId, BrgStandardFormulariumType model)
    {
        return new BrgStandardFormulariumDto(
            BrgId: brgId,
            FormulariumId: model.Formularium.FormulariumId,
            FormulariumName: model.Formularium.FormulariumName
        );
    }

    public BrgStandardFormulariumType ToModel()
    {
        var formularium = FormulariumType.Create(FormulariumId, FormulariumName);
        return BrgStandardFormulariumType.Create(BrgId, formularium);
    }
}