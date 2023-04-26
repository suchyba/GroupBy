namespace GroupBy.Design.DTO.Project
{
    public class ProjectUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public bool Independent { get; set; }
        public Guid? ProjectGroupId { get; set; }
        public Guid ParentGroupId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
