using System.Xml.Linq;

namespace Importer.XmlParser
{
    public class DocumentParser
    {
        private XDocument xmlDocument;

        public DocumentParser()
        {
            xmlDocument = XDocument.Load(@"C:\Users\Erik\Desktop\full.xml");
        }

        public XDocument XmlDocument
        {
            get { return xmlDocument; }
        }
    }
}