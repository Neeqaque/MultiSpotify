using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using MultiSpotify.Annotations;
using MultiSpotify.Model;
using MultiSpotify.Source;

namespace MultiSpotify.ViewModel
{
    class DefaultPage_ViewModel:INotifyPropertyChanged
    {
        
        private DefaultPage_Model model;

        public int SelectedIndexUpper
        {
            get => model.selectedIndexUpper;
            set
            {
                model.selectedIndexUpper = value;
                model.selectedIndexLower = -1;
                OnPropertyChanged(nameof(SelectedIndexLower));
                OnPropertyChanged(nameof(SelectedIndexUpper));
            }
        }

        public int SelectedIndexLower
        {
            get => model.selectedIndexLower;
            set
            {
                model.selectedIndexUpper = -1;
                model.selectedIndexLower = value;
                OnPropertyChanged(nameof(SelectedIndexLower));
                OnPropertyChanged(nameof(SelectedIndexUpper));
            }
        }
        public string CurrentPage
        {
            get => model.currentPage;
            set
            {
                model.currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public ICommand ChangePageCommand { get; set; }

        public DefaultPage_ViewModel()
        {
            model = new DefaultPage_Model();
            ChangePageCommand = new Command(ChangePage);
        }

        private void ChangePage(object newPage)
        {
            CurrentPage = newPage.ToString();
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
