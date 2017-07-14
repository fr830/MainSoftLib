using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Security;

using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using MainSoftLib.PayuLatam.Entities.Ping;
using MainSoftLib.PayuLatam.Entities.Order;
using MainSoftLib.PayuLatam.Entities.Transaction;
using MainSoftLib.PayuLatam.Entities.Payment;

namespace MainSoftLib.PayuLatam.Factories
{
    public class PayuLatamFactory
    {
        string Api_Payment_Test = "https://stg.api.payulatam.com/payments-api/4.0/service.cgi";
        string Api_Payment_Prod = "https://api.payulatam.com/payments-api/4.0/service.cgi";

        string Api_Query_Test = "https://sandbox.api.payulatam.com/reports-api/4.0/service.cgi";
        string Api_Query_Prod = "https://api.payulatam.com/reports-api/4.0/service.cgi";

        bool test;
        string language = "es";
        string merchantId;
        string apiLogin;
        string apiKey;
        string accountId;
        byte[] ApiCertHash = { 0x71, 0x0c, 0x0a, 0x75, 0xc4, 0x88, 0xb4, 0x09, 0x35, 0x20, 0x12, 0x42, 0x05, 0xd0, 0x35, 0xe4, 0x90, 0x35, 0x97, 0x6f };

        public PayuLatamFactory(string apiLogin, string apiKey, string accountId, string merchantId, string language, bool usedTest)
        {
            this.merchantId = merchantId;
            this.accountId = accountId;
            this.apiLogin = apiLogin;
            this.apiKey = apiKey;
            this.language = language;
            this.test = usedTest;
        }

        private bool ValidateServerCertficate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            bool certMatch = false;
            byte[] certHash = cert.GetCertHash();

            if (certHash.Length == ApiCertHash.Length)
            {
                certMatch = true;
                for (int idx = 0; idx < certHash.Length; idx++)
                {
                    if (certHash[idx] != ApiCertHash[idx])
                    {
                        certMatch = false;
                        break;
                    }
                }
            }

            return certMatch;
        }

        public Task<string> httpWebRequestPost(string Url, string Json, HttpMethod httpMethod)
        {
            Task<string> task = Task.Run(() =>
            {
                string Response = null;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });                    
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);

                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Accept = "application/json";
                    httpWebRequest.Method = httpMethod.ToString();

                    if ((httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put) && Json != null)
                    {
                        using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(Json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }

                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                    {
                        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                Response = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Response = null;
                }

                return Response;
            });
            return task;            
        }

        public async Task<EntyPingResult> MakerPing()
        {
            try
            {
                EntyPing Ping = new EntyPing(test, language, apiLogin, apiKey, "PING");

                string Json = JsonConvert.SerializeObject(Ping);
                string Response = await httpWebRequestPost(test ? Api_Query_Test : Api_Query_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyPingResult>(Response); }else{ return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyOrderDetailsResult> ConsultOrders(int orderId)
        {
            try
            {
                EntyOrderDetails Order = new EntyOrderDetails(test, language, apiLogin, apiKey, "ORDER_DETAIL");
                Order.details.orderId = orderId;

                string Json = JsonConvert.SerializeObject(Order);
                string Response = await httpWebRequestPost(test ? Api_Query_Test : Api_Query_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyOrderDetailsResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyTransactionDetailsResult> ConsultTransaction(string transactionId)
        {
            try
            {
                EntyTransactionDetails Transaction = new EntyTransactionDetails(test, language, apiLogin, apiKey, "TRANSACTION_RESPONSE_DETAIL");

                Transaction.details.transactionId = transactionId;

                string Json = JsonConvert.SerializeObject(Transaction);
                string Response = await httpWebRequestPost(test ? Api_Query_Test : Api_Query_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyTransactionDetailsResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyOrderRefResult> ConsultReference(string ReferenceCode)
        {
            try
            {
                EntyOrderReference Order = new EntyOrderReference(test, language, apiLogin, apiKey, "ORDER_DETAIL_BY_REFERENCE_CODE");
                Order.details.referenceCode = ReferenceCode;

                string Json = JsonConvert.SerializeObject(Order);
                string Response = await httpWebRequestPost(test ? Api_Query_Test : Api_Query_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyOrderRefResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyPaymentMethodsResult> ConsultSuppliers()
        {
            try
            {
                EntyPaymentMethods Suppliers = new EntyPaymentMethods(test, language, apiLogin, apiKey, "GET_PAYMENT_METHODS");

                string Json = JsonConvert.SerializeObject(Suppliers);
                string Response = await httpWebRequestPost(test ? Api_Payment_Test : Api_Payment_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyPaymentMethodsResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyPaymentCreditResult> Payment(EntyPaymentCredit.Transaction transaction)
        {
            try
            {
                transaction.order.accountId = accountId;
                transaction.order.language = language;
                transaction.order.signature = MD5Hash(apiKey + "~" + merchantId + "~" + transaction.order.referenceCode + "~" + transaction.order.additionalValues.TX_VALUE.value + "~" + transaction.order.additionalValues.TX_VALUE.currency);

                EntyPaymentCredit Payment = new EntyPaymentCredit(test, language, apiLogin, apiKey, "SUBMIT_TRANSACTION", transaction);

                string Json = JsonConvert.SerializeObject(Payment);
                string Response = await httpWebRequestPost(test ? Api_Payment_Test : Api_Payment_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyPaymentCreditResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<EntyPaymentCashResult> Payment(EntyPaymentCash.Transaction transaction)
        {
            try
            {
                transaction.order.accountId = accountId;
                transaction.order.language = language;
                transaction.order.signature = MD5Hash(apiKey + "~" + merchantId + "~" + transaction.order.referenceCode + "~" + transaction.order.additionalValues.TX_VALUE.value + "~" + transaction.order.additionalValues.TX_VALUE.currency);

                EntyPaymentCash Payment = new EntyPaymentCash(test, language, apiLogin, apiKey, "SUBMIT_TRANSACTION", transaction);

                string Json = JsonConvert.SerializeObject(Payment);
                string Response = await httpWebRequestPost(test ? Api_Payment_Test : Api_Payment_Prod, Json, HttpMethod.Post);
                if (Response != null) { return JsonConvert.DeserializeObject<EntyPaymentCashResult>(Response); } else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string MD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);
            
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
