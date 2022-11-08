using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class User
    {
        [Column("order_id")]
        public uint Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("age")]
        public byte Age { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
