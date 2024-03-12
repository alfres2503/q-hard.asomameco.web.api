using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using src.Controllers;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Controllers
{
    public class RolesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRoleService> _serviceMock;
        private readonly RolesController _controller;

        public RolesControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IRoleService>>();
            _controller = new RolesController(_serviceMock.Object);
        }

        // Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var rolesMock = new List<Role>()
            {
                new Role { Id = 1, Description = "Admin"},
                new Role { Id = 2,Description = "Member"}
            };

            _serviceMock.Setup(service => service.GetAll(1, 10)).ReturnsAsync(rolesMock);

            // Act
            var result = await _controller.GetRoles().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Role>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(rolesMock.GetType());
            _serviceMock.Verify(service => service.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll(1, 10)).ReturnsAsync((List<Role>)null);

            // Act
            var result = await _controller.GetRoles().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(service => service.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll(1, 10)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetRoles().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.GetAll(1, 10), Times.Once);
        }

        // Test Get By ID
        [Fact]
        public async Task GetByID_ShouldReturnOkResponse_WhenValidInput()
        {
            // Arrange
            var roleMock = new Role()
            {
                Description = "Prueba",
            };

            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(roleMock);

            // Act
            var result = await _controller.GetRoleById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Role>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(roleMock.GetType());
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        [Fact]
        public async Task GetByID_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            Role response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetRoleById(id).ConfigureAwait(false);

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
            var result = await _controller.GetRoleById(id).ConfigureAwait(false);

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
            var result = await _controller.GetRoleById(id).ConfigureAwait(false);

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
            var result = await _controller.GetRoleById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedResponse_WhenValidInput()
        {
            // Arrange
            var request = new Role()
            {
               Description = "Prueba",
            };

            var response = new Role()
            {
                Description = "Prueba",
            };

            _serviceMock.Setup(service => service.Create(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateRole(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Role>>();
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = new Role()
            {
                Description = "Prueba",
            };

            _controller.ModelState.AddModelError("Description", "Name is required");

            // Act
            var result = await _controller.CreateRole(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInputIsNull()
        {
            // Arrange
            Role request = null;

            // Act
            var result = await _controller.CreateRole(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new Role()
            {
                Description = "Prueba",
            };


            _serviceMock.Setup(service => service.Create(request)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.CreateRole(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }
    }
}
