using Application.Commands.AnswerCommands.Add;
using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.AnswerTests
{
    [TestFixture]
    [Category("Answer/UnitTests/AddAnswer")]
    public class AddAnswerTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AddCommandHandler _handler;
        private IGenericRepository<Answer, int> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAnswerDto, Answer>();
                cfg.CreateMap<Answer, GetAnswerDto>();
            });

            _mapper = config.CreateMapper();

            _repositoryMock = A.Fake<IGenericRepository<Answer, int>>();

            _handler = new AddCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task AddAnswer_ShouldCallRepositoryAdd_Method()
        {
            // Arrange
            var newAnswerDto = new AddAnswerDto
            {
                QuestionId = 1,
                Text = "Test Answer",
                IsCorrect = true
            };

            var command = new AddCommand(newAnswerDto);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.AddAsync(A<Answer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task AddAnswer_ShouldAddAnswer_Successfully()
        {
            // Arrange
            var newAnswerDto = new AddAnswerDto
            {
                QuestionId = 1,
                Text = "Test Answer",
                IsCorrect = true
            };

            var command = new AddCommand(newAnswerDto);

            var createdAnswer = new Answer
            {
                Id = 1,
                QuestionId = 1,
                Text = "Test Answer",
                IsCorrect = true
            };

            A.CallTo(() => _repositoryMock.AddAsync(A<Answer>.Ignored))
                .Returns(Task.FromResult(createdAnswer));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual("Test Answer", result.Data.Text);

            A.CallTo(() => _repositoryMock.AddAsync(A<Answer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddCommandValidator_ShouldFail_WhenTextIsEmpty()
        {
            // Arrange
            var newAnswerDto = new AddAnswerDto
            {
                QuestionId = 1,
                Text = "",
                IsCorrect = true
            };
            var command = new AddCommand(newAnswerDto);
            var validator = new AddCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Text is required", result.Errors[0].ErrorMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
