using Microsoft.JSInterop;
using System.Timers;

namespace TuraProductsViewer.Services.Notification
{
    public class NotificationService : IDisposable
    {
        public event Action<string, NotificationLevel> OnShow;
        public event Action OnHide;
        private System.Timers.Timer _countdown;

        public void ShowNotification(string message, NotificationLevel level)
        {
            this.OnShow?.Invoke(message, level);
            
        }

        private void StartCountdown()
        {
            this.InitializeCountdown();

            if(this._countdown.Enabled)
            {
                this._countdown.Stop();
                this._countdown.Start();
            }
            else
            {
                this._countdown.Start();
            }
        }

        private void InitializeCountdown()
        {
            if(this._countdown == null)
            {
                this._countdown = new System.Timers.Timer(3000);
                this._countdown.Elapsed += HideAlert;
                this._countdown.AutoReset = false;
            }
        }

        private void HideAlert(object? source, ElapsedEventArgs args)
        {
            this.OnHide?.Invoke();
        }

        public void Dispose()
        {
            this._countdown?.Dispose();
        }
    }
}
