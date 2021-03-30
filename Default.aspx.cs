using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_FileDownloadCounter
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // string fileName = "csharp.png"; // test için
            string fileName = Context.Request["file"];

            if (fileName.ToString() != "")
            {
                WriteToLog(fileName);
                Download(fileName);
            }
        }

        void Download(string fileName)
        {
            FileInfo fi;
            long n;
            fi = new FileInfo(MapPath("File/" + fileName));
            n = fi.Length;
            Response.Buffer = false;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.ContentType = "application/x-zip-compressed"; // zip için
            Response.ContentType = "image/png"; // image için
            Response.AppendHeader("content-disposition", "attachment;filename=" + fileName);
            Response.AppendHeader("Content-Length", Convert.ToString(n));
            Response.TransmitFile("File/" + fileName);
        }

        private void WriteToLog(string Message)
        {
            string logFileName = AppDomain.CurrentDomain.BaseDirectory + "Log\\Log_" + DateTime.Now.Date.ToShortDateString().Replace("/", "_") + ".txt";
            using (StreamWriter sw = File.AppendText(logFileName))
            {
                sw.WriteLine(DateTime.Now + " => " + Message);
            }
        }
    }
}