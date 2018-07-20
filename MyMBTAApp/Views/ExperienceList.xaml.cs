using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
using MyMBTAApp.Models;
using System.Linq;

namespace MyMBTAApp.Views
{
    public partial class ExperienceList : ContentPage
    {
        public ExperienceList()
        {
            InitializeComponent();
        }
		protected override void OnAppearing()
        {
            base.OnAppearing();
			using (SQLiteConnection connection = new SQLiteConnection(App.dbLocation))
            {
                connection.CreateTable<Post>();
                var posts = connection.Table<Post>().ToList();
                //Console.WriteLine(posts);
                postListView.ItemsSource = posts;
            }
        }
    }
}
