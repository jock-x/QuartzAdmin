using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliFor.Tasks.QuartzNet.utils
{
    public class DescibeHttpResponse
    {
        public string ResponseString = "";


        public string DescibeResponseString(List<string> listmessage)
        {
            if (ResponseString.IsNotEmptyOrNull())
            {

                ResponseString += "<br>";

            }
            foreach(string item in listmessage)
            {


                ResponseString += item + "<br>";

            }
            return ResponseString;
        }
        

        public string outstring()
        {
            return ResponseString;
        }
    }
}
