using AutoMapper;
using FluentAssertions;
using Moq;
using Nadin.Application.Contracts.Persistence;
using Nadin.Application.Features.UserFeature.Commands.CreateUser;
using Nadin.Application.Features.UserFeature.Queries.GetUser;
using Nadin.Application.UnitTests.Common;
using Nadin.Domain.Entities;
using SharedKernel.Contracts.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.UserFeature.Commands
{
    public class CreateUserTests
    {
        private readonly Mock<IUserRepository> _userRepository = new();
        private readonly Mock<IEncryptionService> _encryptionService = new();
        private readonly Mock<IDateTimeService> _dateTimeService = new();
        private readonly IMapper _mapper;

        public CreateUserTests()
        {
            _mapper.GetMapper();
            _dateTimeService.Setup(d => d.Now).Returns(DateTime.Now);
        }

        [Fact]
        public async Task Handle_NewUser_AddToUserRepo()
        {
            //Arrage
            var request = new CreateUserCommand
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                UserName = "NewUserName",
                Password = "NewPassword",
                Mobile = "09111540028"
            };
            _encryptionService.Setup(e => e.HashPassword(request.Password)).Returns("HashedNewPassword");
            var user = new User
            {
                Id = 1,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Password = _encryptionService.Object.HashPassword(request.Password),
                Mobile = request.Mobile,
                CreatedDate = _dateTimeService.Object.Now,
                IsActive = true,
            };
            _userRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>(), CancellationToken.None)).ReturnsAsync(user);

            var handler = new CreateUserCommandHandler(_userRepository.Object, _encryptionService.Object, _dateTimeService.Object, _mapper);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Mobile.Should().Be(request.Mobile);
            result.UserName.Should().Be(request.UserName);
            result.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
            result.Should().BeOfType<UserDto>();

            user.Password.Should().NotBeEmpty();
            user.Password.Should().NotBe(_encryptionService.Object.HashPassword("OldPassword"));
            user.Password.Should().Be(_encryptionService.Object.HashPassword(request.Password));
        }
    }
}
