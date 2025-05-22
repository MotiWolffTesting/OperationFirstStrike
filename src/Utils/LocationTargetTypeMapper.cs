namespace OperationFirstStrike.Utils
{
    public static class LocationTargetTypeMapper
    {
        public static string GetTargetType(string location)
        {
            return location switch
            {
                "home" => "Building",
                "in a car" => "Vehicle",
                "outside" => "OpenArea",
                _ => "Unknown"

            };
        }
    }
}