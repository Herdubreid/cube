using System.Linq;

namespace Celin.F9860
{
    public class Row
    {
        public string F9860_OBNM { get; set; }
        public string F9860_MD { get; set; }
    }
    public class Response : AIS.FormResponse
    {
        public AIS.Form<AIS.FormData<Row>> fs_DATABROWSE_F9860 { get; set; }
    }
    public class Request : AIS.DatabrowserRequest
    {
        public Request(string search, bool exact = false)
        {
            outputType = GRID_DATA;
            dataServiceType = BROWSE;
            targetType = table;
            targetName = "F9860";
            returnControlIDs = "MD|OBNM";
            maxPageSize = "20";
            var tokens = search.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            query = new AIS.Query
            {
                matchType = AIS.Query.MATCH_ALL,
                condition = new[]
                {
                    new AIS.Condition
                    {
                        controlId = "F9860.FUNO",
                        @operator = AIS.Condition.EQUAL,
                        value = new[]
                        {
                            new AIS.Value
                            {
                                content = "UBE",
                                specialValueId = AIS.Value.LITERAL
                            }
                        }
                    },
                    new AIS.Condition
                    {
                        controlId = "F9860.OBNM",
                        @operator = exact ? AIS.Condition.EQUAL : AIS.Condition.STR_START_WITH,
                        value = new []
                        {
                            new AIS.Value
                            {
                                content = tokens[0].ToUpper(),
                                specialValueId = AIS.Value.LITERAL
                            }
                        }
                    }
                }
            };
            foreach (var token in tokens.Skip(1))
            {
                query.condition = query.condition.Append(new AIS.Condition
                {
                    controlId = "F9860.MD",
                    @operator = AIS.Condition.STR_CONTAIN,
                    value = new[]
                    {
                        new AIS.Value
                        {
                            content = token,
                            specialValueId = AIS.Value.LITERAL
                        }
                    }
                });
            }
        }
    }
}