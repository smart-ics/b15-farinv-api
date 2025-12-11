using Farinv.Domain.BrgContext.BrgFeature;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Application.BrgContext.BrgFeature;

public interface IBrgRepo :
    ISaveChange<BrgType>,
    ILoadEntity<BrgType, IBrgKey>,
    IDeleteEntity<IBrgKey>,
    IListData<BrgType>
{
}