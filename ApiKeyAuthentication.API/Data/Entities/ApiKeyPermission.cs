namespace ApiKeyAuthentication.API.Data.Entities
{
    public class ApiKeyPermission
    {
        public Guid ApiKeyId { get; set; }
        public int PermissionId { get; set; }
        public ApiKey ApiKey { get; set; }
        public Permission Permission { get; set; }
    }
}
