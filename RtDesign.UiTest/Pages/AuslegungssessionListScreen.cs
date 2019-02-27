using OpenQA.Selenium.Appium.Windows;
using RtDesign.UiTest.Infrastructure;

namespace RtDesign.UiTest.Pages
{
    public class AuslegungssessionListScreen : Page
    {
        public AuslegungssessionListScreen(Session session, WindowsElement element)
            : base(session, element)
        {
        }

        public static AuslegungssessionListScreen Find(Session session)
        {
            var element = session.FindElementByClassName("AuslegungssessionListScreen");
            return new AuslegungssessionListScreen(session, element);
        }

        public ServerSelectionPanel GetServerSelectionPanel()
        {
            var element = Element.FindElementByAccessibilityId("ServerSelectionPanel") as WindowsElement;
            return new ServerSelectionPanel(Session, element);
        }

        public AuslegungssessionSearch GetAuslegungssessionSearch()
        {
            var element = Element.FindElementByClassName("AuslegungssessionSearch") as WindowsElement;
            return new AuslegungssessionSearch(Session, element);
        }
    }
}
