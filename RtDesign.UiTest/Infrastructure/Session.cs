using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using RtDesign.UiTest.Infrastructure.Logging;

namespace RtDesign.UiTest.Infrastructure
{
    public class Session : WindowsDriver<WindowsElement>
    {
        public Session(string pathToAppExe) : base(new Uri("http://127.0.0.1:4723"), new DesiredCapabilities(new Dictionary<string, object> { { "app", pathToAppExe } }))
        {
        }

        public void LogData()
        {
            LogManager.Data.LogSourceAndScreenshot(this);
        }

        public void Wait()
        {
            Thread.Sleep(3000);
        }

        public static void EnsureWinAppDriverRunning(string pathToWinAppDriverExe)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.GetAsync("http://127.0.0.1:4723/status").Wait();
                }
                catch
                {
                    if (pathToWinAppDriverExe != null && File.Exists(pathToWinAppDriverExe))
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = Path.GetFileName(pathToWinAppDriverExe),
                            WorkingDirectory = Path.GetDirectoryName(pathToWinAppDriverExe)
                        };
                        Process.Start(psi);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
