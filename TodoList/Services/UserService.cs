using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TD.Api.Dtos;


namespace TodoList.Services
{


    public interface IUserService
    {
        Task<string> Login(string mail, string mdp);
        Task<string> Register(string prenom, string nom, string mail, string mdp);
        //Task<string> PostPlace(string title, string description, 0, 0, 0);
    }



    public class UserService : IUserService
    {

        private const string TOKEN = "TOKEN";
        private const string REFRESH_TOKEN = "REFRESH_TOKEN";

        private string _token;
        private string _refresh_token;

        public UserService User { get; private set; }

        public UserService()
        {
        }


        public async Task<string> Login(string mail, string mdp)
        {
            ApiClient api = new ApiClient();

            HttpResponseMessage httpResponse = await api.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", new LoginRequest() { Email = mail, Password = mdp });

            Response<LoginResult> response = await api.ReadFromResponse<Response<LoginResult>>(httpResponse);


            if (response.IsSuccess) 
            {
                _refresh_token = response.Data.RefreshToken;
                _token = response.Data.AccessToken;

                return await Task.FromResult("");
            }
            else
            {
                return await Task.FromResult("Erreur " + response.ErrorCode + " : " + response.ErrorMessage);
            }
        }



        public async Task<string> Register(string prenom, string nom, string mail, string mdp)
        {
            ApiClient api = new ApiClient();

            HttpResponseMessage httpResponse = await api.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register", new RegisterRequest() { FirstName = prenom, LastName = nom, Email = mail, Password = mdp });

            Response<LoginResult> response = await api.ReadFromResponse<Response<LoginResult>>(httpResponse);


            if (response.IsSuccess)
            {
                _refresh_token = response.Data.RefreshToken;
                _token = response.Data.AccessToken;

                return await Task.FromResult("");
            }
            else
            {
                return await Task.FromResult("Erreur " + response.ErrorCode + " : " + response.ErrorMessage);
            }
        }






    }
}
