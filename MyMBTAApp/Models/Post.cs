using System;
using SQLite;
namespace MyMBTAApp.Models
{
    public class Post
    {
        [MaxLength(256)]
		public string Experience
		{
			get;
			set;
		}
		[PrimaryKey, AutoIncrement]
        public int Id
		{
			get;
			set;
		}
    }
}
