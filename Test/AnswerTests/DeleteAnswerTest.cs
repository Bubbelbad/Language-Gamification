using Application.Commands.AnswerCommands.Delete;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.AnswerTests
{
    [TestFixture]
    [Category("Answer/UnitTests/DeleteAnswer")]
    public class DeleteAnswerTest
    {
        private IGenericRepository<Answer, int> _repositoryMock;
        private DeleteCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = A.Fake<IGenericRepository<Answer, int>>();
            _handler = new DeleteCommandHandler(_repositoryMock);
        }

        [Test]
        public async Task DeletingAnswer_ShouldCallRepositoryDelete_Method()
        {
            var answerId = 1;
            var command = new DeleteCommand(answerId);

            A.CallTo(() => _repositoryMock.DeleteAsync(answerId))
                .Returns(Task.FromResult(true));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.DeleteAsync(answerId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeletingAnswer_ShouldDeleteAnswer_Successfully()
        {
            var answerId = 1;
            var command = new DeleteCommand(answerId);

            A.CallTo(() => _repositoryMock.DeleteAsync(answerId))
                .Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(true, result.Data);
        }

        [Test]
        public void DeleteCommandValidator_ShouldFail_WhenAnswerIsInvalid()
        {
            var invalidCommand = new DeleteCommand(0);
            var validator = new DeleteCommandValidator();

            var result = validator.Validate(invalidCommand);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Answer ID must be a positive number.", result.Errors[0].ErrorMessage);
        }
    }
}
