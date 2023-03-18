namespace ApiKeyAuthentication.API.Data.Entities
{
    public class ApiKey
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid ClientId { get; set; }
        public bool Active { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<ApiKeyPermission> Permissions { get; set; }
    }
}
