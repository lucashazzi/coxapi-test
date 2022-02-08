using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace CoxAPITest.Methods
{
    public static class Client
    {
        public static async Task<string> getBaseContent(string baseUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if(data is null || data == "{}") return null;
                            return data;
                        }
                    }
                }
            } catch(Exception exception)
            {
                Console.WriteLine("----- Client Exception -----\n");
                Console.WriteLine(exception);
                return null;
            }
        }
    }
    
}