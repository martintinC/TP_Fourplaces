using System;

using System.Collections.Generic;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using Storm.Mvvm.Services;
using TodoList.Models;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {

        private Lazy<IUserService> _userService;
        private Lazy<INavigationService> _navigationService;

        public UserService User { get; set; }

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

        private bool _errorMsg;
        public bool ErrorMsg
        {
            get => _errorMsg;
            set
            {
                OnPropertyChanged("ErrorBool");
                SetProperty(ref _errorMsg, value);
            }
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




        ///
        public ICommand ConnectionCommand { get;  }
        public ICommand RegisterCommand { get; }

        public LoginPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _userService = new Lazy<IUserService>(() => DependencyService.Resolve<IUserService>());
            ConnectionCommand = new Command(ConnectionAction);

            RegisterCommand = new Command(RegisterAction);

            IsEnableButton = true;
        }

        public async void RegisterAction()
        {
            await _navigationService.Value.PushAsync<RegisterPage>();
        }

        private async void ConnectionAction()
        {
            IsEnableButton = false;
            UserService userService = new UserService();
            string result = await userService.Login( _mail, _mdp);
            //string result = await _userService.Login("", "");
            
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
