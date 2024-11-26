<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
                xmlns:ci="http://www.ujf-grenoble.fr/l3miage/medical"
                xmlns:act="http://www.ujf-grenoble.fr/l3miage/actes">
    <xsl:output method="html"/>

    <xsl:param name="destinedId" select="001"/>
    <xsl:variable name="visiteDuJour" select="count(//ci:patient/ci:visite[@intervenant=$destinedId])"/>
    <xsl:variable name="actes" select="document('../xml/actes.xml', /)/act:ngap"/>

    <xsl:template match="/">
        <html>
            <head>
                <title>Infirmier <xsl:value-of select='concat(//ci:infirmier[@id=$destinedId]/ci:nom, " ", //ci:infirmier[@id=$destinedId]/ci:prénom)'/></title>
                <link rel="stylesheet" type="text/css" href="../css/infirmiers.css" />
                <script type="text/javascript" src="../js/facture.js"></script>
            </head>
            <body>
                <div class="header">
                    <!-- Insertion de l'image de l'infirmière -->
                    <img src="../images/" alt="Photo" class="infirmiere-photo"/>

                    <!-- Titre de la page centré -->
                    <h1 class="title"><xsl:value-of select='concat(//ci:infirmier[@id=$destinedId]/ci:nom, " ", //ci:infirmier[@id=$destinedId]/ci:prénom)'/></h1>
                </div>

                <!-- Message de bienvenue -->
                <xsl:template match="ci:infirmier">
                    Bonjour <xsl:value-of select='//ci:infirmier[@id=$destinedId]/ci:nom'/>,<br/>
                    <p>Aujourd'hui, vous avez <xsl:value-of select="$visiteDuJour"/> patients.<br/></p>
                </xsl:template>                
                <!-- Début du tableau -->
                <table border="1">
                    <thead>
                        <tr>
                            <th>Nom</th>
                            <th>Prénom</th>
                            <th>Adresse</th>
                            <th>Soin(s) à effectuer</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Afficher chaque patient -->
                        <xsl:apply-templates select="//ci:patient[ci:visite[@intervenant=$destinedId]]" />
                    </tbody>
                </table>
                <!-- Fin du tableau -->

            </body>
        </html>
    </xsl:template>

    <!--Template pour afficher les informations du patient dans le tableau-->
    <xsl:template match="ci:patient">
        <tr>
            <td><xsl:value-of select="ci:nom"/></td>
            <td><xsl:value-of select="ci:prénom"/></td>
            <td>
                <xsl:value-of select="ci:adresse/ci:etage"/>
                <xsl:value-of select="ci:adresse/ci:numéro"/>,
                <xsl:value-of select="ci:adresse/ci:rue"/>,
                <xsl:value-of select="ci:adresse/ci:ville"/>,
                <xsl:value-of select="ci:adresse/ci:codePostal"/>
            </td>
            <td>
                <xsl:apply-templates select="ci:visite"/>
            </td>
        </tr>
    </xsl:template>

    <!--Template visite : Affichez la date de la visite, puis les actes associés-->
    <xsl:template match="ci:visite">
        Visite du <xsl:value-of select="@date"/><br/>
        <xsl:apply-templates select="ci:acte"/>
    </xsl:template>

    <!--Template Acte : Affichez l'acte en fonction de l'ID dans le fichier actes.xml-->
    <xsl:template match="ci:acte">
        <xsl:variable name="id" select="@id"/>
        <xsl:variable name="nom" select="../../ci:nom"/>
        <xsl:variable name="prénom" select="../../ci:prénom"/>

        <xsl:value-of select="$actes/act:actes/act:acte[@id = $id]"/><br/>

        <button class="button" type="button">
            <xsl:attribute name="onclick">
                openFacture(`<xsl:value-of select="$nom"/>`,
                            `<xsl:value-of select="$prénom"/>`,
                            `<xsl:value-of select="$actes/act:actes/act:acte[@id = $id]"/>`)
            </xsl:attribute>
            Facture
        </button>
    </xsl:template>

</xsl:stylesheet>