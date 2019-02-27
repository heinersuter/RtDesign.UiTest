using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using RtDesign.UiTest.Infrastructure;

namespace RtDesign.UiTest.Pages
{
    public class ServerSelectionPanel : Page
    {
        public ServerSelectionPanel(Session session, WindowsElement element)
            : base(session, element)
        {
        }

        public void SelectServer(string connectionStringName)
        {
            var comboBox = Element.FindElementByClassName("ComboBox");
            comboBox.Click();

            Session.LogData();

            Session.Keyboard.SendKeys(Keys.ArrowUp);

            var dbItem = Element.FindElementByName(connectionStringName);
            dbItem.Click();

            var verbindenButton = Element.FindElementByName("Verbinden");
            verbindenButton.Click();
        }
    }
}
