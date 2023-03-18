namespace ApiKeyAuthentication.API.Data.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<ApiKeyPermission> ApiKeys { get; set; }
    }
}
