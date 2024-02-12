using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.DTOs.CartDTOs
{
    public class GetCartItemsDto
    {
        public List<Car> cars { get; set; }
        public string Error { get; set; }

        public bool result { get;set; }
    }
}
