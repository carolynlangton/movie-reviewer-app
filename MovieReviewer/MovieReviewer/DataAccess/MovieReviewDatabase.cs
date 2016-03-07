using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MovieReviewer
{
	public class MovieReviewDatabase
	{
		static object locker = new object();

		SQLiteConnection database = null;

		public MovieReviewDatabase ()
		{
			// Create the database connection
			this.database = new SQLiteConnection(this.databasePath);

			// Create the table
			this.database.CreateTable<MovieReview>();
		}

		private string databasePath
		{
			get {
				var sqliteFilename = "MovieReviews.db3";

				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
				#else
				#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
				#else
				// WinPhone
				var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
				#endif
				#endif

				return path;
			}
		}

		public IEnumerable<MovieReview> GetReviews()
		{
			lock (locker)
			{
				return this.database.Table<MovieReview> ();
			}
		}

		public MovieReview GetReview(int id)
		{
			lock (locker) 
			{
				return this.database.Table<MovieReview> ().SingleOrDefault (r => r.ID == id);
			}
		}

		public int SaveReview(MovieReview review)
		{
			lock (locker) 
			{
				int id = -1;

				// update
				if (review.ID != 0)
				{
					id = this.database.Update (review);
				}
				else
				{
					// add
					id = this.database.Insert (review);
				}

				return id;
			}
		}

		public int DeleteReview(int id)
		{
			lock (locker) 
			{
				return this.database.Delete<MovieReview> (id);
			}
		}
	}
}

