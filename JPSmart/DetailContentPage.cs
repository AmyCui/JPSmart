using System;
using Xamarin.Forms;
namespace JPSmart
{
	public class DetailContentPage:ContentPage
	{
		public DetailContentPage()
		{
			Content = new StackLayout()
			{
				Children =
				{
					new Label() {Text = "detail page"}
				}
			};

		}
	}
}

