using System.Xml;

namespace TestUmkConstructor.FileGeneration
{
    public static class TemplateReader
    {
        private static XmlDocument TemplateDocument = new XmlDocument();
        
        public static void ReadTemplate()
        {
            try
            {
                TemplateDocument.Load("template/TemplateDescriptions.xml");
            }
            catch { }
        }

        public static XmlNode getXmlTemplate(string templateName)
        {
            if (TemplateDocument.DocumentElement != null)
            {
                return TemplateDocument.SelectSingleNode(templateName)!;
            }
            else
            {
                // кидать эксепшен
                throw new Exception("Невалидное описание шаблонов");
            }
        }



    }
}
