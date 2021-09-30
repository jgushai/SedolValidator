using SedolValidatorInterfaces;

namespace SedolValidator
{
    public class SedolValidator : ISedolValidator
    {
        public ISedolValidationResult ValidateSedol(string input)
        {
            var sedol = new Sedol(input);
            var result = new SedolValidationResult(input,false,false,null);
            

            if (!sedol.IsValidLength)
            {
                result.ValidationDetails = ValidationDetailsConstants.INVALID_INPUT_STRING_LENGTH;
                return result;
            }

            if (!sedol.IsAlphaNumeric)
            {
                result.ValidationDetails = ValidationDetailsConstants.INVALID_CHARACTER;
                return result;
            }

            result.IsUserDefined = sedol.IsUserDefined;
            result.IsValidSedol = sedol.HasValidCheckDigit;

            if (!sedol.HasValidCheckDigit)
                result.ValidationDetails = ValidationDetailsConstants.INVALID_CHECKSUM;            
                
            return result;
        }
    }
}
