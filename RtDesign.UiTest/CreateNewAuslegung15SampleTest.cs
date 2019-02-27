using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RtDesign.UiTest.Infrastructure;
using RtDesign.UiTest.Pages;

namespace RtDesign.UiTest
{
    [TestClass]
    public class OpenAuslegung15Test
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Session.EnsureWinAppDriverRunning(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
        }

        [TestMethod]
        public void OpenAuslegung15()
        {
            using (var session = new Session(@"D:\Projects\RAG\RSP_trunk_UI_Automation\Quellcode\RTD\bin\Debug\RTDesign.exe"))
            {
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                session.Manage().Window.Maximize();

                var auslegungssessionListScreen = AuslegungssessionListScreen.Find(session);

                var serverSelectionPanel = auslegungssessionListScreen.GetServerSelectionPanel();
                serverSelectionPanel.SelectServer("Local");

                var search = auslegungssessionListScreen.GetAuslegungssessionSearch();
            }
        }
    }
}
