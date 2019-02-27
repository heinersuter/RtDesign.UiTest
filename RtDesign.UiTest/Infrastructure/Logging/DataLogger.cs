using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OpenQA.Selenium;

namespace RtDesign.UiTest.Infrastructure.Logging
{
    public class DataLogger
    {
        private int _fileIndex;

        public DataLogger()
        {
            if (Directory.Exists(OutputDirectory))
            {
                try
                {
                    Directory.Delete(OutputDirectory, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Directory.CreateDirectory(OutputDirectory);
        }

        public string OutputDirectory { get; } = Path.GetFullPath("CollectedData");

        public void LogSourceAndScreenshot(Session session)
        {
            var fileIndex = _fileIndex++;
            //SaveScreenshot(session, fileIndex);
            SavePageSource(session, fileIndex);
        }

        private void SaveScreenshot(Session session, int fileIndex)
        {
            try
            {
                var screenshot = session.GetScreenshot();
                screenshot.SaveAsFile(Path.Combine(OutputDirectory, $"{fileIndex:D5}_Screenshot.png"),
                    ScreenshotImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SavePageSource(Session session, int fileIndex)
        {
            try
            {
                var pageSource = session.PageSource;
                //Task.Run(() =>
                //{
                pageSource = pageSource
                    .Replace(" AcceleratorKey=\"\"", string.Empty)
                    .Replace(" AccessKey=\"\"", string.Empty)
                    .Replace(" AutomationId=\"\"", string.Empty)
                    .Replace(" FrameworkId=\"WPF\"", string.Empty)
                    .Replace(" HelpText=\"\"", string.Empty)
                    .Replace(" ItemStatus=\"\"", string.Empty)
                    .Replace(" ItemType=\"\"", string.Empty);

                pageSource = OrderAttributes(pageSource);

                File.WriteAllText(Path.Combine(OutputDirectory, $"{fileIndex:D5}_PageSource.xml"), pageSource);
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string OrderAttributes(string pageSource)
        {
            var document = XDocument.Parse(pageSource);
            foreach (var element in document.Descendants())
            {
                var attributes = element.Attributes().ToList();
                attributes.Remove();
                attributes.Sort((a, b) => string.Compare(a.Name.LocalName, b.Name.LocalName, StringComparison.Ordinal));
                var classNameAttribute = attributes.FirstOrDefault(attribute => attribute.Name.LocalName == "ClassName");
                var nameAttribute = attributes.FirstOrDefault(attribute => attribute.Name.LocalName == "Name");
                var automationIdAttribute = attributes.FirstOrDefault(attribute => attribute.Name.LocalName == "AutomationId");
                element.Add(classNameAttribute);
                element.Add(nameAttribute);
                element.Add(automationIdAttribute);
                element.Add(attributes.Except(new[] { classNameAttribute, nameAttribute, automationIdAttribute }));
            }
            return document.ToString();
        }
    }
}
