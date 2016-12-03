using System;
using Xamarin.Forms;
using JPSmart;
using JPSmart.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Media;
using Android.Content.Res;
using Android.Util;
using System.IO;
using Android.App;


[assembly: ExportRenderer(typeof(AudioPlayer), typeof(AudioPlayerRenderer))]
namespace JPSmart.Droid
{	
	public class AudioPlayerRenderer : FrameRenderer, IAudioPlayer, MediaPlayer.IOnTimedTextListener
	{
		private MediaPlayer m_Player;
		public AudioPlayerRenderer()
		{
		} 

		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement == null && e.NewElement != null)
			{
				m_Player = new MediaPlayer();
				((AudioPlayer)(e.NewElement)).SetNativePlayer(this);

			}
		}

		public void Reset()
		{
			if (m_Player != null)
			{
				m_Player.Reset();
				if (Source != null)
				{
					var source = global::Android.App.Application.Context.Assets.OpenFd(Source);
					m_Player.SetDataSource(source.FileDescriptor, source.StartOffset, source.Length);
					m_Player.PlaybackParams.SetSpeed(1.5f);
					m_Player.SetAudioStreamType(Android.Media.Stream.Music);

					m_Player.Prepare();

				}
			}
		}

		public void Play()
		{
			m_Player.Start();
		}

		public void Pause()
		{
			if (m_Player != null)
				m_Player.Pause();
		}

		public void Stop()
		{
			if (m_Player != null)
				m_Player.Stop();
		}

		public string Source
		{
			get;
			set;
		}

		public void SeekTo(int position)
		{
			if (m_Player != null)
				m_Player.SeekTo(position);
		}

		public int GetCurrentPosition()
		{
			if (m_Player != null)
				return m_Player.CurrentPosition;
			else
				return -1;
		}

		public int GetDuration()
		{
			if (m_Player != null)
				return m_Player.Duration;
			else
				return -1;
		}

		public void AddTimedTextSource(string source)
		{
			if (m_Player != null)
			{
				m_Player.AddTimedTextSource(getSubtitleFile(source), MediaPlayer.MediaMimetypeTextSubrip);

				var textTrackIndex = findTrackIndexFor(
					MediaTrackType.Timedtext,
					m_Player.GetTrackInfo());
				System.Diagnostics.Debug.WriteLine("textTrackIndex : " + textTrackIndex);
				if (textTrackIndex >= 0)
				{
					m_Player.SelectTrack(textTrackIndex);
				}
				else {
					System.Diagnostics.Debug.WriteLine( "Cannot find text track!");
				}
				m_Player.SetOnTimedTextListener(this);
				m_Player.TimedText += OnTimedText;

			}
		}



		private int findTrackIndexFor(MediaTrackType mediaTrackType, MediaPlayer.TrackInfo[] trackInfo)
		{
			int index = -1;
			for (int i = 0; i < trackInfo.Length; i++)
			{
				if (trackInfo[i].TrackType == mediaTrackType)
				{
					return i;
				}
			}
			return index;
		}

		private String getSubtitleFile(string filename)
		{
			AssetManager assets = this.Context.Assets;


			string destPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);

			if (!File.Exists(destPath))
			{
				//using (System.IO.Stream stream = assets.Open(filename))
				//{
				//	using (var newfile = File.Create(destPath))
				//	{
				//		long length = new System.IO.FileInfo(destPath).Length;
				//		System.Diagnostics.Debug.WriteLine("copied file length: " + length);
				//		stream.CopyTo(newfile);
				//	}
				//}


				using (StreamReader reader = new StreamReader(assets.Open(filename)))
				{
					using (StreamWriter writer = new StreamWriter(File.Create(destPath)))
					{
						writer.Write(reader.ReadToEnd());
					}
				}

			}
			//long length1 = new System.IO.FileInfo(destPath).Length;
			//System.Diagnostics.Debug.WriteLine("copied file length: " + length1);

			return destPath;
		}

		public void OnTimedText(object sender, MediaPlayer.TimedTextEventArgs args)
		{
			System.Diagnostics.Debug.WriteLine("OnTimedText: " + args.ToString());
		}

		public void OnTimedText(MediaPlayer player, TimedText text)
		{

		}

	}
}
