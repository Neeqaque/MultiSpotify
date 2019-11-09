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
    class PlaylistPage_ViewModel:INotifyPropertyChanged
    {
        private SpotifyApiInteraction.PlaylistInfo _playlist;
        public SpotifyApiInteraction.PlaylistInfo Playlist
        {
            get => (App.Current.Resources["Shared"] as SharedData).SharedPlaylist ?? new SpotifyApiInteraction.PlaylistInfo();
            set
            {
                (App.Current.Resources["Shared"] as SharedData).SharedPlaylist = value;
                OnPropertyChanged(nameof(Playlist));
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
