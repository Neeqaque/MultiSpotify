using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiSpotify.Annotations;
using MultiSpotify.Source;
using MultiSpotify.View;

namespace MultiSpotify.ViewModel
{
    public class MenuViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<SpotifyApiInteraction.PlaylistInfo> _playlists = new ObservableCollection<SpotifyApiInteraction.PlaylistInfo>();
        private object currentPage = new HomePage();
        private int selectedIndex_Home = 0;
        private int selectedIndex_Library = -1;
        private int selectedIndex_Playlists = -1;

        public ICommand OpenHomeCommand { get; set; }
        public ICommand OpenBrowseCommand { get; set; }
        public ICommand OpenRadioCommand { get; set; }
        public ICommand OpenMadeForYouCommand { get; set; }
        public ICommand OpenRecentlyPlayedCommand { get; set; }
        public ICommand OpenLikedSongsCommand { get; set; }
        public ICommand OpenAlbumsCommand { get; set; }
        public ICommand OpenArtistsCommand { get; set; }
        public ICommand OpenPodcastsCommand { get; set; }
        public ICommand OpenPlaylistCommand { get; set; }

        public object CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public int SelectedIndex_Home
        {
            get => selectedIndex_Home;
            set
            {
                selectedIndex_Home = value;
                selectedIndex_Playlists = -1;
                selectedIndex_Library = -1;
                OnPropertyChanged(nameof(selectedIndex_Playlists));
                OnPropertyChanged(nameof(selectedIndex_Home));
                OnPropertyChanged(nameof(selectedIndex_Library));
            }
        }

        public int SelectedIndex_Library
        {
            get => selectedIndex_Library;
            set
            {
                selectedIndex_Home = -1;
                selectedIndex_Library = value;
                selectedIndex_Playlists = -1;
                OnPropertyChanged(nameof(selectedIndex_Playlists));
                OnPropertyChanged(nameof(selectedIndex_Home));
                OnPropertyChanged(nameof(selectedIndex_Library));
            }
        }

        public int SelectedIndex_Playlists
        {
            get => selectedIndex_Playlists;
            set
            {
                selectedIndex_Home = -1;
                selectedIndex_Library = -1;
                selectedIndex_Playlists = value;
                OnPropertyChanged(nameof(selectedIndex_Playlists));
                OnPropertyChanged(nameof(selectedIndex_Home));
                OnPropertyChanged(nameof(selectedIndex_Library));
            }
        }

        public ObservableCollection<SpotifyApiInteraction.PlaylistInfo> Playlists
        {
            get => _playlists;
            set
            {
                _playlists = value;
                OnPropertyChanged(nameof(Playlists));
            }
        }

        public MenuViewModel()
        {
            OpenHomeCommand = new Command(OpenHome);
            OpenBrowseCommand = new Command(OpenBrowse);
            OpenRadioCommand = new Command(OpenRadio);
            OpenPlaylistCommand = new Command(OpenPlaylist);
        }

        private void OpenHome()
        {
            CurrentPage = new HomePage();
        }
        private void OpenBrowse()
        {
            CurrentPage = new BrowsePage();
        }
        private void OpenRadio()
        {
            CurrentPage = new RadioPage();
        }

        private void OpenPlaylist(object playlistInfo)
        {
            ViewModelLocator loc = new ViewModelLocator();
            loc.PlaylistPage.Playlist = playlistInfo as SpotifyApiInteraction.PlaylistInfo;
            CurrentPage = new PlaylistPage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
