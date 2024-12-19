using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("patients", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Patients {
    [XmlElement("patient")]
    public List<Patient> ListePatients { get; set; }
    
    public override string ToString() {
        var s = String.Empty;
        foreach (var pat in ListePatients) {
            s += pat.ToString();
        }
        return s;
    }
    
}