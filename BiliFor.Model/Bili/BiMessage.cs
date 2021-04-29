using System;
using System.Collections.Generic;

namespace BiliFor.Model.Bili
{
    public class BiMessage : HttpResponse<object>
    {

    }
    public class HttpResponse<TData>
    {
        public int Code { get; set; } = int.MinValue;

        public List<string> Message { get; set; } = new List<string>();

         public TData Data { get; set; }

    }
}
