using System.ComponentModel;
using System.Runtime.CompilerServices;
using MultiSpotify.Annotations;

namespace MultiSpotify.Source
{
    public class SharedData : INotifyPropertyChanged
    {
        private SpotifyApiInteraction.PlaylistInfo _sharedPlaylist;
        public SpotifyApiInteraction.PlaylistInfo SharedPlaylist
        {
            get => _sharedPlaylist ?? new SpotifyApiInteraction.PlaylistInfo();
            set
            {
                _sharedPlaylist = value;
                OnPropertyChanged(nameof(SharedPlaylist));
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
