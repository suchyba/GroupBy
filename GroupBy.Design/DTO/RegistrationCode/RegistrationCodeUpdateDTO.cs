namespace GroupBy.Design.TO.RegistrationCode
{
    public class RegistrationCodeUpdateDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid TargetGroupId { get; set; }
        public Guid? TargetRankId { get; set; }
    }
}
