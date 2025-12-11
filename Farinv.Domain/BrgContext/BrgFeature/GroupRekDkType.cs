using Ardalis.GuardClauses;

namespace Farinv.Domain.BrgContext.BrgFeature;

public record GroupRekDkType : IGroupRekDkKey
{
    #region CREATION
    public GroupRekDkType(string groupRekDkId, string groupRekDkName)
    {
        Guard.Against.NullOrWhiteSpace(groupRekDkId);
        Guard.Against.NullOrWhiteSpace(groupRekDkName);

        GroupRekDkId = groupRekDkId;
        GroupRekDkName = groupRekDkName;
    }

    public static GroupRekDkType Create(string groupRekDkId, string groupRekDkName)
    {
        return new GroupRekDkType(groupRekDkId, groupRekDkName);
    }

    public static GroupRekDkType Default => new("-", "-");

    public static IGroupRekDkKey Key(string id) => Default with { GroupRekDkId = id };
    #endregion

    #region PROPERTIES
    public string GroupRekDkId { get; init; }
    public string GroupRekDkName { get; init; }
    #endregion
}

public interface IGroupRekDkKey
{
    string GroupRekDkId { get; }
}
