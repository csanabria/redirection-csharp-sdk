using Newtonsoft.Json;
using PlacetoPay.Integrations.Library.CSharp.Contracts;
using PlacetoPay.Integrations.Library.CSharp.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace PlaceToPay.Integrations.WebForms.Tests
{
    public partial class Retorno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String seed = (DateTime.UtcNow).ToString("yyyy-MM-ddTHH:mm:sszzz");
            //// lo obtiene arriba String seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");    //Fecha actual, la cual se genera en formato ISO 8601.
            String expirationTEC = (DateTime.UtcNow.AddMinutes(15)).ToString("yyyy-MM-ddTHH:mm:ssZ");

            Gateway gateway = utilP2P.GenerarGatewayP2PTEC(seed);

            string requestIdEsperada = Session["RequestId"].ToString();

            RedirectInformation response = gateway.Query(requestIdEsperada);

            //TODO: revisar response con el resultado de la transacción


            var resultado = response;  // como veo si se aprobó? //cambio
            //var resJson = JsonSerializer.Serialize(resultado);
            string resultadoText = Newtonsoft.Json.JsonConvert.SerializeObject(resultado);
            txtResultado.Text = resultadoText;

            if (resultado.Status.status == "APPROVED")
            {
                lblResultado.ForeColor = System.Drawing.Color.Blue;
                lblResultado.Text = "Transacción aprobada";
            }
            else if (resultado.Status.status == "REJECTED")
            {
                lblResultado.ForeColor = System.Drawing.Color.Red;
                lblResultado.Text = "Transacción RECHAZADA";
            }
            //falta revisar los demás poribles estados
            else
            {
                txtResultado.Text = "Resultado desconocido: " + resultado.Status.status;
            }
        }

    }
}