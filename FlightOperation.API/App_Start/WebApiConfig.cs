using System.Linq;
using System.Web.Http;

namespace FlightOperation.API.App_Start
{
    /// <summary>
    /// WebAPiConfig
    /// </summary>
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            config.MapHttpAttributeRoutes();
        }
    }
}