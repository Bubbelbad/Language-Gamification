
using Application.Commands.AnswerCommands.Update;
using Application.Commands.QuestionCommands.Update;
using Application.Dtos.AnswerDtos;
using Application.Dtos.QuestionDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using System.Reflection.Metadata;

namespace Test.QuestionTests
{
    [TestFixture]
    [Category("Question/UnitTests/UpdateQuestion")]
    public class UpdateQuestionTest
    {
        private IGenericRepository<Question, int> _repositoryMock;
        private IMapper _mapper;
        private UpdateQuestionCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = A.Fake<IGenericRepository<Question, int>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateQuestionDto, Question>();
                cfg.CreateMap<Question, GetQuestionDto>();
            });

            _mapper = config.CreateMapper();
            _handler = new UpdateQuestionCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task UpdatingQuestion_SHouldCallRepositoryUpdate_Method()
        {
            var updateDto = new UpdateQuestionDto
            {
                Id = 1,
                Text = "Update Question",
                ChallengeId = 1
            };

            var command = new UpdateQuestionCommand(updateDto);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Question>.Ignored))
                .Returns(Task.FromResult(new Question()));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Question>.Ignored)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public async Task UpdatingQuestion_ShouldUpdateQuestion_Successfully()
        {
            var updateDto = new UpdateQuestionDto
            {
                Id = 1,
                Text = "Updated Question",
                ChallengeId = 1
            };

            var command = new UpdateQuestionCommand(updateDto);

            var updatedQuestion = new Question
            {
                Id = 1,
                Text = "Updated Question",
                ChallengeId = 1
            };

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Question>.Ignored))
                .Returns(Task.FromResult(updatedQuestion));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Question", result.Data.Text);
        }

        [Test]
        public void UpdateCommandValidator_ShouldFail_WhenTextIsEmpty()
        {
            var invalidCommand = new UpdateQuestionCommand(new UpdateQuestionDto
            {
                Id = 1,
                Text = "",
                ChallengeId = 1
            });

            var validator = new UpdateQuestionCommandValidator();

            var result = validator.Validate(invalidCommand);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Text is required.", result.Errors[0].ErrorMessage);
        }

    }

    
}
