using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("patient", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Patient {
    [XmlElement("nom")] public String _Nom { set; get; }
    [XmlElement("prénom")] public String _Prenom { set; get; }
    [XmlElement("sexe")] public String _Sexe { set; get; }
    [XmlElement("naissance")] public String _Naissance { set; get; }
    [XmlElement("numéro")] public string _NumeroSecurite { set; get; }
    [XmlElement("adresse")] public Adresse _AdressePatient { set; get; }
    [XmlElement("visite")] public Visite _Visite { set; get; }
    public override string ToString() {
        var s = String.Empty;
        s += "\nPatient : ";
        s += "\n \tNom : " + _Nom;
        s += "\n \tPrenom : " + _Prenom;
        s += "\n \tSexe : " + _Sexe;
        s += "\n \tNaissance : " + _Naissance;
        s += "\n \tNumero : " + _NumeroSecurite;
        s += _Visite;
        s += "\n \tAdresse : " + _AdressePatient;
        return s;
    }
    
}