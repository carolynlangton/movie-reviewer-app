using System;
using SQLite;

namespace MovieReviewer
{
	public class MovieReview
	{
		public MovieReview ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string MovieTitle { get; set; }
		
		public double Rating { get; set; }

		public string Review { get; set; }

		public DateTime ReviewDate { get; set; }
	}
}

