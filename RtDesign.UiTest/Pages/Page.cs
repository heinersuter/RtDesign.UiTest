using OpenQA.Selenium.Appium.Windows;
using RtDesign.UiTest.Infrastructure;

namespace RtDesign.UiTest.Pages
{
    public abstract class Page
    {
        protected Page(Session session, WindowsElement element)
        {
            Session = session;
            Element = element;
            Session.LogData();
        }

        public Session Session { get; }

        public WindowsElement Element { get; }

    }
}
