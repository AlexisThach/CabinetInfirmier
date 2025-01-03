using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Adresse {

    private int _etage;
    private int _numero;
    private int _codePostal;
    private string _rue;
    private string _ville;
    
    // l'étage doit être un entier positif
    [XmlElement("étage")] public int _Etage {
        set => _etage = (value < 0) ? 0 : value;
        get => _etage;
    }
    
    // le numéro doit être un entier positif
    [XmlElement("numéro")] public int _Numero {
        set => _numero = (value < 0) ? 0 : value;
        get => _numero;
    }

    [XmlElement("rue")]
    public String _Rue
    {
        set => _rue = value;
        get => _rue;
    }
    
    // le code postal doit être un entier de 5 chiffres
    [XmlElement("codePostal")] public int _CodePostal {
        set
        {
            if (value.ToString().Length != 5 || value < 0)
                _codePostal = 38000; 
            else
                _codePostal = value;
        }
        get { return _codePostal; }
    }

    [XmlElement("ville")]
    public String _Ville
    {
        set => _ville = value;
        get => _ville;
    }
    
    public override string ToString() {
        var s = String.Empty;
        s += "Adresse : " + _Numero + " " + _Rue + " " + _Etage + " " + _CodePostal + " " + _Ville;
        return s;
    }
}