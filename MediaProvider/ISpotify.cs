namespace MediaProvider
{
    public interface ISpotify
    {
        void PlaySong(string artist, string track);
        void PlayPlaylist(string playlist);
        void Next();
        void Stop();
    }
}
