using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.TextToSpeech;

namespace MovieReviewer
{
	public partial class ReviewPage : ContentPage
	{
		public ReviewPage ()
		{
			this.InitializeComponent ();

			// Add suggestions, spell checking and sentence capitalization to review editor
			this.reviewEditor.Keyboard = Keyboard.Create (KeyboardFlags.All);

			this.saveButton.Clicked += (object sender, EventArgs e) => 
			{
				// get the review from the binding context
				var review = this.BindingContext as MovieReview;

				if (review != null)
				{
					// save it if it isn't null
					App.Database.SaveReview(review);
				}

				this.Navigation.PopAsync(true);
			};

			this.deleteButton.Clicked += (object sender, EventArgs e) => 
			{
				// get the review from the binding context
				var review = this.BindingContext as MovieReview;

				if (review != null)
				{
					// delete it if it isn't null
					App.Database.DeleteReview(review.ID);
				}

				this.Navigation.PopAsync(true);
			};
			
			this.speakButton.Clicked += (object sender, EventArgs e) => 
			{
				// get the review from the binding context
				var review = this.BindingContext as MovieReview;

				// make phone speak the title and rating
				CrossTextToSpeech.Current.Speak(string.Format("{0} has a rating of {1}", 
					review.MovieTitle, review.Rating.ToString()));
			};

		}
	}
}

