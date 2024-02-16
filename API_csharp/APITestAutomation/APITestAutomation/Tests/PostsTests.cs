using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace APITestAutomation.Tests
{
    [TestFixture]
    public class PostsTests
    {
        private int _idpost; //para elimara el post creado
        [Test]
        public void GetPostsTest()
        {
            var postsPage = new PostsPage();
            var response = postsPage.GetPosts();

            // Agrega aquí las aserciones para verificar la respuesta
            Console.WriteLine(response.Content);
            Assert.That(response.IsSuccessful);

           

        }
        [Test]
        public void GetStatusCodeTest()
        {
            var postsPage = new PostsPage();
            var response = postsPage.GetPosts();

            // Agrega aquí las aserciones para verificar la respuesta
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            // Verifica si la respuesta de la solicitud HTTP fue exitosa (código de estado 200)
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);


        }
        [Test]
        public void GetPostByIdTest()
        {
            var postsPage = new PostsPage();
            string id = "38";
            var response = postsPage.GetPostByID(id);

            // Agrega aquí las aserciones para verificar la respuesta
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);

            int idPost = jsonResponse["id"].Value<int>();
            Assert.AreEqual(int.Parse(id), idPost); //Validamos que sea el dato esperado


        }

        [Test]
        public void PostNewTest()
        {
            var postsPage = new PostsPage();
            string title = "QA REGRESION POST";
            string body = "Esto es solo un ejercicio";
            int userID = 12;

            var response = postsPage.PostNewPost(title,body, userID);

            
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);

            string postTitle = jsonResponse["title"].Value<string>();
            string postBody = jsonResponse["body"].Value<string>();
            int userIdPost = jsonResponse["userID"].Value<int>();

            this._idpost = jsonResponse["id"].Value<int>();

            Assert.AreEqual(title, postTitle); //Validamos que sea el dato esperado
            Assert.AreEqual(body, postBody);
            Assert.AreEqual(userID, userIdPost);


        }
        [Test]
        public void DeleteNewPostTest()
        {
            var postsPage = new PostsPage();

            var response = postsPage.DeleteNewPost(this._idpost.ToString());


            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);


            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);



        }
        [Test]
        public void PutPostTest()
        {
            var postsPage = new PostsPage();
            string title = "QA REGRESION POST";
            string body = "Esto es solo un ejercicio";
            int userID = 12;
            var response = postsPage.PutPost(this._idpost,title,body,userID);


            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);


            
            string postTitle = jsonResponse["title"].Value<string>();
            string postBody = jsonResponse["body"].Value<string>();
            int userIdPost = jsonResponse["userID"].Value<int>();

            this._idpost = jsonResponse["id"].Value<int>();

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(title, postTitle); //Validamos que sea el dato esperado
            Assert.AreEqual(body, postBody);
            Assert.AreEqual(userID, userIdPost);


        }
        [Test]
        public void ParchPostTest()
        {
            var postsPage = new PostsPage();
            string title = "QA REGRESION PATCH";
            int id = 1;
            string body = "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto";
            int userID = 1;
            var response = postsPage.patPost(title, id);


            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);


            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);

            string postTitle = jsonResponse["title"].Value<string>();
            string postBody = jsonResponse["body"].Value<string>();
            int userIdPost = jsonResponse["userId"].Value<int>();
            int idPost = jsonResponse["id"].Value<int>();
           

            //Validamos que sea el dato esperado
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(id, idPost);
            Assert.AreEqual(title, postTitle); 
            Assert.AreEqual(body, postBody);
            Assert.AreEqual(userID, userIdPost);


        }


    }
}
