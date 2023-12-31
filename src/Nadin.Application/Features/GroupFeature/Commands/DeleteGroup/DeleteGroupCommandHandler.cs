﻿using MediatR;
using Nadin.Application.Contracts.Persistence;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Features.GroupFeature.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetAsync(request.Id, cancellationToken);
            if (group == null)
                throw new NotFoundException(nameof(Group), request.Id);

            await _groupRepository.DeleteAsync(group, cancellationToken);
            return Unit.Value;
        }
    }
}
