using Application.Commands.ChallengeCommands.Add;
using Application.Commands.UserChallengeCommands.Add;
using Application.Dtos.ChallengeDtos;
using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.UserChallengeTest
{
    [TestFixture]
    [Category("UserChallenge/UnitTest/AddUserChallenge")]
    public class AddUserChallengeTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AddUserChallengeCommandHandler _handler;
        private IGenericRepository<UserChallenge, int> _repositoryMock;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddUserChallengeDto, UserChallenge>();
                cfg.CreateMap<UserChallenge, GetUserChallengeDto>();
            });

            _mapper = config.CreateMapper();
            _repositoryMock = A.Fake<IGenericRepository<UserChallenge, int>>();
            _handler = new AddUserChallengeCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task AddUserChallenge_ShouldCallRepositoryAdd_Method()
        {
            var newUserChallengeDto = new AddUserChallengeDto 
            {
                UserId = "",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow
            };
            var command = new AddUserChallengeCommand(newUserChallengeDto);

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.AddAsync(A<UserChallenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task AddUserChallenge_ShouldAddUserChallenge_Successfully()
        {
            var newUserChallengeDto = new AddUserChallengeDto 
            {
                UserId = "New User Challenge",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow
            };
            var command = new AddUserChallengeCommand(newUserChallengeDto);

            var createdUserChallenge = new UserChallenge 
            {
                Id = 1,
                UserId = "New User Challenge",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = newUserChallengeDto.CompletedAt
            };

            var expectedDto = new GetUserChallengeDto
            {
                UserId = "New User Challenge",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = newUserChallengeDto.CompletedAt
            };

            A.CallTo(() => _repositoryMock.AddAsync(A<UserChallenge>.Ignored))
                .Returns(Task.FromResult(createdUserChallenge));

            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual(expectedDto.UserId, result.Data.UserId);
            Assert.AreEqual(expectedDto.ChallengeId, result.Data.ChallengeId);
            Assert.AreEqual(expectedDto.Score, result.Data.Score);
            Assert.AreEqual(expectedDto.CompletedAt, result.Data.CompletedAt);


            A.CallTo(() => _repositoryMock.AddAsync(A<UserChallenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddCommandValidator_ShouldFail_WhenTitleIsEmpty()
        {
            var newUserChallengeDto = new AddUserChallengeDto 
            {
                UserId = "",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow
            };

            var command = new AddUserChallengeCommand(newUserChallengeDto);
            var validator = new AddUserChallengeCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("UserId is required.", result.Errors[0].ErrorMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
