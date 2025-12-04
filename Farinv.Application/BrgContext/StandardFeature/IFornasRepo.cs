using Farinv.Domain.BrgContext.StandardFeature;
using Nuna.Lib.DataAccessHelper;

namespace Farinv.Application.BrgContext.StandardFeature;

public interface IFornasRepo :
    ISaveChange<FornasType>,
    ILoadEntity<FornasType, IFornasKey>,
    IDeleteEntity<IFornasKey>,
    IListData<FornasType>
{
}