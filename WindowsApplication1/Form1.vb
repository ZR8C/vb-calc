' todo: automatically updateFields instead of calling it every time
Public Class Form1

    Private cal As Calculator = New Calculator

    'Numbers 0-9 button actions
    Private Sub Numeral_Click(sender As System.Object, e As System.EventArgs) Handles Btn0.Click, Btn1.Click, Btn2.Click, Btn3.Click, Btn4.Click, Btn5.Click, Btn6.Click, Btn7.Click, Btn8.Click, Btn9.Click
        cal.appendOperand1(CType(sender, Button).Text)

        updateFields()
    End Sub

    '+ - * / button actions
    Private Sub Operator_Click(sender As System.Object, e As System.EventArgs) Handles AddBtn.Click, SubBtn.Click, MulBtn.Click, DivBtn.Click, PowBtn.Click
        Select Case (CType(sender, Button).Text)
            Case "+"
                cal.OperatorType() = BinOperators.Add
            Case "-"
                cal.OperatorType() = BinOperators.Subtract
            Case "x"
                cal.OperatorType() = BinOperators.Multiply
            Case "÷"
                cal.OperatorType() = BinOperators.Divide
            Case "x^y"
                cal.OperatorType() = BinOperators.Pow
        End Select

        updateFields()
    End Sub

    '= Button pressed
    Private Sub Equals_Click(send As System.Object, e As System.EventArgs) Handles EqualsBtn.Click
        Dim success As Boolean = False
        Try
            cal.Calculate()
            success = True
        Catch ex As DivideByZeroException
            CalcError("Can't divide by 0")
        Catch ex As OverflowException
            CalcError("Number too large")
        Catch ex As NotFiniteNumberException
            CalcError("Invalid number")
        Catch ex As Exception
            CalcError("Unknown error")
        End Try

        If (success) Then
            updateFields()
        End If
    End Sub

    Private Sub CalcError(ByVal msg As String)
        OperandBox1.Text = ""
        OperandBox2.Text = msg
        OperatorBox.Text = ""
    End Sub

    'Clear button pressed
    Private Sub Clear_Click(send As System.Object, e As System.EventArgs) Handles ClearBtn.Click
        cal.clear()
        updateFields()
    End Sub

    'Delete button pressed
    Private Sub Delete_Click(send As System.Object, e As System.EventArgs) Handles DeleteBtn.Click
        cal.rhsReduceOperand1()
        updateFields()
    End Sub

    'Decimal button pressed
    Private Sub Decimal_Click(send As System.Object, e As System.EventArgs) Handles DecimalBtn.Click
        cal.appendFPOperand1()
        updateFields()
    End Sub

    'Sign button pressed (+/-)
    Private Sub Sign_Click(send As System.Object, e As System.EventArgs) Handles SignBtn.Click
        cal.signOperand1()
        updateFields()
    End Sub

    'Square root button clicked (√)
    Private Sub Sqrt_Click(send As System.Object, e As System.EventArgs) Handles SqrtBtn.Click
        Dim success As Boolean = False

        Try
            cal.sqrtOperand1()
            success = True
        Catch ex As OverflowException
            CalcError("Cannot sqrt negative number")
        End Try

        If (success) Then
            updateFields()
        End If
    End Sub

    'Square button clicked (x^2)
    Private Sub Square_Click(send As System.Object, e As System.EventArgs) Handles SquareBtn.Click
        Dim success As Boolean = False

        Try
            cal.squareOperand1()
            success = True
        Catch ex As OverflowException
            CalcError("Number too large")
        End Try

        If (success) Then
            updateFields()
        End If
    End Sub

    Private Sub updateFields()
        OperandBox1.Text = cal.getOperand1Str
        OperandBox2.Text = cal.getOperand2.ToString
        Select Case cal.OperatorType
            Case BinOperators.Add
                OperatorBox.Text = "+"
            Case BinOperators.Subtract
                OperatorBox.Text = "-"
            Case BinOperators.Multiply
                OperatorBox.Text = "x"
            Case BinOperators.Divide
                OperatorBox.Text = "÷"
            Case BinOperators.Pow
                OperatorBox.Text = "^"
            Case Else
                OperatorBox.Text = ""
        End Select
    End Sub

End Class
