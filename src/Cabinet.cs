using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("cabinet", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Cabinet {
    [XmlElement("adresse")] public Adresse _Adresse { set; get; }
    [XmlElement("infirmiers")] public Infirmiers _Infirmiers { set; get; }
    [XmlElement("patients")] public Patients _Patients { set; get; }
    
    public override string ToString() {
        var s = String.Empty;
        s += "Cabinet : " + _Adresse;
        s += "\nInfirmiers : " + _Infirmiers;
        return s;
    }
    
}