using System.ComponentModel.DataAnnotations;

namespace DapperRelization.DAL.Entity
{
    public class ShortLink
    {
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string FullUrl { get; set; } = default!;

        [MaxLength(500)]
        public string ShortUrl { get; set; } = default!;

        public DateTime? CreatedDate { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
