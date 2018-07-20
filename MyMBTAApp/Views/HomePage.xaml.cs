using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MyMBTAApp.DataHandler;
namespace MyMBTAApp.Views
{
	public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
		public void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			Navigation.PushAsync(new NewExperiencePage());
        }
		public void RefreshSchedules(DirectionEnum dir)
        {

            foreach (object o in this.Children)
            {
                if (o is SchedulePage)
                {
                    ((SchedulePage)o).RefreshSchedules(dir);
                }
            }

        }
    }
}
