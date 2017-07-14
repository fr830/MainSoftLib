using System.Collections.Generic;

namespace MainSoftLib.PayuLatam.Entities.Order
{
    public class EntyOrderReference : EntryPayu
    {
        public EntyOrderReference(bool test, string language, string apiLogin, string apiKey, string command) : base (test, language, apiLogin, apiKey, command)
        {
            details = new Details();
        }

        
        public Details details { get; set; }

        public class Details
        {
            public string referenceCode { get; set; }
        }
    }

    public class EntyOrderRefResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public Result result { get; set; }

        public class ShippingAddress
        {
            public object street1 { get; set; }
            public object street2 { get; set; }
            public object city { get; set; }
            public object state { get; set; }
            public string country { get; set; }
            public object postalCode { get; set; }
            public object phone { get; set; }
        }

        public class Buyer
        {
            public object merchantBuyerId { get; set; }
            public string fullName { get; set; }
            public string emailAddress { get; set; }
            public object contactPhone { get; set; }
        }

        public class CreditCard
        {
            public string maskedNumber { get; set; }
            public string name { get; set; }
            public object issuerBank { get; set; }
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

        public class Payer
        {
            public object merchantPayerId { get; set; }
            public string fullName { get; set; }
            public object billingAddress { get; set; }
            public string emailAddress { get; set; }
            public object contactPhone { get; set; }
            public object dniNumber { get; set; }
        }

        public class PMTAXADMINISTRATIVEFEE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXADDITIONALVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAXADMINISTRATIVEFEERETURNBASE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAXRETURNBASE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMADMINISTRATIVEFEE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class PMVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXADMINISTRATIVEFEE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXNETWORKVALUE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class PMNETWORKVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMADDITIONALVALUE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXRETURNBASE
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAX
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAX
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXADMINISTRATIVEFEE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXADMINISTRATIVEFEERETURNBASE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues
        {
            public PMTAXADMINISTRATIVEFEE PM_TAX_ADMINISTRATIVE_FEE { get; set; }
            public TXADDITIONALVALUE TX_ADDITIONAL_VALUE { get; set; }
            public PMTAXADMINISTRATIVEFEERETURNBASE PM_TAX_ADMINISTRATIVE_FEE_RETURN_BASE { get; set; }
            public TXVALUE TX_VALUE { get; set; }
            public PMTAXRETURNBASE PM_TAX_RETURN_BASE { get; set; }
            public PMADMINISTRATIVEFEE PM_ADMINISTRATIVE_FEE { get; set; }
            public PMVALUE PM_VALUE { get; set; }
            public TXADMINISTRATIVEFEE TX_ADMINISTRATIVE_FEE { get; set; }
            public TXNETWORKVALUE TX_NETWORK_VALUE { get; set; }
            public PMNETWORKVALUE PM_NETWORK_VALUE { get; set; }
            public PMADDITIONALVALUE PM_ADDITIONAL_VALUE { get; set; }
            public TXTAXRETURNBASE TX_TAX_RETURN_BASE { get; set; }
            public PMTAX PM_TAX { get; set; }
            public TXTAX TX_TAX { get; set; }
            public TXTAXADMINISTRATIVEFEE TX_TAX_ADMINISTRATIVE_FEE { get; set; }
            public TXTAXADMINISTRATIVEFEERETURNBASE TX_TAX_ADMINISTRATIVE_FEE_RETURN_BASE { get; set; }
        }

        public class ExtraParameters
        {
            public string RESPONSE_URL { get; set; }
            public string INSTALLMENTS_NUMBER { get; set; }
        }

        public class Transaction
        {
            public string id { get; set; }
            public object order { get; set; }
            public CreditCard creditCard { get; set; }
            public string type { get; set; }
            public object parentTransactionId { get; set; }
            public string paymentMethod { get; set; }
            public object source { get; set; }
            public string paymentCountry { get; set; }
            public TransactionResponse transactionResponse { get; set; }
            public object deviceSessionId { get; set; }
            public string ipAddress { get; set; }
            public string cookie { get; set; }
            public string userAgent { get; set; }
            public object expirationDate { get; set; }
            public Payer payer { get; set; }
            public AdditionalValues additionalValues { get; set; }
            public ExtraParameters extraParameters { get; set; }
        }

        public class PMTAXADMINISTRATIVEFEE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXADDITIONALVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAXADMINISTRATIVEFEERETURNBASE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMADMINISTRATIVEFEE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAXRETURNBASE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXADMINISTRATIVEFEE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXNETWORKVALUE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class PMNETWORKVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMADDITIONALVALUE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXRETURNBASE2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class PMTAX2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAX2
        {
            public double value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXADMINISTRATIVEFEE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class TXTAXADMINISTRATIVEFEERETURNBASE2
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues2
        {
            public PMTAXADMINISTRATIVEFEE2 PM_TAX_ADMINISTRATIVE_FEE { get; set; }
            public TXADDITIONALVALUE2 TX_ADDITIONAL_VALUE { get; set; }
            public PMTAXADMINISTRATIVEFEERETURNBASE2 PM_TAX_ADMINISTRATIVE_FEE_RETURN_BASE { get; set; }
            public TXVALUE2 TX_VALUE { get; set; }
            public PMADMINISTRATIVEFEE2 PM_ADMINISTRATIVE_FEE { get; set; }
            public PMTAXRETURNBASE2 PM_TAX_RETURN_BASE { get; set; }
            public PMVALUE2 PM_VALUE { get; set; }
            public TXADMINISTRATIVEFEE2 TX_ADMINISTRATIVE_FEE { get; set; }
            public TXNETWORKVALUE2 TX_NETWORK_VALUE { get; set; }
            public PMNETWORKVALUE2 PM_NETWORK_VALUE { get; set; }
            public PMADDITIONALVALUE2 PM_ADDITIONAL_VALUE { get; set; }
            public TXTAXRETURNBASE2 TX_TAX_RETURN_BASE { get; set; }
            public PMTAX2 PM_TAX { get; set; }
            public TXTAX2 TX_TAX { get; set; }
            public TXTAXADMINISTRATIVEFEE2 TX_TAX_ADMINISTRATIVE_FEE { get; set; }
            public TXTAXADMINISTRATIVEFEERETURNBASE2 TX_TAX_ADMINISTRATIVE_FEE_RETURN_BASE { get; set; }
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
            public string notifyUrl { get; set; }
            public ShippingAddress shippingAddress { get; set; }
            public Buyer buyer { get; set; }
            public List<Transaction> transactions { get; set; }
            public AdditionalValues2 additionalValues { get; set; }
        }

        public class Result
        {
            public List<Payload> payload { get; set; }
        }
    }
}