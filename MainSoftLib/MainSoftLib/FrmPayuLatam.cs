using MainSoftLib.PayuLatam.Entities.Payment;
using PayuLatamSharp.Factories;
using System;
using System.Windows.Forms;

namespace MainSoftLib
{
    public partial class FrmPayuLatam : Form
    {
        PayuLatamFactory PayuLatam = null; 

        public FrmPayuLatam()
        {
            InitializeComponent();
        }

        private void MainSoftPayuLatamDemo_Load(object sender, EventArgs e)
        {
            //PayuLatam = new PayuLatamFactory("pRRXKOl8ikMmt9u", "4Vj8eK4rloUd272L48hsrarnUA", "512321", "508029", "es", true);
            PayuLatam = new PayuLatamFactory("418C8Eai86vqere", "Ov95N7533LeGWYgq465BDl07FH", "539979", "537923", "es", false);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //EntyPingResult test = await PayuLatam.MakerPing();
            //EntyOrderDetailsResult Test2 = await PayuLatam.ConsultOrders(840711219);
            //EntyTransactionDetailsResult Test3 = await PayuLatam.ConsultTransaction("e6d7793a-6a9f-437b-8401-e1c8de55f6b1");
            //EntyOrderRefResult Test4 = await PayuLatam.ConsultReference("payment_test_00000001");

            //EntyPaymentMethodsResult Test5 = await PayuLatam.ConsultSuppliers();
            //List<EntyPaymentMethodsResult.PaymentMethod> Test6 = Test5.paymentMethods.FindAll(x => x.country == "CO");

            //TransationCredito();
            // TransationEfectivo();

            EntyPaymentCash.Transaction transaction = new EntyPaymentCash.Transaction();
            transaction.type = "AUTHORIZATION_AND_CAPTURE";
            transaction.paymentMethod = "BALOTO";
            transaction.paymentCountry = "CO";
            transaction.expirationDate = "2017-03-15T00:00:00";
            transaction.ipAddress = "127.0.0.1";

            // Order
            transaction.order.referenceCode = "J-100-5";
            transaction.order.description = "Arreglo";
            transaction.order.notifyUrl = "";
            transaction.order.additionalValues.TX_VALUE.value = 20000;
            transaction.order.additionalValues.TX_VALUE.currency = "COP";


            transaction.order.buyer.emailAddress = "judero01@gmail.com";

            EntyPaymentCashResult Test7 = await PayuLatam.Payment(transaction);
        }

        public async void TransationCredito()
        {
            EntyPaymentCredit.Transaction transaction = new EntyPaymentCredit.Transaction();
            transaction.type = "AUTHORIZATION_AND_CAPTURE";
            transaction.paymentMethod = "VISA";
            transaction.paymentCountry = "CO";
            transaction.deviceSessionId = "vghs6tvkcle931686k1900o6e1";
            transaction.ipAddress = "127.0.0.1";
            transaction.cookie = "pt1t38347bs6jc9ruv2ecpv7o2";
            transaction.userAgent = "Mozilla/5.0 (Windows NT 5.1; rv:18.0) Gecko/20100101 Firefox/18.0";

            // Order
            transaction.order.referenceCode = "J-100-3";
            transaction.order.description = "Arreglo";
            transaction.order.notifyUrl = "";
            transaction.order.additionalValues.TX_VALUE.value = 20000;
            transaction.order.additionalValues.TX_VALUE.currency = "COP";

            transaction.order.buyer.merchantBuyerId = "1";
            transaction.order.buyer.fullName = "APPROVED";
            transaction.order.buyer.emailAddress = "judero01@gmail.com";
            transaction.order.buyer.dniNumber = "1043843544";

            transaction.order.buyer.shippingAddress.street1 = "Cra 59 #59-205";
            transaction.order.buyer.shippingAddress.city = "Barranquilla";
            transaction.order.buyer.shippingAddress.state = "Atlantico";
            transaction.order.buyer.shippingAddress.country = "CO";
            transaction.order.buyer.shippingAddress.postalCode = "080020";
            transaction.order.buyer.shippingAddress.phone = "3103511403";

            transaction.order.shippingAddress.street1 = "Cra 59 #59-205";
            transaction.order.shippingAddress.city = "Barranquilla";
            transaction.order.shippingAddress.state = "Atlantico";
            transaction.order.shippingAddress.country = "CO";
            transaction.order.shippingAddress.postalCode = "080020";
            transaction.order.shippingAddress.phone = "3103511403";

            // Payer
            transaction.payer.merchantPayerId = "1";
            transaction.payer.fullName = "Jurgen De Leon Rodriguez";
            transaction.payer.emailAddress = "judero01@gmail.com";
            transaction.payer.dniNumber = "1043843544";

            transaction.payer.billingAddress.street1 = "Cra 59 #59-205";
            transaction.payer.billingAddress.city = "Barranquilla";
            transaction.payer.billingAddress.state = "Atlantico";
            transaction.payer.billingAddress.country = "CO";
            transaction.payer.billingAddress.postalCode = "080020";
            transaction.payer.billingAddress.phone = "3103511403";

            // CreditCard
            transaction.creditCard.number = "4111111111111111";
            transaction.creditCard.securityCode = "123";
            transaction.creditCard.expirationDate = "2018/08";
            transaction.creditCard.name = "APPROVED";

            // Extra
            transaction.extraParameters.INSTALLMENTS_NUMBER = 1;

            EntyPaymentCreditResult Test7 = await PayuLatam.Payment(transaction);
        }

        public async void TransationEfectivo()
        {

        }
    }
}
