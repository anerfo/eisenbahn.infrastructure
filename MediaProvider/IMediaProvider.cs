using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaProvider
{
    public interface IMediaProvider
    {
        void PlayMusic(string filename, Options options = Options.None);
        void StopAll();

        ISpotify Spotify { get; }
    }
}
