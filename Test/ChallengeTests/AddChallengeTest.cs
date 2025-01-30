using Application.Commands.AnswerCommands.Add;
using Application.Commands.ChallengeCommands.Add;
using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.ChallengeTests
{
    [TestFixture]
    [Category("Challenge/UnitTest/AddChallenge")]
    public class AddChallengeTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AddChallengeCommandHandler _handler;
        private IGenericRepository<Challenge, int> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddChallengeDto, Challenge>();
                cfg.CreateMap<Challenge, GetChallengeDto>();
            });

            _mapper = config.CreateMapper();
            _repositoryMock = A.Fake<IGenericRepository<Challenge, int>>();
            _handler = new AddChallengeCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task AddChallenge_ShouldCallRepositoryAdd_Method()
        {
            var newChallengeDto = new AddChallengeDto { Title = "New Challenge" };
            var command = new AddChallengeCommand(newChallengeDto);

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.AddAsync(A<Challenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task AddChallenge_ShouldAddChallenge_Successfully()
        {
            var newChallengeDto = new AddChallengeDto { Title = "New Challenge" };
            var command = new AddChallengeCommand(newChallengeDto);

            var createdChallenge = new Challenge { Id = 1, Title = "New Challenge" };

            A.CallTo(() => _repositoryMock.AddAsync(A<Challenge>.Ignored))
                .Returns(Task.FromResult(createdChallenge));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual("New Challenge", result.Data.Title);

            A.CallTo(() => _repositoryMock.AddAsync(A<Challenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddCommandValidator_ShouldFail_WhenTitleIsEmpty()
        {
            var newChallengeDto = new AddChallengeDto { Title = "" };

            var command = new AddChallengeCommand(newChallengeDto);
            var validator = new AddChallengeCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Title is required", result.Errors[0].ErrorMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
