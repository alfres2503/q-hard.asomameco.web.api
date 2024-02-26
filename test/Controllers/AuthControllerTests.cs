using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
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
    public class AuthControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IMemberService> _memberServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _fixture = new Fixture();
            _authServiceMock = _fixture.Freeze<Mock<IAuthService>>();
            _memberServiceMock = _fixture.Freeze<Mock<IMemberService>>();

            var inMemorySettings = new Dictionary<string, string> {
                {"Logging:LogLevel:Default", "Information"},
                {"Logging:LogLevel:Microsoft.AspNetCore", "Warning"},
                {"AllowedHosts", "*"},
                {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=asomameco_db;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"},
                {"Jwt:Key", "3D1C24BBDB4F428767FD26E4F769C71F41A1B0C4A9C5CB4624EDB16F3470D0C0"},
                {"Jwt:Issuer", "http://localhost:7220/"},
                {"Jwt:Audience", "http://localhost:3000/"},
                {"Jwt:Subject", "baseWebApiSubject"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new AuthController(
                _memberServiceMock.Object, 
                configuration,
                _authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOkResponse_WhenCredentialsAreValid()
        {
            // Arrange
            Member memberMock = _fixture.Create<Member>();


            var credentials = new { email = memberMock.Email, password = memberMock.Password };
            string jsonCredentials = JsonConvert.SerializeObject(credentials);

            _memberServiceMock.Setup(service => service.GetByEmail(credentials.email)).ReturnsAsync(memberMock);
            _authServiceMock.Setup(service => service.Authenticate(memberMock, credentials.password)).ReturnsAsync(true);

            // Act
            var result = await _controller.Login(jsonCredentials).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();


            _memberServiceMock.Verify(service => service.GetByEmail(credentials.email), Times.Once);
            _authServiceMock.Verify(service => service.Authenticate(memberMock, credentials.password), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenEmailIsNotRegistered()
        {
            // Arrange
            var credentials = new { email = "fakemail", password = "fakepass" };
            string jsonCredentials = JsonConvert.SerializeObject(credentials);

            _memberServiceMock.Setup(service => service.GetByEmail(credentials.email)).ReturnsAsync((Member)null);

            // Act
            var result = await _controller.Login(jsonCredentials).ConfigureAwait(false);

            result.Should().NotBeNull();
            result.Should().BeOfType<UnauthorizedObjectResult>();
            _memberServiceMock.Verify(
                service => service.GetByEmail(credentials.email), 
                               Times.Once);
            _authServiceMock.Verify(
                service => service.Authenticate(It.IsAny<Member>(), It.IsAny<string>()), 
                               Times.Never);
        }
    }
}
