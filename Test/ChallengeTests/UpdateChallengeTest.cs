using Application.Commands.AnswerCommands.Update;
using Application.Commands.ChallengeCommands.Update;
using Application.Commands.UserChallengeCommands.Update;
using Application.Dtos.AnswerDtos;
using Application.Dtos.ChallengeDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;

namespace Test.ChallengeTests
{
    [TestFixture]
    [Category("Challenge/UnitTest/UpdateChallenge")]
    public class UpdateChallengeTest
    {
        private IGenericRepository<Challenge, int> _repositoryMock;
        private IMapper _mapper;
        private UpdateChallengeCommandHandler _handler;

        [SetUp]
        public void Setuo()
        {
            _repositoryMock = A.Fake<IGenericRepository<Challenge, int>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateChallengeDto, Challenge>();
                cfg.CreateMap<Challenge, GetChallengeDto>();
            });

            _mapper = config.CreateMapper();
            _handler = new UpdateChallengeCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task UpdatingChallenge_ShouldCallRepositoryUpdate_Method()
        {
            var updateDto = new UpdateChallengeDto
            {
                Id = 1,
                Title = "Updated Challenge",
                Description = "Update Challenge"
            };

            var command = new UpdateChallengeCommand(updateDto);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Challenge>.Ignored))
                .Returns(Task.FromResult(new Challenge
                { 
                    Id = 1,
                    Title = "Updated Challenge",
                    Description = "Updated Challenge"

                }));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Challenge>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task UpdatingChallenge_ShouldUpdateChallenge_Successfully()
        {
            var updateDto = new UpdateChallengeDto
            {
                Id = 1,
                Title = "Update Challenge",
                Description = "Update Challenge",
            };

            var command = new UpdateChallengeCommand(updateDto);

            var updatedChallenge = new Challenge
            {
                Id = 1,
                Title ="Update Challenge",
                Description ="Update Challenge"
            };

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Challenge>.Ignored))
                .Returns(Task.FromResult(updatedChallenge));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Update Challenge", result.Data.Title);
            Assert.AreEqual("Update Challenge", result.Data.Description);
        }

        [Test]
        public void UpdateChallengeCommandValidator_ShouldFail_WhenTitleIsEmpty()
        {
            var invalidCommand = new UpdateChallengeCommand(new UpdateChallengeDto
            {
                Id= 1,
                Title = "",
                Description = ""
                
            });

            var validator = new UpdateChallengeCommandValidator();

            var result = validator.Validate(invalidCommand);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Title is required.", result.Errors[0].ErrorMessage);
        }
    }
}
