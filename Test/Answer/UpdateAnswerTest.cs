using Application.Commands.AnswerCommands.Update;
using Application.Dtos.AnswerDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AnswerTests
{
    [TestFixture]
    [Category("Answer/UnitTests/UpdateAnswer")]

    public class UpdateAnswerTest
    {
        private IGenericRepository<Answer, int> _repositoryMock;
        private IMapper _mapper;
        private UpdateCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = A.Fake<IGenericRepository<Answer, int>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateAnswerDto, Answer>();
                cfg.CreateMap<Answer, GetAnswerDto>();
            });

            _mapper = config.CreateMapper();
            _handler = new UpdateCommandHandler(_repositoryMock, _mapper);
        }

        [Test]
        public async Task UpdatingAnswer_SHouldCallRepositoryUpdate_Method()
        {
            var updateDto = new UpdateAnswerDto
            {
                Id = 1,
                Text = "Updated answer",
                QuestionId = 1,
                IsCorrect = true,
            };

            var command = new UpdateCommand(updateDto);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Answer>.Ignored))
                .Returns(Task.FromResult(new Answer()));

            await _handler.Handle(command, CancellationToken.None);

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Answer>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task UpdatingAnswer_ShouldUpdateAnswer_Successfully()
        {
            var updateDto = new UpdateAnswerDto
            {
                Id = 1,
                Text = "Updated Answer",
                QuestionId = 1,
                IsCorrect = true
            };

            var command = new UpdateCommand(updateDto);

            var updatedAnswer = new Answer
            {
                Id = 1,
                Text = "Updated Answer",
                QuestionId = 1,
                IsCorrect = true
            };

            A.CallTo(() => _repositoryMock.UpdateAsync(A<Answer>.Ignored))
                .Returns(Task.FromResult(updatedAnswer));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Updated Answer", result.Data.Text);
        }

        [Test]
        public void UpdateCommandValidator_ShouldFail_WhenTextIsEmpty()
        {
            var invalidCommand = new UpdateCommand(new UpdateAnswerDto
            {
                Id = 1,
                Text = "", 
                QuestionId = 1,
                IsCorrect = true
            });

            var validator = new UpdateCommandValidator();

            var result = validator.Validate(invalidCommand);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Text is required.", result.Errors[0].ErrorMessage);
        }
    }
}
