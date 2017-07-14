namespace MainSoftLib.PayuLatam.Entities.Payment
{
    public class EntyPaymentCredit : EntryPayu
    {
        public Transaction transaction { get; set; }

        public EntyPaymentCredit(bool test, string language, string apiLogin, string apiKey, string command, Transaction transaction) : base(test, language, apiLogin, apiKey, command)
        {
            this.transaction = transaction;
        }

        public class Transaction
        {
            public Order order { get; set; }
            public Payer payer { get; set; }
            public CreditCard creditCard { get; set; }
            public ExtraParameters extraParameters { get; set; }

            public string type { get; set; }
            public string paymentMethod { get; set; }
            public string paymentCountry { get; set; }
            public string deviceSessionId { get; set; }
            public string ipAddress { get; set; }
            public string cookie { get; set; }
            public string userAgent { get; set; }

            public Transaction()
            {
                order = new Order();
                payer = new Payer();
                creditCard = new CreditCard();
                extraParameters = new ExtraParameters();
            }
        }

        public class TXVALUE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues
        {
            public AdditionalValues()
            {
                TX_VALUE = new TXVALUE();
            }

            public TXVALUE TX_VALUE { get; set; }
        }

        public class ShippingAddress
        {
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string postalCode { get; set; }
            public string phone { get; set; }
        }

        public class Buyer
        {
            public Buyer()
            {
                shippingAddress = new ShippingAddress();
            }

            public string merchantBuyerId { get; set; }
            public string fullName { get; set; }
            public string emailAddress { get; set; }
            public string contactPhone { get; set; }
            public string dniNumber { get; set; }
            public ShippingAddress shippingAddress { get; set; }
        }

        public class ShippingAddress2
        {
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string postalCode { get; set; }
            public string phone { get; set; }
        }

        public class Order
        {
            public Order()
            {
                additionalValues = new AdditionalValues();
                buyer = new Buyer();
                shippingAddress = new ShippingAddress2();
            }

            public string accountId { get; set; }
            public string referenceCode { get; set; }
            public string description { get; set; }
            public string language { get; set; }
            public string signature { get; set; }
            public string notifyUrl { get; set; }
            public AdditionalValues additionalValues { get; set; }
            public Buyer buyer { get; set; }
            public ShippingAddress2 shippingAddress { get; set; }
        }

        public class BillingAddress
        {
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string postalCode { get; set; }
            public string phone { get; set; }
        }

        public class Payer
        {
            public Payer()
            {
                billingAddress = new BillingAddress();
            }

            public string merchantPayerId { get; set; }
            public string fullName { get; set; }
            public string emailAddress { get; set; }
            public string contactPhone { get; set; }
            public string dniNumber { get; set; }
            public BillingAddress billingAddress { get; set; }
        }

        public class CreditCard
        {
            public string number { get; set; }
            public string securityCode { get; set; }
            public string expirationDate { get; set; }
            public string name { get; set; }
        }

        public class ExtraParameters
        {
            public int INSTALLMENTS_NUMBER { get; set; }
        }
    }

    public class EntyPaymentCreditResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public TransactionResponse transactionResponse { get; set; }

        public class TransactionResponse
        {
            public int orderId { get; set; }
            public string transactionId { get; set; }
            public string state { get; set; }
            public string paymentNetworkResponseCode { get; set; }
            public object paymentNetworkResponseErrorMessage { get; set; }
            public string trazabilityCode { get; set; }
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

    public class EntyPaymentCash : EntryPayu
    {
        public EntyPaymentCash(bool test, string language, string apiLogin, string apiKey, string command, Transaction transaction) : base(test, language, apiLogin, apiKey, command)
        {
            this.transaction = transaction;
        }

        public Transaction transaction { get; set; }


        public class TXVALUE
        {
            public int value { get; set; }
            public string currency { get; set; }
        }

        public class AdditionalValues
        {
            public AdditionalValues()
            {
                TX_VALUE = new TXVALUE();
            }

            public TXVALUE TX_VALUE { get; set; }
        }

        public class Buyer
        {
            public string emailAddress { get; set; }
        }

        public class Order
        {
            public Order()
            {
                additionalValues = new AdditionalValues();
                buyer = new Buyer();
            }

            public string accountId { get; set; }
            public string referenceCode { get; set; }
            public string description { get; set; }
            public string language { get; set; }
            public string signature { get; set; }
            public string notifyUrl { get; set; }
            public AdditionalValues additionalValues { get; set; }
            public Buyer buyer { get; set; }
        }

        public class Transaction
        {
            public Transaction()
            {
                order = new Order();
            }

            public Order order { get; set; }
            public string type { get; set; }
            public string paymentMethod { get; set; }
            public string expirationDate { get; set; }
            public string paymentCountry { get; set; }
            public string ipAddress { get; set; }
        }
    }

    public class EntyPaymentCashResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public TransactionResponse transactionResponse { get; set; }

        public class ExtraParameters
        {
            public string URL_PAYMENT_RECEIPT_HTML { get; set; }
            public long EXPIRATION_DATE { get; set; }
            public int REFERENCE { get; set; }
        }

        public class TransactionResponse
        {
            public int orderId { get; set; }
            public string transactionId { get; set; }
            public string state { get; set; }
            public object paymentNetworkResponseCode { get; set; }
            public object paymentNetworkResponseErrorMessage { get; set; }
            public object trazabilityCode { get; set; }
            public object authorizationCode { get; set; }
            public string pendingReason { get; set; }
            public string responseCode { get; set; }
            public object errorCode { get; set; }
            public object responseMessage { get; set; }
            public object transactionDate { get; set; }
            public object transactionTime { get; set; }
            public object operationDate { get; set; }
            public ExtraParameters extraParameters { get; set; }
        }


    }
}