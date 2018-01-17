## MainSoftLib.MathExpressions

```
MathEvaluator eval = new MathEvaluator();

//basic math
double result = eval.Evaluate("(2 + 1) * (1 + 2)");

//calling a function
result = eval.Evaluate("sqrt(4)");

//evaluate trigonometric 
result = eval.Evaluate("cos(pi * 45 / 180.0)");

//convert inches to feet
result = eval.Evaluate("12 [in->ft]");

//use variable
result = eval.Evaluate("answer * 10");

//add variable
eval.Variables.Add("x", 10);
result = eval.Evaluate("x * 10");

```

## Installing
```
PM> Install-Package MainSoftLib.MathExpressions
```
