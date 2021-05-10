namespace Celin.F983051
{
    public class Row
    {
        public string F983051_PID { get; set; }
        public string F983051_VERS { get; set; }
        public string F983051_JD { get; set; }
        public bool Selected { get; set; }
    }
    public class Response : AIS.FormResponse
    {
        public AIS.Form<AIS.FormData<Row>> fs_DATABROWSE_F983051 { get; set; }
    }
    public class Request : AIS.DatabrowserRequest
    {
        public Request(string pid)
        {
            outputType = GRID_DATA;
            dataServiceType = BROWSE;
            targetType = table;
            targetName = "F983051";
            returnControlIDs = "PID|VERS|JD";
            query = new AIS.Query
            {
                matchType = AIS.Query.MATCH_ALL,
                condition = new[]
                {
                    new AIS.Condition
                    {
                        controlId = "F983051.PID",
                        @operator = AIS.Condition.EQUAL,
                        value = new[]
                        {
                            new AIS.Value
                            {
                                content = pid,
                                specialValueId = AIS.Value.LITERAL
                            }
                        }
                    }
                }
            };
        }
    }
}
