using System;
using System.Xml.Serialization;

[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class InfirmierRO
{
    [XmlAttribute("id")]
    public string _Id { get; init; }

    [XmlElement("nom")]
    public string _Nom { get; init; }

    [XmlElement("prénom")]
    public string _Prenom { get; init; }

    [XmlElement("photo")]
    public string _Photo { get; init; }
    

    public override string ToString()
    {
        return $"Id: {_Id}, Nom: {_Nom}, Prénom: {_Prenom}, Photo: {_Photo}";
    }
}