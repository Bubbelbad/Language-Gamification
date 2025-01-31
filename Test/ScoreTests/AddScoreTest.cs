using Application.Commands.ChallengeCommands.Add;
using Application.Commands.ScoreCommands.Add;
using Application.Dtos.ChallengeDtos;
using Application.Dtos.ScoreDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.ScoreTests
{
    [TestFixture]
    [Category("Score/UnitTest/AddScore")]
    public class AddScoreTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AddScoreCommandHandler _handler;
        private IGenericRepository<Score, int> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddScoreDto, Score>();
                cfg.CreateMap<Score, GetScoreDto>();
            });

            _mapper = config.CreateMapper();
            _repositoryMock = A.Fake<IGenericRepository<Score, int>>();
            _handler = new AddScoreCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task AddScore_ShouldCallRepositoryAdd_Method()
        {
            var newScoreDto = new AddScoreDto 
            {
                UserId = "",
                ChallengeId = 1,
                Points = 0,
                CompletedAt = DateTime.UtcNow
            };
            var command = new AddScoreCommand(newScoreDto);

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.AddAsync(A<Score>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task AddScore_ShouldAddScore_Successfully()
        {
            var newScoreDto = new AddScoreDto 
            {
                UserId = "",
                ChallengeId = 1,
                Points = 0,
                CompletedAt = DateTime.UtcNow
            };
            var command = new AddScoreCommand(newScoreDto);

            var createdScore = new Score
            {
                Id = 1,
                UserId = "",
                ChallengeId = 1,
                Points = 100,
                CompletedAt = newScoreDto.CompletedAt
            };

            A.CallTo(() => _repositoryMock.AddAsync(A<Score>.Ignored))
                .Returns(Task.FromResult(createdScore));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("", result.Data.UserId);
            Assert.AreEqual(1, result.Data.ChallengeId);
            Assert.AreEqual(100, result.Data.Points);
            Assert.AreEqual(newScoreDto.CompletedAt, result.Data.CompletedAt);


            A.CallTo(() => _repositoryMock.AddAsync(A<Score>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddScore_ShouldThrowException_WhenRepositoryFails()
        {
            var newScoreDto = new AddScoreDto
            {
                UserId = "",
                ChallengeId = 1,
                Points = 100,
                CompletedAt = DateTime.UtcNow
            };
            var command = new AddScoreCommand(newScoreDto);

            A.CallTo(() => _repositoryMock.AddAsync(A<Score>.Ignored))
                .Throws(new Exception("Database failure"));

            Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(command, CancellationToken.None));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

    }
}
