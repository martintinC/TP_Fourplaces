using System;
using System.Windows.Input;
using Newtonsoft.Json;

namespace TodoList.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public int IdApi { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }


        public string Title { get; set; }

        public int ImageId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICommand DeleteTodoCommand { get; internal set; }

        public Todo() { }

    }
}
