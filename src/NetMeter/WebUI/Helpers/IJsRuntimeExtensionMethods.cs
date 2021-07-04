using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebUI.Helpers
{
    public static class IJsRuntimeExtensionMethods
    {
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
        {
            return js.InvokeAsync<object>("localStorage.setItem", key, content);
        }

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<string>("localStorage.getItem", key);
        }

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<object>("localStorage.removeItem", key);
        }
    }
}
