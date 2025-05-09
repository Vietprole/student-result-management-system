// using Microsoft.EntityFrameworkCore;
// using Moq;
// using Moq.EntityFrameworkCore;
// using Student_Result_Management_System.Data;
// using Student_Result_Management_System.DTOs.BaiKiemTra;
// using Student_Result_Management_System.Interfaces;
// using Student_Result_Management_System.Models;
// using Student_Result_Management_System.Services;
// using Student_Result_Management_System.Utils;

// namespace StudentResultManagementSystem.Tests
// {
//     public class BaiKiemTraServiceTests
//     {
//         private readonly Mock<ApplicationDBContext> _mockContext;
//         private readonly Mock<ICauHoiService> _mockCauHoiService;
//         private readonly BaiKiemTraService _service;
//         private readonly List<BaiKiemTra> _baiKiemTras;
//         private readonly List<LopHocPhan> _lopHocPhans;

//         public BaiKiemTraServiceTests()
//         {
//             _mockContext = new Mock<ApplicationDBContext>(new DbContextOptions<ApplicationDBContext>());

//             // Initialize test data
//             _lopHocPhans = new List<LopHocPhan>
//             {
//                 new() {
//                     Id = 1,
//                     TenLopHocPhan = "Test Class 1",
//                     BaiKiemTras = []
//                 }
//             };

//             _baiKiemTras = new List<BaiKiemTra>
//             {
//                 new BaiKiemTra
//                 {
//                     Id = 1,
//                     Loai = "QT",
//                     TrongSo = 0.3m,
//                     TrongSoDeXuat = 0.3m,
//                     LopHocPhanId = 1,
//                     CauHois = []
//                 },
//                 new BaiKiemTra
//                 {
//                     Id = 2,
//                     Loai = "Test BKT 2",
//                     Loai = "GK",
//                     TrongSo = 0.3f,
//                     TrongSoDeXuat = 0.3f,
//                     LopHocPhanId = 1,
//                     CauHois = new List<CauHoi>()
//                 },
//                 new BaiKiemTra
//                 {
//                     Id = 3,
//                     Loai = "Test BKT 3",
//                     Loai = "CK",
//                     TrongSo = 0.4f,
//                     TrongSoDeXuat = 0.4f,
//                     LopHocPhanId = 2,
//                     CauHois = new List<CauHoi>()
//                 }
//             };

//             // Link BaiKiemTras to the LopHocPhan
//             _lopHocPhans[0].BaiKiemTras.Add(_baiKiemTras[0]);
//             _lopHocPhans[0].BaiKiemTras.Add(_baiKiemTras[1]);

//             // Setup DbSets
//             _mockContext.Setup(c => c.BaiKiemTras).ReturnsDbSet(_baiKiemTras);
//             _mockContext.Setup(c => c.LopHocPhans).ReturnsDbSet(_lopHocPhans);

//             _service = new BaiKiemTraService(_mockContext.Object, _mockCauHoiService.Object);
//         }

//         [Fact]
//         public async Task GetAllBaiKiemTrasAsync_ReturnsAllBaiKiemTras()
//         {
//             // Act
//             var result = await _service.GetAllBaiKiemTrasAsync();

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(3, result.Count);
//             Assert.Equal("Test BKT 1", result[0].Loai);
//         }

//         [Fact]
//         public async Task GetBaiKiemTrasByLopHocPhanIdAsync_ReturnsFilteredBaiKiemTras()
//         {
//             // Act
//             var result = await _service.GetBaiKiemTrasByLopHocPhanIdAsync(1);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count);
//             Assert.All(result, item => Assert.Equal(1, item.LopHocPhanId));
//         }

//         [Fact]
//         public async Task GetBaiKiemTraByIdAsync_WithValidId_ReturnsBaiKiemTra()
//         {
//             // Act
//             var result = await _service.GetBaiKiemTraByIdAsync(1);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(1, result.Id);
//             Assert.Equal("Test BKT 1", result.Loai);
//         }

//         [Fact]
//         public async Task GetBaiKiemTraByIdAsync_WithInvalidId_ReturnsNull()
//         {
//             // Act
//             var result = await _service.GetBaiKiemTraByIdAsync(99);

//             // Assert
//             Assert.Null(result);
//         }

//         [Fact]
//         public async Task CreateBaiKiemTraAsync_AddsNewBaiKiemTra()
//         {
//             // Arrange
//             var createDto = new CreateBaiKiemTraDTO
//             {
//                 Loai = "New BKT",
//                 Loai = "KT",
//                 TrongSo = 0.2f,
//                 TrongSoDeXuat = 0.2f,
//                 LopHocPhanId = 1
//             };

//             _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(1)
//                 .Callback(() => _baiKiemTras.Add(new BaiKiemTra { Id = 4, Loai = createDto.Loai }));

//             // Act
//             var result = await _service.CreateBaiKiemTraAsync(createDto);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal("New BKT", result.Loai);
//             _mockContext.Verify(c => c.BaiKiemTras.AddAsync(It.IsAny<BaiKiemTra>(), It.IsAny<CancellationToken>()), Times.Once);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//         }

//         [Fact]
//         public async Task UpdateBaiKiemTraAsync_WithValidId_UpdatesBaiKiemTra()
//         {
//             // Arrange
//             var updateDto = new UpdateBaiKiemTraDTO
//             {
//                 Loai = "Updated BKT",
//                 Loai = "QT",
//                 TrongSo = 0.25f,
//                 TrongSoDeXuat = 0.25f
//             };

