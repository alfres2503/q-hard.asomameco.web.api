using AutoFixture;
using FluentAssertions;
using Moq;
using src.Models;
using src.Repository;
using src.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Services
{
    public class RoleServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IRoleService _service;
        private readonly Mock<IRoleRepository> _repositoryMock;

        public RoleServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IRoleRepository>>();
            _service = new RoleService(_repositoryMock.Object);
        }

        //Test Get All
        [Fact]
        public async Task GetAll_ShouldReturnData_WhenDataFound()
        {
            //Arrange
            var rolesMock = new List<Role>()
            {
                new Role { Id = 1, Description = "Admin"},
                new Role { Id = 2,Description = "Member"}
            };

            _repositoryMock.Setup(repo => repo.GetAll(1,10)).ReturnsAsync(rolesMock);

            //Act
            var result = await _service.GetAll(1, 10).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Role>>();
            result.Should().BeEquivalentTo(rolesMock);
            _repositoryMock.Verify(repo => repo.GetAll(1, 10), Times.Once);
        }


        [Fact]
        public async Task GetAll_ShouldReturnEmpty_WhenNoDataFound()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAll(1, 10)).ReturnsAsync(Enumerable.Empty<Role>());

            // Act
            var result = await _service.GetAll(1, 10).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Role>>();
            result.Should().BeEmpty();
            _repositoryMock.Verify(repo => repo.GetAll(1, 10), Times.Once);
        }


        // Test Get By ID
        [Fact]
        public async Task GetByID_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var eventMock = new Role()
            {
                Description = "Prueba",
            };

            _repositoryMock.Setup(repo => repo.GetByID(eventMock.Id)).ReturnsAsync(eventMock);

            // Act
            var result = await _service.GetByID(eventMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Role>();
            result.Should().BeEquivalentTo(eventMock);
            _repositoryMock.Verify(repo => repo.GetByID(eventMock.Id), Times.Once);
        }


        [Fact]
        public async Task GetByID_ShouldReturnNull_WhenNoDataFound()
        {
            // Arrange
            var memberMock = _fixture.Create<Role>();

            _repositoryMock.Setup(repo => repo.GetByID(memberMock.Id)).ReturnsAsync((Role)null);

            // Act
            var result = await _service.GetByID(memberMock.Id).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.GetByID(memberMock.Id), Times.Once);
        }


        // Test Create
        [Fact]
        public async Task Create_ShouldReturnData_WhenDataCreated()
        {
            // Arrange
            var eventMock = new Role()
            {
                Description = "Prueba"
            };

            _repositoryMock.Setup(repo => repo.Create(eventMock)).ReturnsAsync(eventMock);

            // Act
            var result = await _service.Create(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Role>();
            result.Should().BeEquivalentTo(eventMock);
            _repositoryMock.Verify(repo => repo.Create(eventMock), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnNull_WhenDataNotCreated()
        {
            // Arrange
            var eventMock = new Role()
            {
                Description = "Prueba"
            };

            _repositoryMock.Setup(repo => repo.Create(eventMock)).ReturnsAsync((Role)null);

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
            var eventMock = new Role()
            {
                Description = "Prueba"
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
            var eventMock = new Role()
            {
                Description = "Prueba"
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
            var eventMock = new Role()
            {
                Description = "Prueba"
            };

            var response = new Role()
            {
                Description = "Prueba"
            
        };

            _repositoryMock.Setup(repo => repo.Update(eventMock)).ReturnsAsync(response);

            // Act
            var result = await _service.Update(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Role>();
            result.Should().BeEquivalentTo(response);
            _repositoryMock.Verify(repo => repo.Update(eventMock), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenDataNotUpdated()
        {
            // Arrange
            var eventMock = new Role()
            {
                Description = "Prueba"
            };

            _repositoryMock.Setup(repo => repo.Update(eventMock)).ReturnsAsync((Role)null);

            // Act
            var result = await _service.Update(eventMock).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.Update(eventMock), Times.Once);
        }
    }
}
