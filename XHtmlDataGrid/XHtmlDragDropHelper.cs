using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Divaltohtml
{

	[Serializable]
	public class DataGridDataContainer : ISerializable
	{
		public int ProcessId { get; set; }
		public uint SourceId { get; set; }
		public string DragName { get; set; }
		public string DragNameDefault { get; set; }
		public string DragData { get; set; }
		public bool DragRow { get; set; }
		public ushort ColIndex { get; set; }
		public ushort RowIndex { get; set; }

		public DataGridDataContainer() { }

		// Deserialization constructor
		protected DataGridDataContainer(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info");

			ProcessId = (int)info.GetValue("ProcessID", typeof(int));
			SourceId = (uint)info.GetValue("SourceId", typeof(uint));
			DragName = (string)info.GetValue("DragName", typeof(string));
			DragData = (string)info.GetValue("DragData", typeof(string));
			DragRow = (bool)info.GetValue("DragRow", typeof(bool));
			ColIndex = (ushort)info.GetValue("ColIndex", typeof(ushort));
			RowIndex = (ushort)info.GetValue("RowIndex", typeof(ushort));
		}

		// ISerializable Members
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info");

			info.AddValue("ProcessID", ProcessId);
			info.AddValue("SourceId", SourceId);
			info.AddValue("DragName", DragName);
			info.AddValue("DragData", DragData);
			info.AddValue("DragRow", DragRow);
			info.AddValue("ColIndex", ColIndex);
			info.AddValue("RowIndex", RowIndex);
		}

	}


	public class DragAdorner // !!!!!!!!!!!!!!!!!! : Adorner
	{
		private readonly UIElement child;
		private const double XCenter = 50;
		private const double YCenter = 10;

		private double leftOffset;
		public double LeftOffset
		{
			get { return leftOffset; }
			set
			{
				leftOffset = value - XCenter;
				UpdatePosition();
			}
		}

		private double topOffset;
		public double TopOffset
		{
			get { return topOffset; }
			set
			{
				topOffset = value - YCenter;
				UpdatePosition();
			}
		}

		/*
		/// <summary>
		/// Constructeur de base
		/// </summary>
		/// <param name="adornedElement">objet recevant l'adorner</param>
		public DragAdorner(UIElement adornedElement) : base(adornedElement) { }

		/// <summary>
		/// Constructeur paramétré
		/// </summary>
		/// <param name="adornedElement">objet recevant l'adorner</param>
		/// <param name="containsRow">si la représentation est celle d'une ligne entière et non pas seulement d'une cellule</param>
		public DragAdorner(UIElement adornedElement, bool containsRow)
			: base(adornedElement)
		{
			var fill = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
			fill.Freeze();

			child = new Rectangle
			{
				RadiusX = 2,
				RadiusY = 2,
				Width = containsRow ? 500 : 100,
				Height = 20,
				Fill = fill
			};
		}
		*/

		private void UpdatePosition()
		{
			/*
			var adorner = (AdornerLayer)Parent;
			if (adorner != null) adorner.Update(AdornedElement);
			 * */
		}

		/*
		protected override Visual GetVisualChild(int index)
		{
			return child;
		}

		protected override int VisualChildrenCount
		{
			get
			{
				return 1;
			}
		}

		protected override Size MeasureOverride(Size constraint)
		{
			child.Measure(constraint);
			return child.DesiredSize;
		}
		protected override Size ArrangeOverride(Size finalSize)
		{

			child.Arrange(new Rect(child.DesiredSize));
			return finalSize;
		}

		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			var result = new GeneralTransformGroup();

			result.Children.Add(base.GetDesiredTransform(transform));
			result.Children.Add(new TranslateTransform(leftOffset, topOffset));
			return result;
		}
		 * */
	}


	public class DataGridDragAdorner // !!!!!!!!!!!!!!!!! : Adorner
	{
		private readonly UIElement child;
		private double topOffset;
		private double offset;
		public double Offset
		{
			get { return offset; }
			set
			{
				offset = value;
				UpdatePosition();
			}
		}


		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="adornedElement">objet recevant l'adorner</param>
		//public DataGridDragAdorner(UIElement adornedElement)
		//   : base(adornedElement)
		//{
		//   var element = adornedElement as FrameworkElement;
		//   if (element == null) return;

		//   var stroke = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
		//   var fill = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
		//   stroke.Freeze();
		//   fill.Freeze();

		//   child = new Rectangle
		//   {
		//      RadiusX = 2,
		//      RadiusY = 2,
		//      Width = element.ActualWidth,
		//      Height = element.ActualHeight,
		//      Stroke = stroke,
		//      Fill = fill
		//   };
		//}


		private void UpdatePosition()
		{
			/*
			var adorenedElement = AdornedElement as FrameworkElement;
			if (adorenedElement != null)
			{
				if (offset < adorenedElement.ActualHeight / 3 || offset > 2 * adorenedElement.ActualHeight / 3) ((Rectangle)child).Height = 3;
				else ((Rectangle)child).Height = adorenedElement.ActualHeight;

				if (offset > 2 * adorenedElement.ActualHeight / 3) topOffset = adorenedElement.ActualHeight - 3;
				else topOffset = 0;
			}

			var adorner = (AdornerLayer)Parent;
			if (adorner != null) adorner.Update(AdornedElement);
			 * */
		}

		/*
		protected override Visual GetVisualChild(int index)
		{
			return child;
		}

		protected override int VisualChildrenCount
		{
			get
			{
				return 1;
			}
		}

		protected override Size MeasureOverride(Size constraint)
		{
			child.Measure(constraint);
			return child.DesiredSize;
		}
		protected override Size ArrangeOverride(Size finalSize)
		{
			child.Arrange(new Rect(child.DesiredSize));
			return finalSize;
		}

		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			var result = new GeneralTransformGroup();

			result.Children.Add(base.GetDesiredTransform(transform));
			result.Children.Add(new TranslateTransform(0, topOffset));
			return result;
		}
		 * */
	}

}
