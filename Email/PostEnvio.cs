using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


public class PostEnvio
{
    [Microsoft.SqlServer.Server.SqlFunction()]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
    public static string send(SqlString from, SqlString to, SqlString subject, SqlString htmlbody, SqlString textbody)
        {
        string gRequest = "";
        try
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://9r5pjy.api.infobip.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "U2VydmljaW9CYXRhOiQlNTNydjFjMTBCNHQ0JSQ=");
            var request = new MultipartFormDataContent();
            request.Add(new StringContent(from.ToString()), "from");
            request.Add(new StringContent(to.ToString()), "to");
            request.Add(new StringContent(subject.ToString()), "subject");          
            request.Add(new StringContent(htmlbody.ToString()), "html");         
            var response = client.PostAsync("email/2/send", request).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                gRequest = responseString;


                gRequest = "{\"to\":" + "\"" + to.ToString() + "\"" + "," +
                               "\"SubmittedAt\":" + "\"" + DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString() + "\"" + "," +
                               "\"MessageID\":" + "\"" + "1" + "\"" + "," +
                               "\"ErrorCode\":" + "\"" + "0" + "\"" + "," +
                               "\"Message\":" + "\"" + "ok" + "\"" + "}";
            }
            else
            {
                gRequest = "{\"to\":" + "\"" + to.ToString() + "\"" + "," +
                              "\"SubmittedAt\":" + "\"" + DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString() + "\"" + "," +
                              "\"MessageID\":" + "\"" + "1" + "\"" + "," +
                              "\"ErrorCode\":" + "\"" + "1" + "\"" + "," +
                              "\"Message\":" + "\"" + "sin envio de correo" + "\"" + "}";
            }

           
        }
        catch (Exception exc)
        {

            gRequest = "{\"to\":" + "\"" + to.ToString() + "\"" + "," +
                           "\"SubmittedAt\":" + "\"" + DateTime.Today.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString() + "\"" + "," +
                           "\"MessageID\":" + "\"" + "1" + "\"" + "," +
                           "\"ErrorCode\":" + "\"" + "1" + "\"" + "," +
                           "\"Message\":" + "\"" + exc.Message + "\"" + "}";
           
        }
        return gRequest;
    }
}