//             var baiKiemTraToUpdate = new BaiKiemTra { Id = 1, Loai = "Test BKT 1" };
//             _mockContext.Setup(c => c.BaiKiemTras.FindAsync(1)).ReturnsAsync(baiKiemTraToUpdate);

//             // Act
//             var result = await _service.UpdateBaiKiemTraAsync(1, updateDto);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal("Updated BKT", result.Loai);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//         }

//         [Fact]
//         public async Task UpdateBaiKiemTraAsync_WithInvalidId_ReturnsNull()
//         {
//             // Arrange
//             var updateDto = new UpdateBaiKiemTraDTO
//             {
//                 Loai = "Updated BKT"
//             };

//             _mockContext.Setup(c => c.BaiKiemTras.FindAsync(99)).ReturnsAsync((BaiKiemTra)null);

//             // Act
//             var result = await _service.UpdateBaiKiemTraAsync(99, updateDto);

//             // Assert
//             Assert.Null(result);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
//         }

//         [Fact]
//         public async Task DeleteBaiKiemTraAsync_WithValidId_DeletesBaiKiemTra()
//         {
//             // Arrange
//             var baiKiemTraToDelete = new BaiKiemTra { Id = 1, Loai = "Test BKT 1" };
//             _mockContext.Setup(c => c.BaiKiemTras.FindAsync(1)).ReturnsAsync(baiKiemTraToDelete);

//             // Act
//             var result = await _service.DeleteBaiKiemTraAsync(1);

//             // Assert
//             Assert.True(result);
//             _mockContext.Verify(c => c.BaiKiemTras.Remove(baiKiemTraToDelete), Times.Once);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//         }

//         [Fact]
//         public async Task DeleteBaiKiemTraAsync_WithInvalidId_ReturnsFalse()
//         {
//             // Arrange
//             _mockContext.Setup(c => c.BaiKiemTras.FindAsync(99)).ReturnsAsync((BaiKiemTra)null);

//             // Act
//             var result = await _service.DeleteBaiKiemTraAsync(99);

//             // Assert
//             Assert.False(result);
//             _mockContext.Verify(c => c.BaiKiemTras.Remove(It.IsAny<BaiKiemTra>()), Times.Never);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
//         }

//         [Fact]
//         public async Task CheckDuplicateBaiKiemTraLoaiInLopHocPhan_WithDuplicate_ReturnsTrue()
//         {
//             // Act
//             var result = await _service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan("QT", 1);

//             // Assert
//             Assert.True(result);
//         }

//         [Fact]
//         public async Task CheckDuplicateBaiKiemTraLoaiInLopHocPhan_WithoutDuplicate_ReturnsFalse()
//         {
//             // Act
//             var result = await _service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan("TH", 1);

//             // Assert
//             Assert.False(result);
//         }

//         [Fact]
//         public async Task CheckDuplicateBaiKiemTraLoaiInLopHocPhan_WithNullLoai_ReturnsFalse()
//         {
//             // Act
//             var result = await _service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(null, 1);

//             // Assert
//             Assert.False(result);
//         }

//         [Fact]
//         public async Task UpdateCongThucDiem_UpdatesExistingAndAddsNewBaiKiemTra()
//         {
//             // Arrange
//             var createDtos = new List<CreateBaiKiemTraDTO>
//             {
//                 new CreateBaiKiemTraDTO
//                 {
//                     Loai = "Updated QT",
//                     Loai = "QT",
//                     TrongSo = 0.25f,
//                     TrongSoDeXuat = 0.25f
//                 },
//                 new CreateBaiKiemTraDTO
//                 {
//                     Loai = "New TH",
//                     Loai = "TH",
//                     TrongSo = 0.15f,
//                     TrongSoDeXuat = 0.15f
//                 }
//             };

//             // Act
//             var result = await _service.UpdateCongThucDiem(1, createDtos);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count);
//             _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//         }

//         [Fact]
//         public async Task UpdateCongThucDiem_WithNonExistentLopHocPhan_ThrowsNotFoundException()
//         {
//             // Arrange
//             _mockContext.Setup(c => c.LopHocPhans.Include(l => l.BaiKiemTras))
//                 .ReturnsDbSet(new List<LopHocPhan>());

//             var createDtos = new List<CreateBaiKiemTraDTO>
//             {
//                 new CreateBaiKiemTraDTO { Loai = "QT", TrongSo = 0.3f }
//             };

//             // Act & Assert
//             await Assert.ThrowsAsync<NotFoundException>(() =>
//                 _service.UpdateCongThucDiem(99, createDtos));
//         }

//         [Fact]
//         public async Task UpdateCongThucDiem_RemovesBaiKiemTraNotInList()
//         {
//             // Arrange
//             var createDtos = new List<CreateBaiKiemTraDTO>
//             {
//                 new CreateBaiKiemTraDTO { Loai = "QT", TrongSo = 0.3f }
//             };

//             // Act
//             var result = await _service.UpdateCongThucDiem(1, createDtos);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Single(result);
//             Assert.Equal("QT", result[0].Loai);
//             _mockContext.Verify(c => c.BaiKiemTras.Remove(It.IsAny<BaiKiemTra>()), Times.Once);
//         }
//     }
// }