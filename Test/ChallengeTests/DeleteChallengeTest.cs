using Application.Commands.ChallengeCommands.Delete;
using Application.Commands.UserChallengeCommands.Delete;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;

namespace Test.ChallengeTests
{
    [TestFixture]
    [Category("Challenge/UnitTests/DeleteChallenge")]
    public class DeleteChallengeTest
    {
        private IGenericRepository<Challenge, int> _repositoryMock;
        private DeleteChallengeCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = A.Fake<IGenericRepository<Challenge, int>>();
            _handler = new DeleteChallengeCommandHandler(_repositoryMock);
        }

        [Test]
        public async Task DeletingAnswer_ShouldCallRepositoryDelete_Method()
        {
            var challengeId = 1;
            var command = new DeleteChallengeCommand(challengeId);

            A.CallTo(() => _repositoryMock.DeleteAsync(challengeId))
                .Returns(Task.FromResult(true));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.DeleteAsync(challengeId)).MustHaveHappenedOnceExactly();
        }
    }
}
