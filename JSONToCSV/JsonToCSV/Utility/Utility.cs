using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace JsonToCSV
{
    public static class Utility
    {
        /// <summary>
        /// Write into csv file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        public static void WriteFileCSV(string file, StringBuilder content)
        {
            // Ghi nội dung vào file CSV
            File.WriteAllText(file, content.ToString());
            MessageBox.Show($"Chuyển đổi thành công! File is in { file }");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ReadJSONFile(string filepath)
        {
            // Parse JSON
            long fileSize = new FileInfo(filepath).Length;
            if (fileSize > 200 * 1024 * 1024) //consider when file is over 200MB
            {
                throw new Exception(ConstantMessage.ErrorLargeSize);
            }

            string jsonContent = File.ReadAllText(filepath);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = (int)(fileSize * 1.1);
            var data = serializer.Deserialize<object>(jsonContent);
            List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();
            if (data is IEnumerable<object>)
            {
                elements.AddRange((data as IEnumerable<object>).Cast<Dictionary<string, object>>());
            }
            else if (data is Dictionary<string, object>)
            {
                elements.Add(data as Dictionary<string, object>);
            }
            else
            {
                throw new Exception(ConstantMessage.ErrJSONObject);
            }

            if (elements.Count > 1)
            {
                throw new Exception(ConstantMessage.ErrorContact);
            }
            return elements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatString(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }

        /// <summary>
        /// Displau error message
        /// </summary>
        /// <param name="msg"></param>
        public static void DisplayErrorMessage(string msg)
        {
            MessageBox.Show(msg, ConstantMessage.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
