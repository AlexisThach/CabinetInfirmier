namespace CabinetInfirmier;

using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text;

public class XMLUtils {
    public static async Task ValidateXmlFileAsync(string schemaNamespace, string xsdFilePath, string xmlFilePath)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(schemaNamespace, xsdFilePath);
        settings.ValidationType = ValidationType.Schema;

        Console.WriteLine("Nombre de schémas utilisés dans la validation : " + settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallBack;
        var readItems = XmlReader.Create(xmlFilePath, settings);
        while (readItems.Read())
        {
            // Boucle pour lire tout le contenu du fichier XML
        }

        readItems.Close(); // Fermer le lecteur après utilisation
    }
    
    private static void ValidationCallBack(object? sender, ValidationEventArgs e)
    {
        if (e.Severity.Equals(XmlSeverityType.Warning))
        {
            Console.Write("WARNING: ");
            Console.WriteLine(e.Message);
        }
        else if (e.Severity.Equals(XmlSeverityType.Error))
        {
            Console.Write("ERROR: ");
            Console.WriteLine(e.Message);
        }
    }
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath){
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        
        XsltSettings settings = new XsltSettings(true, true);
        XmlResolver resolver = new XmlUrlResolver();
        xslt.Load(xsltFilePath, settings, resolver);
        //xslt.Load(xsltFilePath);
        
        XsltArgumentList xslArg = new XsltArgumentList();
        xslArg.AddParam("destinedId", "","001" );
        /*
        using (XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8)) {
            xslt.Transform(xpathDoc, xslArg, htmlWriter);
        }
        */
        using (StreamWriter htmlWriter = new StreamWriter(htmlFilePath, false, Encoding.UTF8)) {
            xslt.Transform(xpathDoc, xslArg, htmlWriter);
        }
    }
   
}