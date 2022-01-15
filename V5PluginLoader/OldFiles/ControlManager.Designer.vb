<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlManager
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Varview = New System.Windows.Forms.DataGridView
        Me.Variablenname = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Variableninhalt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Aktualisieren = New System.Windows.Forms.Button
        Me.setzten = New System.Windows.Forms.Button
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.Varview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Window
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 16)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(121, 303)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(389, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "laufende Control-Plugins:  Aktionen der Controls:                                " & _
            "    Variablen:"
        '
        'Varview
        '
        Me.Varview.AllowUserToAddRows = False
        Me.Varview.AllowUserToDeleteRows = False
        Me.Varview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Varview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Varview.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Variablenname, Me.Variableninhalt})
        Me.Varview.Location = New System.Drawing.Point(338, 16)
        Me.Varview.Name = "Varview"
        Me.Varview.Size = New System.Drawing.Size(251, 288)
        Me.Varview.TabIndex = 2
        '
        'Variablenname
        '
        Me.Variablenname.HeaderText = "Variablenname"
        Me.Variablenname.Name = "Variablenname"
        Me.Variablenname.ReadOnly = True
        '
        'Variableninhalt
        '
        Me.Variableninhalt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Variableninhalt.HeaderText = "Inhalt"
        Me.Variableninhalt.Name = "Variableninhalt"
        '
        'Aktualisieren
        '
        Me.Aktualisieren.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Aktualisieren.Location = New System.Drawing.Point(425, 310)
        Me.Aktualisieren.Name = "Aktualisieren"
        Me.Aktualisieren.Size = New System.Drawing.Size(79, 23)
        Me.Aktualisieren.TabIndex = 3
        Me.Aktualisieren.Text = "Aktualisieren"
        Me.Aktualisieren.UseVisualStyleBackColor = True
        '
        'setzten
        '
        Me.setzten.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.setzten.Location = New System.Drawing.Point(510, 310)
        Me.setzten.Name = "setzten"
        Me.setzten.Size = New System.Drawing.Size(79, 23)
        Me.setzten.TabIndex = 4
        Me.setzten.Text = "setzten"
        Me.setzten.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(127, 16)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(205, 303)
        Me.ListBox2.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 315)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "In die Listen Klicken um Eintrag hier anzusehen"
        '
        'ControlManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.setzten)
        Me.Controls.Add(Me.Aktualisieren)
        Me.Controls.Add(Me.Varview)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "ControlManager"
        Me.Size = New System.Drawing.Size(592, 337)
        CType(Me.Varview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Varview As System.Windows.Forms.DataGridView
    Friend WithEvents Aktualisieren As System.Windows.Forms.Button
    Friend WithEvents setzten As System.Windows.Forms.Button
    Friend WithEvents Variablenname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Variableninhalt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
