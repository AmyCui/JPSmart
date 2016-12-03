using System;
using Xamarin.Forms;
namespace JPSmart
{
	public class AudioPlayer:Frame
	{
		private IAudioPlayer m_Player;
		private string m_AudioSource;
		private Image m_PlayBtn;
		private Image m_PauseBtn;
		private Grid m_PlayPauseControl;
		private string m_TimedTextSource;
		//private Slider m_SeekBar;
		//private Image m_BackwardBtn;
		//private Image m_ForwardBtn;
		//private Label m_CurrentLapse;
		//private Label m_TotalTime;

		public IAudioPlayer Player
		{
			get { return m_Player;}
		}

		public string AudioSource
		{
			get { return m_AudioSource;}
			set
			{
				m_AudioSource = value;
				if (m_Player != null)
				{
					m_Player.Source = m_AudioSource;
					m_Player.Reset();
				}

			}
		}

		public string TimedTextSource
		{
			get { return m_TimedTextSource;}
			set
			{
				m_TimedTextSource = value;
				if (m_Player != null)
				{
					m_Player.AddTimedTextSource(m_TimedTextSource);
				}
			}
		}


		public AudioPlayer()
		{
			m_PlayBtn = new Image() { Source = "play.png", HorizontalOptions = LayoutOptions.CenterAndExpand };
			var m_PlayBtnTapGesture = new TapGestureRecognizer();
			m_PlayBtnTapGesture.Tapped += PlayBtn_Clicked;
			m_PlayBtn.GestureRecognizers.Add(m_PlayBtnTapGesture);

			m_PauseBtn = new Image() { Source = "pause.png", HorizontalOptions = LayoutOptions.CenterAndExpand };
			var m_PauseBtnTapGesture = new TapGestureRecognizer();
			m_PauseBtnTapGesture.Tapped += PauseBtn_Clicked;
			m_PauseBtn.GestureRecognizers.Add(m_PauseBtnTapGesture);

			m_PlayPauseControl = new Grid() { HorizontalOptions = LayoutOptions.CenterAndExpand};
			m_PlayPauseControl.Children.Add(m_PlayBtn);

			var m_ControlStack = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Horizontal,
			};

			m_ControlStack.Children.Add(m_PlayPauseControl);

			Content = m_ControlStack;
		}

		public void SetNativePlayer(IAudioPlayer player)
		{
			m_Player = player;
			m_Player.Source = m_AudioSource;
			m_Player.Reset();
			m_Player.AddTimedTextSource(m_TimedTextSource);
		}

		void PlayBtn_Clicked (object sender, EventArgs e)
		{
			if (m_Player != null)
			{
				m_PlayPauseControl.Children.Clear();
				m_PlayPauseControl.Children.Add(m_PauseBtn);
				m_Player.Play();
			}
		}
		void PauseBtn_Clicked(object sender, EventArgs e)
		{
			if (m_Player != null)
			{
				m_PlayPauseControl.Children.Clear();
				m_PlayPauseControl.Children.Add(m_PlayBtn);
				m_Player.Pause();
			}
		}
	}
}
