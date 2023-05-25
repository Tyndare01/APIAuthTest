using DAL.Models;

namespace BLL.Interfaces;

public interface IJwtService
{
  public string GenerateToken(User user);
}