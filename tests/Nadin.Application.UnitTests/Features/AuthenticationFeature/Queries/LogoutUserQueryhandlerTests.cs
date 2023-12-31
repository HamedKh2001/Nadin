﻿using Moq;
using Nadin.Application.Contracts.Infrastructure;
using Nadin.Application.Features.AuthenticationFeature.Queries.LogoutUser;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nadin.Application.UnitTests.Features.AuthenticationFeature.Queries
{
    public class LogoutUserQueryhandlerTests
    {
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;
        private readonly LogoutUserQueryhandler _handler;

        public LogoutUserQueryhandlerTests()
        {
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _handler = new LogoutUserQueryhandler(_authenticationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallLogoutAsync_WhenQueryIsValid()
        {
            // Arrange
            var query = new LogoutUserQuery { UserId = 1 };

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            _authenticationServiceMock.Verify(x => x.LogoutAsync(query, CancellationToken.None), Times.Once);
        }
    }

}
