using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgrineUI.Animations
{
    /// <summary>
    /// Settings of animation
    /// </summary>
    public class AGAnimation
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(AGPointFConverter))]
        public PointF SlideCoeff { get; set; }

        public float RotateCoeff { get; set; }
        public float RotateLimit { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(AGPointFConverter))]
        public PointF ScaleCoeff { get; set; }

        public float TransparencyCoeff { get; set; }
        public float LeafCoeff { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(AGPointFConverter))]
        public PointF MosaicShift { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(AGPointFConverter))]
        public PointF MosaicCoeff { get; set; }

        public int MosaicSize { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Advanced), TypeConverter(typeof(AGPointFConverter))]
        public PointF BlindCoeff { get; set; }

        public float TimeCoeff { get; set; }
        public float MinTime { get; set; }
        public float MaxTime { get; set; }
        public Padding Padding { get; set; }
        public bool AnimateOnlyDifferences { get; set; }


        public bool IsNonLinearTransformNeeded
        {
            get
            {
                if (this.BlindCoeff == PointF.Empty)
                    if (this.MosaicCoeff == PointF.Empty || this.MosaicSize == 0)
                        if (this.TransparencyCoeff == 0f)
                            if (this.LeafCoeff == 0f)
                                return false;

                return true;
            }
        }

        public AGAnimation()
        {
            this.MinTime = 0f;
            this.MaxTime = 1f;
            this.AnimateOnlyDifferences = true;
        }

        public AGAnimation Clone()
        {
            return (AGAnimation)MemberwiseClone();
        }

        public static AGAnimation Rotate { get { return new AGAnimation { RotateCoeff = 1f, TransparencyCoeff = 1, Padding = new Padding(50, 50, 50, 50) }; } }
        public static AGAnimation HorizSlide { get { return new AGAnimation { SlideCoeff = new PointF(1, 0) }; } }
        public static AGAnimation VertSlide { get { return new AGAnimation { SlideCoeff = new PointF(0, 1) }; } }
        public static AGAnimation Scale { get { return new AGAnimation { ScaleCoeff = new PointF(1, 1) }; } }
        public static AGAnimation ScaleAndRotate { get { return new AGAnimation { ScaleCoeff = new PointF(1, 1), RotateCoeff = 0.5f, RotateLimit = 0.2f, Padding = new Padding(30, 30, 30, 30) }; } }
        public static AGAnimation HorizSlideAndRotate { get { return new AGAnimation { SlideCoeff = new PointF(1, 0), RotateCoeff = 0.3f, RotateLimit = 0.2f, Padding = new Padding(50, 50, 50, 50) }; } }
        public static AGAnimation ScaleAndHorizSlide { get { return new AGAnimation { ScaleCoeff = new PointF(1, 1), SlideCoeff = new PointF(1, 0), Padding = new Padding(30, 0, 0, 0) }; } }
        public static AGAnimation Transparent { get { return new AGAnimation { TransparencyCoeff = 1 }; } }
        public static AGAnimation Leaf { get { return new AGAnimation { LeafCoeff = 1 }; } }
        public static AGAnimation Mosaic { get { return new AGAnimation { MosaicCoeff = new PointF(100f, 100f), MosaicSize = 20, Padding = new Padding(30, 30, 30, 30) }; } }
        public static AGAnimation Particles { get { return new AGAnimation { MosaicCoeff = new PointF(200, 200), MosaicSize = 1, MosaicShift = new PointF(0, 0.5f), Padding = new Padding(100, 50, 100, 150), TimeCoeff = 2 }; } }
        public static AGAnimation VertBlind { get { return new AGAnimation { BlindCoeff = new PointF(0f, 1f) }; } }
        public static AGAnimation HorizBlind { get { return new AGAnimation { BlindCoeff = new PointF(1f, 0f) }; } }

        public void Add(AGAnimation a)
        {
            this.SlideCoeff = new PointF(SlideCoeff.X + a.SlideCoeff.X, SlideCoeff.Y + a.SlideCoeff.Y);
            this.RotateCoeff += a.RotateCoeff;
            this.RotateLimit += a.RotateLimit;
            this.ScaleCoeff = new PointF(ScaleCoeff.X + a.ScaleCoeff.X, ScaleCoeff.Y + a.ScaleCoeff.Y);
            this.TransparencyCoeff += a.TransparencyCoeff;
            this.LeafCoeff += a.LeafCoeff;
            this.MosaicShift = new PointF(MosaicShift.X + a.MosaicShift.X, MosaicShift.Y + a.MosaicShift.Y);
            this.MosaicCoeff = new PointF(MosaicCoeff.X + a.MosaicCoeff.X, MosaicCoeff.Y + a.MosaicCoeff.Y);
            this.MosaicSize += a.MosaicSize;
            this.BlindCoeff = new PointF(BlindCoeff.X + a.BlindCoeff.X, BlindCoeff.Y + a.BlindCoeff.Y);
            this.TimeCoeff += a.TimeCoeff;
            this.Padding += a.Padding;
        }

    }

    public enum AnimationType
    {
        Custom = 0,
        Rotate,
        HorizSlide,
        VertSlide,
        Scale,
        ScaleAndRotate,
        HorizSlideAndRotate,
        ScaleAndHorizSlide,
        Transparent,
        Leaf,
        Mosaic,
        Particles,
        VertBlind,
        HorizBlind
    }
}
