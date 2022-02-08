using System;

namespace CoxAPITest
{
    public class Address
    {
        public string street { get; init; }
        public string suite { get; init; }
        public string city {get; init;}

        public string zipcode { get; init; }

        public Geo geo { get; init; }
    }
}
