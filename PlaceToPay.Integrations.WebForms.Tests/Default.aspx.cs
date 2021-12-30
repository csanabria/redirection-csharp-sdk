using AuthenticationRedirection;
using Newtonsoft.Json;
using PlacetoPay.Integrations.Library.CSharp;
using PlacetoPay.Integrations.Library.CSharp.Entities;
using PlacetoPay.Integrations.Library.CSharp.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
//placetopay
using P2P = PlacetoPay.Integrations.Library.CSharp.PlacetoPay;

namespace PlaceToPay.Integrations.WebForms.Tests
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAuth_Click(object sender, EventArgs e)
        {


            // Datos brindados al TEC para pruebas
            string loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            string secretKeyTEC = "l860A3q3uZno05CG";

            //Autenticacion
            var nonce = "10001";                    //Valor aleatorio para cada solicitud.
            var nonceBase64 = util.Base64Encode(nonce);  //Valor aleatorio para cada solicitud codificado en Base64.
            var seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); //Fecha actual, la cual se genera en formato ISO 8601.

            //calculo del trankey
            var cadena = nonce + seed + secretKeyTEC;
            var hashCadena = util.HashSha256(cadena);
            var tranKey = util.Base64Encode(hashCadena);

            var expirationTEC = DateTime.UtcNow.AddMinutes(15).ToString("yyyy-MM-ddTHH:mm:ssZ");
            var userAgentTEC = "Chrome/51.0.2704.64 Safari/537.36";


            PlacetoPay.Integrations.Library.CSharp.PlacetoPay p = new PlacetoPay.Integrations.Library.CSharp.PlacetoPay(loginTEC, tranKey, new Uri("http://wwww.itcr.ac.cr"), "REST");
            PlacetoPay.Integrations.Library.CSharp.Contracts.Gateway gateway = new P2P(loginTEC, tranKey, new Uri("http://127.0.0.1"), PlacetoPay.Integrations.Library.CSharp.Contracts.Gateway.TP_REST);

            //
            //RedirectInformation response = gateway.Query(requestId);

            //Ejemplo de llamada:

            Amount amount = new Amount(10, "CRC");
            Payment payment = new Payment("REFERENCE", "DESCRIPTION", amount);
            RedirectRequest request = new RedirectRequest(
                payment: payment,
                returnUrl: "127.0.0.1",
                ipAddress: "127.0.0.1",
                userAgent: userAgentTEC,
                expiration: expirationTEC
            );
            RedirectResponse response = gateway.Request(request);


            //p.Collect(new CollectRequest())


            //Authentication auth = new Authentication(loginTEC, tranKey);

            //Collect
            //var documento = nonce;
            //var tipoDocumento = "Recibo";
            //var payment = new Payment(
            //    reference: documento,
            //    description: "Recibo de prueba",
            //    amount: new Amount(100, "CRC"),
            //    allowPartial: false,
            //    items: new List<Item>()
            //        {
            //            new Item("MAT", "Matricula de Créditos Prueba", "AYR", 4, 4000, 0)
            //        }
            //);

            //Instrument instrument = new Instrument(
            //    bank: new Bank(
            //        code: "01",
            //        name: "BCR"
            //    ),
            //    card: new Card(
            //        name: "Prueba",
            //        number: "numero",
            //        cvv: "cvv",
            //        expirationMonth: "mes",
            //        expirationYear: "anno",
            //        installments: 1,            //? 
            //        kind: "C"                  //valor default
            //   ),
            //   token: new Token(
            //       token: "",
            //       status: null,
            //       subtoken: null,
            //       franchiseName: null,
            //       issuerName: null,
            //       lastDigits: null,
            //       validUntil: null,
            //       cvv: null,
            //       installments: null
            //   ),
            //   pin: null,
            //   password : null
            //);

            //Person payer = new Person(documento, tipoDocumento, "Christian", "Sanabria", "csanabria@itcr.ac.cr", company: "TEC", mobile: "+50688189979");
            //CollectRequest cr = new CollectRequest(payer, payment, instrument, "es_CR");
        }

        //De: https://www.codegrepper.com/code-examples/javascript/httpclient+post+request+c%23+add+json+body+and+headers
        private static async Task PostBasicAsync(object content, CancellationToken cancellationToken)
        {
            string url = "";
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(content);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
        }

        protected void btnPagar2_Click(object sender, EventArgs e)
        {
            // Datos brindados al TEC para pruebas
            string loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            string secretKeyTEC = "l860A3q3uZno05CG";

            //Autenticacion
            var nonce = "10001";                    //Valor aleatorio para cada solicitud.
            var nonceBase64 = util.Base64Encode(nonce);  //Valor aleatorio para cada solicitud codificado en Base64.
            var seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); //Fecha actual, la cual se genera en formato ISO 8601.

            //calculo del trankey
            var cadena = nonceBase64 + seed + secretKeyTEC;     //SI se usa el nonce base64 encoded
            var hashCadena = util.HashSha256(cadena);
            var tranKey = util.Base64Encode(hashCadena);

            ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://checkout-test.placetopay.com/api/session");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new {
                    locale = "es_CR",
                    auth = new {
                        login = loginTEC,
                        tranKey = tranKey,
                        nonce = nonce,          //NO es el base64 encoded
                        seed = seed
                    }
                });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                txtResult.Text = result;
            }
        }

        protected async void btnPagar3_Click(object sender, EventArgs e)
        {
            // Datos brindados al TEC para pruebas
            string loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            string secretKeyTEC = "l860A3q3uZno05CG";

            //Autenticacion
            var nonce = "10001";                                            //Valor aleatorio para cada solicitud.
            var nonceBase64 = util.Base64Encode(nonce);                          //Valor aleatorio para cada solicitud codificado en Base64.
            var seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");    //Fecha actual, la cual se genera en formato ISO 8601.

            //calculo del trankey
            var cadena = nonce + seed + secretKeyTEC;                 //SI se usa el nonce base64 encoded
            var hashCadena = util.HashSha256(cadena);
            var tranKey = util.Base64Encode(hashCadena);

            ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string cadenaJson = new JavaScriptSerializer().Serialize(new
            {
                locale = "es_CR",
                auth = new
                {
                    login = loginTEC,
                    tranKey = tranKey,
                    nonce = nonce,                                          //NOOOO user el base64 encoded
                    seed = seed
                }
            });

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://checkout-test.placetopay.com/api/");

                // serialize your json using newtonsoft json serializer then add it to the StringContent
                var content = new StringContent(cadenaJson, Encoding.UTF8, "application/json");

                // method address would be like api/callUber:SomePort for example
                var result = await client.PostAsync("session", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                txtResult.Text = resultContent;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("usoRestSharp.aspx");
        }
    }
}