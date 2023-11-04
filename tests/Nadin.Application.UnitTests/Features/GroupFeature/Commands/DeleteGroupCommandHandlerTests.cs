using Moq;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.GroupFeature.Commands.DeleteGroup;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.GroupFeature.Commands
{
    public class DeleteGroupCommandHandlerTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly DeleteGroupCommandHandler _sut;

        public DeleteGroupCommandHandlerTests()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _sut = new DeleteGroupCommandHandler(_groupRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_DeletesGroup()
        {
            // Arrange
            var groupId = 1;
            var command = new DeleteGroupCommand { Id = groupId };
            var group = new Group { Id = groupId };
            _groupRepositoryMock.Setup(repo => repo.GetAsync(groupId, CancellationToken.None)).ReturnsAsync(group);

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            _groupRepositoryMock.Verify(repo => repo.DeleteAsync(group, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsNotFoundException()
        {
            // Arrange
            var groupId = 1;
            var command = new DeleteGroupCommand { Id = groupId };
            _groupRepositoryMock.Setup(repo => repo.GetAsync(groupId, CancellationToken.None)).ReturnsAsync((Group)null);

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }

}
