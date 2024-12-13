using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("acte",Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Acte {
    [XmlAttribute("id")]
    public string _Id { get; set; }

    public override string ToString() {
        return "Acte : " + _Id;
    }
}