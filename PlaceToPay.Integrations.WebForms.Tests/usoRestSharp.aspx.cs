using PlacetoPay.Integrations.Library.CSharp.Contracts;
using PlacetoPay.Integrations.Library.CSharp.Entities;
using PlacetoPay.Integrations.Library.CSharp.Message;
using PlaceToPay.Integrations.WebForms.Tests.SDK;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using P2P = PlacetoPay.Integrations.Library.CSharp.PlacetoPay;

namespace PlaceToPay.Integrations.WebForms.Tests
{
    public partial class usoRestSharp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            // Datos brindados al TEC para pruebas
            string loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            string secretKeyTEC = "l860A3q3uZno05CG";

            //Autenticacion
            var nonce = "10001";                                            //Valor aleatorio para cada solicitud.
            var nonceBase64 = util.Base64Encode(nonce);                          //Valor aleatorio para cada solicitud codificado en Base64.
            var seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");    //Fecha actual, la cual se genera en formato ISO 8601.

            //calculo del trankey
            var cadena = nonce + seed + secretKeyTEC;                 //NO se usa el nonce base64 encoded
            var hashCadena = util.HashSha256(cadena);
            var tranKey = util.Base64Encode(hashCadena);

            ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;



            RestClient client = new RestClient("https://checkout-test.placetopay.com/api/");
            RestRequest request = new RestRequest("session", Method.POST);
            request.AddHeader("Accept", "application/json");
            var body = new
            {
                locale = "es_CR",
                auth = new
                {
                    login = loginTEC,
                    tranKey = tranKey,
                    nonce = nonceBase64,                                          //user el base64 encoded
                    seed = seed
                }
            };
            request.AddJsonBody(body);

            var response = client.Execute(request).Content;

            txtResponse.Text = response;
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            //De: https://gist.github.com/dnetix/c18cc44861c5702d2b8ff2327b031c3e

            //String login = "usuarioprueba";
            //String tranKey = "ABCD1234";

            // Datos brindados al TEC para pruebas
            //String loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            //String secretKeyTEC = "l860A3q3uZno05CG";


            //Authentication auth = new Authentication(loginTEC, secretKeyTEC);

            //// Example of the values to use. YOU NEED TO CHANGE FOR YOUR OWN LOGIN AND TRANKEY
            //var Login = auth.getLogin();
            //var TranKey = auth.getTranKey();
            //var Seed = auth.getSeed();
            //var Nonce = auth.getNonce();

            //// lo obtiene arriba String nonce = "1001";
            String seed = (DateTime.UtcNow).ToString("yyyy-MM-ddTHH:mm:sszzz");
            //// lo obtiene arriba String seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");    //Fecha actual, la cual se genera en formato ISO 8601.
            String expirationTEC = (DateTime.UtcNow.AddMinutes(15)).ToString("yyyy-MM-ddTHH:mm:ssZ");

            //auth.setNonce(Nonce).setSeed(Seed);

            //Gateway gateway = new P2P(loginTEC, secretKeyTEC, new Uri("https://checkout-test.placetopay.com"), Gateway.TP_REST);

            Gateway gateway = utilP2P.GenerarGatewayP2PTEC(seed);

            //Crear una nueva sesión de pago
            //Solicita la creación de la sesión(petición de cobro o suscripción) y retorna el identificador y la URL de procesamiento.

            Amount amount = new Amount(100, "CRC");
            Payment payment = new Payment("Num. Recibo", "Prueba TEC con Pasarela de pago", amount);
            RedirectRequest request = new RedirectRequest(payment, Request.UserHostAddress, "http://127.0.0.1", Request.UserAgent, expirationTEC);

            //cosas que veo faltantes en el request
            request.Locale = "es_CR";
            request.IpAddress = "127.0.0.1";
            request.ReturnUrl = "https://localhost:44341/Retorno?RequestId="; // ??? + requestId;


            //inicio versión en prueba
            RedirectResponse response = gateway.Request(request);

            if (response != null)
            {
                if (response.IsSuccessful())
                {
                    Session.Add("RequestId", response.RequestId);
                    Response.Redirect(response.ProcessUrl);
                }
                else
                {
                    txtResponse.Text = response.Status.Message;
                }
            }
            else
            {
                txtResponse.Text = "Retornó Null";
            }

            //fin versión en prueba
            //inicio versión ya probagda
            //RedirectResponse response = gateway.Request(request);


            //if (response != null) { 
            //    if (response.IsSuccessful())
            //    {

            //        Response.Redirect(response.ProcessUrl);
            //    }
            //    else
            //    {
            //        txtResponse.Text = response.Status.Message;
            //    }
            //}
            //else
            //{
            //    txtResponse.Text = "Retornó Null";
            //}
        }
    }
}