namespace src.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Member> Members { get; set; }
    }
}
