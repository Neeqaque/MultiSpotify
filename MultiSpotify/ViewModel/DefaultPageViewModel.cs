﻿using System;
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
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Expression.Interactivity.Core;
using MultiSpotify.Annotations;
using MultiSpotify.Source;
using MultiSpotify.View;

namespace MultiSpotify.ViewModel
{
    public class DefaultPageViewModel:INotifyPropertyChanged
    {
        private SpotifyApiInteraction.UserInfo _userInfo = new SpotifyApiInteraction.UserInfo();

        public MenuViewModel MenuVM
        {
            get;
        }
        public string Username
        {
            get => _userInfo.display_name;
        }
        public DefaultPageViewModel()
        {
            MenuVM = new MenuViewModel();
            LoadInfo();
        }

        private async void LoadInfo()
        {
            _userInfo = await SpotifyApiInteraction.GetCurrentUserProfile();
            MenuVM.Playlists = new ObservableCollection<SpotifyApiInteraction.PlaylistInfo>(await SpotifyApiInteraction.GetCurrentUserPlaylists());
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
