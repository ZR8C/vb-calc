Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WindowsApplication1

<TestClass()> Public Class CalculatorTest
    Private cal As Calculator

    <TestInitialize>
    Public Sub Initialise()
        cal = New Calculator
    End Sub

    <TestMethod()> Public Sub TestAppendOperand1()
        cal.appendOperand1("5")
        cal.appendOperand1("6")
        Assert.AreEqual(cal.getOperand1Str, "56")
    End Sub

    <TestMethod()> Public Sub TestAppendFPOperand1()
        cal.appendFPOperand1()
        Assert.AreEqual(cal.getOperand1Str, "0.")
        cal.appendFPOperand1()
        Assert.AreEqual(cal.getOperand1Str, "0.")
    End Sub

    <TestMethod()> Public Sub TestSetOperator1()
        'cant set operator when there are no operands
        cal.OperatorType = BinOperators.Add
        Assert.AreEqual(cal.OperatorType(), BinOperators.Null)
    End Sub

    <TestMethod()> Public Sub TestSetOperator2()
        ' setting operand whilst operand1 is present and operator2 is not should 
        ' result in operand1 being moved to operand2
        cal.appendOperand1("5")
        cal.OperatorType() = BinOperators.Add
        Assert.AreEqual(cal.OperatorType(), BinOperators.Add)
        Assert.AreEqual(cal.getOperand1Str, "")
        Assert.AreEqual(cal.getOperand2, 5D)
    End Sub

    <TestMethod()> Public Sub TestSetOperator3()
        ' setting a different operator should work if there is an operand2
        cal.appendOperand1("8")
        cal.OperatorType() = BinOperators.Add
        cal.OperatorType() = BinOperators.Divide
        Assert.AreEqual(cal.OperatorType(), BinOperators.Divide)
    End Sub

    <TestMethod()> Public Sub TestCalculate()
        'input number 27
        cal.appendOperand1("2")
        cal.appendOperand1("7")
        cal.OperatorType() = BinOperators.Divide
        cal.appendOperand1("3")
        cal.Calculate()

        Assert.AreEqual(cal.getOperand2, 9D)

        cal.OperatorType() = BinOperators.Multiply
        'input number 1.5
        cal.appendOperand1("1")
        cal.appendFPOperand1()
        cal.appendOperand1("5")
        cal.Calculate()

        Assert.AreEqual(cal.getOperand2, 13.5D)
    End Sub

    <TestMethod()> Public Sub TestrhsReduceOperand1()
        cal.appendOperand1("9")
        cal.appendOperand1("2")
        cal.rhsReduceOperand1()

        Assert.AreEqual(cal.getOperand1Str, "9")
    End Sub

    <TestMethod()> Public Sub TestClear()
        cal.appendOperand1("9")
        cal.appendOperand1("2")
        cal.OperatorType = BinOperators.Multiply
        cal.appendOperand1("3")
        cal.clear()
        Assert.AreEqual(cal.getOperand1Str, "")
        Assert.AreEqual(cal.getOperand2, Nothing)
        Assert.AreEqual(cal.OperatorType, BinOperators.Null)
    End Sub

    <TestMethod()> Public Sub TestSign()
        cal.appendOperand1("9")
        cal.appendOperand1("2")
        cal.signOperand1()
        Assert.AreEqual(cal.getOperand1Str, "-92")
        cal.signOperand1()
        Assert.AreEqual(cal.getOperand1Str, "92")
    End Sub

    <TestMethod()> Public Sub TestSqrt()
        cal.appendOperand1("9")
        cal.sqrtOperand1
        Assert.AreEqual(cal.getOperand2, 3D)
    End Sub

    <TestMethod()> Public Sub TestSquare()
        cal.appendOperand1("9")
        cal.squareOperand1()
        Assert.AreEqual(cal.getOperand2, 81D)
    End Sub

    <TestMethod()> Public Sub TestPowCalculate()
        cal.appendOperand1("9")
        cal.OperatorType = BinOperators.Pow
        cal.appendOperand1("5")
        cal.Calculate()
        Assert.AreEqual(cal.getOperand2, 59049D)
    End Sub
End Class