namespace ApiKeyAuthentication.API.Data.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ApiKey> ApiKeys { get; set; }
}
