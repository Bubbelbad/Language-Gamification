
using Application.Commands.ChallengeCommands.Add;
using Application.Commands.QuestionCommands.Add;
using Application.Dtos.ChallengeDtos;
using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.QuestionTests
{
    [TestFixture]
    [Category("Question/UnitTest/AddQuestion")]
    public class AddQuestionTest
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AddQuestionCommandHandler _handler;
        private IGenericRepository<Question, int> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddQuestionDto, Question>();
                cfg.CreateMap<Question, GetQuestionDto>();
            });

            _mapper = config.CreateMapper();
            _repositoryMock = A.Fake<IGenericRepository<Question, int>>();
            _handler = new AddQuestionCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task AddQuestion_ShouldCallRepositoryAdd_Method()
        {
            var newQuestionDto = new AddQuestionDto { Text = "new Question"};
            var command = new AddQuestionCommand(newQuestionDto);

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.AddAsync(A<Question>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task AddQuestion_ShouldAddQuestion_Successfully()
        {
            var newQuestionDto = new AddQuestionDto { Text = "New Question" };
            var command = new AddQuestionCommand(newQuestionDto);

            var createdQuestion = new Question { Id = 1, Text = "New Question" };

            A.CallTo(() => _repositoryMock.AddAsync(A<Question>.Ignored))
                .Returns(Task.FromResult(createdQuestion));

            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            Assert.AreEqual("New Question", result.Data.Text);

            A.CallTo(() => _repositoryMock.AddAsync(A<Question>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddCommandValidator_ShouldFail_WhenTextIsEmpty()
        {
            var newQuestionDto = new AddQuestionDto { Text = "" };

            var command = new AddQuestionCommand(newQuestionDto);
            var validator = new AddQuestionCommandValidator();

            var result = validator.Validate(command);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Text is required.", result.Errors[0].ErrorMessage);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
