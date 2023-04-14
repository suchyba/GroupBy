namespace GroupBy.Design.TO.Document
{
    public class DocumentCreateDTO
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IEnumerable<Guid> GroupsId { get; set; }
        public Guid? RelatedProjectId { get; set; }
    }
}
