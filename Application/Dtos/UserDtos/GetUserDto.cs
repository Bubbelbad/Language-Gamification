﻿namespace Application.Dtos.UserDtos
{
    public class GetUserDto
    {
        public GetUserDto() { }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string TotalPoints { get; set; }
    }
}
