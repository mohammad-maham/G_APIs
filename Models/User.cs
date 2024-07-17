namespace G_APIs.Models;

public class User
{

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string NationalCode { get; set; }

    public string Captcha { get; set; }

    public string JWT { get; set; }

}
