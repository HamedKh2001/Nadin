﻿using AutoMapper;
using FluentAssertions;
using Moq;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.GroupFeature.Commands.CreateGroup;
using Nadin.Application.Features.GroupFeature.Queries.GetGroup;
using Nadin.Application.UnitTests.Common;
using Nadin.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.GroupFeature.Commands
{
    public class CreateGroupTests
    {
        private readonly Mock<IGroupRepository> _groupRepository;
        private readonly IMapper _mapper;

        public CreateGroupTests()
        {
            _groupRepository = new Mock<IGroupRepository>();
            _mapper = _mapper.GetMapper();
        }

        [Fact]
        public async Task Handle_NewGroup_AddToGroupRepo()
        {
            //Arrange
            var request = new CreateGroupCommand { Caption = "New Group" };
            var group = new Group { Id = 1, Caption = request.Caption };
            _groupRepository.Setup(repo => repo.CreateAsync(It.IsAny<Group>(), CancellationToken.None)).ReturnsAsync(group);
            var handler = new CreateGroupCommandHandler(_groupRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(group.Id);
            result.Caption.Should().Be(request.Caption);
            result.Should().BeOfType<GroupDto>();
        }
    }
}
