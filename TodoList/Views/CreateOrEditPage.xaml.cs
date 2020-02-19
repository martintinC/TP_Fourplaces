using Storm.Mvvm.Forms;
using TodoList.ViewModels;

namespace TodoList.Views
{
    public partial class CreateOrEditPage : BaseContentPage
    {
        public CreateOrEditPage()
        {
            BindingContext = new CreateOrEditPageViewModel();
            InitializeComponent();
        }
    }
}
