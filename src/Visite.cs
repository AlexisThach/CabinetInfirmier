using System.Xml.Serialization;

namespace CabinetInfirmier;

[XmlRoot("visite",Namespace="http://www.univ-grenoble-alpes.fr/l3miage/medical")][Serializable]
public class Visite {
    [XmlAttribute("date")] public string _Date { init; get; }
    [XmlAttribute("intervenant")] public string _Intervenant { set; get; }
    [XmlElement("acte")] public Acte _Acte { init; get; }
    
    public override string ToString() {
        return "\ndate de visite \n"+_Date + "\nintervenant " + _Intervenant + "\nacte " + _Acte+"\n";
    }
    
}