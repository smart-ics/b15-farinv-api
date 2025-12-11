using Ardalis.GuardClauses;
using Farinv.Domain.BrgContext.StandardFeature;

namespace Farinv.Domain.BrgContext.BrgFeature;

public class BrgStandardFormulariumType
{
    #region CREATION
    public BrgStandardFormulariumType(
        string brgId,
        FormulariumType formularium)
    {
        BrgId = brgId;
        Formularium = formularium;
    }

    public static BrgStandardFormulariumType Create(
        string brgId,
        FormulariumType formularium)
    {
        Guard.Against.NullOrWhiteSpace(brgId, nameof(brgId));
        Guard.Against.Null(formularium, nameof(formularium));

        return new BrgStandardFormulariumType(brgId, formularium);
    }

    public static BrgStandardFormulariumType Default => new(
        brgId: "-",
        formularium: FormulariumType.Default);
    #endregion

    #region PROPERTIES
    public string BrgId { get; init; }
    public FormulariumType Formularium { get; init; }
    #endregion
}