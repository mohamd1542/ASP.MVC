namespace StudentsMarkWithExcel.Models
{
    public partial class GroupStudent
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public virtual Student? Student { get; set; }
    }

}
