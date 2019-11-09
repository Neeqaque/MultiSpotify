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

        public class TracksInfo
        {
            public string href { get; set; }
            public int total { get; set; }
        }

        public class PlaylistsInfo:INotifyPropertyChanged
        {
            private string _href;
            private int _limit;
            private string _next;
            private int _offset;
            private string _previous;
            private int _total;
            private ObservableCollection<PlaylistInfo> _items;
            public string href
            {
                get => _href;
                set
                {
                    _href = value;
                    OnPropertyChanged(nameof(href));
                }
            }
            public int limit
            {
                get => _limit;
                set
                {
                    _limit = value;
                    OnPropertyChanged(nameof(limit));
                }
            }
            public string next
            {
                get => _next;
                set
                {
                    _next = value;
                    OnPropertyChanged(nameof(next));
                }
            }
            public int offset
            {
                get => _offset;
                set
                {
                    _offset = value;
                    OnPropertyChanged(nameof(offset));
                }
            }
            public string previous
            {
                get => _previous;
                set
                {
                    _previous = value;
                    OnPropertyChanged(nameof(previous));
                }
            }
            public int total
            {
                get => _total;
                set
                {
                    _total = value;
                    OnPropertyChanged(nameof(total));
                }
            }
            public ObservableCollection<PlaylistInfo> items
            {
                get
                {
                    if (_items == null)
                    {
                        _items = new ObservableCollection<PlaylistInfo>();
                    }
                    return _items;
                }
                set
                {
                    _items = value;
                    OnPropertyChanged(nameof(items));
                }
            }

            public void UpdateEverything()
            {
                OnPropertyChanged(nameof(items));
                OnPropertyChanged(nameof(total));
                OnPropertyChanged(nameof(previous));
                OnPropertyChanged(nameof(offset));
                OnPropertyChanged(nameof(next));
                OnPropertyChanged(nameof(limit));
                OnPropertyChanged(nameof(href));
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
            private TracksInfo _tracks;
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

            public TracksInfo tracks
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
