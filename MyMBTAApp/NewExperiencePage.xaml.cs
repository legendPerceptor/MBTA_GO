using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using MyMBTAApp.Models;

namespace MyMBTAApp
{
    public partial class NewExperiencePage : ContentPage
    {
        public NewExperiencePage()
        {
            InitializeComponent();
        }
		public void SQLiteButton_Clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = experience.Text
            };

			SQLiteConnection connection = new SQLiteConnection(App.dbLocation);

            connection.CreateTable<Post>();
            int rows = connection.Insert(post);

            connection.Close();

            if (rows > 0)
            {
                DisplayAlert("Success", "Experience Succesfully inserted", "OK");
            }
        }
    }
}
