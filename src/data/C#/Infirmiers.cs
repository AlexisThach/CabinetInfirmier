using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("infirmiers", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Infirmiers {
    [XmlElement("infirmier")]
    public List<Infirmier> ListeInfirmiers { get; set; }
    
    public override string ToString() {
        var s = String.Empty;
        foreach (var inf in ListeInfirmiers) {
            s += inf.ToString();
        }
        return s;
    }
}