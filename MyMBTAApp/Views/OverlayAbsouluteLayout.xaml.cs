using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyMBTAApp.Views
{
    public partial class OverlayAbsouluteLayout : ContentPage
    {
        public OverlayAbsouluteLayout()
        {
            InitializeComponent();
        }
        void On5JobButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = true;
            TimeSpan duration = TimeSpan.FromSeconds(5);
            DateTime startTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                double progress = (DateTime.Now - startTime).TotalMilliseconds / duration.TotalMilliseconds;
                progressBar.Progress = progress;
                bool continueTimer = progress < 1;
                if (!continueTimer)
                {
                    overlay.IsVisible = false;
                }
                return continueTimer;
            });
        }

    }
}
