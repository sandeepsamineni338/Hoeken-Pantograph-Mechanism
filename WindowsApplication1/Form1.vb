Imports System.Drawing.Drawing2D
Imports System.Math

Public Class Form1

    Dim g As Graphics
    Dim M_inversetransformation As Matrix
    Dim ptfs() As PointF = {New Point(0.0F, 0.0F)}
    Dim ClickPoints() As PointF = {New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F), New PointF(0.0F, 0.0F)}
    Dim points(216) As PointF
    Dim wheel As Double
    Dim paints(432) As PointF
    Dim rab, rac, rbq, s, pointO2
    Dim o2a, ab, o4b, pb, o4a, cp, po2ax, po2ay
    Dim z As Integer = 0
    Dim xx As Single
    Dim yy As Single
    Dim angel As Single
    Dim x, y, x1, y1, x2, y2

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        g = getgraphicsobject(PictureBox1)
        Label5.Visible = False
        PictureBox1.Enabled = False
        Label5.Parent = PictureBox1
        Label5.BackColor = Color.Transparent
        
    End Sub

    Public Sub setscale(ByVal g As Graphics, ByVal g_width As Integer, ByVal g_height As Integer, ByVal left_x As Single, ByVal right_x As Single, ByVal top_y As Single, ByVal bottom_y As Single)
        g.ResetTransform()
        g.ScaleTransform(g_width / (right_x - left_x), g_height / (bottom_y - top_y))
        g.TranslateTransform(-left_x, -top_y)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PictureBox1.Enabled = True
        If Not (TextBox1.Text = Nothing Or
                TextBox2.Text = Nothing Or
                TextBox3.Text = Nothing Or
                TextBox4.Text = Nothing) Then
            setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
        Else
            MsgBox("please fill the textboxes with xmin,xmax,ymin,ymax")
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove

        ptfs(0) = New PointF(e.X, e.Y)


        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)
        

        Label5.Visible = True
        Label5.Text = "(" & ptfs(0).X.ToString & ", " & ptfs(0).Y.ToString & ")"

        If (e.X + 15 + Label5.Width) > (PictureBox1.Right) Then

            Label5.Location = New Point(e.X + 15 - Label5.Width, e.Y + 15)

        Else
            Label5.Location = New Point(e.X + 15, e.Y + 15)
        End If

            If RadioButton3.Checked = True Then
                RadioButton4.Checked = False
                g = getgraphicsobject(PictureBox1)
                setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
                TextBox5.Text = ptfs(0).X.ToString
                TextBox6.Text = ptfs(0).Y.ToString
                If Not (TextBox7.Text = Nothing Or
                       TextBox8.Text = Nothing Or
                       TextBox9.Text = Nothing) Then
                    plot(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))
                Else
                    MsgBox(" Fill theta1, theta2, l2")
                End If
            End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        g.ResetTransform()
        PictureBox1.Enabled = False
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.Click

        Dim ptfs() As PointF = {New PointF(e.X, e.Y)}

        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)
        RadioButton3.Checked = False
        'RadioButton1.Checked = True

        If RadioButton1.Checked = True Then
            RadioButton4.Checked = False
            TextBox5.Text = ptfs(0).X.ToString
            TextBox6.Text = ptfs(0).Y.ToString
            ClickPoints(0) = New PointF(ptfs(0).X, ptfs(0).Y)
        End If
        If RadioButton4.Checked = True Then
            RadioButton1.Checked = False
            TextBox12.Text = ptfs(0).X.ToString
            TextBox13.Text = ptfs(0).Y.ToString
            ClickPoints(1) = New PointF(ptfs(0).X, ptfs(0).Y)
        End If
        If RadioButton6.Checked = True Then
            TextBox22.Text = ptfs(0).X.ToString
            TextBox23.Text = ptfs(0).Y.ToString
            ClickPoints(2) = New PointF(ptfs(0).X, ptfs(0).Y)
        End If
        If RadioButton7.Checked = True Then
            TextBox24.Text = ptfs(0).X.ToString
            TextBox25.Text = ptfs(0).Y.ToString
            ClickPoints(3) = New PointF(ptfs(0).X, ptfs(0).Y)
        End If

    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

        If TextBox7.Text > 359 Then
            TextBox7.Text = 0
        ElseIf TextBox7.Text < -359 Then
            TextBox7.Text = 0
        End If
    End Sub

    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel

        g = getgraphicsobject(PictureBox1)
        setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))

        'Dim ptfs() As PointF = {New PointF(e.X, e.Y)}

        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)
        If RadioButton2.Checked = True Then

            If e.Delta > 0 Then
                wheel += e.Delta / 120
                TextBox7.Text = wheel.ToString

            ElseIf e.Delta < 0 Then
                wheel -= -e.Delta / 120
                TextBox7.Text = wheel.ToString

            End If
        End If
        If RadioButton5.Checked = True Then

            If e.Delta > 0 Then
                wheel += e.Delta / 120
                TextBox9.Text = wheel.ToString

            ElseIf e.Delta < 0 Then
                wheel -= -e.Delta / 120
                TextBox9.Text = wheel.ToString

            End If
        End If

        'plot(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))
        'pantograph(rab, rac, rbq, s)

    End Sub

    Public Sub plot(ByVal XO2 As TextBox, ByVal YO2 As TextBox, ByVal L2 As Double, ByVal theta1 As Double)

        Dim L1, L3, L4, AP As Double
        Dim Theta2, Theta3, theta41, Theta42, Beta As Double
        Dim XA, YA As Double
        Dim XB, YB As Double
        Dim XO4, YO4 As Double
        Dim XP, YP As Double
        Dim XR, YR, LR, ThetaR As Double
        Dim XR3, YR3 As Double


        L1 = L2 * 2
        L3 = L2 * 2.5
        L4 = L3
        AP = 2 * L3

        For t1 As Integer = 0 To 359 Step 5

            Theta2 = t1 * (PI / 180)
            XR = L2 * Cos(Theta2) - L1 * Cos(theta1)
            YR = L2 * Sin(Theta2) - L1 * Sin(theta1)

            ThetaR = Atan2(YR, XR)
            LR = Sqrt((XR ^ 2) + (YR ^ 2))

            Beta = Acos(((L4 ^ 2) + (LR ^ 2) - (L3 ^ 2)) / (2 * LR * L4))

            theta41 = ThetaR + Beta
            Theta42 = ThetaR - Beta

            XO4 = CDbl(XO2.Text) + L1
            YO4 = CDbl(YO2.Text)


            XR3 = L4 * Cos(Theta42) - LR * Cos(ThetaR)
            YR3 = L4 * Sin(Theta42) - LR * Sin(ThetaR)

            Theta3 = Atan2(YR3, XR3)

            XA = CDbl(XO2.Text) + L2 * Cos(Theta2)
            YA = CDbl(YO2.Text) + L2 * Sin(Theta2)

            XB = XA + L3 * Cos(Theta3)
            YB = YA + L3 * Sin(Theta3)

            XP = XA + 2 * L3 * Cos(Theta3)
            YP = YA + 2 * L3 * Sin(Theta3)

            Dim thetaP As Double
            thetaP = Atan2(YP, XP)


            Dim a1 As Integer = t1 / 5
            points(a1) = New PointF(XP, YP)
            points(a1 + 72) = New PointF(XA, YA)
            points(a1 + 144) = New PointF(XB, YB)
            points(216) = New PointF(XO4, YO4)

        Next

        Dim mypen As New Pen(Color.Black, 0.1)
        Dim mypen2 As New Pen(Color.Red, 0.1)
        Dim mypen3 As New Pen(Color.Green, 0.08)
        Dim mypen4 As New Pen(Color.Blue, 0.1)
        Dim s = Abs(CDbl(TextBox9.Text))
        Do While s > 359
            s = s - 360
        Loop

        s = s \ 5

        pointO2 = New PointF(CDbl(TextBox5.Text), CDbl(TextBox6.Text))
        g.DrawLine(mypen2, pointO2.X, pointO2.Y, points(s + 72).X, points(s + 72).Y) 'red
        g.DrawLine(mypen, points(s + 72), points(s + 144)) 'black
        g.DrawLine(mypen4, points(s + 144), points(s)) 'blue
        g.DrawLine(mypen3, points(s + 144), points(216)) 'green
        TextBox10.Text = points(s).X
        TextBox11.Text = points(s).Y
        For p As Integer = 0 To 70 Step 1
            g.DrawLine(mypen, points(p), points(p + 1))
            g.DrawLine(mypen, points(71), points(0))
        Next
        Dim O2pivot() As PointF = {New PointF(pointO2.X - 0.2, pointO2.Y - 0.1), New PointF(pointO2.X + 0.2, pointO2.Y - 0.1), New PointF(pointO2.X, pointO2.Y + 0.2)}
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(1).X, O2pivot(1).Y)
        g.DrawLine(mypen3, O2pivot(1).X, O2pivot(1).Y, O2pivot(2).X, O2pivot(2).Y)
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(2).X, O2pivot(2).Y)

        Dim O4pivot() As PointF = {New PointF(points(216).X - 0.2, points(216).Y - 0.1), New PointF(points(216).X + 0.2, points(216).Y - 0.1), New PointF(points(216).X, points(216).Y + 0.2)}
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(1).X, O4pivot(1).Y)
        g.DrawLine(mypen3, O4pivot(1).X, O4pivot(1).Y, O4pivot(2).X, O4pivot(2).Y)
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(2).X, O4pivot(2).Y)
        RadioButton1.Checked = False
        'RadioButton5.Checked = True
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        g = getgraphicsobject(PictureBox1)
        Dim mypen As New Pen(Color.Black, 0.1)
        Dim mypen2 As New Pen(Color.Red, 0.1)
        Dim mypen3 As New Pen(Color.Green, 0.08)
        Dim mypen4 As New Pen(Color.Blue, 0.1)
        If Not (TextBox1.Text = Nothing Or
                TextBox2.Text = Nothing Or
                TextBox3.Text = Nothing Or
                TextBox4.Text = Nothing) Then
            setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
            Dim L2length = CDbl(TextBox8.Text)
        End If
        plot1(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))

        pointO2 = New PointF(CDbl(TextBox5.Text), CDbl(TextBox6.Text))
        Dim s = Abs(CDbl(TextBox9.Text))
        Do While s > 359
            s = s - 360
        Loop
        s = s \ 5

        g.DrawLine(mypen2, pointO2.X, pointO2.Y, points(s + 72).X, points(s + 72).Y) 'red
        g.DrawLine(mypen, points(s + 72), points(s + 144)) 'black
        g.DrawLine(mypen4, points(s + 144), points(s)) 'blue
        g.DrawLine(mypen3, points(s + 144), points(216)) 'green
        TextBox10.Text = points(s).X
        TextBox11.Text = points(s).Y

        Dim O2pivot() As PointF = {New PointF(pointO2.X - 0.2, pointO2.Y - 0.1), New PointF(pointO2.X + 0.2, pointO2.Y - 0.1), New PointF(pointO2.X, pointO2.Y + 0.2)}
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(1).X, O2pivot(1).Y)
        g.DrawLine(mypen3, O2pivot(1).X, O2pivot(1).Y, O2pivot(2).X, O2pivot(2).Y)
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(2).X, O2pivot(2).Y)

        Dim O4pivot() As PointF = {New PointF(points(216).X - 0.2, points(216).Y - 0.1), New PointF(points(216).X + 0.2, points(216).Y - 0.1), New PointF(points(216).X, points(216).Y + 0.2)}
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(1).X, O4pivot(1).Y)
        g.DrawLine(mypen3, O4pivot(1).X, O4pivot(1).Y, O4pivot(2).X, O4pivot(2).Y)
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(2).X, O4pivot(2).Y)
        RadioButton1.Checked = False
        'RadioButton5.Checked = True
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button5.Click


        g = getgraphicsobject(PictureBox1)
        setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))

        Dim ptfs() As PointF = {New PointF(e.X, e.Y)}

        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)
        plot1(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))
        Dim mypen As New Pen(Color.Black, 0.1)
        Dim mypen2 As New Pen(Color.Red, 0.1)
        Dim mypen3 As New Pen(Color.Green, 0.08)
        Dim mypen4 As New Pen(Color.Blue, 0.1)
        Dim s = Abs(CDbl(TextBox9.Text))
        Do While s > 359
            s = s - 360
        Loop
        s = s \ 5

        pointO2 = New PointF(CDbl(TextBox5.Text), CDbl(TextBox6.Text))
        g.DrawLine(mypen2, pointO2.X, pointO2.Y, points(s + 72).X, points(s + 72).Y) 'red
        Dim o2ax = pointO2.X - points(s + 72).X
        Dim o2ay = pointO2.Y - points(s + 72).Y
        o2a = Sqrt((o2ax ^ 2) + (o2ay ^ 2))
        g.DrawLine(mypen, points(s + 72), points(s + 144)) 'black
        Dim abx = points(s + 144).X - points(s + 72).X
        Dim aby = points(s + 144).Y - points(s + 72).Y
        ab = Sqrt((abx ^ 2) + (aby ^ 2))
        g.DrawLine(mypen4, points(s + 144), points(s)) 'blue
        Dim pbx = points(s + 144).X - points(s).X
        Dim pby = points(s + 144).Y - points(s).Y
        pb = Sqrt((pbx ^ 2) + (pby ^ 2))
        g.DrawLine(mypen3, points(s + 144), points(216)) 'green
        Dim o4bx = points(s + 144).X - points(216).X
        Dim o4by = points(s + 144).Y - points(216).Y
        o4b = Sqrt((o4bx ^ 2) + (o4by ^ 2))
        Dim o4ax = points(s + 72).X - points(216).X
        Dim o4ay = points(s + 72).Y - points(216).Y
        o4a = Sqrt((o4ax ^ 2) + (o4ay ^ 2))
        TextBox10.Text = points(s).X
        TextBox11.Text = points(s).Y
        For p As Integer = 0 To 70 Step 1
            g.DrawLine(mypen, points(p), points(p + 1))
            g.DrawLine(mypen, points(71), points(0))
        Next
        Dim O2pivot() As PointF = {New PointF(pointO2.X - 0.2, pointO2.Y - 0.1), New PointF(pointO2.X + 0.2, pointO2.Y - 0.1), New PointF(pointO2.X, pointO2.Y + 0.2)}
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(1).X, O2pivot(1).Y)
        g.DrawLine(mypen3, O2pivot(1).X, O2pivot(1).Y, O2pivot(2).X, O2pivot(2).Y)
        g.DrawLine(mypen3, O2pivot(0).X, O2pivot(0).Y, O2pivot(2).X, O2pivot(2).Y)

        Dim O4pivot() As PointF = {New PointF(points(216).X - 0.2, points(216).Y - 0.1), New PointF(points(216).X + 0.2, points(216).Y - 0.1), New PointF(points(216).X, points(216).Y + 0.2)}
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(1).X, O4pivot(1).Y)
        g.DrawLine(mypen3, O4pivot(1).X, O4pivot(1).Y, O4pivot(2).X, O4pivot(2).Y)
        g.DrawLine(mypen3, O4pivot(0).X, O4pivot(0).Y, O4pivot(2).X, O4pivot(2).Y)
        RadioButton1.Checked = False
        
        'RadioButton5.Checked = True
    End Sub

    Public Sub plot1(ByVal XO2 As TextBox, ByVal YO2 As TextBox, ByVal L2 As Double, ByVal theta1 As Double)

        Dim L1, L3, L4, AP As Double
        Dim Theta2, Theta3, theta41, Theta42, Beta As Double
        Dim XA, YA As Double
        Dim XB, YB As Double
        Dim XO4, YO4 As Double
        Dim XP, YP As Double
        Dim XR, YR, LR, ThetaR As Double
        Dim XR3, YR3 As Double


        L1 = L2 * 2
        L3 = L2 * 2.5
        L4 = L3
        AP = 2 * L3

        For t1 As Integer = 0 To 359 Step 5
            Theta2 = t1 * (PI / 180)
            XR = L2 * Cos(Theta2) - L1 * Cos(theta1)
            YR = L2 * Sin(Theta2) - L1 * Sin(theta1)

            ThetaR = Atan2(YR, XR)
            LR = Sqrt((XR ^ 2) + (YR ^ 2))

            Beta = Acos(((L4 ^ 2) + (LR ^ 2) - (L3 ^ 2)) / (2 * LR * L4))

            theta41 = ThetaR + Beta
            Theta42 = ThetaR - Beta

            XO4 = CDbl(XO2.Text) + L1
            YO4 = CDbl(YO2.Text)


            XR3 = L4 * Cos(Theta42) - LR * Cos(ThetaR)
            YR3 = L4 * Sin(Theta42) - LR * Sin(ThetaR)

            Theta3 = Atan2(YR3, XR3)

            XA = CDbl(XO2.Text) + L2 * Cos(Theta2)
            YA = CDbl(YO2.Text) + L2 * Sin(Theta2)

            XB = XA + L3 * Cos(Theta3)
            YB = YA + L3 * Sin(Theta3)

            XP = XA + 2 * L3 * Cos(Theta3)
            YP = YA + 2 * L3 * Sin(Theta3)

            Dim thetaP As Double
            thetaP = Atan2(YP, XP)


            Dim a1 As Integer = t1 \ 5
            points(a1) = New PointF(XP, YP)
            points(a1 + 72) = New PointF(XA, YA)
            points(a1 + 144) = New PointF(XB, YB)
            points(216) = New PointF(XO4, YO4)

        Next
    End Sub
    Public Sub pantograph(ByVal rab As Double, ByVal rac As Double, ByVal rbq As Double, ByVal s As Double)

        Dim mypen As New Pen(Color.Black, 0.1)
        Dim mypen2 As New Pen(Color.Red, 0.1)
        Dim mypen3 As New Pen(Color.Green, 0.1)
        Dim mypen4 As New Pen(Color.Blue, 0.1)
        Dim rbx, rby, raqx, raqy, rbqx, rbqy, raq, rcx, rcy, rdx, rdy, rpx, rpy, beta, alpha, rqx, rqy As Double
        Dim thetaaq, thetaab, thetabq As Double



        For t As Integer = 0 To 71 Step 1

            rqx = points(t).X
            rqy = points(t).Y
           
            Dim rax = CDbl(TextBox12.Text)
            Dim ray = CDbl(TextBox13.Text)
            raqx = rqx - rax
            raqy = rqy - ray
           
            raq = Sqrt((raqx ^ 2) + (raqy ^ 2))

            thetaaq = Atan2(raqy, raqx)
            beta = ((raq ^ 2) + (rab ^ 2) - (rbq ^ 2)) / (2 * raq * rab)

            alpha = Acos(beta)
            thetaab = thetaaq + alpha

            rbqx = (rab * Cos(thetaab)) - (raq * Cos(thetaaq))
            rbqy = (rab * Sin(thetaab)) - (raq * Sin(thetaaq))

            thetabq = Atan(rbqy / rbqx)

            TextBox17.Text = beta
            rbx = rax + (rab * Cos(thetaab))
            rby = ray + (rab * Sin(thetaab))

            rcx = rax + (rac * Cos(thetaab))
            rcy = ray + (rac * Sin(thetaab))

            rdx = rcx + (rbq * Cos(thetabq))
            rdy = rcy + (rbq * Sin(thetabq))


            rpx = rcx + (s * rbq * Cos(thetabq))
            rpy = rcy + (s * rbq * Sin(thetabq))

           
            paints(t + 360) = New PointF(rbx, rby)
            paints(t + 72) = New PointF(rcx, rcy)
            paints(t + 144) = New PointF(rdx, rdy)
            paints(t + 216) = New PointF(rpx, rpy)
            paints(t + 288) = New PointF(rax, ray)
            paints(t) = New PointF(rqx, rqy)


        Next

   
        Dim j = Abs(CDbl(TextBox9.Text))
        Do While j > 359
            j = j - 360
        Loop
        j = j \ 5
       
        g.DrawLine(mypen2, paints(j + 288), paints(j + 360))
        g.DrawLine(mypen3, paints(j + 360), paints(j + 72))
        g.DrawLine(mypen, paints(j + 72), paints(j + 144))
        g.DrawLine(mypen2, paints(j + 144), paints(j + 216))
        g.DrawLine(mypen2, paints(j + 144), paints(j))
        g.DrawLine(mypen3, paints(j + 360), paints(j))
        For p As Integer = 0 To 70 Step 1
            g.DrawLine(mypen, paints(p + 216), paints(p + 217))
            g.DrawLine(mypen, paints(287), paints(216))
        Next
        Dim cpx = paints(j + 72).X - paints(j + 216).X
        Dim cpy = paints(j + 72).Y - paints(j + 216).Y
         cp = Sqrt((cpx ^ 2) + (cpy ^ 2))
        po2ax = pointO2.x - paints(j + 288).X
        po2ay = pointO2.y - paints(j + 288).Y
      

    End Sub

    Private Sub Button6_Click(sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button6.Click

        g = getgraphicsobject(PictureBox1)
        setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))

        Dim ptfs() As PointF = {New PointF(e.X, e.Y)}
        Dim rax = CDbl(TextBox12.Text)
        Dim ray = CDbl(TextBox12.Text)
        Dim rbx, rby, rbqx, rbqy As Double
        rab = CDbl(TextBox14.Text)
        s = CDbl(TextBox15.Text)
        Dim theta3 = (CDbl(TextBox16.Text) * (PI / 180))
        Dim rqx = CDbl(TextBox10.Text)
        Dim rqy = CDbl(TextBox11.Text)
        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)


        plot(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))
        rbx = rax + (rab * Cos(theta3))
        rby = ray + (rab * Sin(theta3))

        rbqx = rbx - rqx
        rbqy = rby - rqy

        rbq = Sqrt((rbqx ^ 2) + (rbqy ^ 2))
        rac = s * rab
        pantograph(rab, rac, rbq, s)
        'MsgBox("if it is not generated use mouse wheel to generate pantograph")

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Timer1.Enabled = True

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim mypen As New Pen(Color.Black, 0.1)
        Dim mypen2 As New Pen(Color.Red, 0.1)
        Dim mypen3 As New Pen(Color.Green, 0.1)
        Dim mypen4 As New Pen(Color.Blue, 0.1)
        g = getgraphicsobject(PictureBox1)
        setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)

        If z > 71 Then
            Timer1.Enabled = False
            z = 0
            plot(TextBox5, TextBox6, CDbl(TextBox8.Text), CDbl(TextBox7.Text) * (PI / 180))
            pantograph(rab, rac, rbq, s)
        Else

            Dim pointO2 = New PointF(CDbl(TextBox5.Text), CDbl(TextBox6.Text))
            g.DrawLine(mypen2, pointO2.X, pointO2.Y, points(z + 72).X, points(z + 72).Y) 'red
            g.DrawLine(mypen, points(z + 72), points(z + 144)) 'black
            g.DrawLine(mypen4, points(z + 144), points(z)) 'blue
            g.DrawLine(mypen3, points(z + 144), points(216)) 'green

            g.DrawLine(mypen2, paints(z + 288), paints(z + 360))
            g.DrawLine(mypen3, paints(z + 360), paints(z + 72))
            g.DrawLine(mypen, paints(z + 72), paints(z + 144))
            g.DrawLine(mypen2, paints(z + 144), paints(z + 216))
            g.DrawLine(mypen2, paints(z + 144), paints(z))
            g.DrawLine(mypen3, paints(z + 360), paints(z))
            For p As Integer = 1 To z Step 1
                g.DrawLine(mypen, points(p), points(p - 1))
                'If z = 71 Then
                '    'g.DrawLine(mypen, points(71), points(0))
                'End If
            Next
            For q As Integer = 1 To z Step 1
                g.DrawLine(mypen, paints(q + 216), paints(q + 215))
                'If z = 71 Then
                '    'g.DrawLine(mypen, paints(287), paints(216))
                'End If
            Next
            z = z + 1
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click

        g = getgraphicsobject(PictureBox1)
        Dim mypen As New Pen(Color.Black, 1)
        Dim mybrush As New SolidBrush(Color.Green)
        xx += CDbl(TextBox20.Text)
        yy += CDbl(TextBox21.Text)
        g.TranslateTransform(xx, yy)

        g.FillRectangle(mybrush, 75, 75, 100, 50)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click

        g = getgraphicsobject(PictureBox1)
        g.TranslateTransform(CSng(PictureBox1.Width) / 2, CSng(PictureBox1.Height) / 2)
        angel += (TextBox19.Text)
        g.RotateTransform(-angel)
        g.TranslateTransform(-CSng(PictureBox1.Width) / 2, -CSng(PictureBox1.Height) / 2)
        Dim mybrush As New SolidBrush(Color.Green)
        g.FillRectangle(mybrush, 75, 75, 100, 50)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click

        g = getgraphicsobject(PictureBox1)
        g.TranslateTransform(CSng(PictureBox1.Width) / 2, CSng(PictureBox1.Height) / 2)
        g.ScaleTransform(CDbl(TextBox18.Text), CDbl(TextBox18.Text))
        g.TranslateTransform(-CSng(PictureBox1.Width) / 2, -CSng(PictureBox1.Height) / 2)
        Dim mybrush As New SolidBrush(Color.Green)
        g.FillRectangle(mybrush, 75, 75, 100, 50)

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click

        g = getgraphicsobject(PictureBox1)
        g.TranslateTransform(CSng(PictureBox1.Width) / 2, CSng(PictureBox1.Height) / 2)
        g.RotateTransform(-TextBox19.Text)
        g.ScaleTransform(CDbl(TextBox18.Text), CDbl(TextBox18.Text))
        g.TranslateTransform(-CSng(PictureBox1.Width) / 2, -CSng(PictureBox1.Height) / 2)
        g.TranslateTransform(CDbl(TextBox20.Text), CDbl(TextBox21.Text))
        Dim mybrush As New SolidBrush(Color.Green)
        g.FillRectangle(mybrush, 75, 75, 100, 50)

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

        g = getgraphicsobject(PictureBox1)
        Dim mybrush As New SolidBrush(Color.Green)
        g.FillRectangle(mybrush, 75, 75, 100, 50)

    End Sub

    
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

        MsgBox(o2a.ToString)
        MsgBox(ab.ToString)
        MsgBox(o4b.ToString)
        MsgBox(pb.ToString)
        MsgBox(o4a.ToString)
        MsgBox(rab.ToString)
        MsgBox(rac.ToString)
        MsgBox(rbq.ToString)
        MsgBox(cp.ToString)
        MsgBox(po2ax.ToString)
        MsgBox(po2ay.ToString)

    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button14.Click

        g = getgraphicsobject(PictureBox1)
        If Not (TextBox1.Text = Nothing Or
                TextBox2.Text = Nothing Or
                TextBox3.Text = Nothing Or
                TextBox4.Text = Nothing) Then
            setscale(g, PictureBox1.Width, PictureBox1.Height, CSng(TextBox1.Text), CSng(TextBox2.Text), CSng(TextBox4.Text), CSng(TextBox3.Text))
        Else
            MsgBox("please fill the textboxes with xmin,xmax,ymin,ymax")
        End If
        ptfs(0) = New PointF(e.X, e.Y)
        M_inversetransformation = g.Transform
        M_inversetransformation.Invert()
        M_inversetransformation.TransformPoints(ptfs)

        If Not (TextBox22.Text = Nothing Or
                TextBox23.Text = Nothing Or
                TextBox24.Text = Nothing Or
                TextBox25.Text = Nothing) Then

            drawrect()
        Else
            MsgBox("Fill textboxes with opposite corners of the rectangle")
        End If
       
    End Sub
    Public Sub drawrect()

        Dim myPen As New Pen(Color.Blue, 0)
        Dim myBrush As New SolidBrush(Color.Blue)
        
        x1 = CSng(TextBox22.Text)
        y1 = CSng(TextBox23.Text)
        x2 = CSng(TextBox24.Text)
        y2 = CSng(TextBox25.Text)
        Dim point1 As New Point(x1, y1)
        Dim point2 As New Point(x2, y1)
        Dim point3 As New Point(x1, y2)
        Dim point4 As New Point(x2, y2)

        If x1 < x2 And y1 > y2 Then
            x = x1
            y = y2
        End If
        If x1 > x2 And y1 < y2 Then
            x = x2
            y = y1
        End If
        If x1 < x2 And y1 < y2 Then
            x = x1
            y = y1
        End If
        If x1 > x2 And y1 > y2 Then
            x = x2
            y = y2
        End If
        g.DrawRectangle(myPen, x, y, Abs(x2 - x1), Abs(y2 - y1))
    End Sub

End Class
