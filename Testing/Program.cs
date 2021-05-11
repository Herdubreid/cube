using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Celin
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var e1 = new AIS.Server("https://demo.steltix.com/jderest/v2/");
            e1.AuthRequest.username = "demo";
            e1.AuthRequest.password = "demo";

            var rs = await e1.RequestAsync(new AIS.DiscoverUBERequest
            {
                reportName = "R45300",
                reportVersion = "ZJDE0001"
            });

            foreach (var d in rs.dataSelectionColumns)
            {
                Console.WriteLine("{0} {1}", d.dictItem, d.displayString);
            }
            Console.ReadKey();
        }
    }
}
