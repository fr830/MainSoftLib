namespace MainSoftLib.PayuLatam.Entities.Transaction
{
    public class EntyTransactionDetails : EntryPayu
    {
        public EntyTransactionDetails(bool test, string language, string apiLogin, string apiKey, string command) : base(test, language, apiLogin, apiKey, command)
        {
            details = new Details();
        }

        public Details details { get; set; }

        public class Details
        {
            public string transactionId { get; set; }
        }
    }

    public class EntyTransactionDetailsResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public Payload payload { get; set; }
        }

        public class Payload
        {
            public string state { get; set; }
            public object paymentNetworkResponseCode { get; set; }
            public object paymentNetworkResponseErrorMessage { get; set; }
            public object trazabilityCode { get; set; }
            public object authorizationCode { get; set; }
            public object pendingReason { get; set; }
            public string responseCode { get; set; }
            public object errorCode { get; set; }
            public object responseMessage { get; set; }
            public object transactionDate { get; set; }
            public object transactionTime { get; set; }
            public object operationDate { get; set; }
            public object extraParameters { get; set; }
        }
    }
    
   
}
