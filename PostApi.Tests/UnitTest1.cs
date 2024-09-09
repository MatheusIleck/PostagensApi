using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PostagensApi.Models;
using PostagensApi.Response;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace PostApi.Tests;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task UserRegister_ShouldReturnCreatedAndUser()
    {
        // Arrange
        var client = _factory.CreateClient(); // Cria o cliente HTTP para o teste

        var newUser = new
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "Test@1234"
        };

        // Act
        var response = await client.PostAsJsonAsync("/register", newUser);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode); // Verifica se o status é 201 Created

        var registeredUser = await response.Content.ReadFromJsonAsync<User>(); // Altere para o tipo de resposta correta

        Assert.NotNull(registeredUser); // Verifica se a resposta não é nula
        Assert.Equal("Test User", registeredUser.Name); // Verifica o nome do usuário registrado
        Assert.Equal("testuser@example.com", registeredUser.Email); // Verifica o email
        // Outros asserts que se aplicam ao seu modelo de retorno
    }
}