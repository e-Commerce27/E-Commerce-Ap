namespace E_Commerce_App.Models.DTO
{
    public class UserDTO    
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public IList<string> Roles { get; internal set; }
    }
}
