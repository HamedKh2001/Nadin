using AutoMapper;
using Moq;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.GroupFeature.Commands.UpdateGroup;
using Nadin.Application.UnitTests.Common;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.GroupFeature.Commands
{
    public class UpdateGroupCommandHandlerTests
    {
        private readonly Mock<IGroupRepository> _mockGroupRepository;

        private readonly UpdateGroupCommandHandler _handler;
        private readonly IMapper _mapper;

        public UpdateGroupCommandHandlerTests()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
            _mapper = _mapper.GetMapper();

            _handler = new UpdateGroupCommandHandler(_mockGroupRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_GroupToUpdateIsNull_ThrowsNotFoundException()
        {
            // Arrange
            var request = new UpdateGroupCommand { Id = 1 };
            _mockGroupRepository.Setup(repo => repo.GetAsync(request.Id, CancellationToken.None)).ReturnsAsync((Group)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GroupToUpdateExists_MapsRequestAndUpdateGroup()
        {
            // Arrange
            var request = new UpdateGroupCommand { Id = 1 };
            var groupToUpdate = new Group();
            _mockGroupRepository.Setup(repo => repo.GetAsync(request.Id, CancellationToken.None)).ReturnsAsync(groupToUpdate);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockGroupRepository.Verify(repo => repo.UpdateAsync(groupToUpdate, CancellationToken.None), Times.Once);
        }
    }

}
