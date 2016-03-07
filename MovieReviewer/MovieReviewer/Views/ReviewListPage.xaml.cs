using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Media;

namespace MovieReviewer
{
	public partial class ReviewListPage : ContentPage
	{
		public ReviewListPage ()
		{
			this.InitializeComponent ();

			Action addItemAction = () => {
				var reviewPage = new ReviewPage();
				var review = new MovieReview() { ReviewDate = DateTime.Today };
				reviewPage.BindingContext = review;
				this.Navigation.PushAsync(reviewPage, true);
			};

			ToolbarItem toolBarItem = null;

			toolBarItem = Device.OnPlatform<ToolbarItem> (
				new ToolbarItem ("+", null, addItemAction),
				new ToolbarItem ("+", "plus", addItemAction),
				null);

			this.ToolbarItems.Add (toolBarItem);

			this.reviewListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem == null)
				{
					return;
				}

				var reviewPage = new ReviewPage();
				reviewPage.BindingContext = e.SelectedItem as MovieReview;
				this.Navigation.PushAsync(reviewPage, true);
			};

			this.photoButton.Clicked += PhotoButton_Clicked;
		}

		private async void PhotoButton_Clicked (object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await this.DisplayAlert ("No camera", "No camera is available", "OK");
				return;
			}

			var file = await CrossMedia.Current.TakePhotoAsync (new Plugin.Media.Abstractions.StoreCameraMediaOptions () 
			{
					Directory = "photos",
					Name = "test.jpg"
			});

			if (file == null)
			{
				return;
			}

			var photoPage = new PhotoPage ();
			photoPage.Photo.Source = ImageSource.FromStream (() => {
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

			await this.Navigation.PushAsync (photoPage, true);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			this.reviewListView.ItemsSource = App.Database.GetReviews();
		}
	}
}

