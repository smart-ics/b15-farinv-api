// using Farinv.Domain.InventoryContext.StokFeature;
// using Farinv.Infrastructure.InventoryContext.StokFeature;
// using FluentAssertions;
// using Moq;
//
// namespace Farinv.Test.InventoryContext.StokFeature;
//
// public class StokRepoTests
// {
//     private readonly Mock<IStokDal> _stokDalMock;
//     private readonly Mock<IStokLayerDal> _stokLayerDalMock;
//     private readonly Mock<IStokBukuDal> _stokBukuDalMock;
//     private readonly StokRepo _repository;
//     public StokRepoTests()
//     {
//         _stokDalMock = new Mock<IStokDal>();
//         _stokLayerDalMock = new Mock<IStokLayerDal>();
//         _stokBukuDalMock = new Mock<IStokBukuDal>();
//         _repository = new StokRepo(_stokDalMock.Object, _stokLayerDalMock.Object, _stokBukuDalMock.Object);
//     }
//     
//     [Fact]
//     public void UT1_GivenNonExistingStok_WhenLoadEntity_ThenNoneIsReturned()
//     {
//         // Arrange
//         var key = CreateTestKey();
//         _stokDalMock
//             .Setup(x => x.GetData(key))
//             .Returns((StokDto)null!);
//         // Act
//         var result = _repository.LoadEntity(key);
//         // Assert
//         result.HasValue.Should().BeFalse();
//         result.Match(
//             onSome: _ => Assert.Fail("Expected None but got Some"),
//             onNone: () => { });
//     }
//     
//     [Fact]
//     public void UT2_GivenStokWithSingleLayer_WhenLoadEntity_ThenStokWithSingleLayerIsReturned()
//     {
//         // Arrange
//         var key = CreateTestKey();
//         var stokDto = CreateTestStokDto();
//         var stokLayerDto = CreateTestStokLayerDto();
//         var stokBukuDto = CreateTestStokBukuDto();
//         
//         _stokDalMock
//             .Setup(x => x.GetData(key))
//             .Returns(stokDto);
//         _stokLayerDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokLayerDto> { stokLayerDto });
//         _stokBukuDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokBukuDto> { stokBukuDto });
//         // Act
//         var result = _repository.LoadEntity(key);
//         // Assert
//         result.HasValue.Should().BeTrue();
//         result.Match(
//             onSome: model => 
//             {
//                 model.ListLayer.Count().Should().Be(1);
//                 var layer = model.ListLayer.First();
//                 layer.ListBuku.Count().Should().Be(1);
//             },
//             onNone: () => Assert.Fail("Expected Some but got None"));
//     }
//     
//     [Fact]
//     public void UT3_GivenStokWithMultipleLayer_WhenLoadEntity_ThenStokWithMultipleLayerIsReturned()
//     {
//         // Arrange
//         var key = CreateTestKey();
//         var stokDto = CreateTestStokDto();
//         var stokLayerDto1 = CreateTestStokLayerDto();
//         var stokLayerDto2 = CreateTestStokLayerDto() with { StokLayerId = "B" };
//         var stokBukuDto1 = CreateTestStokBukuDto();
//         var stokBukuDto2 = CreateTestStokBukuDto() with { StokLayerId = "B", NoUrut = 2 };
//         
//         _stokDalMock
//             .Setup(x => x.GetData(key))
//             .Returns(stokDto);
//         _stokLayerDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokLayerDto> { stokLayerDto1, stokLayerDto2 });
//         _stokBukuDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokBukuDto> { stokBukuDto1, stokBukuDto2 });
//         // Act
//         var result = _repository.LoadEntity(key);
//         // Assert
//         result.HasValue.Should().BeTrue();
//         result.Match(
//             onSome: model => 
//             {
//                 model.ListLayer.Count().Should().Be(2);
//                 model.ListLayer.First().ListBuku.Count().Should().Be(1);
//                 model.ListLayer.Last().ListBuku.Count().Should().Be(1);
//             },
//             onNone: () => Assert.Fail("Expected Some but got None"));
//     }
//     
//     [Fact]
//     public void UT4_GivenStokWithNoLayer_WhenLoadEntity_ThenStokWithEmptyLayerIsReturned()
//     {
//         // Arrange
//         var key = CreateTestKey();
//         var stokDto = CreateTestStokDto();
//         
//         _stokDalMock
//             .Setup(x => x.GetData(key))
//             .Returns(stokDto);
//         _stokLayerDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokLayerDto>());
//         _stokBukuDalMock
//             .Setup(x => x.ListData(key))
//             .Returns(new List<StokBukuDto>());
//         // Act
//         var result = _repository.LoadEntity(key);
//         // Assert
//         result.HasValue.Should().BeTrue();
//         result.Match(
//             onSome: model => 
//             {
//                 model.ListLayer.Count().Should().Be(0);
//             },
//             onNone: () => Assert.Fail("Expected Some but got None"));
//     }
//     
//     private static StokDto CreateTestStokDto()
//         => StokDto.FromModel(StokModel.Default); 
//     
//     private static StokLayerDto CreateTestStokLayerDto()
//         => new StokLayerDto(
//             StokLayerId: "A",
//             BrgId: "B",
//             LayananId: "C",
//             TrsReffInId: "D",
//             TrsReffInDate: DateTime.Now,
//             PurchaseId: "E",
//             ReceiveId: "F",
//             ExpDate: DateTime.Now.AddDays(30),
//             BatchNo: "G",
//             QtyIn: 1,
//             QtySisa: 1,
//             Hpp: 1000,
//             BrgName: "H",
//             LayananName: "I");
//     
//     private static StokBukuDto CreateTestStokBukuDto()
//         => new StokBukuDto(
//             StokBukuId: "A",
//             StokLayerId: "A",
//             NoUrut: 1,
//             BrgId: "B",
//             LayananId: "C",
//             TrsReffId: "D",
//             TrsReffDate: DateTime.Now,
//             PurchaseId: "E",
//             ReceiveId: "F",
//             ExpDate: DateTime.Now.AddDays(30),
//             BatchNo: "G",
//             UseCase: "H",
//             QtyIn: 1,
//             QtyOut: 0,
//             Hpp: 1000,
//             EntryDate: DateTime.Now,
//             BrgName: "I",
//             LayananName: "J");
//     
//     private static IStokKey CreateTestKey()
//         => StokModel.Key("B", "C"); 
// }