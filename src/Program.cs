using CabinetInfirmier;
using System.Xml.Serialization;

AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

string pathXmlCabinet = "./src/data/xml/cabinet.xml";
string pathXmlPatient = "./src/data/xml/patient.xml";
string pathXmlActes = "./src/data/xml/actes.xml";
string URImedical = "http://www.univ-grenoble-alpes.fr/l3miage/medical";

// Tests de validation de fichiers XMLSchema
//XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical","./src/data/xsd/cabinet.xsd", pathXmlCabinet);
//XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical","./src/data/xsd/patient.xsd", pathXmlPatient);

// Tests de transformation XSLT
//XMLUtils.XslTransform(pathXmlCabinet, "./src/data/xslt/cabinet.xsl", "./src/data/html/cabinet_test_cs.html");
//XMLUtils.XslTransform(pathXmlPatient, "./src/data/xslt/patient_html.xsl", "./src/data/html/patient_test_cs.html");

// Tests Parser XMLReader
/*
Console.WriteLine("-------------------- XMLReader --------------------");
CabinetXmlReader.AnalyseGlobal(pathXmlCabinet);
CabinetXmlReader.GetTexteFromElements(pathXmlCabinet , "nom", "infirmiers");
CabinetXmlReader.GetNbActes(pathXmlActes);
*/

// Test Parser DOM

Console.WriteLine("-------------------- DOM --------------------");
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

