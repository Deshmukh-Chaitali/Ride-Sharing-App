namespace auth_service.Services;

public interface ITokenService {
    string CreateToken(string email);
}