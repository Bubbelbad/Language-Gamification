using Application.Commands.ChallengeCommands.Update;
using Application.Commands.UserChallengeCommands.Update;
using Application.Dtos.ChallengeDtos;
using Application.Dtos.UserChallengeDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;

namespace Test.UserChallengeTest
{
    [TestFixture]
    [Category("UserChallenge/UnitTests/UpdateUserChallenge")]
    public class UpdateUserChallengeTest
    {
        private IGenericRepository<UserChallenge, int> _repositoryMock;
        private IMapper _mapper;
        private UpdateUserChallengeCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = A.Fake<IGenericRepository<UserChallenge, int>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateUserChallengeDto, UserChallenge>();
                cfg.CreateMap<UserChallenge, GetUserChallengeDto>();
            });

            _mapper = config.CreateMapper();
            _handler = new UpdateUserChallengeCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task UpdatingUserChallenge_ShouldCallRepositoryUpdate_Method()
        {
            var updateDto = new UpdateUserChallengeDto
            {
                Id = 1,
                UserId = "",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow

            };

            var command = new UpdateUserChallengeCommand(updateDto);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<UserChallenge>.Ignored))
                .Returns(Task.FromResult(new UserChallenge
                {
                    Id = 1,
                    UserId = "",
                    ChallengeId = 1,
                    Score = 1,
                    CompletedAt = DateTime.UtcNow
                }));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<UserChallenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task UpdatingUserChallenge_ShouldUpdateUserChallenge_Successfully()
        {
            var updateDto = new UpdateUserChallengeDto
            {
                Id = 1,
                UserId ="",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow
            };

            var command = new UpdateUserChallengeCommand(updateDto);

            var updatedUserChallenge = new UserChallenge
            {
                Id = 1,
                UserId ="",
                ChallengeId = 1,
                Score = 1,
                CompletedAt = DateTime.UtcNow
            };

            A.CallTo(() => _repositoryMock.UpdateAsync(A<UserChallenge>.Ignored))
                .Returns(Task.FromResult(updatedUserChallenge));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(updateDto.Id, result.Data.Id);
            Assert.AreEqual(updateDto.UserId, result.Data.UserId);
            Assert.AreEqual(updateDto.ChallengeId, result.Data.ChallengeId);
            Assert.AreEqual(updateDto.Score, result.Data.Score);
            Assert.AreEqual(updateDto.CompletedAt, result.Data.CompletedAt);
        }
    }
}
