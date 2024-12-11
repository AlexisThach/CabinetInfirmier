using CabinetInfirmier;

AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);

string pathXmlCabinet = "./src/data/xml/cabinet.xml";
string pathXmlPatient = "./src/data/xml/patient.xml";
string pathXmlActes = "./src/data/xml/actes.xml";

// Tests de validation de fichiers XMLSchema
//XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical","./src/data/xsd/cabinet.xsd", pathXmlCabinet);
//XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical","./src/data/xsd/patient.xsd", pathXmlPatient);

// Tests de transformation XSLT
//XMLUtils.XslTransform(pathXmlCabinet, "./src/data/xslt/cabinet.xsl", "./src/data/html/cabinet_test_cs.html");
//XMLUtils.XslTransform(pathXmlPatient, "./src/data/xslt/patient_html.xsl", "./src/data/html/patient_test_cs.html");

// Parser XMLReader
//CabinetXmlReader.AnalyseGlobal(pathXmlCabinet);
//CabinetXmlReader.GetTexteFromElements(pathXmlCabinet , "nom", "infirmiers");
//CabinetXmlReader.GetNbActes(pathXmlActes);





