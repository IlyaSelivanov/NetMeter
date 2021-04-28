namespace Application.Services
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string name);
    }
}
