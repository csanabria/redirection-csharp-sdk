using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlacetoPay.Integrations.Library.CSharp;
using PlacetoPay.Integrations.Library.CSharp.Carrier;
using PlacetoPay.Integrations.Library.CSharp.Contracts;
using PlacetoPay.Integrations.Library.CSharp.Entities;
using PlacetoPay.Integrations.Library.CSharp.Message;
using PlaceToPay.Integrations.WebForms.Tests.SDK;
using P2P = PlacetoPay.Integrations.Library.CSharp.PlacetoPay;

namespace PlaceToPay.Integrations.WebForms.Tests
{
    public class utilP2P
    {
        internal static Gateway GenerarGatewayP2PTEC(string seed)
        { 
                //De: https://gist.github.com/dnetix/c18cc44861c5702d2b8ff2327b031c3e

            //String login = "usuarioprueba";
            //String tranKey = "ABCD1234";

            // Datos brindados al TEC para pruebas
            String loginTEC = "c042f82333f407bd2b2b7ae36735eb6f";
            String secretKeyTEC = "l860A3q3uZno05CG";


            SDK.Authentication auth = new SDK.Authentication(loginTEC, secretKeyTEC);

            // Example of the values to use. YOU NEED TO CHANGE FOR YOUR OWN LOGIN AND TRANKEY
            var Login = auth.getLogin();
            var TranKey = auth.getTranKey();
            var Seed = auth.getSeed();
            var Nonce = auth.getNonce();

            // lo obtiene arriba String nonce = "1001";
            //String seed = (DateTime.UtcNow).ToString("yyyy-MM-ddTHH:mm:sszzz");
            // lo obtiene arriba String seed = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");    //Fecha actual, la cual se genera en formato ISO 8601.
            String expirationTEC = (DateTime.UtcNow.AddMinutes(15)).ToString("yyyy-MM-ddTHH:mm:ssZ");

            auth.setNonce(Nonce).setSeed(Seed);

            Gateway gateway = new P2P(loginTEC, secretKeyTEC, new Uri("https://checkout-test.placetopay.com"), Gateway.TP_REST);

            return gateway;
        }
    }
}