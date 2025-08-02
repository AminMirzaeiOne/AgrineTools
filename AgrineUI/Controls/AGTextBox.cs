using AgrineUI.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class AGTextBox : UserControl, IAGControlBorder
{
    private TextBox textBox1;

    public AGTextBox()
    {
        this.InitializeComponent();
    }

    [Category("Border")]
    public byte BorderSize { get; set; } = 2;

    [Category("Border")]
    public byte BorderRadius { get; set; } = 20;

    [Category("Border")]
    public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Tomato;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
        using (GraphicsPath GraphPath = AgrineUI.Practical.Graphics.AGRadius.GetRoundPath(Rect, this.BorderRadius))
        {
            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(this.BorderColor, this.BorderSize))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
            }
        }
    }
    private void InitializeComponent()
    {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 31);
            this.textBox1.TabIndex = 0;
            // 
            // AGTextBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AGTextBox";
            this.Size = new System.Drawing.Size(269, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
