using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Adresse {

    private int _etage;
    private int _numero;
    private int _codePostal;
    
    // l'étage doit être un entier positif
    [XmlElement("étage")] public int _Etage {
        set
        {
            _etage = value;
            if (_etage < 0) _etage = int.Abs(value);
        }
        get { return _etage; }
    }
    
    // le numéro doit être un entier positif
    [XmlElement("numéro")] public int _Numero {
        set {
            _numero = value;
            if (_numero < 0)
                _numero = int.Abs(value);
        }
        get { return _numero; }
    }
    
    [XmlElement("rue")] public String _Rue { set; get; }
    
    // le code postal doit être un entier de 5 chiffres
    [XmlElement("codePostal")] public int _CodePostal {
        set {
            _codePostal = value;
            if (_codePostal.ToString().Length != 5)
                _codePostal = 26000;
            else if (_codePostal < 0) {
                _codePostal = int.Abs(value);
            }
        }
        get { return _codePostal; }
    }
    
    [XmlElement("ville")] public String _Ville { set; get; }
    
    public override string ToString() {
        var s = String.Empty;
        s += "Adresse : " + _Numero + " " + _Rue + " " + _Etage + " " + _CodePostal + " " + _Ville;
        return s;
    }
}