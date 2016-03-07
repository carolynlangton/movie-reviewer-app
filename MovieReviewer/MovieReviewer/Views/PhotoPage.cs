using System;

using Xamarin.Forms;

namespace MovieReviewer
{
	public class PhotoPage : ContentPage
	{
		private Image photo = null;

		public PhotoPage ()
		{
			photo = new Image ();

			Content = new StackLayout { 
				Children = {
					new Label { Text = "My photo" },
					photo
				}
			};
		}

		public Image Photo
		{
			get
			{
				return this.photo;
			}
		}
	}
}


