# MainSoftLib.PayuLatam


## Installing
PayuLatam api is available as a Nuget package from https://www.nuget.org/packages/MainSoftLib.PayuLatam/ and installable using the following command in the Package Manager Console:
```
PM> Install-Package MainSoftLib.PayuLatam
```

```
PayuLatamFactory PayuLatam = new PayuLatamFactory("pRRXKOl8ikMmt9u", "4Vj8eK4rloUd272L48hsrarnUA", "512321", "508029", "es", true);

EntyPingResult Ping = await PayuLatam.MakerPing();
EntyOrderDetailsResult OrderDetails = await PayuLatam.ConsultOrders(840711219);
EntyTransactionDetailsResult TransactionDetails = await PayuLatam.ConsultTransaction("e6d7793a-6a9f-437b-8401-e1c8de55f6b1");
EntyOrderRefResult OrderRef = await PayuLatam.ConsultReference("payment_test_00000001");
EntyPaymentMethodsResult PaymentMethods = await PayuLatam.ConsultSuppliers();
```
