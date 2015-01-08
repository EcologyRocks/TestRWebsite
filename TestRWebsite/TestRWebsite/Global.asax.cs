using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using TestRWebsite;
using RDotNet;
using RDotNet.NativeLibrary;
using System.IO;

namespace TestRWebsite
{
    public class Global : HttpApplication
    {
        private static string logFilename = "c:\\RSpace\\test\\log.txt";
        public static REngine engine;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Code that runs on application startup
            try
            {
                // set up enviromental path for R.dll      
                REngine.SetEnvironmentVariables();

                engine = REngine.GetInstance();

                // Initializes settings
                engine.Initialize();

                engine.Evaluate("library(Cairo);");
            }
            catch (Exception ex)
            {
                using (StreamWriter streamWriter = new StreamWriter(logFilename, false))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString());
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(ex.ToString());
                    streamWriter.WriteLine();
                }
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            engine.Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
