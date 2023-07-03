using System.ComponentModel;
using LibraryManagement.Core.Response;

namespace LibraryManagement.Core.Exception
{
    // Provides error handling functionality.
    public class ErrorHandling
    {
        // Represents the success message.
        public static string Success => "Success";

        // Represents the error message.
        public static string Error => "Error";

        // Represents the different types of errors.
        public enum ErrorType
        {
            [DefaultValue("Book Not Found")]
            [Description("Book Not Found")]
            BookNotFound = 201,

            [DefaultValue("User Not Found")]
            [Description("User Not Found")]
            UserNotFound = 202,

            [DefaultValue("Image Upload Error")]
            [Description("Image Upload Error")]
            ImageUploadError = 203,

            [DefaultValue("File Extension Error")]
            [Description("File Extension Error")]
            FileExtensionError = 203,
        }
    }

    // Provides helper methods for working with enums.
    public static class EnumHelper
    {
        // Adds an error of the specified type to the error list.
        public static List<ErrorResponse> Add(this List<ErrorResponse> errorList, ErrorHandling.ErrorType errorType)
        {
            var errorResponse = new ErrorResponse
            {
                Code = (int)errorType,
                Key = errorType.GetKey()
            };
            errorResponse.Description = errorType.GetDescription();
            errorList.Add(errorResponse);
            return errorList;
        }

        // Gets the key value of the enum using the DefaultValue attribute.
        public static string GetKey(this Enum enumarator)
        {
            var enumType = enumarator.GetType();
            var memberInfos = enumType.GetMember(enumarator.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo?.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            var defaultValue = ((DefaultValueAttribute)valueAttributes[0]).Value;
            return defaultValue.ToString();
        }

        // Gets the description of the enum using the Description attribute.
        public static string GetDescription(this Enum enumarator)
        {
            var enumType = enumarator.GetType();
            var memberInfos = enumType.GetMember(enumarator.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)valueAttributes[0]).Description;
            return description;
        }
    }
}
