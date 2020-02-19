using System;
using System.Collections.Generic;
using TodoList.ViewModels;
using Xamarin.Forms;
using Storm.Mvvm.Forms;

namespace TodoList.Views
{
    public partial class RegisterPage : BaseContentPage
    {
        public RegisterPage()
        {
            BindingContext = new RegisterPageViewModel();
            InitializeComponent();
        }
    }
}
