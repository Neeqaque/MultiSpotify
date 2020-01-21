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
        public SpotifyApiInteraction.PlaylistInfo Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
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
