Public Enum BinOperators
    Add
    Subtract
    Multiply
    Divide
    Pow
    Null
End Enum

Public Class Calculator
    Private operand1 As String                      'must only consist of digits 0-9 and maximum one decimal point todo: better datatype
    Private operand2 As Decimal?                    ' represents both second operand and result
    Private myOperator As BinOperators

    Public Sub New()
        operand1 = ""
        operand2 = Nothing
        myOperator = BinOperators.Null
    End Sub

    Public Property OperatorType() As BinOperators
        Get
            Return myOperator
        End Get
        Set(value As BinOperators)
            If Not (operand1 = "") Then
                If (operand2 IsNot Nothing) Then
                    'both operands present
                    myOperator = value
                Else
                    'only operand1 present
                    myOperator = value
                    operand2 = Convert.ToDecimal(operand1)
                    operand1 = ""
                End If
            ElseIf (operand2 IsNot Nothing) Then
                'only operand2 is present
                myOperator = value
            End If
        End Set
    End Property

    ' Performs the binary calculation
    ' may throw ArithmeticException derived exceptions
    Public Sub Calculate()
        If Not ((operand1 Is Nothing) Or (operand2 Is Nothing) Or (myOperator = BinOperators.Null)) Then
            Dim op1Dec As Decimal = Convert.ToDecimal(operand1)
            Dim result As Decimal?
            Try
                Select Case myOperator
                    Case BinOperators.Add
                        result = op1Dec + operand2
                    Case BinOperators.Subtract
                        result = operand2 - op1Dec
                    Case BinOperators.Multiply
                        result = op1Dec * operand2
                    Case BinOperators.Divide
                        result = operand2 / op1Dec
                    Case BinOperators.Pow
                        result = Math.Pow(operand2, operand1)
                End Select
            Catch ex As Exception
                clear()
                Throw ex
            End Try

            operand1 = ""
            myOperator = BinOperators.Null
            operand2 = result
        End If
    End Sub

    Public Function getOperand1Str() As String
        Return operand1
    End Function

    ' Accepts only digit chars 0-9
    Public Sub appendOperand1(ByVal i As Char)
        If (Asc(i) >= 48 And Asc(i) <= 57) AndAlso operand1.Length < 26 Then
            'clear operand2 if there is no operator present
            If (operand2 IsNot Nothing And myOperator = BinOperators.Null) Then
                operand2 = Nothing
            End If

            If (operand1 = "") Then
                operand1 = i
            Else
                operand1 += i
            End If
        End If
    End Sub

    ' Adds a floating point to the first operand if it hasn't got one already
    Public Sub appendFPOperand1()
        'clear operand2 if there is no operator present
        If (operand2 IsNot Nothing And myOperator = BinOperators.Null) Then
            operand2 = Nothing
        End If

        If (operand1 = "") Then
            operand1 = "0."
        ElseIf Not (operand1.ToString.Contains(".")) Then
            operand1 += "."
        End If
    End Sub

    'to add or remove a sign from operand1 (e.g. -)
    Public Sub signOperand1()
        If Not (operand1 = "") Then
            If (operand1.StartsWith("-")) Then
                operand1 = operand1.TrimStart("-")
            Else
                operand1 = "-" + operand1
            End If
        End If
    End Sub

    'Performs sqrt on operand1, may throw overflowexception if user tries to sqrt a negative number
    Public Sub sqrtOperand1()
        If Not (operand1 = "") Then
            Try
                operand2 = Math.Sqrt(Convert.ToDecimal(operand1))
            Catch ex As OverflowException
                clear()
                Throw ex
            End Try
            operand1 = ""
            OperatorType = BinOperators.Null
        End If
    End Sub

    'Squares operand1
    Public Sub squareOperand1()
        If Not (operand1 = "") Then
            Try
                operand2 = Math.Pow(Convert.ToDecimal(operand1), 2)
            Catch ex As OverflowException
                clear()
                Throw ex
            End Try

            operand1 = ""
            OperatorType = BinOperators.Null
        End If
    End Sub

    'for deleting from the right hand side of operand1
    Public Sub rhsReduceOperand1()
        If Not (operand1 = "") Then
            operand1 = operand1.Substring(0, operand1.Length - 1)
        End If
    End Sub

    ' Gets operand2 which can be either a result or a secondary operand
    ' Returns Nothing if operand2 doesnt exist
    Public Function getOperand2() As Decimal?
        Return operand2
    End Function

    ' Clears all fields of the calculator, ready to start fresh
    Public Sub clear()
        operand1 = ""
        operand2 = Nothing
        myOperator = BinOperators.Null
    End Sub

End Class
