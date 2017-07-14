using System.Collections.Generic;

namespace MainSoftLib.PayuLatam.Entities.Order
{
    public class EntyOrderDetails: EntryPayu
    {
        public EntyOrderDetails(bool test, string language, string apiLogin, string apiKey, string command) : base(test, language, apiLogin, apiKey, command)
        {
            details = new Details();
        }

        public Details details { get; set; }

        public class Details
        {
            public int orderId { get; set; }
        }
    }

    public class EntyOrderDetailsResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public Result result { get; set; }


        public class Buyer
        {
            public object merchantBuyerId { get; set; }
            public string fullName { get; set; }
            public string emailAddress { get; set; }
            public object contactPhone { get; set; }
        }

        public class TransactionResponse
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

        public class TXTAXRETURNBASE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAX
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues
        {
            public TXTAXRETURNBASE TX_TAX_RETURN_BASE { get; set; }
            public TXTAX TX_TAX { get; set; }
            public TXVALUE TX_VALUE { get; set; }
        }

        public class ExtraParameters
        {
            public string CHECKOUT_VERSION { get; set; }
        }

        public class Transaction
        {
            public string id { get; set; }
            public object order { get; set; }
            public object creditCard { get; set; }
            public object bankAccount { get; set; }
            public string type { get; set; }
            public object parentTransactionId { get; set; }
            public object paymentMethod { get; set; }
            public string source { get; set; }
            public object paymentCountry { get; set; }
            public TransactionResponse transactionResponse { get; set; }
            public object deviceSessionId { get; set; }
            public string ipAddress { get; set; }
            public string cookie { get; set; }
            public string userAgent { get; set; }
            public object expirationDate { get; set; }
            public object payer { get; set; }
            public AdditionalValues additionalValues { get; set; }
            public ExtraParameters extraParameters { get; set; }
        }

        public class TXTAXRETURNBASE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAX2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues2
        {
            public TXTAXRETURNBASE2 TX_TAX_RETURN_BASE { get; set; }
            public TXTAX2 TX_TAX { get; set; }
            public TXVALUE2 TX_VALUE { get; set; }
        }

        public class Payload
        {
            public int id { get; set; }
            public int accountId { get; set; }
            public string status { get; set; }
            public string referenceCode { get; set; }
            public string description { get; set; }
            public object airlineCode { get; set; }
            public string language { get; set; }
            public object notifyUrl { get; set; }
            public object shippingAddress { get; set; }
            public Buyer buyer { get; set; }
            public object antifraudMerchantId { get; set; }
            public List<Transaction> transactions { get; set; }
            public AdditionalValues2 additionalValues { get; set; }
        }

        public class Result
        {
            public Payload payload { get; set; }
        }
    }

   
}