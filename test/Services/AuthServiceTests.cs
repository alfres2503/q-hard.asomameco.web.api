using AutoFixture;
using FluentAssertions;
using Moq;
using src.Models;
using src.Repository;
using src.Services;
using src.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Services
{
    public class AuthServiceTests
    {
        private readonly IFixture _fixture;
        private readonly IAuthService _service;
        private readonly Mock<IMemberRepository> _repositoryMock;

        public AuthServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IMemberRepository>>();
            _service = new AuthService();
        }

        // Test Authenticate

        [Fact]
        public async Task Authenticate_ShouldReturnTrue_WhenPasswordMatch()
        {
            // Arrange
            Member member = new Member()
            {
                IdRole = 1,
                IdCard = "12345678",
                FirstName = "John",
                LastName = "Doe",
                Email = "jdoe@mail.com",
                Password = Cryptography.EncryptAES("password"),
                IsActive = true
            };
            string password = "password";

            // Act
            var result = await _service.Authenticate(member, password).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Authenticate_ShouldReturnFalse_WhenPasswordNotMatch()
        {
            // Arrange
            Member member = new Member()
            {
                IdRole = 1,
                IdCard = "12345678",
                FirstName = "John",
                LastName = "Doe",
                Email = "jdoe@mail.com",
                Password = Cryptography.EncryptAES("password"),
                IsActive = true
            };
            string password = "wrongpassword";

            // Act
            var result = await _service.Authenticate(member, password).ConfigureAwait(false);

            // Assert
            result.Should().BeFalse();
        }
    }
}
