namespace online_bookstore.Dtos
{
    public class RoleClaimDto
    {
        public string RoleName { get; set; } = null!;
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;
    }
}
