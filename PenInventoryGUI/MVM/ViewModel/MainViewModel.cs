using PenInventoryGUI.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PenInventoryGUI.MVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand AddViewCommand { get; set; }
        public RelayCommand StatusViewCommand { get; set; }
        public RelayCommand RemoveViewCommand { get; set; }
        public RelayCommand ModifyViewCommand{get;set;}

        public AddViewModel AddVM { get; set; }
        public StatusViewModel StatusVM { get; set; }
        public RemoveViewModel RemoveVM { get; set; }
        public ModifyViewModel ModifyVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
           

            AddVM = new AddViewModel();
            StatusVM = new StatusViewModel();
            RemoveVM = new RemoveViewModel();
            ModifyVM = new ModifyViewModel();
            CurrentView = StatusVM;

            StatusViewCommand = new RelayCommand(o =>
              {
                  CurrentView = StatusVM;
              });
            AddViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddVM;
            });
            RemoveViewCommand = new RelayCommand(o =>
            {
                CurrentView = RemoveVM;
            });
            ModifyViewCommand = new RelayCommand(o =>
            {
                CurrentView = ModifyVM;
            });
        }
    }
}
