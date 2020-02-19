using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using TD.Api.Dtos;
using TodoList.Models;

namespace TodoList.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetAllTodos();
        Task DeleteTodo(Todo todo);
        Task CreateTodo(Todo todo);
        Task EditTodo(Todo todo);
        Task<List<Todo>> GetLieux();
    }

    public class TodoService : ITodoService
    {
        private const string TODO_LIST = nameof(TODO_LIST);

        private List<Todo> _todoList;

        public TodoService() { }

        public Task CreateTodo(Todo todo)
        {
            _todoList.Add(todo);
            SaveTodos();
            return Task.CompletedTask;
        }

        public Task DeleteTodo(Todo todo)
        {
            _todoList.Remove(todo);
            SaveTodos();
            return Task.CompletedTask;
        }

        public Task EditTodo(Todo todo)
        {
            //edited by reference
            SaveTodos();
            return Task.CompletedTask;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            await InitializeIfNeeded();
            return _todoList;
        }

       

        private void SaveTodos()
        {
            CrossSettings.Current.AddOrUpdateValue(TODO_LIST, JsonConvert.SerializeObject(_todoList));
        }


        public async Task<List<Todo>> GetLieux()
        {
            await InitializeIfNeeded();
            return await Task.FromResult(_todoList);
        }

        private async Task InitializeIfNeeded()
        {
            if (_todoList is null)
            {



                var serializedLieuList = CrossSettings.Current.GetValueOrDefault(TODO_LIST, string.Empty);


                if (string.IsNullOrEmpty(serializedLieuList))
                {

                    _todoList = new List<Todo>();
                }
                else
                {

                    _todoList = JsonConvert.DeserializeObject<List<Todo>>(serializedLieuList);
                }

                ApiClient api = new ApiClient();

                HttpResponseMessage httpResponse = await api.Execute(HttpMethod.Get,
                    "https://td-api.julienmialon.com/places");

                Response<List<PlaceItem>> response = await api.ReadFromResponse<Response<List<PlaceItem>>>(httpResponse);

                if (httpResponse.IsSuccessStatusCode)
                {
                    foreach (PlaceItem item in response.Data)
                    {

                        _todoList.Add(new Todo()
                        {
                            IdApi = item.Id,
                            Description = item.Description,
                            Title = item.Title,
                            ImageId = item.ImageId,
                            Latitude = item.Latitude,
                            Longitude = item.Longitude
                        });
                    }
                }
            }
        }


    }
}
