using System;
using Xamarin.Forms;
namespace JPSmart
{
	public class DetailContentPage:ContentPage
	{
		AudioPlayer player;
		public DetailContentPage()
		{
			var button = new Button() { Text = "detail page" };
			button.Clicked += Button_Clicked;
			player = new AudioPlayer();
			player.AudioSource = "Clothes.m4a";
			player.TimedTextSource = "sample.srt";
			Content = new StackLayout()
			{
				
				Children =
				{
					button,
					player
				}
			};



		}


		void Button_Clicked(object sender, EventArgs args)
		{
			
			player.AudioSource = "Clothes.m4a";
			player.Player.Play();
		}
	}
}

