using API.Entities;
using API.Entities.Admin;

namespace API.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(MstrAgents user);
    }
}