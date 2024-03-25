using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Controllers;
using src.Models;
using src.Services;
using src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Controllers
{
    public class CateringServiceControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICateringServiceService> _serviceMock;
        private readonly CateringServicesController _controller;

        public CateringServiceControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<ICateringServiceService>>();
            _controller = new CateringServicesController(_serviceMock.Object);
        }

        // Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var cateringserviceMock = _fixture.CreateMany<CateringService>(50).ToList();

            _serviceMock.Setup(service => service.GetAll(1, 10,null,null)).ReturnsAsync(cateringserviceMock);

            // Act
            var result = await _controller.GetCateringServices().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<PagedResult<CateringService>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull();
              
            _serviceMock.Verify(service => service.GetAll(1, 10, null, null), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll(1, 10, null, null)).ReturnsAsync((List<CateringService>)null);

            // Act
            var result = await _controller.GetCateringServices().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(service => service.GetAll(1, 10, null, null), Times.Once);
        }


        [Fact]
        public async Task GetAll_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll(1, 10, null, null)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetCateringServices().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.GetAll(1, 10, null, null), Times.Once);
        }

        // Test Get By ID
        [Fact]
        public async Task GetByID_ShouldReturnOkResponse_WhenValidInput()
        {
            // Arrange
            var cateringserviceMock = _fixture.Create<CateringService>();
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(cateringserviceMock);

            // Act
            var result = await _controller.GetCateringServiceById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<CateringService>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(cateringserviceMock.GetType());
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        [Fact]
        public async Task GetByID_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            CateringService response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetCateringServiceById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        [Fact]
        public async Task GetByID_ShouldReturnBadRequest_WhenInputIsZero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = await _controller.GetCateringServiceById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.GetByID(id), Times.Never);
        }

        [Fact]
        public async Task GetByID_ShouldReturnBadRequest_WhenInputIsLessThanZero()
        {
            // Arrange
            var id = -1;

            // Act
            var result = await _controller.GetCateringServiceById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.GetByID(id), Times.Never);
        }

        //test exception
        [Fact]
        public async Task GetByID_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetCateringServiceById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        // Test Create
        [Fact]
        public async Task Create_ShouldReturnCreatedResponse_WhenValidInput()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            var response = _fixture.Create<CateringService>();
            _serviceMock.Setup(service => service.Create(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateCateringService(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<CateringService>>();
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            _serviceMock.Verify(service => service.Create(request), Times.Once);    
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.CreateCateringService(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInputIsNull()
        {
            // Arrange
            CateringService request = null;

            // Act
            var result = await _controller.CreateCateringService(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            _serviceMock.Setup(service => service.Create(request)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.CreateCateringService(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }

        // Test Update
        [Fact]
        public async Task Update_ShouldReturnAcceptedAtAction_WhenValidInput()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            var response = _fixture.Create<CateringService>();
            _serviceMock.Setup(service => service.Update(request.Id, request)).ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateCateringService(request.Id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<CateringService>>();
            result.Result.Should().BeOfType<AcceptedAtActionResult>();

            _serviceMock.Verify(service => service.Update(request.Id, request), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.UpdateCateringService(request.Id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Update(request.Id, request), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenInputIsNull()
        {
            // Arrange
            CateringService request = null;

            // Act
            var result = await _controller.UpdateCateringService(request.Id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Update(request.Id, request), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = _fixture.Create<CateringService>();
            _serviceMock.Setup(service => service.Update(request.Id, request)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.UpdateCateringService(request.Id, request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.Update(request.Id, request), Times.Once);
        }

        // Test Change State
        [Fact]
        public async Task ChangeState_ShouldReturnAcceptedAtAction_WhenValidInput()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var response = _fixture.Create<CateringService>();
            _serviceMock.Setup(service => service.ChangeState(id)).ReturnsAsync(response);

            // Act
            var result = await _controller.ChangeState(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<CateringService>>();
            result.Result.Should().BeOfType<AcceptedAtActionResult>();

            _serviceMock.Verify(service => service.ChangeState(id), Times.Once);
        }

        [Fact]
        public async Task ChangeState_ShouldReturnBadRequest_WhenInputIsZero()
        {
            // Arrange
            var id = 0;

            // Act
            var result = await _controller.ChangeState(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.ChangeState(id), Times.Never);
        }

        [Fact]
        public async Task ChangeState_ShouldReturnBadRequest_WhenInputIsLessThanZero()
        {
            // Arrange
            var id = -1;

            // Act
            var result = await _controller.ChangeState(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.ChangeState(id), Times.Never);
        }

        [Fact]
        public async Task ChangeState_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.ChangeState(id)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.ChangeState(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.ChangeState(id), Times.Once);
        }

    }
}
