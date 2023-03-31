using CatalogApi.Models;


namespace CatalogApi.Services;

public interface ITokenService
{
    string CreateToken(string key, string issuer, string audience, UserModel user);
}
