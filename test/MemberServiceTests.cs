using Moq;
using src.Models;
using src.Repository;
using src.Services;
using src.Utils;

namespace test
{
    public class MemberServiceTests
    {
        private readonly IMemberService _memberService;
        private readonly Mock<IMemberRepository> _memberRepositoryMock = new Mock<IMemberRepository>();

        public MemberServiceTests()
        {
            _memberService = new MemberService(_memberRepositoryMock.Object);
        }

        // Test Get All
        // Standard: El nombre del método se compone de: MethodName_Condition_ExpectedOutcome
        [Fact]
        public async Task GetAll_CallsRepo_ReturnsMembers()
        {
            // Arrange
            var members = new List<Member>
            {
                new Member { Id = 1, IdRole = 1, IdCard = "111111111", FirstName = "Tony", LastName = "Ramírez", Email = "tony@limon.com", Password = "mogambos", IsActive = true},
                new Member { Id = 2, IdRole = 1, IdCard = "111111112", FirstName = "Fio", LastName = "Babab", Email = "fio@example.com", Password = "fio123", IsActive = true},
                new Member { Id = 3, IdRole = 1, IdCard = "111111113", FirstName = "Rabo", LastName = "Culate", Email = "rabo_culate@example.com", Password = "rabito123", IsActive = true}
            };

            _memberRepositoryMock.Setup(repository => repository.GetAll()).ReturnsAsync(members); // Setup the mock to return the members

            // Act
            var result = await _memberService.GetAll();

            // Assert
            _memberRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
            Assert.Equal(members, result);
        }

        // Test get all when things go wrong
        [Fact]
        public async Task GetAll_CallsRepo_ReturnsEmptyList()
        {
            // Arrange
            var members = new List<Member>();

            _memberRepositoryMock.Setup(repository => repository.GetAll()).ReturnsAsync(members);

            // Act
            var result = await _memberService.GetAll();

            // Assert
            _memberRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAll_RepoThrowsException_ThrowsException()
        {
            // Arrange
            _memberRepositoryMock.Setup(repository => repository.GetAll()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _memberService.GetAll());
        }

        // Test Get By ID
        [Fact]
        public async Task GetByID_CallsRepo_ReturnsMember()
        {
            // Arrange
            var member = new Member { Id = 1, IdRole = 1, IdCard = "111111111", FirstName = "Tony", LastName = "Ramírez", Email = "tony@limon.com", Password = "mogambos", IsActive = true };

            _memberRepositoryMock.Setup(repository => repository.GetByID(1)).ReturnsAsync(member);

            // Act 
            var result = await _memberService.GetByID(1);

            // Assert
            _memberRepositoryMock.Verify(repository => repository.GetByID(1), Times.Once);
            Assert.Equal(member, result);
        }

        // Test Add member to database
        [Fact]
        public async Task Add_AddsMember_ReturnsMember()
        {
            // Arrange
            var member = new Member { Id = 1, IdRole = 1, IdCard = "111111111", FirstName = "Tony", LastName = "Ramírez", Email = "tony@limon.com", Password = "mogambos", IsActive = true };

            _memberRepositoryMock.Setup(repository => repository.Add(member)).ReturnsAsync(member);

            // Act 
            var result = await _memberService.Add(member);

            // Assert
            _memberRepositoryMock.Verify(repository => repository.Add(member), Times.Once);
            Assert.Equal(member, result);
        }

        // Test Add member to database
        [Fact]
        public async Task Add_AddsIncompleteMember_ReturnsNull()
        {
            // Arrange
            var member = new Member { IdCard = "111111111"};

            // Act
            var result = await _memberService.Add(member);
            _memberRepositoryMock.Verify(repository => repository.Add(It.IsAny<Member>()), Times.Once);

            // Assert
            Assert.Null(result);
        }
    }
}