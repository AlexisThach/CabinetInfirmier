using CabinetInfirmier;
using System.Xml.Serialization;

namespace CabinetInfirmier;

public class Program
{
    public static void Main()
    {
        string pathXmlCabinet = "./src/data/xml/cabinet.xml";
        string pathXmlPatient = "./src/data/xml/patient.xml";
        string pathXmlActes = "./src/data/xml/actes.xml";
        string pathXmlAdresse = "./src/data/xml/adresse.xml";
        string pathXmlInfirmier = "./src/data/xml/infirmier.xml";
        string pathXmlInfirmiers = "./src/data/xml/infirmiers.xml";
        string URImedical = "http://www.univ-grenoble-alpes.fr/l3miage/medical";

        AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);
        
        // Tests de validation de fichiers XMLSchema 
        XMLUtils.ValidateXmlFileAsync(URImedical, "./src/data/xsd/cabinet.xsd", pathXmlCabinet);
        XMLUtils.ValidateXmlFileAsync(URImedical, "./src/data/xsd/patient.xsd", pathXmlPatient);

        // Tests de transformation XSLT
        XMLUtils.XslTransform(pathXmlCabinet, "./src/data/xslt/cabinet.xsl", "./src/data/html/cabinet_test_cs.html");
        XMLUtils.XslTransform(pathXmlPatient, "./src/data/xslt/patient_html.xsl",
            "./src/data/html/patient_test_cs.html");

        Console.WriteLine("-------------------- XMLReader -------------------------------");
        CabinetXmlReader read = new CabinetXmlReader();
        read.AnalyseGlobal(pathXmlCabinet);
        Console.WriteLine("--------------------------------------------------------------");
        read.GetTexteFromElements(pathXmlCabinet, "nom", "infirmiers");
        Console.WriteLine("--------------------------------------------------------------");
        read.GetTexteFromElements(pathXmlCabinet, "nom", "patients");
        Console.WriteLine("--------------------------------------------------------------");
        read.GetNbActes(pathXmlActes);

        
        Console.WriteLine("-------------------- DOM -------------------------------------");
        CabinetDOM cabinet = new CabinetDOM(pathXmlCabinet);
        Console.WriteLine("Nombre d'infirmiers : " + cabinet.GetNbInfirmiers());
        Console.WriteLine("Nombre de patients : " + cabinet.GetNbPatients());
        Console.WriteLine("Adresse complète du cabinet : " + cabinet.HasAdresse(pathXmlCabinet, "//ci:cabinet/ci:adresse", URImedical));
        Console.WriteLine("Adresse complète des patients : " + cabinet.HasAdresse(pathXmlPatient, "//ci:patient/ci:adresse", URImedical));
        Console.WriteLine("Adresse est complet pour Orouge : {0}", cabinet.HasAdresse(pathXmlCabinet, "//ci:cabinet/ci:patients/ci:patient[ci:nom='Orouge']/ci:adresse", URImedical));

        // 7.3.3 Modification de l’arbre DOM et de l’instance XML.

        cabinet.AddInfirmier("Jean", "Némard");
        cabinet.AddPatient(
            "Niskotch",
            "Nicole",
            "F",
            "2002-06-20",
            "202062602305206",
            "13",
            "rue Pasteur",
            "69000",
            "Lyon"
            );
        cabinet.AddVisite("Niskotch", "001", "2016-10-01", "108");
        cabinet.Save(pathXmlCabinet); // Sauvegarde les modifications dans le fichier XML mais supprime les espaces...

        Console.WriteLine("-------------------- SERIALISATION ----------------------------------------");
        Adresse adresse;
        Infirmier inf;
        Infirmiers infirmiers;

        // Test de desérialisation de la classe Adresse
        using (StreamReader reader = new StreamReader(pathXmlAdresse))
        {
            var xmlAdr = new XmlSerializer(typeof(Adresse));
            adresse = (Adresse)xmlAdr.Deserialize(reader);
        }

        //Test de serialisation de la classe Adresse
        using (StreamWriter writer = new StreamWriter(pathXmlAdresse))
        {
            var xmlAdr = new XmlSerializer(typeof(Adresse));
            xmlAdr.Serialize(writer, adresse);
        }

        Console.WriteLine(adresse);

        // Test de sérialisation de la classe Infirmier
        using (TextReader reader = new StreamReader(pathXmlInfirmier))
        {
            var xmlInf = new XmlSerializer(typeof(Infirmier));
            inf = (Infirmier)xmlInf.Deserialize(reader);
            Console.WriteLine(inf);
        }

        // Test de sérialisation de la classe Infirmiers
        using (TextReader reader = new StreamReader(pathXmlInfirmiers))
        {
            var xmlInfs = new XmlSerializer(typeof(Infirmiers));
            infirmiers = (Infirmiers)xmlInfs.Deserialize(reader);
            Console.WriteLine(infirmiers);
        }
    }
}

