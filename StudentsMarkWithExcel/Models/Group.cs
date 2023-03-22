namespace StudentsMarkWithExcel.Models
{
    public class Group
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public virtual ICollection<GroupStudent> GroupStudent { get; } = new List<GroupStudent>();

    }
}
