using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;

namespace test123
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = index.GetJsonContent("http://www.ktec.gov.tw/ktec_api.php?type=json");
            Data data = JsonConvert.DeserializeObject<Data>(content);

            message.InnerHtml += "<Caption><h1>高雄市政府相關求才資訊發佈</h1></Caption>";
            message.InnerHtml += "<Table><TR><TH>類型</TH><TH>主題</TH><TH>發表日期</TH></TR>";

            int i = 0;
            foreach(var item in data.entries)
            {
                if (item.title.Length > 35)
                {
                    item.title = item.title.Substring(0, 35);
                    item.title += "...<詳情請點>";
                    message.InnerHtml += "<tr>" + "<td>" + item.category + "</td>" +
                        "<td><a href=\"detail.aspx?i=" + i + "\">"+ item.title + "</a></td>" +
                        "<td>" + item.publication_date + "</td></tr>";
                            i++;
                }
                else
                {
                    message.InnerHtml += "<tr>" + "<td>" + item.category + "</td>" +
                                        "<td><a href=\"detail.aspx?i=" + i + "\">" + item.title + "</a></td>" +
                                        "<td>" + item.publication_date + "</td></tr>";
                                        i++;
                }
            }
            message.InnerHtml += "</Table>";

        }

        private static string GetJsonContent(string Url)
        {
            string targetURI = Url;
            var request = System.Net.WebRequest.Create(targetURI);
            request.ContentType = "aplication/json; charset=utf-8;";
            var response = request.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream() ) )
            {
                text = sr.ReadToEnd();
            }

            return text;
        }

    }
}