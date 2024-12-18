using Application.Interfaces;
using Application.Interfaces.ServiceInterfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult<User>>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordEncryptionService _encryptionService;

        public RegisterCommandHandler(IGenericRepository<User> repository, IMapper mapper, IPasswordEncryptionService service)
        {
            _repository = repository;
            _mapper = mapper;
            _encryptionService = service;
        }

        public async Task<OperationResult<User>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userToCreate = _mapper.Map<User>(request.NewUser);
                userToCreate.Id = Guid.NewGuid().ToString();
                userToCreate.PasswordHash = _encryptionService.HashPassword(request.NewUser.Password);

                var createdUser = await _repository.AddAsync(userToCreate);
                return OperationResult<User>.Success(createdUser);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding new User.", ex);
            }
        }
    }
}
