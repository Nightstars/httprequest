using System;
using System.IO;
using System.Net;
using System.Text;

namespace httprequest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string result_post = Post("http://unknowsite.site:8080/api/getall", "{\"typeid1\":\"1\"}");
            string result_get = Get("http://unknowsite.site:8080/api/getall?typeid1=1", 3000);
            //Console.WriteLine(result_post);
            Console.WriteLine(result_get);
            Console.ReadKey();
        }
        public static string Post(string url, string content)
        {
            //申明一个容器result接收数据
            string result = "";
            //首先创建一个HttpWebRequest,申明传输方式POST
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            //添加POST参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            //申明一个容器resp接收返回数据
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容 
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static string Get(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
