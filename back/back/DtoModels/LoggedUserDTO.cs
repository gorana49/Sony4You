﻿namespace back.DtoModels
{
    public class LoggedUserDTO
    {
        private bool v1;
        private string v2;

        public LoggedUserDTO(int id, string username, string password, bool v1, string v2)
        {
            Id = id;
            Username = username;
            Password = password;
            this.v1 = v1;
            this.v2 = v2;
        }

        public LoggedUserDTO()
        {

        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }
        public string Role { get; set; }
    }
}
