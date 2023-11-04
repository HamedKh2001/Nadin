﻿using AutoMapper;
using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.UserFeature.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.Id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
