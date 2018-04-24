using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneDAL
{
    public class LogFile
    {
        public static void DataFile(string alternateMessage = null, Exception ex = null)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(ConfigurationManager.AppSettings["DataFile"], true);
                if (ex != null)
                {
                    writer.WriteLine(DateTime.Now.ToString() + "  " + ex.Message + "  " + ex.Source + "  " + "Warning");
                    writer.WriteLine(ex.StackTrace);
                    writer.WriteLine();
                }
                if (alternateMessage != null)
                {
                    writer.WriteLine(alternateMessage);
                    writer.WriteLine();
                }
            }
            catch (IOException writerException)
            {
                writer = new StreamWriter("emergencylog.txt", false);
                if (ex != null)
                {
                    writer.WriteLine(DateTime.Now.ToString() + "  " + ex.Message + "  " + ex.Source + "  " + "Warning");
                    writer.WriteLine(ex.StackTrace);
                    writer.WriteLine();
                    writer.WriteLine(writerException.Message);
                }
                if (alternateMessage != null)
                {
                    writer.WriteLine(alternateMessage);
                    writer.WriteLine();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}