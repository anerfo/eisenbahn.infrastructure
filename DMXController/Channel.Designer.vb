<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Channel
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
        Me.ValueLabel = New System.Windows.Forms.Label()
        Me.ChannelLabel = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ValueLabel
        '
        Me.ValueLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValueLabel.AutoSize = True
        Me.ValueLabel.Location = New System.Drawing.Point(3, 315)
        Me.ValueLabel.Name = "ValueLabel"
        Me.ValueLabel.Size = New System.Drawing.Size(33, 13)
        Me.ValueLabel.TabIndex = 1
        Me.ValueLabel.Text = "value"
        '
        'ChannelLabel
        '
        Me.ChannelLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChannelLabel.AutoSize = True
        Me.ChannelLabel.Location = New System.Drawing.Point(-3, 0)
        Me.ChannelLabel.Name = "ChannelLabel"
        Me.ChannelLabel.Size = New System.Drawing.Size(46, 13)
        Me.ChannelLabel.TabIndex = 2
        Me.ChannelLabel.Text = "Channel"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(3, 16)
        Me.TrackBar1.Maximum = 255
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBar1.Size = New System.Drawing.Size(45, 296)
        Me.TrackBar1.TabIndex = 3
        '
        'Channel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.ChannelLabel)
        Me.Controls.Add(Me.ValueLabel)
        Me.Name = "Channel"
        Me.Size = New System.Drawing.Size(37, 328)
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ValueLabel As System.Windows.Forms.Label
    Friend WithEvents ChannelLabel As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar

End Class
