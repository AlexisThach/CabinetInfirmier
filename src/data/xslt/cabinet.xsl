<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes">
    <xsl:output method="html"/>

    <xsl:param name="destinedId" select="001"/>
    <xsl:variable name="visiteDuJour" select="count(//ci:patient/ci:visite[@intervenant=$destinedId])"/>
    <xsl:variable name="actes" select="document('../xml/actes.xml', /)/act:ngap"/>
    
    <xsl:template match="/">
        <html>
            <head>
                <meta charset="UTF-8"/>
                <title>Infirmier <xsl:value-of select='concat(//ci:infirmier[@id=$destinedId]/ci:nom, " ", //ci:infirmier[@id=$destinedId]/ci:prénom)'/></title>
                <link rel="stylesheet" type="text/css" href="../css/infirmierPage.css" />
                <script type="text/javascript" src="../js/buttonScript.js"></script>
            </head>
            <body>
                <div class="header">
                    <img src="../img/frechie.png" alt="Photo" class="infirmiere-photo"/>
                    <h1 class="title"><xsl:value-of select='concat(//ci:infirmier[@id=$destinedId]/ci:nom, " ", //ci:infirmier[@id=$destinedId]/ci:prénom)'/></h1>
                </div>
                
                <!-- Message de bienvenue -->
                Bonjour <xsl:value-of select='//ci:infirmier[@id=$destinedId]/ci:nom'/>,<br/>
                <p>Aujourd'hui, vous avez <xsl:value-of select="$visiteDuJour"/> patients.<br/></p>
                
                <!-- Début du tableau -->
                <table border="1">
                    <tr>
                        <th>Nom</th>
                        <th>Prénom</th>
                        <th>Adresse</th>
                        <th>Soin(s) à effectuer</th>
                    </tr>
                    <!-- Afficher chaque patient dont l'infirmier est le destinataire -->
                    <xsl:apply-templates select="//ci:patient[ci:visite[@intervenant=$destinedId]]" />
                </table>
            </body>
        </html>
    </xsl:template>

    <!--Template pour afficher les informations du patient telles que le nom, prénom, adresse et 
        son acte de visite dans le tableau-->

    <xsl:template match="ci:patient">
        <tr>
            <td><xsl:value-of select="ci:nom"/></td>
            <td><xsl:value-of select="ci:prénom"/></td>
            <td>
                <!-- Adresse avec gestion des virgules -->
                <xsl:choose>
                    <!-- Si l'étage est présent -->
                    <xsl:when test="ci:adresse/ci:étage">
                        <xsl:text>Étg n°</xsl:text>
                        <xsl:value-of select="ci:adresse/ci:étage"/>
                        <xsl:text>, </xsl:text>
                    </xsl:when>
                </xsl:choose>
                
                <xsl:choose>
                    <!-- Si le numéro de rue est présent -->
                    <xsl:when test="ci:adresse/ci:numéro">
                        <xsl:value-of select="ci:adresse/ci:numéro"/>
                        <xsl:text>, </xsl:text>
                    </xsl:when>
                </xsl:choose>
                
                <xsl:value-of select="ci:adresse/ci:rue"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="ci:adresse/ci:ville"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="ci:adresse/ci:codePostal"/>
            </td>
            <td>
                <xsl:apply-templates select="ci:visite"/>
            </td>
        </tr>
    </xsl:template>
    
    <!--Template visite : Affiche la date de la visite, puis les actes associés -->
    <xsl:template match="ci:visite">
        Visite du <xsl:value-of select="@date"/>
        <br/>
        <br/>
        <xsl:apply-templates select="ci:acte"/>
    </xsl:template>

    <!--Template Acte : Affiche l'acte en fonction de l'ID dans le fichier actes.xml-->
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