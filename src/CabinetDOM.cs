namespace CabinetInfirmier;

using System.Xml;

public class CabinetDOM {
    
    private XmlDocument doc;
    private XmlNode root;
    private XmlNamespaceManager nsmgr;
    public CabinetDOM(String filename) {
        doc = new XmlDocument();
        doc.Load(filename);
        root = doc.DocumentElement;
        nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ci","http://www.univ-grenoble-alpes.fr/l3miage/medical");
    }
    
    public String GetNSPrefix() {
        return root.Prefix;
    }

    public String GetNSURI() {
        return root.NamespaceURI;
    }
    public XmlNodeList getXPath(String expression, String nsPrefix, String nsURI){
        XmlNode root = doc.DocumentElement;  
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable); 
        nsmgr.AddNamespace(nsPrefix, nsURI);
        return root.SelectNodes(expression, nsmgr);
    }
    public uint GetNbInfirmiers() {
        XmlElement rootElt = (XmlElement)root;
        XmlNodeList infirmierNL = rootElt.GetElementsByTagName("infirmier");
        return (uint) infirmierNL.Count;
    }
    public uint GetNbPatients() {
        XmlElement rootElt = (XmlElement)root;
        XmlNodeList patientNL = rootElt.GetElementsByTagName("patient");
        return (uint) patientNL.Count;
    }
    public bool HasAdresse(string xmlFilePath, string expression, string nsURI) {
        XmlNodeList AdresseNL = getXPath(expression, "ci", nsURI);
        if(AdresseNL != null && AdresseNL[0].ChildNodes.Count >= 3) {
            return true;         // 3 éléments obligatoires : rue, code postal, ville 
                                // 2 éléments optionnels : étage, numéro
        }
        return false;
    }
    
    public void AddInfirmier(string prenom, string nom) {
        // Recherche de tous les infirmiers pour déterminer le plus grand ID
        XmlNodeList infirmiers = root.SelectNodes("//ci:infirmier", nsmgr);

        int maxId = 0;
        foreach (XmlNode infirmier in infirmiers)
        {
            string idStr = infirmier.Attributes["id"]?.Value;
            if (int.TryParse(idStr, out int id))
            {
                maxId = Math.Max(maxId, id);
            }
        }

        // Calcul du prochain ID formaté avec trois chiffres
        string newId = (maxId + 1).ToString("D3");

        // Création du nouvel infirmier
        XmlElement newInfirmier = doc.CreateElement("infirmier", GetNSURI());
        newInfirmier.SetAttribute("id", newId);

        XmlElement newNom = doc.CreateElement("nom", GetNSURI());
        newNom.InnerText = nom;

        XmlElement newPrenom = doc.CreateElement("prénom", GetNSURI());
        newPrenom.InnerText = prenom;

        XmlElement newPhoto = doc.CreateElement("photo", GetNSURI());
        newPhoto.InnerText = $"{prenom.ToLower()}.png";

        // Ajout des éléments enfants au nouvel infirmier
        newInfirmier.AppendChild(newNom);
        newInfirmier.AppendChild(newPrenom);
        newInfirmier.AppendChild(newPhoto);

        // Ajout du nouvel infirmier à la liste des infirmiers
        XmlNode infirmiersParent = root.SelectSingleNode("//ci:infirmiers", nsmgr);
        if (infirmiersParent != null)
        {
            infirmiersParent.AppendChild(newInfirmier);
            Console.WriteLine($"Infirmier ajouté : {nom} {prenom}, ID : {newId}");
        }
        else
        {
            Console.WriteLine("Erreur : Impossible de trouver la liste des infirmiers.");
        }
    }
    
    public void AddPatient(string nom, string prenom, string sexe, string naissance, string numeroSS, string numeroAdresse, string rue, string codePostal, string ville) {
        // Trouver le nœud racine des patients
        XmlNode patientsNode = root.SelectSingleNode("//ci:patients", nsmgr);
        if (patientsNode == null) {
            Console.WriteLine("Le nœud <patients> est introuvable.");
            return;
        }

        // Créer un nouveau nœud patient
        XmlElement newPatient = doc.CreateElement("patient", nsmgr.LookupNamespace("ci"));

        // Ajouter les enfants au patient
        XmlElement nomNode = doc.CreateElement("nom", nsmgr.LookupNamespace("ci"));
        nomNode.InnerText = nom;
        newPatient.AppendChild(nomNode);

        XmlElement prenomNode = doc.CreateElement("prénom", nsmgr.LookupNamespace("ci"));
        prenomNode.InnerText = prenom;
        newPatient.AppendChild(prenomNode);

        XmlElement sexeNode = doc.CreateElement("sexe", nsmgr.LookupNamespace("ci"));
        sexeNode.InnerText = sexe;
        newPatient.AppendChild(sexeNode);

        XmlElement naissanceNode = doc.CreateElement("naissance", nsmgr.LookupNamespace("ci"));
        naissanceNode.InnerText = naissance;
        newPatient.AppendChild(naissanceNode);

        XmlElement numeroSSNode = doc.CreateElement("numéroSS", nsmgr.LookupNamespace("ci"));
        numeroSSNode.InnerText = numeroSS;
        newPatient.AppendChild(numeroSSNode);

        // Ajouter l'adresse
        XmlElement adresseNode = doc.CreateElement("adresse", nsmgr.LookupNamespace("ci"));

        XmlElement numeroNode = doc.CreateElement("numéro", nsmgr.LookupNamespace("ci"));
        numeroNode.InnerText = numeroAdresse;
        adresseNode.AppendChild(numeroNode);

        XmlElement rueNode = doc.CreateElement("rue", nsmgr.LookupNamespace("ci"));
        rueNode.InnerText = rue;
        adresseNode.AppendChild(rueNode);

        XmlElement codePostalNode = doc.CreateElement("codePostal", nsmgr.LookupNamespace("ci"));
        codePostalNode.InnerText = codePostal;
        adresseNode.AppendChild(codePostalNode);

        XmlElement villeNode = doc.CreateElement("ville", nsmgr.LookupNamespace("ci"));
        villeNode.InnerText = ville;
        adresseNode.AppendChild(villeNode);

        newPatient.AppendChild(adresseNode);

        // Ajouter le nouveau patient au nœud <patients>
        patientsNode.AppendChild(doc.CreateTextNode("\n  ")); // Ajout d'un saut de ligne pour la lisibilité
        patientsNode.AppendChild(newPatient);
        patientsNode.AppendChild(doc.CreateTextNode("\n")); 
    }
    
    public void AddVisite(string patientNom, string intervenantId, string date, string acteId)
    {
        // Namespace utilisé dans le fichier XML
        XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
        nsManager.AddNamespace("ci", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        // Trouver le patient par son nom
        XmlNode patientNode = doc.SelectSingleNode($"//ci:patient[ci:nom='{patientNom}']", nsManager);

        if (patientNode == null)
        {
            Console.WriteLine($"Erreur : Aucun patient trouvé avec le nom '{patientNom}'.");
            return;
        }

        // Créer l'élément "visite"
        XmlElement visiteElement = doc.CreateElement("visite", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
        visiteElement.SetAttribute("date", date);
        visiteElement.SetAttribute("intervenant", intervenantId);

        // Créer l'élément "acte" avec l'ID de l'acte
        XmlElement acteElement = doc.CreateElement("acte", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
        acteElement.SetAttribute("id", acteId);
        visiteElement.AppendChild(acteElement);

        // Ajouter la nouvelle visite directement sous le patient
        patientNode.AppendChild(visiteElement);

        Console.WriteLine($"Une visite a été ajoutée pour le patient '{patientNom}' avec l'intervenant '{intervenantId}' le '{date}' et l'acte '{acteId}'.");
    }
    
    
    public void Save(string filename) {
        doc.Save(filename);
        Console.WriteLine($"Les modifications ont été sauvegardées dans le fichier : {filename}");
    }
}
