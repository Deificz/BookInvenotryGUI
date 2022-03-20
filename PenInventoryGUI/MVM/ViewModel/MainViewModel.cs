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
<<<<<<< HEAD
        
=======
        public ModifyViewModel ModifyVM { get; set; }
>>>>>>> 729fc74fcc263445233d36162f88b8cb19d3df04

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
<<<<<<< HEAD
            
=======
            ModifyVM = new ModifyViewModel();
>>>>>>> 729fc74fcc263445233d36162f88b8cb19d3df04
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
<<<<<<< HEAD
           
=======
            ModifyViewCommand = new RelayCommand(o =>
            {
                CurrentView = ModifyVM;
            });
>>>>>>> 729fc74fcc263445233d36162f88b8cb19d3df04
        }
    }
}
