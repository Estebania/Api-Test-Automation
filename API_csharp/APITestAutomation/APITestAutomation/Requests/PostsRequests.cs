﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;
using System.Configuration;//Para leer los endpoinds del app.config

namespace APITestAutomation
{
    public class PostsRequest
    {
        private readonly RestClient _client;
       

        public PostsRequest()
        {
            //Endpoint del API de pruebas
            string apiEndPoint = ConfigurationManager.AppSettings["PostApiEndpoint"];
            _client = new RestClient(apiEndPoint);
        }

        public RestResponse GetPosts()
        {
            //creamos un nuevo objeto RestRequest, que representa una solicitud HTTP
            var request = new RestRequest("/posts", Method.Get);

            //ejecutamos la solicitud HTTP utilizando el cliente RestSharp (_client) y pasamos el objeto RestRequest 
            return _client.Execute(request); //El método Execute realiza la solicitud HTTP y devuelve un objeto RestResponse
        }
        //Metodo para enviar el request por el id
        public RestResponse GetPostByID(string id)
        {
            var request = new RestRequest("/posts/"+id , Method.Get);
            return _client.Execute(request);
        }
        //Metodo para resgistrar un nuevo post
        public RestResponse PostNewPost(string title, string body, int userID)
        {
            var request = new RestRequest("/posts", Method.Post);
            //como es un post se envian los campos del request
            request.AddJsonBody(new { title, body, userID });
            return _client.Execute(request);
        }
        //Metodo para Eliminar el post creado
        public RestResponse DeleteNewPost(string idPost)
        {
            var request = new RestRequest("/posts/" + idPost, Method.Delete);
            return _client.Execute(request);
        }
        public RestResponse PutPost(int idPost, string title, string body, int userId)
        {
            var request = new RestRequest($"/posts/{idPost.ToString()}" + idPost, Method.Put);
            request.AddJsonBody(new { idPost, title, body, userId });
            return _client.Execute(request);
        }

        public RestResponse patPost(string title, int idPost)
        {
            var request = new RestRequest($"/posts/{idPost}", Method.Patch);
            request.AddJsonBody(new { title });
            return _client.Execute(request);
        }
        public RestResponse GetPostComment( int idPost)
        {
            var request = new RestRequest($"/posts/{idPost}/comments", Method.Get);
            return _client.Execute(request);
        }


    }
}
