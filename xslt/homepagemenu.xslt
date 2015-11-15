<?xml version="1.0" encoding="utf-8"?>

<!--
Copyright 2010 Ektron. All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are
permitted provided that the following conditions are met:

   1. Redistributions of source code must retain the above copyright notice, this list of
      conditions and the following disclaimer.

   2. Redistributions in binary form must reproduce the above copyright notice, this list
      of conditions and the following disclaimer in the documentation and/or other materials
      provided with the distribution.

THIS SOFTWARE IS PROVIDED BY EKTRON ``AS IS'' AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL EKTRON OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
                                                                                 
The views and conclusions contained in the software and documentation are those of the
authors and should not be interpreted as representing official policies, either expressed
or implied, of Ektron.
******************************************************************************************/
-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="MenuDataResult">
    

      <xsl:apply-templates select="Item/Item"/>
  
  </xsl:template>

  <xsl:template match="Item">
    <xsl:variable name="targetLoc">
      <xsl:choose>
        <xsl:when test="ItemTarget='1'">_blank</xsl:when>
        <xsl:when test="ItemTarget='3'">_parent</xsl:when>
        <xsl:when test="ItemTarget='4'">_top</xsl:when>
        <xsl:otherwise>_self</xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <p>
    <xsl:if test="ItemId != '0'">
      
        <xsl:choose>
          <xsl:when test="Menu != '' and Menu/Link != ''">
            <xsl:call-template name="LinkingMenu"></xsl:call-template>
          </xsl:when>
          <xsl:when test="ItemLink != ''">
            <xsl:call-template name="JustLink"></xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:call-template name="NonLinkingMenu"></xsl:call-template>
          </xsl:otherwise>
        </xsl:choose>
    
    </xsl:if>
    </p>
    <!--<xsl:if test="ItemId = '0'">
      <li >
 

        <xsl:choose>
        <xsl:when test="ItemType='Javascript'">
          <A>
            <xsl:attribute name="onClick">
              Javascript:<xsl:value-of select="ItemLink"/>;return false;
            </xsl:attribute>
            <xsl:attribute name="href">#</xsl:attribute>
            <xsl:attribute name="target">
              <xsl:value-of select="$targetLoc"/>
            </xsl:attribute>
            <xsl:value-of select="ItemTitle"/>
          </A>
        </xsl:when>
        </xsl:choose>
        
      </li>
    </xsl:if>-->
      
      
      
  </xsl:template>

  <xsl:template name="JustLink">
    <xsl:call-template name="GetSelected">
      <xsl:with-param name="ExtraClasses">
        <!--<xsl:text> egMenu_</xsl:text>-->
        <xsl:value-of select="ItemId"/>
      </xsl:with-param>
    </xsl:call-template>
    <a>
      <xsl:call-template name="GetSelected"></xsl:call-template>
      <xsl:attribute name="href">
        <xsl:value-of select="ItemLink"/>
      </xsl:attribute>
      <xsl:call-template name="GetTarget"></xsl:call-template>
      <xsl:value-of select="ItemTitle"/>
    </a>
  </xsl:template>

  <xsl:template name="NonLinkingMenu">
    <xsl:call-template name="GetSelected">
      <xsl:with-param name="ExtraClasses">
        <!--<xsl:text> egDrop</xsl:text>
        <xsl:text> egNoLink</xsl:text>
        <xsl:text> egMenu_</xsl:text>
        <xsl:value-of select="ItemId"/>-->
      </xsl:with-param>
    </xsl:call-template>
    <xsl:value-of select="ItemTitle"/>
    <xsl:if test="Menu/Item">
      <p>
        <xsl:apply-templates select="Menu/Item"/>
      </p>
    </xsl:if>
  </xsl:template>
  <xsl:variable name="smallcase" select="'abcdefghijklmnopqrstuvwxyz'" />
  <xsl:variable name="uppercase" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />

  <xsl:template name="LinkingMenu">
    <xsl:call-template name="GetSelected">
      <xsl:with-param name="ExtraClasses">

        <!--<xsl:value-of select="ItemTitle"/>-->
        <xsl:value-of select="translate(translate(ItemTitle, $uppercase, $smallcase),' ', '-')" />
      </xsl:with-param>
    </xsl:call-template>
    <a>
      <!--<xsl:call-template name="GetSelected"></xsl:call-template>-->
      <xsl:attribute name="href">
        <xsl:value-of select="Menu/Link"/>
      </xsl:attribute>
      <xsl:call-template name="GetTarget"></xsl:call-template>
      <xsl:value-of select="ItemTitle"/>
    </a>
    <xsl:if test="Menu/Item">
      <p  >
        <xsl:attribute name="class">
          <xsl:text>drop-</xsl:text>
          <xsl:value-of select="translate(substring(ItemTitle,1,1), $uppercase, $smallcase)" />
        </xsl:attribute>
        <xsl:apply-templates select="Menu/Item"/>
      </p>
    </xsl:if>
  </xsl:template>


  <xsl:template name="GetSelected">
    <xsl:param name="ExtraClasses"></xsl:param>
    <xsl:choose>
      <xsl:when test="count(.//ItemSelected[text() = 'true']) &gt; 0 or count(.//MenuSelected[text() = 'true']) &gt; 0 or count(.//ChildMenuSelected[text() = 'true']) &gt; 0">
        <xsl:attribute name="class">
          <xsl:value-of select="$ExtraClasses"/>
          <!--<xsl:text> egSelected</xsl:text>
          <xsl:text> egMenuLevel_</xsl:text>
          <xsl:value-of select="count(ancestor::Menu)"/>-->
        </xsl:attribute>
      </xsl:when>
      <xsl:otherwise>
        <xsl:attribute name="class">
          <xsl:value-of select="$ExtraClasses"/>
          <!--<xsl:text> egMenuLevel_</xsl:text>
          <xsl:value-of select="count(ancestor::Menu)"/>-->
        </xsl:attribute>
      </xsl:otherwise>
    </xsl:choose>
    <!--<xsl:if test="count(.//ItemSelected[text() = 'true']) &gt; 0 or count(.//MenuSelected[text() = 'true']) &gt; 0 or count(.//ChildMenuSelected[text() = 'true']) &gt; 0">
      <xsl:attribute name="class">
        <xsl:value-of select="$ExtraClasses"/> egSelected</xsl:attribute>
    </xsl:if>-->
  </xsl:template>

  <xsl:template name="GetTarget">
    <xsl:if test="ItemTarget">
      <xsl:choose>
        <xsl:when test="ItemTarget = '1'">
          <xsl:attribute name="target">
            <xsl:text>_blank</xsl:text>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="ItemTarget = '2'">
          <xsl:attribute name="target">
            <xsl:text>_self</xsl:text>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="ItemTarget = '3'">
          <xsl:attribute name="target">
            <xsl:text>_parent</xsl:text>
          </xsl:attribute>
        </xsl:when>
        <xsl:when test="ItemTarget = '4'">
          <xsl:attribute name="target">
            <xsl:text>_top</xsl:text>
          </xsl:attribute>
        </xsl:when>
      </xsl:choose>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>