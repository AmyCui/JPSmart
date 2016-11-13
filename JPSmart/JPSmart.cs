using System;

using Xamarin.Forms;

namespace JPSmart
{
	public class App : Application
	{
		#region fields
		private Grid mContentGrid;
		private ContentPage content;
		#endregion

		#region constructor
		public App()
		{
			//initialize layout
			mContentGrid = new Grid()
			{
				ColumnDefinitions =
				{
					new ColumnDefinition() {Width = new GridLength(1, GridUnitType.Star)}
				},
				ColumnSpacing = 0,
				BackgroundColor = Color.White,
				RowSpacing = 25,
				Padding = 25
			};
			//test code
			var card1 = new CardView()
			{
				CardImage = "clothes.png",
				CardTitle = "Clothes",
				TitleTextColor = Color.Black,
				CardColor = Color.White
			};

			var card2 = new CardView()
			{
				CardImage = "clothes.png",
				CardTitle = "Clothes",
				TitleTextColor = Color.Black,
				CardColor = Color.White
			};

			var card3 = new CardView()
			{
				CardImage = "clothes.png",
				CardTitle = "Clothes",
				TitleTextColor = Color.Black,
				CardColor = Color.White
			};

			AddCard(card1);
			AddCard(card2);
			AddCard(card3);

			content = new ContentPage
			{
				Content = new ScrollView()
				{
					Content = mContentGrid
				},
				Title = "JPSmart"
			};

			MainPage = new NavigationPage(content);
		}
		#endregion

		#region private methods
		private void AddCard(CardView card)
		{
			if (mContentGrid != null)
			{
				mContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
				mContentGrid.Children.Add(card, 0, mContentGrid.RowDefinitions.Count);
				card.Tapped += async (sender, e) => { await content.Navigation.PushAsync(new DetailContentPage());};
			}
		}
		#endregion

		#region override methods

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
		#endregion
	}
}
