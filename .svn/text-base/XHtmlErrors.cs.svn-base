//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlErrors.cs
// Description : enumération des codes d'erreur XHtml + exception
//___________________________________________________________________________

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Divalto.Systeme.XHtml
{
	public enum XHtmlErrorCodes
	{
		None = 0,
		UnknownProperty = 0x1F01,				// propriété inconnue en lecture de dvBuffer
	}

	public enum XHtmlErrorLocations
	{
		None = 0,
		Application = 1,
		Window = 2,
		Page = 3,
		Button = 4,
		Cadre = 5,
		CheckBox = 7,
		ComboBox = 8,
		DatePicker = 9,
		GroupBox = 10,
		Label = 11,
		ListBox = 12,
		Menu = 13,
		MenuItem = 14,
		Presentation = 15,
		Hog = 16,
		PasswordBox = 17,
		RadioButton = 18,
		RadioGroup = 19,
		Font = 20,
		Padding = 21,
		Border = 22,
		Color = 23,
		ImageFile = 24,
		RichText = 25,
		TabControl = 26,
		TabItem = 27,
		TextBox = 28,
		ToolBar = 29,
		ToolBarItem = 30,
		YGraph = 31,

		Calendar = 35,

		DataGrid = 100,
		Row = 110,

		DataGridCheckBoxColumn = 120,
		DataGridComboBoxColumn = 121,
		DataGridDatePickerColumn = 122,
		DataGridHogColumn = 123,
		DataGridImageColumn = 124,
		DataGridPasswordBoxColumn = 125,
		DataGridTextBoxColumn = 126,
		DataGridTreeColumn = 127,

		DataGridCheckBoxCell = 130,
		DataGridComboBoxCell = 131,
		DataGridDatePickerCell = 132,
		DataGridHogCell = 133,
		DataGridImageCell = 134,
		DataGridPasswordBoxCell = 135,
		DataGridTextBoxCell = 136,
		DataGridTreeCell = 137,
		DataGridPresentationCell = 138,


	}

	[Serializable]
	public class XHtmlException : System.Exception
	{
		public XHtmlErrorCodes ErrorCode { get; set; }
		public XHtmlErrorLocations ErrorLocation { get; set; }
		public string ErrorParameter { get; set; }

		public XHtmlException() { }
		public XHtmlException(string message) : base(message) { }
		public XHtmlException(string message, System.Exception inner) : base(message, inner) { }
		protected XHtmlException(SerializationInfo info, StreamingContext context) : base(info, context) { }

		public XHtmlException(XHtmlErrorCodes errorCode, XHtmlErrorLocations errorLocation, string errorParameter)
		{
			ErrorCode = errorCode;
			ErrorLocation = errorLocation;
			ErrorParameter = errorParameter;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("ErrorCode", ErrorCode);
			info.AddValue("ErrorLocation", ErrorLocation);
			info.AddValue("ErrorParameter", ErrorParameter);
		}
	}
}
