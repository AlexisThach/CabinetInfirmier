<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes">
    <xsl:output method="html" indent="yes"/>
    
    <!-- Réaliser une feuille XSLT qui transforme alecole.xml en une
         page html alecole.html présentant les renseignements voulus. -->
    
    <xsl:template match="/">
        
        <html>
            <head>
                <title>Patient <xsl:value-of select='concat(//ci:patient/ci:nom, " ", //ci:patient/ci:prénom)'/></title>
                <link rel="stylesheet" type="text/css" href="../css/pagePatient.css" /> 
            </head>
            <body>
                <div class="header">
                    <img src="../img/patient.png" alt="Photo" class="patient-photo"/>
                    <h1 class="title"><xsl:value-of select='concat(//ci:patient/ci:nom, " ", //ci:patient/ci:prénom)'/></h1>
                </div>
                <table border="1">
                    <tr>
                        <th>Nom</th>
                        <th>Prénom</th>
                        <th>Sexe</th>
                        <th>Naissance</th>
                        <th>N° sécurité sociale</th>
                        <th>Adresse</th>
                        <th>Visite</th>
                    </tr>
                    <xsl:apply-templates select="//ci:patient"/>
                </table>
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="ci:patient">
        <tr>
            <td><xsl:value-of select="ci:nom"/></td>
            <td><xsl:value-of select="ci:prénom"/></td>
            <td><xsl:value-of select="ci:sexe"/></td>
            <td><xsl:value-of select="ci:naissance"/></td>
            <td><xsl:value-of select="ci:numéroSS"/></td>
            <td>
                <xsl:text>Étg n°</xsl:text>
                <xsl:value-of select="ci:adresse/ci:étage"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="ci:adresse/ci:numéro"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="ci:adresse/ci:rue"/>
                <xsl:text>, </xsl:text>
                <xsl:value-of select="ci:adresse/ci:codePostal"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="ci:adresse/ci:ville"/>
            </td>
            <td>
                <xsl:apply-templates select="ci:visite"/>
            </td>
        </tr>
    </xsl:template>
    
    <xsl:template match="ci:visite">
        <xsl:value-of select="@date"/>
        <xsl:value-of select="ci:acte"/>
        <xsl:value-of select="@id"/>
        <xsl:text> - Infirmier: </xsl:text>
        <xsl:value-of select="@intervenant"/>
    </xsl:template>
</xsl:stylesheet>