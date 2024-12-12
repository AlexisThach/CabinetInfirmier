<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act="http://www.univ-grenoble-alpes.fr/l3miage/actes"
                xmlns:xs="http://www.w3.org/2001/XMLSchema-instance">
    <xsl:output method="xml" indent="yes"/>
    
    <xsl:param name="destinedName" select="'Alécole'" />
    
    <!-- Template principale : Produire un document XML à partir des fichiers XML acte.xml 
   et cabinet.xml. On commence par ouvrir une balise "patient" et on sélectionne le patient recherché 
   en utilisant un chemin XPath spécifié. -->

    <xsl:template match="/">
        <ci:patient>
            <xsl:attribute name="xs:schemaLocation">http://www.univ-grenoble-alpes.fr/l3miage/medical ../xsd/patient.xsd</xsl:attribute>
            <xsl:apply-templates select="//ci:patient[ci:nom=$destinedName]"/>
        </ci:patient>
    </xsl:template>


    <!--Template Patient : Récupère les informations du patient
        depuis le fichier XML et applique le template "visite" 
        pour afficher toutes les visites associées au patient.-->
    
    <xsl:template match="ci:patient">
        <ci:nom><xsl:value-of select="ci:nom"/></ci:nom>
        <ci:prénom><xsl:value-of select="ci:prénom"/></ci:prénom>
        <ci:sexe><xsl:value-of select="ci:sexe"/></ci:sexe>
        <ci:naissance><xsl:value-of select="ci:naissance"/></ci:naissance>
        <ci:numéro><xsl:value-of select="ci:numéro"/></ci:numéro>
        <ci:adresse>
            <ci:étage><xsl:value-of select="ci:adresse/ci:étage"/></ci:étage>
            <ci:numéro><xsl:value-of select="ci:adresse/ci:numéro"/></ci:numéro>
            <ci:rue><xsl:value-of select="ci:adresse/ci:rue"/></ci:rue>
            <ci:codePostal><xsl:value-of select="ci:adresse/ci:codePostal"/></ci:codePostal>
            <ci:ville><xsl:value-of select="ci:adresse/ci:ville"/></ci:ville>
        </ci:adresse>
        <xsl:apply-templates select="ci:visite"/>

    </xsl:template>


    <!--Template Visite : Ce template affiche la balise "visite" 
        avec un attribut "date". Il inclut également une balise 
        "intervenant" contenant le nom et le prénom de l'infirmier 
        responsable de la visite du patient.-->
    
    <xsl:template match="ci:visite">
        <ci:visite>
            <xsl:attribute name="date"><xsl:value-of select="@date"/></xsl:attribute>
            <xsl:attribute name="intervenant"><xsl:value-of select="@intervenant"/></xsl:attribute>

            <!-- Pour chaque acte on affiche sa description-->
            <xsl:apply-templates select="ci:acte"/>
        </ci:visite>
    </xsl:template>

    <!--Template Acte : Affiche la description de chaque acte récupérée à partir du document acte.xml. -->
    <xsl:template match="ci:acte">

        <!-- Pour chaque acte du patient -->
        <xsl:variable name="idActe" select="@id"/>
        
        <!-- Pour chaque id du patient on regarde dans l'arborescence
        du fichiers actes.xml lorsque l'acte est égale à l'id du patient-->
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap/act:actes/act:acte[@id=$idActe]"/>
        
        <!-- Affichage de la description pour chaque acte-->
        <ci:acte id="{$idActe}"><xsl:value-of select="$actes"/></ci:acte>
    </xsl:template>

</xsl:stylesheet>