using System;
using Xamarin.Forms;

namespace JPSmart
{
	public class CardView : Frame
	{
		#region constants
		private readonly int CARD_IMG_GRIDLENGTH = 8;
		private readonly int CARD_TITLE_GRIDLENGTH = 2;
		#endregion

		#region fields
		private Grid mContentGrid;
		private Image mCardImg;
		private Label mCardTitleLabel;
		private Label mCardDescription;
		#endregion

		#region properties
		/// <summary>
		/// Gets or sets the color of the card backgroud.
		/// </summary>
		/// <value>The color of the card.</value>
		public Color CardColor
		{
			get { return mContentGrid.BackgroundColor; }
			set { mContentGrid.BackgroundColor = value; }
		}
		/// <summary>
		/// Gets or sets the card image source.
		/// </summary>
		/// <value>The card image.</value>
		public ImageSource CardImage
		{
			get { return mCardImg?.Source; }
			set { if (mCardImg != null) mCardImg.Source = value; }
		}
		/// <summary>
		/// Gets or sets the card title text
		/// </summary>
		/// <value>The card title.</value>
		public string CardTitle
		{
			get { return mCardTitleLabel?.Text; }
			set { if (mCardTitleLabel != null) mCardTitleLabel.Text = value; }
		}
		/// <summary>
		/// Gets or sets the card description text.
		/// </summary>
		/// <value>The card description.</value>
		public string CardDescription
		{
			get { return mCardDescription?.Text; }
			set { if (mCardDescription != null) mCardDescription.Text = value; }
		}
		/// <summary>
		/// Gets or sets the color of the title text.
		/// </summary>
		/// <value>The color of the title text.</value>
		public Color TitleTextColor
		{
			get { if(mCardTitleLabel!=null)return mCardTitleLabel.TextColor; else return default(Color);}
			set { if (mCardTitleLabel != null) mCardTitleLabel.TextColor = value; }
		}
		#endregion

		#region event
		public EventHandler Tapped;
		#endregion

		#region styles
		Style ImageStyle = new Style(typeof(Image))
		{
			Setters =
			{
				new Setter {Property = Image.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand},
				new Setter {Property = Image.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand},
				new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
			}
		};

		Style LabelStyle = new Style(typeof(Label))
		{
			Setters =
			{
				new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand},
				new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand},
				new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center},
				new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center},
			}
		};
		#endregion

		#region constructor
		public CardView()
		{
			//initialize ui elements
			mCardImg = new Image() { Style = ImageStyle };
			mCardTitleLabel = new Label { Style = LabelStyle, FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label)) };
			mCardDescription = new Label { Style = LabelStyle, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
			//create layout
			mContentGrid = new Grid();
			mContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(CARD_IMG_GRIDLENGTH, GridUnitType.Star) });
			mContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(CARD_TITLE_GRIDLENGTH, GridUnitType.Star) });
			mContentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			mContentGrid.RowSpacing = 0;
			this.OutlineColor = Color.Silver;
			this.HasShadow = true;
			//populate content
			mContentGrid.Children.Add(mCardImg, 0, 0);
			mContentGrid.Children.Add(mCardTitleLabel, 0, 1);
			mContentGrid.Children.Add(mCardDescription, 0, 2);
			Content = mContentGrid;
			//sizing
			SizeChanged += CardView_SizeChanged;
			//tap gesture
			TapGestureRecognizer tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += (sender, e) => { if (Tapped != null) Tapped.Invoke(this, null);};
			mContentGrid.GestureRecognizers.Add(tapGesture);
			mCardImg.GestureRecognizers.Add(tapGesture);
			mCardTitleLabel.GestureRecognizers.Add(tapGesture);

		}
		#endregion

		#region public methods

		void CardView_SizeChanged(object sender, EventArgs e)
		{
			if (mCardImg != null)
				mCardImg.WidthRequest = mContentGrid.Width;
		}
		#endregion

		#region private methods
		#endregion

	}
}
