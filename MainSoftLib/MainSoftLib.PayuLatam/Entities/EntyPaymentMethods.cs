using System.Collections.Generic;

namespace MainSoftLib.PayuLatam.Entities.Payment
{
    public class EntyPaymentMethods : EntryPayu
    {
        public EntyPaymentMethods(bool test, string language, string apiLogin, string apiKey, string command) : base(test, language, apiLogin, apiKey, command)
        {

        }
    }

    public class EntyPaymentMethodsResult
    {
        public string code { get; set; }
        public object error { get; set; }
        public List<PaymentMethod> paymentMethods { get; set; }

        public class PaymentMethod
        {
            public string id { get; set; }
            public string description { get; set; }
            public string country { get; set; }
            public bool enabled { get; set; }
            public object reason { get; set; }
        }
    }
}
