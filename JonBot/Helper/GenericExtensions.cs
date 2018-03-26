using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JonBot.Helper
{
    public static class GenericExtensions
    {
        public static T SelectRandomdly<T>(this IEnumerable<T> list) => list.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
    }
}