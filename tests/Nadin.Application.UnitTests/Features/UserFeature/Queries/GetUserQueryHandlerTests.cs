using AutoMapper;
using FluentAssertions;
using Moq;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Application.UnitTests.Common;
using Nadin.Domain.Entities;
using SharedKernel.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.UserFeature.Queries
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetUserQueryHandler _getUserQueryHandler;
        private readonly IMapper _mapper;

        public GetUserQueryHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapper.GetMapper();
            _getUserQueryHandler = new GetUserQueryHandler(_userRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_WhenUserExists_ShouldReturnUserDto()
        {
            // Arrange
            var userId = 1;
            var userName = "testuser";
            var user = new User
            {
                Id = userId,
                UserName = userName,
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                Mobile = "1234567890",
                CreatedDate = new DateTime(2022, 01, 01),
                IsDelete = false,
                Password = "1234567890",
            };
            var expectedUserDto = new UserDto
            {
                Id = userId,
                UserName = userName,
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                Mobile = "1234567890",
                CreatedDate = new DateTime(2022, 01, 01),
            };
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId, CancellationToken.None)).ReturnsAsync(user);

            // Act
            var result = await _getUserQueryHandler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedUserDto);
        }

        [Fact]
        public async Task Handle_WhenUserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetAsync(userId, CancellationToken.None)).ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await _getUserQueryHandler.Handle(new GetUserQuery { Id = userId }, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"*{nameof(User)}*{userId}*");
        }
    }

}
