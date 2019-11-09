using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MultiSpotify.Annotations;
using MultiSpotify.Source;

namespace MultiSpotify.ViewModel
{
    class DefaultPage_ViewModel:INotifyPropertyChanged
    {
        private string currentPage = "HomePage.xaml";
        private int selectedIndex_Home = 0;
        private int selectedIndex_Library = -1;
        private int selectedIndex_Playlists = -1;

        private SpotifyApiInteraction.UserInfo _userInfo = new SpotifyApiInteraction.UserInfo();
        private ObservableCollection<SpotifyApiInteraction.PlaylistInfo> _playlists = new ObservableCollection<SpotifyApiInteraction.PlaylistInfo>();
        
        public ObservableCollection<SpotifyApiInteraction.PlaylistInfo> Playlists
        {
            get => _playlists;
            set
            {
                _playlists = value;
                OnPropertyChanged(nameof(Playlists));
            }
        }

        public string Username
        {
            get => _userInfo.display_name;
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
        public string CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public ICommand ChangePageCommand { get; set; }
        public ICommand OpenPlaylistCommand { get; set; }

        public DefaultPage_ViewModel()
        {
            ChangePageCommand = new Command(ChangePage);
            OpenPlaylistCommand = new Command(OpenPlaylist);

            LoadInfo();
        }

        private async void LoadInfo()
        {
            _userInfo = await SpotifyApiInteraction.GetCurrentUserProfile();
            Playlists = (await SpotifyApiInteraction.GetCurrentUserPlaylists()).items;
        }

        private void ChangePage(object newPage)
        {
            CurrentPage = newPage.ToString();
        }

        private void OpenPlaylist(object playlistInfo)
        {
            CurrentPage = "PlaylistPage.xaml";
            (App.Current.Resources["Shared"] as SharedData).SharedPlaylist = playlistInfo as SpotifyApiInteraction.PlaylistInfo;
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
