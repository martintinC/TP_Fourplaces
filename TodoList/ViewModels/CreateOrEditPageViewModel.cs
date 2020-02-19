using System;
using System.Collections.Generic;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using Storm.Mvvm.Services;
using TodoList.Models;
using TodoList.Services;
using Xamarin.Forms;


namespace TodoList.ViewModels
{
    public class CreateOrEditPageViewModel : ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;
        private Lazy<ITodoService> _todoService;

        [NavigationParameter("Todo")]
        public Todo Todo { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private Todo _lieu;
        public Todo Lieu
        {
            get => _lieu;
            set => SetProperty(ref _lieu, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _source;
        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        public ICommand ValidateCommand { get; }

        public CreateOrEditPageViewModel()
        {
            _todoService = new Lazy<ITodoService>(() => DependencyService.Resolve<ITodoService>());
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());

            ValidateCommand = new Command(ValidateAction);
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            if (Todo != null)
            {
                PageName = "Édition";
                
            } else
            {
                PageName = "Création";
            }
        }

        private async void ValidateAction()
        {
            if (Todo != null)
            {
                await _todoService.Value.EditTodo(Todo);
            }
            else
            {
                //var todo = new Todo(Name, Description, Source);
                //await _todoService.Value.CreateTodo(todo);
            }

            await _navigationService.Value.PopAsync();
        }
    }
}
