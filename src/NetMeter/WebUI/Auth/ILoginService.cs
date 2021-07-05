using System.Threading.Tasks;

namespace WebUI.Auth
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}
