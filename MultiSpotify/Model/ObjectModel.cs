using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using MultiSpotify.Annotations;
using RestSharp;

namespace MultiSpotify
{
    public static partial class SpotifyApiInteraction
    {
        public abstract class PagingObject
        {
            public string href;
            public int limit;
            public string next;
            public int offset;
            public string previous;
            public int total;
        }
        public class UserInfo
        {
            public string country { get; set; }
            public string display_name { get; set; }
            public string email { get; set; }
            public ExplicitContentInfo explicit_content { get; set; }
            public FollowersInfo followers { get; set; }
            public ExternalUrlsInfo external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string product { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
            public List<ImageInfo> images { get; set; }
        }

        public class TracksInfoPaging : PagingObject
        {
            public PlaylistTrackInfo[] items;
        }

        public class LinkingInfo
        {
            public ExternalUrlsInfo external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class PlaylistTrackInfo
        {
            public DateTime added_at { get; set; }
            public UserInfo added_by { get; set; }
            public bool is_local { get; set; }
            public TrackInfo track { get; set; }
        }

        public class DisplayedTrack
        {
            public bool is_liked { get; set; } = false;
            public string name { get; set; }
            public string[] artist_names { get; set; }
            public string[] artist_uris { get; set; }
            public string album_name { get; set; }
            public string album_uri { get; set; }
            public string duration { get; set; }
            public DateTime added_at { get; set; }
            public bool currently_playing { get; set; } = false;
            public string track_uri { get; set; }

            public bool @explicit { get; set; }

            public DisplayedTrack(PlaylistTrackInfo track)
            {
                name = track.track.name;
                artist_uris = track.track.artists.Select(x => x.uri).ToArray();
                artist_names = track.track.artists.Select(x => x.name).ToArray();
                album_name = track.track.album.name;
                album_uri = track.track.album.uri;
                added_at = track.added_at;
                track_uri = track.track.uri;
                @explicit = track.track.@explicit;

                int minutes = track.track.duration_ms / 60000;
                int seconds = track.track.duration_ms / 1000 - minutes * 60;
                duration = minutes + ":" + seconds.ToString("D2");
            }
        }

        public class TrackInfo
        {
            public SimplifiedAlbumInfo album { get; set; }
            public  SimplifiedArtistInfo[] artists { get; set; }
            public string[] avaliable_markets { get; set; }
            public int disc_number { get; set; }
            public int duration_ms { get; set; }
            public bool @explicit { get; set; }
            public ExternalUrlsInfo external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public bool is_playable { get; set; }
            public LinkingInfo linked_from { get; set; }
            public string name { get; set; }
            public int popularity { get; set; }
            public string preview_url { get; set; }
            public int track_number { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class PlaylistTracksInfo
        {
            public string href { get; set; }
            public int total { get; set; }
        }

        public class PlaylistInfoPaging : PagingObject
        {
            
            public ObservableCollection<PlaylistInfo> items;
        }

        public class PlaylistInfo:INotifyPropertyChanged
        {
            private bool _collaborative;
            private string _href;
            private string _id;
            private ObservableCollection<ImageInfo> _images;
            private string _name;
            private UserInfo _owner;
            private bool _public;
            private string _snapshot_id;
            private PlaylistTracksInfo _tracks;
            private string _type;
            private string _uri;
            public bool collaborative
            {
                get => _collaborative;
                set
                {
                    _collaborative = value;
                    OnPropertyChanged(nameof(collaborative));
                }
            }
            public string href
            {
                get => _href;
                set
                {
                    _href = value;
                    OnPropertyChanged(nameof(href));
                }
            }
            public string id
            {
                get => _id;
                set
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
            public ObservableCollection<ImageInfo> images
            {
                get => _images;
                set
                {
                    _images = value;
                    OnPropertyChanged(nameof(images));
                }
            }
            public string name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
            public UserInfo owner
            {
                get => _owner;
                set
                {
                    _owner = value;
                    OnPropertyChanged(nameof(owner));
                }
            }
            public bool @public
            {
                get => _public;
                set
                {
                    _public = value;
                    OnPropertyChanged(nameof(@public));
                }
            }

            public string snapshot_id
            {
                get => _snapshot_id; 
                set
                {
                    _snapshot_id = value;
                    OnPropertyChanged(nameof(snapshot_id));
                }
            }

            public PlaylistTracksInfo tracks
            {
                get => _tracks;
                set
                {
                    _tracks = value;
                    OnPropertyChanged(nameof(tracks));
                }
            }
            public string type
            {
                get => _type;
                set
                {
                    _type = value;
                    OnPropertyChanged(nameof(type));
                }
            }

            public string uri
            {
                get => _uri;
                set
                {
                    _uri = value;
                    OnPropertyChanged(nameof(uri));
                }
            }

            #region Interface realization

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }

        public class ImageInfo
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public BitmapImage Image => new BitmapImage(new Uri(url));
        }

        public class FollowersInfo
        {
            public string href { get; set; }
            public int total { get; set; }
        }

        public class ExternalUrlsInfo
        {
            public string spotify { get; set; }
        }

        public class ExplicitContentInfo
        {
            public bool filter_enabled { get; set; }
            public bool filter_locked { get; set; }
        }

        public class SimplifiedAlbumInfo
        {
            public string album_group { get; set; }
            public string album_type { get; set; }
            public SimplifiedArtistInfo[] artists { get; set; }
            public string[] avaliable_markets { get; set; }
            public ExternalUrlsInfo external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public ImageInfo[] images { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
            public string release_date_precision { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class SimplifiedArtistInfo
        {
            public ExternalUrlsInfo external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        [Serializable]
        public class AccessTokenAuth
        {
            private string _token;
            public string access_token
            {
                get { return _token; }
                set
                {
                    receiveTime = DateTime.Now;
                    _token = value;
                }
            }
            public string token_type { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }

            public DateTime receiveTime { get; private set; }
        }

    }
}
