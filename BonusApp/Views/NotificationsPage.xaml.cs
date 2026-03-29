using System;
using System.Collections.Generic;
using System.Text;
using BonusApp.Services;

namespace BonusApp.Views
{
    public partial class NotificationsPage : ContentPage
    {
        private readonly NotificationService _notificationService;
        public NotificationsPage()
        {
            InitializeComponent();
            _notificationService = NotificationService.Instance;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NotificationsCollectionView.ItemsSource = _notificationService.GetNotifications();
        }
    }
}
