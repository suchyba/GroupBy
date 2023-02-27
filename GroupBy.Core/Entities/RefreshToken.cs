using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Expires { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedByIP { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIP { get; set; }
        public virtual RefreshToken ReplacedByToken { get; set; }
        public string ReasonRevoked { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
