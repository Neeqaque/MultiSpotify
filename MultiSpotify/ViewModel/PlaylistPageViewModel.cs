using MultiSpotify.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MultiSpotify.Source;

namespace MultiSpotify.ViewModel
{
    public class PlaylistPageViewModel : INotifyPropertyChanged
    {
        private SpotifyApiInteraction.PlaylistInfo _playlist;
        ICommand _playSongCommand;
        private List<SpotifyApiInteraction.PlaylistTrackInfo> _tracks;
        private ObservableCollection<SpotifyApiInteraction.DisplayedTrack> _displayedTracks = new ObservableCollection<SpotifyApiInteraction.DisplayedTrack>();
        private string _tracksDuration;
        private double _playlistVerticalOffset;
        private Visibility _upperPanelVisibility = Visibility.Hidden;

        public ICommand PlaySongCommand
        {
            get => _playSongCommand;
            set
            {
                _playSongCommand = value;
                OnPropertyChanged(nameof(PlaySongCommand));
            }
        }

        public Visibility UpperPanelVisibility
        {
            get => _upperPanelVisibility;
            set
            {
                _upperPanelVisibility = value;
                OnPropertyChanged(nameof(UpperPanelVisibility));
            }
        }

        public double PlaylistVerticalOffset
        {
            get => _playlistVerticalOffset;
            set
            {
                _playlistVerticalOffset = value;
                if (_playlistVerticalOffset > 200)
                {
                    UpperPanelVisibility = Visibility.Visible;
                }
                else
                {
                    UpperPanelVisibility = Visibility.Hidden;
                }
                OnPropertyChanged(nameof(PlaylistVerticalOffset));
            }
        }

        public string TracksDuration
        {
            get => _tracksDuration;
            set
            {
                _tracksDuration = value;
                OnPropertyChanged(nameof(TracksDuration));
            }
        }

        public ObservableCollection<SpotifyApiInteraction.DisplayedTrack> DisplayedTracks
        {
            get => _displayedTracks;
            set
            {
                _displayedTracks = value;
                OnPropertyChanged(nameof(DisplayedTracks));
            }
        }

        public List<SpotifyApiInteraction.PlaylistTrackInfo> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                DisplayedTracks.Clear();
                foreach (SpotifyApiInteraction.PlaylistTrackInfo track in _tracks)
                {
                    DisplayedTracks.Add(new SpotifyApiInteraction.DisplayedTrack(track));
                }
                OnPropertyChanged(nameof(DisplayedTracks));
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

        public PlaylistPageViewModel()
        {
            PlaySongCommand = new Command(PlaySong);
        }

        private void PlaySong(object track)
        {

        }

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
