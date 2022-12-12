using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TuraProductsViewer.Services
{
    public class NotificationService
    {
        private IJSRuntime _jsRuntime { get; set; }

        public NotificationService(IJSRuntime jSRuntime)
        {
            this._jsRuntime = jSRuntime;
        }

        public async Task Alert(string message)
        {
            try
            {
                await this._jsRuntime.InvokeVoidAsync("alertUser", message);
            }
            catch (Exception ex)
            {
                //User Disconnected
            }
        }

        public async Task AlertSuccess(string message)
        {
            try
            {
                await this._jsRuntime.InvokeVoidAsync("alertUserSuccess", message);
            }
            catch (Exception ex)
            {
                //User Disconnected
            }
        }

        public async Task AlertError(string message)
        {
            try
            {
                await this._jsRuntime.InvokeVoidAsync("alertUserError", message);
            }
            catch(Exception ex)
            {
                //User Disconnected
            }
        }
    }
}
