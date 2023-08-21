namespace DapperRelization.DAL.Entity
{
    public class Tag
    {
        public Guid TagId { get; set; }

        public string? Name { get; set; }
        public List<ShortLink>? ShortLinks { get; set; }
    }
}
