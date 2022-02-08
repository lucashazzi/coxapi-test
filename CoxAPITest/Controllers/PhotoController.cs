using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CoxAPITest.Methods;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CoxAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoController : ControllerBase
    {
        
        private readonly IMemoryCache _memoryCache;
    
        public PhotoController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetAll()
        {
            var cacheKey = "AllPhotos";
    
            if(!_memoryCache.TryGetValue(cacheKey, out List<Photo> cachedPhotos))
            {
                var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/photos");

                if(data  == null){
                    return NotFound();
                }
                
                List<Photo> retrievedPhotos = JsonSerializer.Deserialize<List<Photo>>(data);
                
                if(retrievedPhotos.Count <= 0){
                    return NotFound();
                }
               
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(25)
                };
                
                /* Não consegui desvendar um modo de controlar o retorno da lista.
                Retornar todos os registros irá fazer o browser parar de responder. */

                List<Photo> limitedPhotos = retrievedPhotos;
                if(retrievedPhotos.Count > 1000)
                {
                    limitedPhotos = retrievedPhotos.GetRange(0,1000);
                }

                /*Não entendo como o cache funcionaria aqui, já que o que deixa o retorno lento é o
                retorno da lista de objetos completa */

                _memoryCache.Set(cacheKey, limitedPhotos, cacheExpiryOptions);
                return Ok(limitedPhotos);
            }
            return Ok(cachedPhotos);
        }
        
        [HttpGet("id/{id}")]            
          public async Task<ActionResult<Photo>> GetById(int id)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/photos");

            if(data  == null){
                return NotFound();
            }

            List<Photo> Photos = JsonSerializer.Deserialize<List<Photo>>(data);

            Photo foundPhoto = Photos.Find(item => item.id == id);

            if(foundPhoto?.id is null){
                return NotFound();
            }
            return foundPhoto;
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<List<Photo>>> GetByTitle(string title)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/photos");

            if(data  == null){
                return NotFound();
            }

            List<Photo> Photos = JsonSerializer.Deserialize<List<Photo>>(data);

            List<Photo> foundPhotos = Photos.FindAll(item => item.title.Contains(title));

            if(foundPhotos?.Count <= 0){
                return NotFound();
            }

            return foundPhotos;
        }


        [HttpGet("albumId/{albumId}")]            
        public async Task<ActionResult<List<Photo>>> GetByUserId(int albumId)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/photos");

            if(data  == null){
                return NotFound();
            }

            List<Photo> Photos = JsonSerializer.Deserialize<List<Photo>>(data);

            List<Photo> foundPhotos = Photos.FindAll(item => item.albumId == albumId);

            if(foundPhotos?.Count <= 0){
                return NotFound();
            }
            return foundPhotos;
        }
        

    }
}
