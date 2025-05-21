namespace OperationFirstStrike.Core.Models
{
    public class StrikeOption
    {
        public string UnitName { get; set; }
        public string TargetName { get; set; }
        public string Location { get; set; }
        public bool Success { get; set; }
        public DateTime Time { get; set; }
    }
}
