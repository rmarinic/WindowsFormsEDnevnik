using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace NTPSoapService
{
    /// <summary>
    /// Summary description for NTPSoap
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NTPSoap : System.Web.Services.WebService
    {

        [WebMethod]
        public string ConvertXmlToJson(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc);
            return jsonText;
        }

        [WebMethod]
        public string ConvertJsonToXml(string jsonString)
        {
            XNode node = Newtonsoft.Json.JsonConvert.DeserializeXNode(jsonString, "Root");
            return node.ToString();
        }
    }
}
