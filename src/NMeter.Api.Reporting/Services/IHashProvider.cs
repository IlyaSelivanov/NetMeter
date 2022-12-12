namespace NMeter.Api.Reporting.Services
{
    public interface IHashProvider
    {
        string GenerateHash(object obj);
    }
}