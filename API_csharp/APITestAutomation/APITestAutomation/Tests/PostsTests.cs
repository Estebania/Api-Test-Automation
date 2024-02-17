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
            var postsRequests = new PostsRequest();
            var response = postsRequests.GetPosts();

            // Agrega aquí las aserciones para verificar la respuesta
            Console.WriteLine(response.Content);
            Assert.That(response.IsSuccessful);

           

        }
        [Test]
        public void GetStatusCodeTest()
        {
            var postsRequests = new PostsRequest();
            var response = postsRequests.GetPosts();

            // Agrega aquí las aserciones para verificar la respuesta
            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            // Verifica si la respuesta de la solicitud HTTP fue exitosa (código de estado 200)
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);


        }
        [Test]
        public void GetPostByIdTest()
        {
            var postsRequests = new PostsRequest();
            string id = "38";
            var response = postsRequests.GetPostByID(id);

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
            var postsRequests = new PostsRequest();
            string title = "QA REGRESION POST";
            string body = "Esto es solo un ejercicio";
            int userID = 12;

            var response = postsRequests.PostNewPost(title,body, userID);

            
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
            var postsRequests = new PostsRequest();

            var response = postsRequests.DeleteNewPost(this._idpost.ToString());


            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response
            var jsonResponse = JObject.Parse(response.Content);


            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);



        }
        [Test]
        public void PutPostTest()
        {
            var postsRequests = new PostsRequest();
            string title = "QA REGRESION POST";
            string body = "Esto es solo un ejercicio";
            int userID = 12;
            var response = postsRequests.PutPost(this._idpost,title,body,userID);


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
            var postsRequests = new PostsRequest();
            string title = "QA REGRESION PATCH";
            int id = 1;
            string body = "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto";
            int userID = 1;
            var response = postsRequests.patPost(title, id);


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
        [Test]
        public void GetCommentsTest()//en este validaremos una respuesta con varios nodos
        {
            var postsRequests = new PostsRequest();
            int id = 1;
            var response = postsRequests.GetPostComment(id);


            Console.WriteLine(response.Content);
            Console.WriteLine(response.StatusCode);
            //obteniendo los campos del response

            var jsonResponse = JArray.Parse(response.Content); //En este caso utilizamos un array para almacenar

            bool isIdCorrect = jsonResponse.All(item => (int)item["postId"] == 1);
            Assert.IsTrue(isIdCorrect,$"Al menos un elmento no pertenece al post con el id: {id}");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);



        }

    }
}
