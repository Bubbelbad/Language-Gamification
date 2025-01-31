using Application.Commands.QuestionCommands.Delete;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;

namespace Test.QuestionTests
{
    [TestFixture]
    [Category("Question/UnitTest/DeleteQuestion")]
    public class DeleteQuestionTest
    {
        private IGenericRepository<Question, int> _repositoryMock;
        private DeleteQuestionCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = A.Fake<IGenericRepository<Question, int>>();
            _handler = new DeleteQuestionCommandHandler(_repositoryMock);
        }

        [Test]
        public async Task DeletingQuestion_ShouldCallRepositoryDelete_Method()
        {
            var questionId = 1;
            var command = new DeleteQuestionCommand(questionId);

            A.CallTo(() => _repositoryMock.DeleteAsync(questionId))
                .Returns(Task.FromResult(true));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.DeleteAsync(questionId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeletingAnswer_ShouldDeleteAnswer_Successfully()
        {
            var questionId = 1;
            var command = new DeleteQuestionCommand(questionId);

            A.CallTo(() => _repositoryMock.DeleteAsync(questionId))
                .Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(true, result.Data);
        }

        [Test]
        public async Task DeletingQuestion_WithInvalidId_ShouldReturnFailure()
        {
            var invalidQuestionId = 0;
            var command = new DeleteQuestionCommand(invalidQuestionId);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess, "Handler should return failure for invalid question ID.");
        }
    }
}
