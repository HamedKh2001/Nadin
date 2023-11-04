using Moq;
using Nadin.Application.Contracts.Persistence;

public class GetUserByUserNameQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;

    public GetUserByUserNameQueryHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
    }
}
