namespace Sports.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public List<Comment> Comments { get;set; }
    }
}
