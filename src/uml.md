``` plantuml
 
package cabinet {

' Définition de la classe Cabinet
class Cabinet {
    +nom : String
    +adresse : Adresse
    +infirmiers : Infirmiers
    +patients : Patients
}

' Définition de la classe Infirmiers
class Infirmiers {
    +id : Id
    +nom : String
    +prénom : String
    +photo : Photo
}

' Définition de la classe Patients
class Patients {
    +nom : NomPatient
    +prénom : NomPatient
    +sexe : Sexe
    +naissance : date
    +numéro : NIR
    +adresse : Adresse
    +visite : Visite
}

' Définition de la classe Adresse
class Adresse {
    +étage : String
    +numéro : String
    +rue : String
    +ville : String
    +codePostal : int 
}

' Définition de la classe Visite
class Visite {
    +date : date
    +intervenant : Id
    +acte : Acte
}

class Acte {
    +id : int
}

class NomPatient {
    <<String restrict>>
}

class Id {
    <<pattern>>
}

class NIR {
    <<pattern>>
}

class CodePostal {
    <<String restrict>>
}

class Photo {
    <<String restrict>>
}

Enum Sexe {
    M
    F
}

' Relations entre les différentes classes
Cabinet *-- Infirmiers 
Cabinet *-- Patients 
Cabinet *-- Adresse 
Adresse *-- CodePostal
Patients *-- Adresse 
Patients *-- Visite 
Patients *-- NIR
Patients *-- Sexe
Patients *-- NomPatient
Infirmiers *-- Photo
Visite *-- Id
Visite *-- Acte
Infirmiers *-- Id 
}
```
