using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TodoList.Models;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;


namespace TodoList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<INavigationService> _navigationService;
        private readonly Lazy<IDialogService> _dialogService;

        public ObservableCollection<Todo> TodoList { get;  }

        private Todo _selectedTodo;
        public Todo SelectedTodo
        {
            get => _selectedTodo;
            set
            {
                if (SetProperty(ref _selectedTodo, value) && value != null)
                {
                    EditTodoAction(value);
                    SelectedTodo = null;
                }
            }
        }

        public ICommand CreateTodoCommand { get; }
        

        public MainPageViewModel()
        {
            _todoService = new Lazy<ITodoService>(() => DependencyService.Resolve<ITodoService>());
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

           

            CreateTodoCommand = new Command(CreateTodoAction);
            TodoList = new ObservableCollection<Todo>();



        }

        public override async Task OnResume()
        {
            await base.OnResume();

            
            var todoList = await _todoService.Value.GetLieux();

            TodoList.Clear();
            foreach (var todo in todoList)
            {
                
             
                  TodoList.Add(todo);
                
            }

        }

        public async void CreateTodoAction()
        {
            await _navigationService.Value.PushAsync<CreateOrEditPage>();
        }


        public async void EditTodoAction(Todo todo)
        {
            await _navigationService.Value.PushAsync<CreateOrEditPage>(new System.Collections.Generic.Dictionary<string, object>
            {
                {"Todo",todo }
            });
        }
    }
}
