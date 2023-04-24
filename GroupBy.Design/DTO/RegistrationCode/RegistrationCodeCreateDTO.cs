namespace GroupBy.Design.TO.RegistrationCode
{
    public class RegistrationCodeCreateDTO
    {
        public string Name { get; set; }
        public Guid TargetGroupId { get; set; }
        public Guid? TargetRankId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
