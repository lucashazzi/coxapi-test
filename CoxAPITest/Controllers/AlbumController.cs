using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CoxAPITest.Methods;

namespace CoxAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAll()
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/albums");

            if(data  == null){
                return NotFound();
            }
            
            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(data);
            
            if(albums.Count <= 0){
                return NotFound();
            }

            return albums;
        }
        
        [HttpGet("id/{id}")]            
          public async Task<ActionResult<Album>> GetById(int id)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/albums");

            if(data  == null){
                return NotFound();
            }

            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(data);

            Album foundAlbum = albums.Find(item => item.id == id);

            if(foundAlbum?.id is null){
                return NotFound();
            }
            return foundAlbum;
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<List<Album>>> GetByTitle(string title)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/albums");

            if(data  == null){
                return NotFound();
            }

            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(data);

            List<Album> foundAlbums = albums.FindAll(item => item.title.Contains(title));

            if(foundAlbums?.Count <= 0){
                return NotFound();
            }

            return foundAlbums;
        }


        [HttpGet("userId/{userId}")]            
        public async Task<ActionResult<List<Album>>> GetByUserId(int userId)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/albums");

            if(data  == null){
                return NotFound();
            }

            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(data);

            List<Album> foundAlbums = albums.FindAll(item => item.userId == userId);

            if(foundAlbums?.Count <= 0){
                return NotFound();
            }
            return foundAlbums;
        }
        

    }
}
