using System;
using System.Windows.Media;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Controls
{
    public class ChartScale
    {
        private readonly AxisY2D axis;
        private static readonly ChartScale auto = new ChartScale();
        private static readonly ChartScale create = new ChartScale();

        public ChartScale()
        {
            axis = new SecondaryAxisY2D();
            IsNew = true;
        }

        public ChartScale(AxisY2D axis)
        {
            this.axis = axis;
            IsNew = false;
        }

        public double Max
        {
            get
            {
                if (!(axis is SecondaryAxisY2D))
                {
                    return (double) axis.ActualRange.ActualMaxValue;
                }
                if (axis.Range == null)
                {
                    return 0;
                }
                return (double)axis.Range.MaxValue;
            }
            set
            {
                if (!(axis is SecondaryAxisY2D))
                {
                    axis.ActualRange.MaxValue = value;
                    return;
                }
                axis.Range.MaxValue = value; 
            }
        }

        public double Min
        {
            get
            {
                if (!(axis is SecondaryAxisY2D))
                {
                    return (double)axis.ActualRange.ActualMinValue;
                }
                if (axis.Range == null)
                {
                    return 0;
                }
                return (double)axis.Range.MinValue;
            }
            set
            {
                if (!(axis is SecondaryAxisY2D))
                {
                    axis.ActualRange.MinValue = value;
                    return;
                }
                axis.Range.MinValue = value;
            }
        }

        public string Name
        {
            get
            {
                return ToString();
            }
        }

        /// <summary>
        /// Whether it was created with default constructor (non-parameterized one).
        /// </summary>
        public bool IsNew
        {
            get; 
            set;
        }

        public Brush Color
        {
            get
            {
                return axis.Brush;
            }
        }

        public AxisY2D Axis
        {
            get
            {
                return axis;
            }
        }

        public static ChartScale Auto
        {
            get
            {
                return auto;
            }
        }

        public static ChartScale Create
        {
            get
            {
                return create;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} : {1}", Min, Max);
        }

        public override bool Equals(object obj)
        {
            ChartScale scale = obj as ChartScale;
            if (scale == null)
            {
                throw new ArgumentException();
            }
            return ReferenceEquals(scale.Axis, Axis);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
