namespace MainSoftLib.PayuLatam.Entities
{
    public class EntryPayu
    {
        public bool test { get; set; }
        public string language { get; set; }
        public string command { get; set; }
        public Merchant merchant { get; set; }
        string apiLogin { get; set; }
        string apiKey { get; set; }

        public EntryPayu(bool test, string language, string apiLogin, string apiKey, string command)
        {
            this.test = test;
            this.language = language;
            this.apiLogin = apiLogin;
            this.apiKey = apiKey;
            this.command = command;

            this.merchant = new Merchant(apiLogin, apiKey);
        }

        public class Merchant
        {
            public Merchant(string apiLogin, string apiKey)
            {
                this.apiLogin = apiLogin;
                this.apiKey = apiKey;
            }

            public string apiLogin { get; set; }
            public string apiKey { get; set; }
        }
    }
}
