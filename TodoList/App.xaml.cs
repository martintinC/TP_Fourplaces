using Storm.Mvvm;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new LoginPage())
        {
            InitializeComponent();
            DependencyService.Register<ITodoService, TodoService>();
            DependencyService.Register<IUserService, UserService>();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
