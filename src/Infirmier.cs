using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Infirmier {
    
    private string _photo;
    
    [XmlAttribute("id")] public String _Id { init; get; }
    [XmlElement("nom")] public String _Prenom { init; get; }
    [XmlElement("prénom")] public String _Nom { init; get; }
    
    // les fichiers photos ont un suffixe terminant par .jpg ou .png
    [XmlElement("photo")] public String _Photo {
        set {
            if (value.EndsWith(".jpg") || value.EndsWith(".png")) {
                _photo = value;
            }
        }
        get { return _photo; }
    }
    
    public override string ToString() {
        var s = String.Empty;
        s += "Infirmier "+_Id+" : \n";
        s += "\t Nom : " + _Nom;
        s += "\n \t Prenom : " + _Prenom;
        s += "\n \t Photo : " + _Photo+"\n";        
        return s;
    }
}