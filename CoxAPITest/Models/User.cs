using System;

namespace CoxAPITest
{
    public class User
    {
        public int id { get; init; }
        public string name {get; init; }
        public string username { get; init; }
        public string phone { get; init; }
        public string website { get; init; }
        public string email { get; init; }
        public Company company { get; init; }
        public Address address { get; init; }
    }
}
