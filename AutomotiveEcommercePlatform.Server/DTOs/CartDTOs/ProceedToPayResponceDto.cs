using AutomotiveEcommercePlatform.Server.DTOs.CarInfoPageDTOs;
using DataBase_LastTesting.Models;

namespace AutomotiveEcommercePlatform.Server.DTOs.CartDTOs
{
    public class ProceedToPayResponceDto
    {
        public int orderId { get; set; }
        public List<Car> cars { get; set; }

    }
}
