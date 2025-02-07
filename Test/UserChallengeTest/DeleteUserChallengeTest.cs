
using Application.Commands.ChallengeCommands.Delete;
using Application.Commands.UserChallengeCommands.Delete;
using Application.Interfaces;
using Domain.Entities;
using FakeItEasy;

namespace Test.UserChallengeTest
{
    [TestFixture]
    [Category("UserChallenge/UnitTests/DeleteUserChallenge")]
    public class DeleteUserChallengeTest
    {
        private IGenericRepository<UserChallenge, int> _repositoryMock;
        private DeleteUserChallengeCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<UserChallenge, int>>();
            _handler = new DeleteUserChallengeCommandHandler(_repositoryMock);
        }

        [Test]
        public async Task DeletingUserChallenge_ShouldCallRepositoryDelete_Method()
        {
            var userChallengeId = 1;
            var command = new DeleteUserChallengeCommand(userChallengeId);

            A.CallTo(() => _repositoryMock.DeleteAsync(userChallengeId))
                .Returns(Task.FromResult(true));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.DeleteAsync(userChallengeId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task DeletingUSerChallenge_ShouldDeleteAnswer_Successfully()
        {
            var userChallengeId = 1;
            var command = new DeleteUserChallengeCommand(userChallengeId);

            A.CallTo(() => _repositoryMock.DeleteAsync(userChallengeId))
                .Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(true, result.Data);
        }
    }
}
