using System;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {

        private Lazy<IUserService> _userService;
        private Lazy<INavigationService> _navigationService;

        private string _mail;
        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _mdp;
        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        private string _prenom;
        public string Prenom
        {
            get => _prenom;
            set => SetProperty(ref _prenom, value);
        }

        private string _nom;
        public string Nom
        {
            get => _nom;
            set => SetProperty(ref _nom, value);
        }

        private bool _isEnableButton;
        public bool IsEnableButton
        {
            get => _isEnableButton;
            set
            {
                OnPropertyChanged("IsEnableButton");
                SetProperty(ref _isEnableButton, value);
            }
        }

        private bool _errorBool;
        public bool ErrorBool
        {
            get => _errorBool;
            set
            {
                OnPropertyChanged("ErrorBool");
                SetProperty(ref _errorBool, value);
            }
        }



        public ICommand RegisterCommand { get; }

        public RegisterPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _userService = new Lazy<IUserService>(() => DependencyService.Resolve<IUserService>());

            RegisterCommand = new Command(RegisterAction);

            IsEnableButton = true;
            ErrorBool = false;
        }



        private async void RegisterAction()
        {
            IsEnableButton = false;
            //await _navigationService.Value.PushAsync<RegisterPage>();
            string result = await _userService.Value.Register(Prenom, Nom, Mail, Mdp);

            if (result == null || result == "")
            {
                await _navigationService.Value.PushAsync<MainPage>();
            }
            else
            {
                ErrorBool = true;
                //ErrorMsg = result;
            }
            IsEnableButton = true;
        }
    }
}
