namespace GroupBy.Design.TO.Document
{
    public class DocumentUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public Guid? RelatedProjectId { get; set; }
    }
}
