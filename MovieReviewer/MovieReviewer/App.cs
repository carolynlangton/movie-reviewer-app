using System;

using Xamarin.Forms;

namespace MovieReviewer
{
	public class App : Application
	{
		private static MovieReviewDatabase database = null;

		public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage( new ReviewListPage() );
		}

		public static MovieReviewDatabase Database 
		{
			get 
			{
				App.database = App.database ?? new MovieReviewDatabase ();

				return App.database;
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

