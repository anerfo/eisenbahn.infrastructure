using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MediaProvider
{
    public class SpotifyController : ISpotify
    {
        private string _ClientId;
        private string _ClientSecret;
        private EmbedIOAuthServer _Server;
        private SpotifyClient _SpotifyClient;

        public SpotifyController(string clientId, string clientSecret)
        {
            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _SpotifyClient = null;
            _Server = new EmbedIOAuthServer(new Uri("http://localhost:8080/token"), 8080);
            _Server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
            _Server.ErrorReceived += OnErrorReceived;
            _ = _Server.Start();

            StartLogin();
        }

        private void StartLogin()
        {
            if (string.IsNullOrEmpty(_ClientId) == false)
            {
                var loginRequest = new LoginRequest(_Server.BaseUri, _ClientId, LoginRequest.ResponseType.Code)
                {
                    Scope = new[] { Scopes.UserModifyPlaybackState, Scopes.PlaylistReadPrivate }
                };
                StartBrowser(loginRequest.ToUri());
            }
        }

        private void StartBrowser(Uri uri)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder assemblyUri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(assemblyUri.Path);
            path = Path.GetDirectoryName(path);
            //Process.Start(Path.Combine(path, "Browser.exe"), $"-u {uri} -t \"Eisenbahn authorisieren\"");
            Process.Start(uri.ToString());
        }

        private async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _Server.Stop();

            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
              new AuthorizationCodeTokenRequest(
                _ClientId, _ClientSecret, response.Code, _Server.BaseUri
              )
            );

            _SpotifyClient = new SpotifyClient(tokenResponse.AccessToken);
        }

        private async Task OnErrorReceived(object sender, string error, string state)
        {
            await _Server.Stop();
        }

        public void Next()
        {
            _ = _SpotifyClient.Player.SkipNext();
        }

        public void PlaySong(string artist, string track)
        {
            var trackSearchResult = _SpotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Track, $"{track} artist:{artist}")).Result;
            if (trackSearchResult.Tracks.Items.Count > 0)
            {
                _ = _SpotifyClient.Player.AddToQueue(
                    new PlayerAddToQueueRequest(trackSearchResult.Tracks.Items.FirstOrDefault().Uri)
                );
                Next();
            }
        }

        public void PlayPlaylist(string playlistName)
        {
            var page = _SpotifyClient.Playlists.CurrentUsers().Result;
            SimplePlaylist playlist = null;
            foreach (var current in _SpotifyClient.PaginateAll(page).Result)
            {
                if (current.Name == playlistName)
                {
                    playlist = current;
                    break;
                }
            }
            if (playlist != null)
            {
                _ = _SpotifyClient.Player.ResumePlayback(new PlayerResumePlaybackRequest {
                    ContextUri = playlist.Uri
                });
            }
        }

        public void Stop()
        {
            _ = _SpotifyClient.Player.PausePlayback();
        }
    }
}
