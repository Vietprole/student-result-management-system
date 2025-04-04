using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Student_Result_Management_System.Controllers;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Utils;
using System.Security.Claims;

namespace StudentResultManagementSystem.Tests
{
    public class BaiKiemTraControllerTest
    {
        private readonly Mock<IBaiKiemTraService> _mockBaiKiemTraService;
        private readonly Mock<ICauHoiService> _mockCauHoiService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly BaiKiemTraController _controller;

        public BaiKiemTraControllerTest()
        {
            // Initialize mocks
            _mockBaiKiemTraService = new Mock<IBaiKiemTraService>();
            _mockCauHoiService = new Mock<ICauHoiService>();
            _mockTokenService = new Mock<ITokenService>();

            // Create controller with mocked dependencies
            _controller = new BaiKiemTraController(
                _mockBaiKiemTraService.Object,
                _mockCauHoiService.Object,
                _mockTokenService.Object);

            // Setup HttpContext for authorization tests
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Bearer test-token";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public async Task GetAll_WithNoParameters_ReturnsOkResultWithAllBaiKiemTras()
        {
            // Arrange
            var expectedBaiKiemTras = new List<BaiKiemTraDTO>
            {
                new BaiKiemTraDTO { Id = 1, Loai = "QT", TrongSo = 0.3m },
                new BaiKiemTraDTO { Id = 2, Loai = "GK", TrongSo = 0.3m },
                new BaiKiemTraDTO { Id = 3, Loai = "CK", TrongSo = 0.4m }
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetAllBaiKiemTrasAsync())
                .ReturnsAsync(expectedBaiKiemTras);

            // Act
            var result = await _controller.GetAll(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BaiKiemTraDTO>>(okResult.Value);
            Assert.Equal(3, returnValue.Count);
            Assert.Equal(expectedBaiKiemTras, returnValue);
        }

        [Fact]
        public async Task GetAll_WithLopHocPhanId_ReturnsOkResultWithFilteredBaiKiemTras()
        {
            // Arrange
            int lopHocPhanId = 1;
            var expectedBaiKiemTras = new List<BaiKiemTraDTO>
            {
                new BaiKiemTraDTO { Id = 1, Loai = "QT", TrongSo = 0.3m, LopHocPhanId = lopHocPhanId },
                new BaiKiemTraDTO { Id = 2, Loai = "GK", TrongSo = 0.3m, LopHocPhanId = lopHocPhanId }
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTrasByLopHocPhanIdAsync(lopHocPhanId))
                .ReturnsAsync(expectedBaiKiemTras);

            // Act
            var result = await _controller.GetAll(lopHocPhanId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BaiKiemTraDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.All(returnValue, item => Assert.Equal(lopHocPhanId, item.LopHocPhanId));
            Assert.Equal(expectedBaiKiemTras, returnValue);
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResultWithBaiKiemTra()
        {
            // Arrange
            int baiKiemTraId = 1;
            var expectedBaiKiemTra = new BaiKiemTraDTO 
            { 
                Id = baiKiemTraId, 
                Loai = "QT", 
                TrongSo = 0.3m 
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTraByIdAsync(baiKiemTraId))
                .ReturnsAsync(expectedBaiKiemTra);

            // Act
            var result = await _controller.GetById(baiKiemTraId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaiKiemTraDTO>(okResult.Value);
            Assert.Equal(baiKiemTraId, returnValue.Id);
            Assert.Equal(expectedBaiKiemTra.Loai, returnValue.Loai);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTraByIdAsync(invalidId))
                .ReturnsAsync((BaiKiemTraDTO)null);

            // Act
            var result = await _controller.GetById(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateBaiKiemTraDTO
            {
                Loai = "TH",
                TrongSo = 0.2m,
                TrongSoDeXuat = 0.2m,
                LopHocPhanId = 1
            };

            var expectedBaiKiemTra = new BaiKiemTraDTO
            {
                Id = 4,
                Loai = "TH",
                TrongSo = 0.2m,
                LopHocPhanId = 1
            };

            _mockBaiKiemTraService.Setup(service => 
                service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(createDto.Loai, createDto.LopHocPhanId))
                .ReturnsAsync(false);

            _mockBaiKiemTraService.Setup(service => 
                service.CreateBaiKiemTraAsync(createDto))
                .ReturnsAsync(expectedBaiKiemTra);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(BaiKiemTraController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(expectedBaiKiemTra.Id, createdAtActionResult.RouteValues["id"]);
            var returnValue = Assert.IsType<BaiKiemTraDTO>(createdAtActionResult.Value);
            Assert.Equal(expectedBaiKiemTra.Id, returnValue.Id);
            Assert.Equal(expectedBaiKiemTra.Loai, returnValue.Loai);
        }

        [Fact]
        public async Task Create_WithDuplicateLoai_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateBaiKiemTraDTO
            {
                Loai = "QT",
                TrongSo = 0.3m,
                TrongSoDeXuat = 0.3m,
                LopHocPhanId = 1
            };

            _mockBaiKiemTraService.Setup(service => 
                service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(createDto.Loai, createDto.LopHocPhanId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains($"Bài kiểm tra với loại {createDto.Loai} đã tồn tại trong lớp học phần", 
                badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task Update_WithValidIdAndData_ReturnsOkResult()
        {
            // Arrange
            int baiKiemTraId = 1;
            var updateDto = new UpdateBaiKiemTraDTO
            {
                Loai = "QT Updated",
                TrongSo = 0.25m,
                TrongSoDeXuat = 0.25m
            };

            var existingBaiKiemTra = new BaiKiemTraDTO
            {
                Id = baiKiemTraId,
                Loai = "QT",
                TrongSo = 0.3m,
                LopHocPhanId = 1
            };

            var updatedBaiKiemTra = new BaiKiemTraDTO
            {
                Id = baiKiemTraId,
                Loai = updateDto.Loai,
                TrongSo = updateDto.TrongSo ?? 0.25m, // Add null check here
                LopHocPhanId = 1
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTraByIdAsync(baiKiemTraId))
                .ReturnsAsync(existingBaiKiemTra);

            _mockBaiKiemTraService.Setup(service => 
                service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(updateDto.Loai, existingBaiKiemTra.LopHocPhanId))
                .ReturnsAsync(false);

            // Fix for the token service mock
            _mockTokenService.Setup(service =>
                service.GetFullNameAndRole("test-token"))
                .Returns(Task.FromResult(("Test User")));

            _mockBaiKiemTraService.Setup(service => 
                service.UpdateBaiKiemTraAsync(baiKiemTraId, updateDto))
                .ReturnsAsync(updatedBaiKiemTra);

            // Act
            var result = await _controller.Update(baiKiemTraId, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BaiKiemTraDTO>(okResult.Value);
            Assert.Equal(baiKiemTraId, returnValue.Id);
            Assert.Equal(updateDto.Loai, returnValue.Loai);
            Assert.Equal(updateDto.TrongSo, returnValue.TrongSo);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            var updateDto = new UpdateBaiKiemTraDTO
            {
                Loai = "QT Updated",
                TrongSo = 0.25m
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTraByIdAsync(invalidId))
                .ReturnsAsync((BaiKiemTraDTO)null);

            // Act
            var result = await _controller.Update(invalidId, updateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("Bài kiểm tra không tồn tại", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task Update_WithDuplicateLoai_ReturnsBadRequest()
        {
            // Arrange
            int baiKiemTraId = 1;
            var updateDto = new UpdateBaiKiemTraDTO
            {
                Loai = "GK",  // GK already exists in LopHocPhan 1
                TrongSo = 0.3m,
                TrongSoDeXuat = 0.3m
            };

            var existingBaiKiemTra = new BaiKiemTraDTO
            {
                Id = baiKiemTraId,
                Loai = "QT",
                TrongSo = 0.3m,
                LopHocPhanId = 1
            };

            _mockBaiKiemTraService.Setup(service => 
                service.GetBaiKiemTraByIdAsync(baiKiemTraId))
                .ReturnsAsync(existingBaiKiemTra);

            _mockBaiKiemTraService.Setup(service => 
                service.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(updateDto.Loai, existingBaiKiemTra.LopHocPhanId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(baiKiemTraId, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains($"Bài kiểm tra với loại {updateDto.Loai} đã tồn tại trong lớp học phần", 
                badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int baiKiemTraId = 1;

            _mockBaiKiemTraService.Setup(service => 
                service.DeleteBaiKiemTraAsync(baiKiemTraId))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(baiKiemTraId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;

            _mockBaiKiemTraService.Setup(service => 
                service.DeleteBaiKiemTraAsync(invalidId))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateListCauHoi_WithValidData_ReturnsOkResult()
        {
            // Arrange
            int baiKiemTraId = 1;
            var createCauHoiDTOs = new List<CreateCauHoiDTO>
            {
                new CreateCauHoiDTO { Ten = "Question 1", TrongSo = 5, ThangDiem = 10 },
                new CreateCauHoiDTO { Ten = "Question 2", TrongSo = 5, ThangDiem = 10 }
            };

            var expectedCauHoiDTOs = new List<CauHoiDTO>
            {
                new CauHoiDTO { Id = 1, Ten = "Question 1", TrongSo = 5, BaiKiemTraId = baiKiemTraId, ThangDiem = 10 },
                new CauHoiDTO { Id = 2, Ten = "Question 2", TrongSo = 5, BaiKiemTraId = baiKiemTraId, ThangDiem = 10 }
            };

            _mockCauHoiService.Setup(service => 
                service.UpdateListCauHoiAsync(baiKiemTraId, createCauHoiDTOs))
                .ReturnsAsync(expectedCauHoiDTOs);

            // Act
            var result = await _controller.UpdateListCauHoi(baiKiemTraId, createCauHoiDTOs);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CauHoiDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal(expectedCauHoiDTOs, returnValue);
        }

        [Fact]
        public async Task UpdateListCauHoi_WithBusinessException_ReturnsBadRequest()
        {
            // Arrange
            int baiKiemTraId = 1;
            var createCauHoiDTOs = new List<CreateCauHoiDTO>
            {
                new CreateCauHoiDTO { Ten = "Question 1", TrongSo = 5, ThangDiem = 10 }
            };

            string errorMessage = "Business logic error occurred";
            _mockCauHoiService.Setup(service => 
                service.UpdateListCauHoiAsync(baiKiemTraId, createCauHoiDTOs))
                .ThrowsAsync(new BusinessLogicException(errorMessage));

            // Act
            var result = await _controller.UpdateListCauHoi(baiKiemTraId, createCauHoiDTOs);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateListCauHoi_WithNotFoundException_ReturnsNotFound()
        {
            // Arrange
            int baiKiemTraId = 99;
            var createCauHoiDTOs = new List<CreateCauHoiDTO>
            {
                new CreateCauHoiDTO { Ten = "Question 1", TrongSo = 5, ThangDiem = 10 }
            };

            string errorMessage = "Resource not found";
            _mockCauHoiService.Setup(service => 
                service.UpdateListCauHoiAsync(baiKiemTraId, createCauHoiDTOs))
                .ThrowsAsync(new NotFoundException(errorMessage));

            // Act
            var result = await _controller.UpdateListCauHoi(baiKiemTraId, createCauHoiDTOs);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(errorMessage, notFoundResult.Value);
        }
    }
}