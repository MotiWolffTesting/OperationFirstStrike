namespace OperationFirstStrike.Utils
{
    // Utility class that maps location descriptions to target types
    // Used to determine the appropriate strike unit for a given location
    public static class LocationTargetTypeMapper
    {
        // Maps a location description to its corresponding target type
        // Returns the target type that best matches the location for strike planning
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