﻿namespace AutomotiveEcommercePlatform.Server.DTOs
{
    public class UserRegistrationRequestDto
    {

        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmePassword { get; set; }


    }
}