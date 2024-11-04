using BoreWellManager.Data.Enums;

namespace BoreWellManager.WebApi.Jwt
{
    public class JwtDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public bool IsResponsible {  get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }

    }
}
