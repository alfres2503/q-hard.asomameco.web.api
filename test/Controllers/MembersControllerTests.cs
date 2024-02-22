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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Controllers
{
    public class MembersControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMemberService> _serviceMock;
        private readonly MembersController _controller;

        public MembersControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IMemberService>>();
            _controller = new MembersController(_serviceMock.Object);
        }

        // Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var membersMock = _fixture.CreateMany<Member>(3).ToList();

            _serviceMock.Setup(service => service.GetAll()).ReturnsAsync(membersMock);

            // Act
            var result = await _controller.GetMembers().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Member>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(membersMock.GetType());
            _serviceMock.Verify(service => service.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll()).ReturnsAsync((List<Member>)null);

            // Act
            var result = await _controller.GetMembers().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(service => service.GetAll(), Times.Once);
        }


        [Fact]
        public async Task GetAll_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.GetMembers().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.GetAll(), Times.Once);
        }

        // Test Get By ID
        [Fact]
        public async Task GetByID_ShouldReturnOkResponse_WhenValidInput()
        {
            // Arrange
            var memberMock = _fixture.Create<Member>();
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(memberMock);

            // Act
            var result = await _controller.GetMemberById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Member>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(memberMock.GetType());
            _serviceMock.Verify(service => service.GetByID(id), Times.Once);
        }

        [Fact]
        public async Task GetByID_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            Member response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetMemberById(id).ConfigureAwait(false);

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
            var result = await _controller.GetMemberById(id).ConfigureAwait(false);

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
            var result = await _controller.GetMemberById(id).ConfigureAwait(false);

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
            var result = await _controller.GetMemberById(id).ConfigureAwait(false);

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
            var request = _fixture.Create<Member>();
            var response = _fixture.Create<Member>();
            _serviceMock.Setup(service => service.Create(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateMember(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Member>>();
            result.Result.Should().BeOfType<CreatedAtRouteResult>();

            _serviceMock.Verify(service => service.Create(request), Times.Once);    
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = _fixture.Create<Member>();
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.CreateMember(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInputIsNull()
        {
            // Arrange
            Member request = null;

            // Act
            var result = await _controller.CreateMember(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = _fixture.Create<Member>();
            _serviceMock.Setup(service => service.Create(request)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.CreateMember(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }
    }
}
