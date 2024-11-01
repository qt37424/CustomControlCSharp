using System;
using System.Collections.Generic;

namespace JsonToCSV
{
    public class ConstantMessage
    {
        /// <summary>
        /// For title INFORMATION of message
        /// </summary>
        public const string TitleInfo = "INFORMATION";

        /// <summary>
        /// For title WARNING of message
        /// </summary>
        public const string TitleWarning = "WARNING";

        /// <summary>
        /// For title ERROR of message
        /// </summary>
        public const string TitleError = "ERROR";

        /// <summary>
        /// File is not valid
        /// </summary>
        public const string FileIsNotValid = "You input the invalid file!";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorLoading = "Some error occurs: {0}";
        /// <summary>
        /// 
        /// </summary>
        public const string ErrorContact = "There is an error! Please contact to QuangTT12!";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrorLargeSize = "File is too large!";

        /// <summary>
        /// 
        /// </summary>
        public const string ErrJSONObject = "JSON root is an object or an array object.";

        /// <summary>
        /// 
        /// </summary>
        public const string ProcessSuccess = "Successfully";
    }
}
