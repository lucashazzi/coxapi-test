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
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
    
        public UserController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var cacheKey = "AllUsers";
    
            if(!_memoryCache.TryGetValue(cacheKey, out List<User> cachedUsers))
            {
                var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/users");

                if(data  == null){
                    return NotFound();
                }
                
                List<User> retrievedUsers = JsonSerializer.Deserialize<List<User>>(data);
                
                if(retrievedUsers.Count <= 0){
                    return NotFound();
                }
                
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(25)
                };
                
                _memoryCache.Set(cacheKey, retrievedUsers, cacheExpiryOptions);
                return Ok(retrievedUsers);
            }
            return Ok(cachedUsers);
        }
        
        [HttpGet("id/{id}")]            
        public async Task<ActionResult<User>> GetById(int id)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/users");

            if(data  == null){
                return NotFound();
            }

            List<User> Users = JsonSerializer.Deserialize<List<User>>(data);

            User foundUser = Users.Find(item => item.id == id);

            if(foundUser?.id is null){
                return NotFound();
            }
            return foundUser;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<User>>> GetByName(string name)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/users");

            if(data  == null){
                return NotFound();
            }

            List<User> Users = JsonSerializer.Deserialize<List<User>>(data);

            List<User> foundUsers = Users.FindAll(item => item.name.Contains(name));

            if(foundUsers?.Count <= 0){
                return NotFound();
            } 

            return foundUsers;
        }

        [HttpGet("city/{city}")]            
        public async Task<ActionResult<List<User>>> GetByCity(string city)
        {
            var data = await Client.getBaseContent("https://jsonplaceholder.typicode.com/users");

            if(data  == null){
                return NotFound();
            }

            List<User> Users = JsonSerializer.Deserialize<List<User>>(data);

            List<User> foundUsers = Users.FindAll(item => item.address.city == city);

            if(foundUsers?.Count <= 0){
                return NotFound();
            }

            return foundUsers;
        }
        

    }
}
