using Farinv.Domain.BrgContext.BrgFeature;
using Farinv.Domain.InventoryContext.MutasiFeature;
using Farinv.Domain.InventoryContext.StokFeature;
using Farinv.Domain.Shared.Helpers.CommonValueObjects;
using Farinv.Infrastructure.InventoryContext.MutasiFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.ValidationHelper;

namespace Farinv.Test.InventoryContext.MutasiFeature;

public class OrderMutasiRepoTest
{
    private readonly Mock<IOrderMutasiDal> _mockHeaderDal = new();
    private readonly Mock<IOrderMutasiBrgDal> _mockDetailDal = new();
    private readonly OrderMutasiRepo _sut;

    public OrderMutasiRepoTest()
    {
        _sut = new OrderMutasiRepo(_mockHeaderDal.Object, _mockDetailDal.Object);
    }

    [Fact]
    public void DeleteEntity_Should_Call_Delete_In_Order()
    {
        // Arrange
        var key = OrderMutasiModel.Key("OM001");

        // Act
        _sut.DeleteEntity(key);

        // Assert
        _mockDetailDal.Verify(x => x.Delete(key), Times.Once);
        _mockHeaderDal.Verify(x => x.Delete(key), Times.Once);
    }

    [Fact]
    public void ListData_Should_Return_Headers()
    {
        // Arrange
        var filter = new Periode(new DateTime(2025, 12, 26));
        var dtos = new List<OrderMutasiDto>
        {
            new OrderMutasiDto(
                OrderMutasiId: "OM001",
                OrderMutasiDate: new DateTime(2025, 12, 26),
                State: OrderMutasiStateEnum.Submitted,
                LayananOrderId: "L001",
                LayananOrderName: "Rawat Inap",
                LayananTujuanId: "L002",
                LayananTujuanName: "Rawat Jalan",
                OrderNote: "Note",
                UserCreateId: "U001",
                TglJamCreate: DateTime.Now,
                UserModifyId: "-",
                TglJamModify: new DateTime(3000, 1, 1),
                UserVoidId: "-",
                TglJamVoid: new DateTime(3000, 1, 1)
            )
        };

        _mockHeaderDal.Setup(x => x.ListData(filter)).Returns(dtos);

        // Act
        var actual = _sut.ListData(filter);

        // Assert
        actual.Should().HaveCount(1);
        actual.First().OrderMutasiId.Should().Be("OM001");
    }

    [Fact]
    public void LoadEntity_Should_Return_Model_If_Exists()
    {
        // Arrange
        var key = OrderMutasiModel.Key("OM001");
        var headerDto = new OrderMutasiDto(
            OrderMutasiId: "OM001",
            OrderMutasiDate: new DateTime(2025, 12, 26),
            State: OrderMutasiStateEnum.Submitted,
            LayananOrderId: "L001",
            LayananOrderName: "Rawat Inap",
            LayananTujuanId: "L002",
            LayananTujuanName: "Rawat Jalan",
            OrderNote: "Note",
            UserCreateId: "U001",
            TglJamCreate: new DateTime(2025, 12, 26),
            UserModifyId: "U002",
            TglJamModify: new DateTime(2025, 12, 26),
            UserVoidId: "U003",
            TglJamVoid: new DateTime(2025, 12, 26)
        );

        var detailDtos = new List<OrderMutasiItemDto>
        {
            new OrderMutasiItemDto(
                OrderMutasiId: "OM001",
                NoUrut: 1,
                BrgId: "B001",
                BrgName: "Obat A",
                Qty: 10,
                SatuanId: "S001",
                SatuanName: "Buah"
            )
        };

        _mockHeaderDal.Setup(x => x.GetData(key)).Returns(headerDto);
        _mockDetailDal.Setup(x => x.ListData(key)).Returns(detailDtos);

        // Act
        var actual = _sut.LoadEntity(key);

        // Assert
        actual.HasValue.Should().BeTrue();
        actual.Value.OrderMutasiId.Should().Be("OM001");
        actual.Value.ListItem.Should().HaveCount(1);
    }

    [Fact]
    public void LoadEntity_Should_Return_None_If_Not_Exists()
    {
        // Arrange
        var key = OrderMutasiModel.Key("OM001");
        _mockHeaderDal.Setup(x => x.GetData(key)).Returns((OrderMutasiDto)null);

        // Act
        var actual = _sut.LoadEntity(key);

        // Assert
        actual.HasValue.Should().BeFalse();
    }

    [Fact]
    public void SaveChanges_Should_Insert_If_New()
    {
        // Arrange
        var model = CreateFakeModel(isNew: true);

        _mockHeaderDal.Setup(x => x.GetData(model)).Returns((OrderMutasiDto)null);

        // Act
        _sut.SaveChanges(model);

        // Assert
        _mockHeaderDal.Verify(x => x.Insert(It.IsAny<OrderMutasiDto>()), Times.Once);
        _mockHeaderDal.Verify(x => x.Update(It.IsAny<OrderMutasiDto>()), Times.Never);
        _mockDetailDal.Verify(x => x.Delete(model), Times.Once);
        _mockDetailDal.Verify(x => x.Insert(It.IsAny<IEnumerable<OrderMutasiItemDto>>()), Times.Once);
    }

    [Fact]
    public void SaveChanges_Should_Update_If_Exists()
    {
        // Arrange
        var model = CreateFakeModel(isNew: false);

        _mockHeaderDal.Setup(x => x.GetData(model)).Returns(new OrderMutasiDto(
            OrderMutasiId: "OM001",
            OrderMutasiDate: new DateTime(2025, 12, 26),
            State: OrderMutasiStateEnum.Submitted,
            LayananOrderId: "L001",
            LayananOrderName: "Rawat Inap",
            LayananTujuanId: "L002",
            LayananTujuanName: "Rawat Jalan",
            OrderNote: "Note",
            UserCreateId: "U001",
            TglJamCreate: new DateTime(2025, 12, 26),
            UserModifyId: "U002",
            TglJamModify: new DateTime(2025, 12, 26),
            UserVoidId: "U003",
            TglJamVoid: new DateTime(2025, 12, 26)
        ));

        // Act
        _sut.SaveChanges(model);

        // Assert
        _mockHeaderDal.Verify(x => x.Update(It.IsAny<OrderMutasiDto>()), Times.Once);
        _mockHeaderDal.Verify(x => x.Insert(It.IsAny<OrderMutasiDto>()), Times.Never);
        _mockDetailDal.Verify(x => x.Delete(model), Times.Once);
        _mockDetailDal.Verify(x => x.Insert(It.IsAny<IEnumerable<OrderMutasiItemDto>>()), Times.Once);
    }

    private OrderMutasiModel CreateFakeModel(bool isNew)
    {
        var brg = new BrgReff("B001", "Obat A");
        var satuan = new SatuanType("S001", "Buah");
        var item = new OrderMutasiItemModel(brg, 10, satuan);
        var audit = AuditTrailType.Create("U001", new DateTime(2025, 12, 26));

        return new OrderMutasiModel(
            isNew ? "-" : "OM001",
            new DateTime(2025, 12, 26),
            OrderMutasiStateEnum.Submitted,
            new LayananReff("L001", "Rawat Inap"),
            new LayananReff("L002", "Rawat Jalan"),
            "Note",
            audit,
            new List<OrderMutasiItemModel> { item }
        );
    }
}