namespace back.DtoModels
{
    public class LoggedUserDTO
    {
        public LoggedUserDTO(string username, string password, bool logged, string role)
        {
            Username = username;
            Password = password;
            LoggedIn = logged;
            Role = role;
        }
        public LoggedUserDTO() { }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoggedIn { get; set; }
        public string Role { get; set; }
    }
}
