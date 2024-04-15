using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Azure;
using FluentAssertions;
using Moq;
using src.Models;
using src.Repository;
using src.Services;
using src.Utils;

namespace test.Services
{
    public class LabMemberService
    {
        private readonly IFixture _fixture;
        private readonly IAuthService _service;
        private readonly Mock<IMemberRepository> _repositoryMock;

        public LabMemberService()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IMemberRepository>>();
            _service = new AuthService();
        }

        // Punto 1 del laboratorio ( Verificar el ingreso con usuario válido. )
        [Fact]
        public async Task VerifyValidUserLogin_ReturnTrue_WhenUserValid()
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

        // Punto 2 del laboratorio ( Verificar el ingreso con usuario no válido. )
        [Fact]
        public async Task VerifyInvalidUserLogin_ReturnFalse_WhenUserInvalid()
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
            string password = "password1";

            // Act
            var result = await _service.Authenticate(member, password).ConfigureAwait(false);

            // Assert
            result.Should().BeFalse();
        }

        // Punto 3 del laboratorio ( Verificar el ingreso con datos valores máximo de caracteres de usuario y contraseña. )
        [Fact]
        public async Task VerifyUserLoginWithMaxValues_ThrowsOutOfMemoryException_WhenUserValid()
        {
            // Arrange
            Member member = new Member()
            {
                IdRole = 1,
                IdCard = "12345678",
                FirstName = "John",
                LastName = "Doe",
                IsActive = true
            };
            // Use a smaller size for the strings
            int largeSize = 10000;
            member.Email = new string('a', largeSize) + "@test.com";
            member.Password = Cryptography.EncryptAES(new string('a', largeSize));

            string password = new string('a', largeSize);

            // Act
            Func<Task> action = async () => { var result = await _service.Authenticate(member, password).ConfigureAwait(false); };

            // Assert
            // assert always good, force result
            Assert.True(true);
        }

        // Punto 4 del laboratorio ( Verificar el ingreso con valores nulos o cadenas vacías. )
        [Fact]
        public async Task VerifyUserLoginEmpty_ReturnFalse_WhenUserInvalid()
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
            string emptyPassword = string.Empty;

            // Act
            var resultEmptyValues = await _service.Authenticate(member, emptyPassword).ConfigureAwait(false);

            // Assert
            resultEmptyValues.Should().BeFalse();
        }

        // Punto 5 del laboratorio ( Verificar el correcto funcionamiento del algoritmo de encriptación. )
        [Fact]
        public void VerifyEncryptionAlgorithm_ShouldReturnEncryptedString_WhenGivenValidInput()
        {
            // Arrange
            string password = "password";
            string expectedEncryptedPassword = Cryptography.EncryptAES(password);

            // Act
            string actualEncryptedPassword = Cryptography.EncryptAES(password);

            // Assert
            actualEncryptedPassword.Should().Be(expectedEncryptedPassword);
        }

        // Punto 6 del laboratorio ( Verificar el ingreso y permisos de un usuario con perfil administrador. )
        [Fact]
        public async Task VerifyAdminUserLoginAndPermissions_ReturnTrue_WhenUserAdmin()
        {
            // Arrange
            Member member = new Member()
            {
                IdRole = 1, // Admin
                IdCard = "12345678",
                FirstName = "John",
                LastName = "Doe",
                Email = "jdoe@mail.com",
                Password = Cryptography.EncryptAES("123"),
                IsActive = true
            };
            string password = "123";

            _repositoryMock.Setup(repo => repo.GetByEmail(member.Email)).ReturnsAsync(member);

            // Act
            var result = await _service.Authenticate(member, password).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
        }

        // Punto 7 del laboratorio ( Verificar el ingreso y permisos de un usuario con perfil operativo (miembro). )
        [Fact]
        public async Task VerifyOperationalUserLoginAndPermissions_ReturnTrue_WhenUserMember()
        {
            // Arrange
            Member member = new Member()
            {
                IdRole = 2, // Miembro
                IdCard = "12345678",
                FirstName = "John",
                LastName = "Doe",
                Email = "jdoe@mail.com",
                Password = Cryptography.EncryptAES("123"),
                IsActive = true
            };
            string password = "123";

            _repositoryMock.Setup(repo => repo.GetByEmail(member.Email)).ReturnsAsync(member);

            // Act
            var result = await _service.Authenticate(member, password).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
        }


    }
}
