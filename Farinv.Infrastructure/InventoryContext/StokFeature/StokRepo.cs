// using Farinv.Application.InventoryContext.StokFeature;
// using Farinv.Domain.InventoryContext.StokFeature;
// using Nuna.Lib.PatternHelper;
//
// namespace Farinv.Infrastructure.InventoryContext.StokFeature;
//
// public class StokRepo : IStokRepo
// {
//     private readonly IStokDal _stokDal;
//     private readonly IStokLayerDal _stokLayerDal;
//     private readonly IStokBukuDal _stokBukuDal;
//
//     public StokRepo(IStokDal stokDal, IStokLayerDal stokLayerDal, IStokBukuDal stokBukuDal)
//     {
//         _stokDal = stokDal;
//         _stokLayerDal = stokLayerDal;
//         _stokBukuDal = stokBukuDal;
//     }
//
//     public void SaveChanges(StokModel model)
//     {
//         throw new NotImplementedException();
//     }
//
//     public MayBe<StokModel> LoadEntity(IStokKey key)
//     {
//         var brgDto = _stokDal.GetData(key);
//         if (brgDto is null)
//             return MayBe<StokModel>.None;
//         
//         var listLayerDto = _stokLayerDal.ListData(key)?.ToList() ?? [];
//         var listBukuDto = _stokBukuDal.ListData(key)?.ToList() ?? [];
//         
//         var listBukuByLayer = listBukuDto
//             .GroupBy(dto => dto.StokLayerId)
//             .ToDictionary(group => group.Key, group => group.Select(dto => dto.ToModel()).AsEnumerable());
//         
//         var listLayer = listLayerDto
//             .Select(x => x.ToModel(listBukuByLayer.First(y => y.Key == x.StokLayerId).Value)).ToList();
//
//         var stok = brgDto.ToModel(listLayer);
//         return MayBe<StokModel>.Some(stok);
//     }
//
//     public void DeleteEntity(IStokKey key)
//     {
//         throw new NotImplementedException();
//     }
//
//     public IEnumerable<StokBalanceView> ListData()
//     {
//         throw new NotImplementedException();
//     }
// }