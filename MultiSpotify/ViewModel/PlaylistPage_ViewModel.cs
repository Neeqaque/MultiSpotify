using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MultiSpotify.Annotations;
using MultiSpotify.Properties;
using MultiSpotify.Source;

namespace MultiSpotify.ViewModel
{
    public class PlaylistPage_ViewModel: INotifyPropertyChanged
    {
        private SpotifyApiInteraction.PlaylistInfo _playlist;
        private List<SpotifyApiInteraction.PlaylistTrackInfo> _tracks;
        private string _tracksDuration;

        public string TracksDuration
        {
            get => _tracksDuration;
            set
            {
                _tracksDuration = value;
                OnPropertyChanged(nameof(TracksDuration));
            }
        }

        public List<SpotifyApiInteraction.PlaylistTrackInfo> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                OnPropertyChanged(nameof(Tracks));
            }
        }
        public SpotifyApiInteraction.PlaylistInfo Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
                LoadTracks();
                OnPropertyChanged(nameof(Playlist));
            }
        }

        public string PlaylistName
        {
            get => Playlist.name;
            set
            {
                Playlist.name = value;
                OnPropertyChanged(nameof(PlaylistName));
            }
        }

        public string PlaylistOwner
        {
            get => Playlist.owner.display_name;
            set
            {
                Playlist.owner.display_name = value;
                OnPropertyChanged(nameof(PlaylistOwner));
            }
        }

        public int SongsCount => Playlist.tracks.total;

        private async void LoadTracks()
        {
            TracksDuration = "";
            Tracks = new List<SpotifyApiInteraction.PlaylistTrackInfo>(await SpotifyApiInteraction.LoadTracks(Playlist.tracks.href));

            int generalDuration = Tracks.Select(x => x.track.duration_ms).Sum();
            int hours = generalDuration / 3600000;
            int minutes = (generalDuration / 60000);
            minutes -= hours * 60;
            int seconds = generalDuration / 1000;
            seconds -= hours * 3600;
            seconds -= minutes * 60;

            if (hours > 0)
            {
                TracksDuration += hours + " hr ";
            }

            if (minutes > 0)
            {
                TracksDuration += minutes + " min ";
            }

            if (hours == 0)
            {
                TracksDuration += seconds + " sec";
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
}
