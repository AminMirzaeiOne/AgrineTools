using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Animations
{
    public class AGDoubleBitmapForm : System.Windows.Forms.Form, IFakeControl
    {
        Bitmap bgBmp;
        Bitmap frame;
        Padding padding;
        Control control;
        Point controlLocation;


        public event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        public event EventHandler<PaintEventArgs> FramePainting;
        public event EventHandler<PaintEventArgs> FramePainted;


        public AGDoubleBitmapForm()
        {
            InitializeComponent();
            Visible = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            TopMost = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DoubleBitmapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(426, 403);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DoubleBitmapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DoubleBitmapForm";
            this.ResumeLayout(false);

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                unchecked
                {
                    cp.Style = (int)Flags.WindowStyles.WS_POPUP;
                }
                ;
                cp.ExStyle |= (int)Flags.WindowStyles.WS_EX_NOACTIVATE | (int)Flags.WindowStyles.WS_EX_TOOLWINDOW;
                cp.X = this.Location.X;
                cp.Y = this.Location.Y;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var gr = e.Graphics;

            OnFramePainting(e);

            try
            {
                gr.DrawImage(bgBmp, -Location.X, -Location.Y);

                if (frame != null)
                {
                    var ea = new TransfromNeededEventArg();
                    ea.ClientRectangle = ea.ClipRectangle = new Rectangle(control.Bounds.Left - padding.Left, control.Bounds.Top - padding.Top, control.Bounds.Width + padding.Horizontal, control.Bounds.Height + padding.Vertical);
                    OnTransfromNeeded(ea);
                    gr.SetClip(ea.ClipRectangle);
                    gr.Transform = ea.Matrix;
                    var p = control.Location;
                    gr.DrawImage(frame, p.X - padding.Left, p.Y - padding.Top);
                }

                OnFramePainted(e);
            }
            catch { }

        }

        private void OnTransfromNeeded(TransfromNeededEventArg ea)
        {
            if (TransfromNeeded != null)
                TransfromNeeded(this, ea);
        }

        protected virtual void OnFramePainting(PaintEventArgs e)
        {
            if (FramePainting != null)
                FramePainting(this, e);
        }

        protected virtual void OnFramePainted(PaintEventArgs e)
        {
            if (FramePainted != null)
                FramePainted(this, e);
        }

        public void InitParent(Control control, Padding padding)
        {
            this.control = control;
            Location = new Point(0, 0);
            Size = Screen.PrimaryScreen.Bounds.Size;
            control.VisibleChanged += new EventHandler(control_VisibleChanged);
            this.padding = padding;
        }

        void control_VisibleChanged(object sender, EventArgs e)
        {
            controlLocation = (sender as Control).Location;
            var s = (sender as Control).Size;
        }

        public Bitmap BgBmp
        {
            get
            {
                return bgBmp;
            }
            set
            {
                bgBmp = value;
            }
        }

        public Bitmap Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
            }
        }
    }
}
