using DataBase_LastTesting.Models;
using Newtonsoft.Json;

namespace AutomotiveEcommercePlatform.Server.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set;}
        public string State { get; set;}
        public int ZipCode { get; set;}
        public string DisplayAddress { get; set; }
        public string UserId { get; set;}

        
        //Nav
        [JsonIgnore]
        public User User { get; set; }
    }

}
