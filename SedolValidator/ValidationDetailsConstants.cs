namespace SedolValidator
{
    public static class ValidationDetailsConstants
    {
        public const string INVALID_INPUT_STRING_LENGTH = "Input string was not 7-characters long";
        public const string INVALID_CHARACTER = "SEDOL contains invalid characters";
        public const string INVALID_CHECKSUM = "Checksum digit does not agree with the rest of the input";
    }
}
