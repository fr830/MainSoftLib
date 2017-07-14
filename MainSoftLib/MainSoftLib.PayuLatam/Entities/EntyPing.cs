namespace MainSoftLib.PayuLatam.Entities.Ping
{
    public class EntyPing : EntryPayu
    {
        public EntyPing(bool test, string language, string apiLogin, string apiKey, string command) : base(test, language, apiLogin, apiKey, command)
        {

        }
    }


    public class EntyPingResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public string payload { get; set; }
        }
    }
}
