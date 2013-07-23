//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlDataGridColumnHeader.cs
// Description : Objet Entête de colonne
//___________________________________________________________________________

using System.Windows;
using System.Windows.Media;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlDataGridColumnHeader = Une entête de colonne
	/// </summary>
	public class XHtmlDataGridColumnHeader : DependencyObject
	{
		private static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(XHtmlDataGridColumnHeader));
		internal string Text
		{
			get { return (string)(GetValue(TextProperty)); }
			set { SetValue(TextProperty, value); }
		}

		private static readonly DependencyProperty FontProperty = DependencyProperty.Register("Font", typeof(XHtmlFont), typeof(XHtmlDataGridColumnHeader));

		//internal XHtmlFont Font
		//{
		//   get { return (XHtmlFont)(GetValue(FontProperty)); }
		//   set { SetValue(FontProperty, value); }
		//}
		public ushort? idPolice;
		public ushort? idFond;


		private static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(XHtmlDataGridColumnHeader));
		internal ImageSource Image
		{
			get { return (ImageSource)(GetValue(ImageProperty)); }
			set { SetValue(ImageProperty, value); }
		}

		private static readonly DependencyProperty LeftImageProperty = DependencyProperty.Register("LeftImage", typeof(ImageSource), typeof(XHtmlDataGridColumnHeader));
		internal ImageSource LeftImage
		{
			get { return (ImageSource)(GetValue(LeftImageProperty)); }
			set { SetValue(LeftImageProperty, value); }
		}

		private static readonly DependencyProperty RightImageProperty = DependencyProperty.Register("RightImage", typeof(ImageSource), typeof(XHtmlDataGridColumnHeader));
		internal ImageSource RightImage
		{
			get { return (ImageSource)(GetValue(RightImageProperty)); }
			set { SetValue(RightImageProperty, value); }
		}

		private static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(XHtmlDataGridColumnHeader));
		internal Brush Background
		{
			get { return (Brush)(GetValue(BackgroundProperty)); }
			set { SetValue(BackgroundProperty, value); }
		}

		private static readonly DependencyProperty IsImageProperty = DependencyProperty.Register("IsImage", typeof(bool), typeof(XHtmlDataGridColumnHeader));
		internal bool IsImage
		{
			get { return (bool)(GetValue(IsImageProperty)); }
			set { SetValue(IsImageProperty, value); }
		}

		private static readonly DependencyProperty IsHogProperty = DependencyProperty.Register("IsHog", typeof(bool), typeof(XHtmlDataGridColumnHeader));
		internal bool IsHog
		{
			get { return (bool)(GetValue(IsHogProperty)); }
			set { SetValue(IsHogProperty, value); }
		}

		private static readonly DependencyProperty HogCommandProperty = DependencyProperty.Register("HogCommand", typeof(string), typeof(XHtmlDataGridColumnHeader));
		internal string HogCommand
		{
			get { return (string)(GetValue(HogCommandProperty)); }
			set { SetValue(HogCommandProperty, value); }
		}

		public static readonly DependencyProperty IsFilteredProperty = DependencyProperty.Register("IsFiltered", typeof(bool), typeof(XHtmlDataGridColumnHeader));
		public bool IsFiltered
		{
			get { return (bool)GetValue(IsFilteredProperty); }
			set { SetValue(IsFilteredProperty, value); }
		}

		public static readonly DependencyProperty IsSortedProperty = DependencyProperty.Register("IsSorted", typeof(bool), typeof(XHtmlDataGridColumnHeader));
		public bool IsSorted
		{
			get { return (bool)GetValue(IsSortedProperty); }
			set { SetValue(IsSortedProperty, value); }
		}

		public static readonly DependencyProperty SortDescendingProperty = DependencyProperty.Register("SortDescending", typeof(bool), typeof(XHtmlDataGridColumnHeader));
		public bool SortDescending
		{
			get { return (bool)GetValue(SortDescendingProperty); }
			set { SetValue(SortDescendingProperty, value); }
		}

		public static readonly DependencyProperty SortOrderProperty = DependencyProperty.Register("SortOrder", typeof(int), typeof(XHtmlDataGridColumnHeader));
		public int SortOrder
		{
			get { return (int)GetValue(SortOrderProperty); }
			set { SetValue(SortOrderProperty, value); }
		}

	}
}
