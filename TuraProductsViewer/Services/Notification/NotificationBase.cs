using Microsoft.AspNetCore.Components;

namespace TuraProductsViewer.Services.Notification
{
    public class NotificationBase : ComponentBase, IDisposable
    {
        [Inject] NotificationService NotificationService { get; set; }

        protected string Text { get; set; }
        protected NotificationLevel NotificationLevel { get; set; }

        protected override void OnInitialized()
        {
            this.NotificationService.OnShow += this.ShowNotification;
            this.NotificationService.OnHide += this.HideNotification;
        }

        private void ShowNotification(string text, NotificationLevel level)
        {
            base.StateHasChanged();
        }

        private void HideNotification()
        {
            base.StateHasChanged();
        }

        public void Dispose()
        {
            this.NotificationService.OnShow -= this.ShowNotification;
        }
    }
}
