using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class AdresseRO {
    private int _etage;
    private int _numero;
    private int _codepostal;
    [XmlElement("étage")] public int _Etage {
        init
        {
            _etage = value;
            if (_etage < 0) _etage = int.Abs(value);
        }
        get { return _etage; }
    }
    
    [XmlElement("numéro")] public int _Numero { 
        init
        {
            _numero = value;
            if (_numero < 0) _numero = int.Abs(value);
        }
        get { return _numero; }}
    [XmlElement("rue")] public String _Rue { init; get; }
    [XmlElement("codePostal")] public int _CodePostal { 
        init
        {
            _codepostal = value;
            if (_codepostal.ToString().Length != 5) _codepostal = 38000;
            else if (_codepostal < 0) _codepostal = int.Abs(value);
        }
        get { return _codepostal; }
        
    }
    [XmlElement("ville")] public String _Ville { init; get; }
    
    
    public override string ToString() {
        var s = String.Empty;
        s += "Adresse : \n";
        s += "\tEtage : " + _Etage;
        s += "\n \tNumero : "+_Numero; 
        s += "\n \tRue : " + _Rue;
        s += "\n \tCodePostal : " + _CodePostal;
        s += "\n \tVille : " + _Ville;
        return s;
    }
    
}