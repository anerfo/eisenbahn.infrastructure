using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using MediaProvider.Extensions;

namespace MediaProvider
{
    public class MediaProvider : PluginManagerLibrary.PluginInterface, IMediaProvider
    {
        private List<WMPLib.WindowsMediaPlayer> _Musicplayers = new List<WMPLib.WindowsMediaPlayer>();
        private SpotifyConfigControl _SpotifyConfigControl = null;
        private Daten.DatenInterface _Data = null;
        private string BaseSearchPath;
        public ISpotify Spotify { get; private set; }

        public string beschreibung
        {
            get { return "Bietet Medien Services an"; }
        }

        public string name
        {
            get { return "Medien Anbieter"; }
        }

        public object[] pluginInitalisieren()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(MediaProvider));
            BaseSearchPath = Path.GetFullPath(Path.Combine(assembly.Location, "..", "..", "..", ".."));
            return new [] { this };
        }

        public void pluginStarten(PluginManagerLibrary.InterfaceFuerPlugins Referenz)
        {
            _Data = Referenz.getReferenceToObject("Daten.DatenInterface", this) as Daten.DatenInterface;
            _SpotifyConfigControl = new SpotifyConfigControl(_Data);
            Referenz.registerConfigControl(_SpotifyConfigControl);
            var clientId = _Data.read_from_table("SpotifyData", 0);
            var clientSecret = _Data.read_from_table("SpotifyData", 1).DecryptString();
            Spotify = new SpotifyController(clientId, clientSecret);
        }

        public void pluginStoppen()
        {
            
        }

        public void StopAll()
        {
            foreach (var musicplayer in _Musicplayers)
            {
                musicplayer.close();
            }
            _Musicplayers.Clear();
        }

        public void PlayMusic(string file, Options options = Options.None)
        {
            if (options.HasFlag(Options.StopAllOthers))
            {
                StopAll();
            }
            var fileToPlay = FindFile(file);
            WMPLib.WindowsMediaPlayer player;
            if (options.HasFlag(Options.Parallel) || _Musicplayers.Count == 0)
            {
                player = new WMPLib.WindowsMediaPlayer();
                _Musicplayers.Add(player);
            }
            else
            {
                player = _Musicplayers.FirstOrDefault();
                if (player.URL != fileToPlay || options.HasFlag(Options.Restart))
                {
                    player.controls.stop();
                }
                else
                {
                    player = null;
                }
            }

            if (player != null)
            {
                player.URL = fileToPlay;
                player.controls.play();
            }
        }

        private string FindFile(string file)
        {
            var result = file;
            string searchPattern = Path.GetFileName(file);
            var findFileResult = Directory.GetFiles(BaseSearchPath, searchPattern, SearchOption.AllDirectories);
            var bestMatch = 0;
            var parts = file.Split(Path.DirectorySeparatorChar);
            foreach (var current in findFileResult)
            {
                var currentParts = current.Split(Path.DirectorySeparatorChar);
                var currentMatch = 0;
                foreach (var part in parts)
                {
                    foreach (var currentPart in currentParts)
                    {
                        if(part.Equals(currentPart, StringComparison.InvariantCultureIgnoreCase))
                        {
                            currentMatch++;
                        }
                    }
                }
                if(currentMatch > bestMatch)
                {
                    bestMatch = currentMatch;
                    result = current;
                }
            }
            return result;
        }
    }
}
