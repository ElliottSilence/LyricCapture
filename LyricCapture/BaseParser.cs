using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LyricCapture
{
    abstract class BaseParser: IBaseParser
    {
        private ParserType parserType;

        protected void SetParserType(ParserType parserType)
        {
            this.parserType = parserType;
        }

        public ParserType GetParserType()
        {
            return this.parserType;
        }

        public abstract Boolean CheckSong(String id);

        public abstract Song GetSong(String id);

        public abstract Boolean GetSongMp3(String id, String filename);

        protected String HttpRequestGet(String url)
        {
            return HttpRequestGet(url, Encoding.UTF8);
        }

        protected String HttpRequestGet(String url, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 2000;
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            String retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public override String ToString()
        {
            return parserType.ToString();
        }
    }
}
