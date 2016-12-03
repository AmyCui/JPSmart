using System;
namespace JPSmart
{
	public interface IAudioPlayer
	{
		void Reset();
		void Play();
		void Pause();
		void Stop();
		string Source { get; set;}
		int GetDuration();
		void SeekTo(int position);
		int GetCurrentPosition();
		void AddTimedTextSource(string source);
	}
}
