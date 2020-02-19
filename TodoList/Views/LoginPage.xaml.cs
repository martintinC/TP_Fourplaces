using System.ComponentModel;
using TodoList.ViewModels;
using Storm.Mvvm.Forms;

using Xamarin.Forms;

namespace TodoList.Views
{
    public partial class LoginPage : BaseContentPage
    {
        public LoginPage()
        {
            BindingContext = new LoginPageViewModel();
            InitializeComponent();
        }

    }
}