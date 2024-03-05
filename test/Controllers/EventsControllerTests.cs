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
    public class EventsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEventService> _serviceMock;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IEventService>>();
            _controller = new EventsController(_serviceMock.Object);
        }

        // Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var membersMock = new List<Event>() {
                new Event { Id = 1, IdMember = 2, Name = "Reunión de la junta directiva", Description = "Reunión de la junta directiva para discutir temas importantes", Date = DateOnly.Parse("October 21, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(7, 23, 11), Place = "Sala de juntas" },
                new Event { Id = 2, IdMember = 3, Name = "Fiesta Mariachi", Description = "Fiestón para esuchar Luis Miguel", Date = DateOnly.Parse("December 23, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(10, 30, 11), Place = "Sala de juntas" },
                new Event { Id = 3, IdMember = 2, Name = "Reunión porqué amo a mi esposita", Description = "Reunión recapacitativa", Date = DateOnly.Parse("January 15, 2023", CultureInfo.InvariantCulture), Time = new TimeOnly(14, 00, 11), Place = "Sala de juntas" }
                };

            _serviceMock.Setup(service => service.GetAll(1, 10)).ReturnsAsync(membersMock);

            // Act
            var result = await _controller.GetEvents().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Event>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(membersMock.GetType());
            _serviceMock.Verify(service => service.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoDataFound()
        {
            // Arrange
            _serviceMock.Setup(service => service.GetAll(1, 10)).ReturnsAsync((List<Event>)null);

            // Act
            var result = await _controller.GetEvents().ConfigureAwait(false);

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
            var result = await _controller.GetEvents().ConfigureAwait(false);

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
            var memberMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "chiquillos",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(memberMock);

            // Act
            var result = await _controller.GetEventById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Event>>();
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
            Event response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(service => service.GetByID(id)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetEventById(id).ConfigureAwait(false);

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
            var result = await _controller.GetEventById(id).ConfigureAwait(false);

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
            var result = await _controller.GetEventById(id).ConfigureAwait(false);

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
            var result = await _controller.GetEventById(id).ConfigureAwait(false);

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
            var request = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "chiquillos",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            var response = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "chiquillos",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _serviceMock.Setup(service => service.Create(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateEvent(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Event>>();
            result.Result.Should().BeOfType<CreatedAtActionResult>();

            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "chiquillos",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.CreateEvent(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenInputIsNull()
        {
            // Arrange
            Event request = null;

            // Act
            var result = await _controller.CreateEvent(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            _serviceMock.Verify(service => service.Create(request), Times.Never);
        }

        [Fact]
        public async Task Create_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var request = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "chiquillos",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _serviceMock.Setup(service => service.Create(request)).ThrowsAsync(new Exception("Some error"));

            // Act
            var result = await _controller.CreateEvent(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<ObjectResult>();
            result.Result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(service => service.Create(request), Times.Once);
        }
    }
}
