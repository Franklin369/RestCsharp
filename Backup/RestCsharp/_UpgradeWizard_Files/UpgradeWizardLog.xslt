<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method='html' version='1.0' encoding='UTF-8' indent='yes'/>

<xsl:template match="/">
  <html>  
    <head>
      <META HTTP-EQUIV="Content-Type" content="text/html; charset=utf-8" />
      <link rel="stylesheet" href="_UpgradeWizard_Files\UpgradeWizardLog.css" />
      <title _locID="UpgradeWizardLog">Telerik Reporting UpgradeWizard Log&#32;</title>      
      <script type='text/javascript'>
        function ToggleShowHide(divId, linkId)
        {
          var element = document.getElementById(divId);
          if (element)
          {
            element.className = element.className == "collapsed" ? "expanded" : "collapsed";
          }

          var link = document.getElementById(linkId);
          if (link)
          {
            link.innerHTML = link.innerHTML == "Show details" ? "Hide details" : "Show details";
          }        
        }
      </script>
    </head>
  <body style="background-color: #EEEEEE">
  <h1>Telerik Reporting Upgrade Wizard Log</h1>
  <h2>ANALYZE</h2><p>
  <span> Processed: <xsl:value-of select="count(log/event[@action='Analyze'])"/></span>
  <span style="color: green" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Succeeded: <xsl:value-of select="count(log/event[@action='Analyze' and @type='Success'])"/></span>
  <span style="color: blue" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Warnings: <xsl:value-of select="count(log/event[@action='Analyze' and @type='Warning'])"/></span>
  <span style="color: red" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Failed: <xsl:value-of select="count(log/event[@action='Analyze' and @type='Error'])"/></span></p>
  <p><a id="AnalyzeShowHideLink" href="javascript:ToggleShowHide('AnalyzeDiv', 'AnalyzeShowHideLink');" >Show details</a></p>
    <div id="AnalyzeDiv" class="collapsed">
        <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
          <tr style="border: 1px solid black">
            <td class="header"></td>
            <td class="header">Upgrade Item</td>
            <td class="header">Path</td>
            <td class="header">Result</td>
          </tr>
              <xsl:for-each select="log/event[@action='Analyze']">
              <tr style="font-name: Verdana; font-size: 8pt">
              
                <td valign="top">
                    <xsl:if test=".!=''">
                        <xsl:if test="@type='Error'"><img src="_UpgradeWizard_Files/Error.png" /></xsl:if>
                        <xsl:if test="@type='Warning'"><img src="_UpgradeWizard_Files/Warning.png" /></xsl:if>
                    </xsl:if>
                    <xsl:if test=".=''"><img src="_UpgradeWizard_Files/Success.png" /></xsl:if>
                </td>            
                <td valign="top"><xsl:value-of select="@name"/></td>
                <td valign="top">
                    <xsl:choose>
                        <xsl:when test="@path!=''"><xsl:value-of select="@path"/></xsl:when>      
                        <xsl:otherwise>&#160;</xsl:otherwise>
                    </xsl:choose>                                    
                </td>
                <td valign="top"><xsl:attribute name="class">
                    <xsl:if test=".!=''">
                        <xsl:if test="@type='Warning'">warning</xsl:if>
                        <xsl:if test="@type='Error'">error</xsl:if>
                    </xsl:if>
                    <xsl:if test=".=''">success</xsl:if></xsl:attribute>
                  <xsl:if test=".!=''"><xsl:value-of select="."/></xsl:if>
                  <xsl:if test=".=''"><xsl:value-of select="@type"/></xsl:if>
                 </td>
              </tr>
              </xsl:for-each>
        </table>
    </div>
    <br />
  <h2>UPGRADE</h2>
    <p><span>Processed: <xsl:value-of select="count(log/event[@action='Upgrade'])"/></span>
      <span style="color: green" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Succeeded: <xsl:value-of select="count(log/event[@action='Upgrade' and @type='Success'])"/></span>
      <span style="color: blue" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Warnings: <xsl:value-of select="count(log/event[@action='Upgrade' and @type='Warning'])"/></span>
      <span style="color: red" xml:space="preserve">&#160;&#160;&#160;&#160;&#160;Failed: <xsl:value-of select="count(log/event[@action='Upgrade' and @type='Error'])"/></span></p>
  <p><a id="UpgradeShowHideLink" href="javascript:ToggleShowHide('UpgradeDiv', 'UpgradeShowHideLink');" >Show details</a></p>
    <div id="UpgradeDiv" class="collapsed">
        <table cellpadding="2" cellspacing="0" width="98%" border="1" bordercolor="white" class="infotable">
          <tr>
            <td class="header"></td>
            <td class="header">Upgrade Item</td>
            <td class="header">Path</td>
            <td class="header">Result</td>
          </tr>
          <xsl:for-each select="log/event[@action='Upgrade']">
          <tr style="font-name: Verdana; font-size: 8pt">
            <td valign="top">
                <xsl:if test=".!=''">
                    <xsl:if test="@type='Error'"><img src="_UpgradeWizard_Files/Error.png" /></xsl:if>
                    <xsl:if test="@type='Warning'"><img src="_UpgradeWizard_Files/Warning.png" /></xsl:if>
                </xsl:if>
                <xsl:if test=".=''"><img src="_UpgradeWizard_Files/Success.png" /></xsl:if>
            </td>            
            <td valign="top"><xsl:value-of select="@name"/></td>            
            <td valign="top">
                <xsl:choose>
                    <xsl:when test="@path!=''"><xsl:value-of select="@path"/></xsl:when>
                    <xsl:otherwise>&#160;</xsl:otherwise>
                </xsl:choose>
            </td>
            <td valign="top"><xsl:attribute name="class">
                <xsl:if test=".!=''">
                    <xsl:if test="@type='Warning'">warning</xsl:if>
                    <xsl:if test="@type='Error'">error</xsl:if>
                </xsl:if>
                <xsl:if test=".=''">Success</xsl:if></xsl:attribute>
              <xsl:if test=".!=''"><xsl:value-of select="."/></xsl:if>
              <xsl:if test=".=''"><xsl:value-of select="@type"/></xsl:if>
             </td>
          </tr>
          </xsl:for-each>
        </table>
    </div>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>