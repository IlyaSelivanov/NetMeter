using System.Threading.Tasks;

namespace WebUI.Auth
{
    public interface ILoginService
    {
        Task Logint(string token);
        Task Logout();
    }
}
