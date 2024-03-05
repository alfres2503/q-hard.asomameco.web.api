using AutoFixture;
using FluentAssertions;
using Moq;
using src.Models;
using src.Repository;
using src.Services;
using src.Utils;
using System.Globalization;

namespace test.Services
{
    public class EventServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IEventService _service;
        private readonly Mock<IEventRepository> _repositoryMock;

        public EventServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IEventRepository>>();
            _service = new EventService(_repositoryMock.Object);
        }

        // Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var eventsMock = new List<Event>() {
                new Event { Id = 1, IdMember = 2, Name = "Reunión de la junta directiva", Description = "Reunión de la junta directiva para discutir temas importantes", Date = DateOnly.Parse("October 21, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(7, 23, 11), Place = "Sala de juntas" },
                new Event { Id = 2, IdMember = 3, Name = "Fiesta Mariachi", Description = "Fiestón para esuchar Luis Miguel", Date = DateOnly.Parse("December 23, 2022", CultureInfo.InvariantCulture), Time = new TimeOnly(10, 30, 11), Place = "Sala de juntas" },
                new Event { Id = 3, IdMember = 2, Name = "Reunión porqué amo a mi esposita", Description = "Reunión recapacitativa", Date = DateOnly.Parse("January 15, 2023", CultureInfo.InvariantCulture), Time = new TimeOnly(14, 00, 11), Place = "Sala de juntas" }
                };

            _repositoryMock.Setup(repo => repo.GetAll(1, 10)).ReturnsAsync(eventsMock);

            // Act
            var result = await _service.GetAll(1, 10).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Event>>();
            result.Should().BeEquivalentTo(eventsMock);
            _repositoryMock.Verify(repo => repo.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmpty_WhenNoDataFound()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAll(1, 10)).ReturnsAsync(Enumerable.Empty<Event>());

            // Act
            var result = await _service.GetAll(1, 10).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Event>>();
            result.Should().BeEmpty();
            _repositoryMock.Verify(repo => repo.GetAll(1, 10), Times.Once);
        }

        // Test Get By ID
        [Fact]
        public async Task GetByID_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var eventMock = new Event() { 
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1, 
                Name = "asfa", 
                Place = "asfdas", 
                Time = new TimeOnly() };

            _repositoryMock.Setup(repo => repo.GetByID(eventMock.Id)).ReturnsAsync(eventMock);

            // Act
            var result = await _service.GetByID(eventMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Event>();
            result.Should().BeEquivalentTo(eventMock);
            _repositoryMock.Verify(repo => repo.GetByID(eventMock.Id), Times.Once);
        }

        [Fact]
        public async Task GetByID_ShouldReturnNull_WhenNoDataFound()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.GetByID(eventMock.Id)).ReturnsAsync((Event)null);

            // Act
            var result = await _service.GetByID(eventMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.GetByID(eventMock.Id), Times.Once);
        }

        // Test Create
        [Fact]
        public async Task Create_ShouldReturnData_WhenDataCreated()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.Create(eventMock)).ReturnsAsync(eventMock);

            // Act
            var result = await _service.Create(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Event>();
            result.Should().BeEquivalentTo(eventMock);
            _repositoryMock.Verify(repo => repo.Create(eventMock), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnNull_WhenDataNotCreated()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.Create(eventMock)).ReturnsAsync((Event)null);

            // Act
            var result = await _service.Create(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.Create(eventMock), Times.Once);
        }

        // Test Delete 
        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenDataDeleted()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };
            _repositoryMock.Setup(repo => repo.Delete(eventMock.Id)).ReturnsAsync(true);

            // Act
            var result = await _service.Delete(eventMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(repo => repo.Delete(eventMock.Id), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenDataNotDeleted()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.Delete(eventMock.Id)).ReturnsAsync(false);

            // Act
            var result = await _service.Delete(eventMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().BeFalse();
            _repositoryMock.Verify(repo => repo.Delete(eventMock.Id), Times.Once);
        }

        // Test Update
        [Fact]
        public async Task Update_ShouldReturnUpdatedData_WhenDataUpdated()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };
            var response = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.Update(eventMock)).ReturnsAsync(response);

            // Act
            var result = await _service.Update(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Event>();
            result.Should().BeEquivalentTo(response);
            _repositoryMock.Verify(repo => repo.Update(eventMock), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenDataNotUpdated()
        {
            // Arrange
            var eventMock = new Event()
            {
                Date = new DateOnly(),
                Description = "safsaf",
                IdMember = 1,
                Name = "asfa",
                Place = "asfdas",
                Time = new TimeOnly()
            };

            _repositoryMock.Setup(repo => repo.Update(eventMock)).ReturnsAsync((Event)null);

            // Act
            var result = await _service.Update(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.Update(eventMock), Times.Once);
        }
    }
}
