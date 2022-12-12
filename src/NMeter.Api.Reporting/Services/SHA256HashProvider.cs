using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NMeter.Api.Reporting.Services
{
    public class SHA256HashProvider : IHashProvider, IDisposable
    {
        private readonly Logger<SHA256HashProvider> _logger;
        private readonly SHA256 _sha256;

        public SHA256HashProvider(Logger<SHA256HashProvider> logger)
        {
            _logger = logger;
            _sha256 = SHA256.Create();
        }

        public void Dispose()
        {
            if (_sha256 != null)
                _sha256.Dispose();
        }

        public string GenerateHash(object obj)
        {
            var bytes = ObjectToByteArray(obj);

            if (bytes == null)
                return string.Empty;

            try
            {
                var hash = _sha256.ComputeHash(bytes);
                return ByteArrayToString(hash);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }
        }

        private byte[]? ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;

            var serializationOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            var json = JsonSerializer.Serialize(obj, serializationOptions);
            return Encoding.UTF8.GetBytes(json);
        }

        private string ByteArrayToString(byte[] array)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                sb.Append($"{array[i]:X2}");
                if ((i % 4) == 3) 
                    sb.Append(":");
            }

            return sb.ToString();
        }
    }
}