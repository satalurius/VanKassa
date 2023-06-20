using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VanKassa.Backend.Api.Controllers;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Api.Tests.Controllers;
public class AuthenticationControllerTests
{
    private readonly Mock<IAuthenticationService> authenticationService = new();

    [Fact]
    public async Task AuthenticationController_AuthenticatiedStream_ReturnAuthenticationViewModel()
    {
        // Arrange
        var authenticate = new AuthenticateDto
        {
            Login = "Login",
            Password = "Password"
        };

        var authenticatieViewModel = new AuthenticateViewModel("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        authenticationService.Setup(e => e.AuthenticateAsync(authenticate))
            .ReturnsAsync(authenticatieViewModel);

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = await controller.AuthenticatedStream(authenticate);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(authenticatieViewModel);
        value.Should().BeOfType<AuthenticateViewModel>();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AuthenticationController_AuthenticatiedStream_ThrowNotFoundIfAccountNotExist()
    {
        // Arrange
        var authenticate = new AuthenticateDto
        {
            Login = "Login",
            Password = "Password"
        };

        authenticationService.Setup(e => e.AuthenticateAsync(authenticate))
            .ThrowsAsync(new NotFoundException());

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = async () => await controller.AuthenticatedStream(authenticate);

        // Arrange
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task AuthenticationControler_RefreshToken_ReturnNewToken()
    {
        // Arrange
        var refreshTokenDto = new RefreshTokenDTO { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" };

        var authenticateViewModel = new AuthenticateViewModel("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        authenticationService.Setup(e => e.RefreshTokenAsync(refreshTokenDto.Token))
            .ReturnsAsync(authenticateViewModel);

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = await controller.RefreshToken(refreshTokenDto);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(authenticateViewModel);
        value.Should().BeOfType<AuthenticateViewModel>();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AuthenticationController_RefreshToken_ThrowBadRequestIfTokenInvalid()
    {
        // Arrange
        var refreshTokenDto = new RefreshTokenDTO { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" };

        var authenticateViewModel = new AuthenticateViewModel("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

        authenticationService.Setup(e => e.RefreshTokenAsync(refreshTokenDto.Token))
            .ThrowsAsync(new BadRequestException(AuthenticationErrors.InvalidRefreshToken));

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = async () => await controller.RefreshToken(refreshTokenDto);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task AuthenticationController_RevokeToken_ReturnOkResult()
    {
        // Arrange
        var refreshTokenDto = new RefreshTokenDTO { Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" };

        authenticationService.Setup(e => e.RemoveTokenAsync(refreshTokenDto.Token))
            .Returns(Task.CompletedTask);

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = await controller.RevokeToken(refreshTokenDto);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AuthenticationController_RevokeTokenThrowBadRequestWithEmptyTokenIfTokenEmpty()
    {
        // Arrange
        var refreshTokenDto = new RefreshTokenDTO();

        authenticationService.Setup(e => e.RemoveTokenAsync(refreshTokenDto.Token))
            .ThrowsAsync(new BadRequestException(AuthenticationErrors.TokenRequired));

        var controller = new AuthenticationController(authenticationService.Object);

        // Act
        var result = async () => await controller.RevokeToken(refreshTokenDto);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }
}
