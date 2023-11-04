using Nadin.Application.Features.AuthenticationFeature.Commands.ChangePassword;
using Nadin.Application.Features.AuthenticationFeature.Commands.UpdateUserGroup;
using Nadin.Application.Features.AuthenticationFeature.Queries.Authenticate;
using Nadin.Application.Features.AuthenticationFeature.Queries.LogoutUser;
using Nadin.Application.Features.AuthenticationFeature.Queries.RefreshToken;
using System.Threading;
using System.Threading.Tasks;

namespace Nadin.Application.Contracts.Infrastructure
{
    public interface IAuthenticationService
    {
        Task<AuthenticateDto> AuthenticateAsync(AuthenticateQuery request, CancellationToken cancellationToken);
        Task ChangePasswordAsync(ChangePasswordCommand request, CancellationToken cancellationToken);
        Task LogoutAsync(LogoutUserQuery request, CancellationToken cancellationToken);
        Task<AuthenticateDto> RefreshTokenAsync(RefreshTokenQuery request, CancellationToken cancellationToken);
        Task UpdateUserGroupAsync(UpdateUserGroupCommand request, CancellationToken cancellationToken);
    }
}
