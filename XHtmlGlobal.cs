using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Collections.Specialized;
using Divalto.Systeme;
using Divalto.Systeme.DVOutilsSysteme;
using System.Web.Security;  //bhtest 
using System.Web.Caching;
using System.IO;
//using Divalto.XWeb;
using System.Runtime.InteropServices;
//using Divalto.Systeme.DHTransportLC;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;



namespace Divaltohtml
{


	public class HtmlGlobal
	{
		public XHtmlApplication App;

		public  DataContractJsonSerializer JsonParamCompl = new DataContractJsonSerializer(typeof(ListeParametresComplementaires));
		public  DataContractJsonSerializer JsonParamsRecus = new DataContractJsonSerializer(typeof(ListeParametresRecus));
		public  DataContractJsonSerializer JsonParamsEnvoyes = new DataContractJsonSerializer(typeof(ListeParametresEnvoyes));
		public  DataContractJsonSerializer JsonListeUshort = new DataContractJsonSerializer(typeof(ListeUshort));
		public  DataContractJsonSerializer JsonApplication = new DataContractJsonSerializer(typeof(XHtmlApplication));
		public  DataContractJsonSerializer JsonCreationColonnes = new DataContractJsonSerializer(typeof(ColonneJqGrid));
		public  DataContractJsonSerializer JsonListeCreationColonnes = new DataContractJsonSerializer(typeof(DescriptionJqGrid));
		public DataContractJsonSerializer JsonParametresLancement = new DataContractJsonSerializer(typeof(XHtmlParametresLancement));
		public DataContractJsonSerializer JsonParametresComboBox = new DataContractJsonSerializer(typeof(ComboBoxVersJson));
		public DataContractJsonSerializer JsonParametresCaseACocher = new DataContractJsonSerializer(typeof(CaseACocherVersJson));
		public DataContractJsonSerializer JsonDataComboBox = new DataContractJsonSerializer(typeof(List<UnItemJson>));
		public DataContractJsonSerializer JsonParametresOnglet = new DataContractJsonSerializer(typeof(OngletVersJson));
		public DataContractJsonSerializer JsonClicOnglet = new DataContractJsonSerializer(typeof(ClicOnglet));
		public DataContractJsonSerializer JsonGrille = new DataContractJsonSerializer(typeof(Grid.GridJson));

		public UnicodeEncoding UniEncoding = new UnicodeEncoding();

		public string[] ListeIdentsTablesExistantesCoteClient = new string[1];

		
		public ListeParametresEnvoyes Envois;

		//public static  XHtml Html;
		public UTF8Encoding utf8Encoder = new UTF8Encoding();

		public HtmlGlobal()
		{
			Envois = new ListeParametresEnvoyes();
		}

		public static string CalculerId(uint idObj, string idPage, int niveau)
		{
			return idObj.ToString() + "_" + idPage.ToString() + "_" + niveau.ToString();
		}
		public string CalculerId(uint idObj, string idPage)
		{
			return idObj.ToString() + "_" + idPage.ToString() + "_" + App.StackOfWindows.Count;
		}

		public string CalculerIdDataGrid(uint idDataGrid, string idPage)
		{
			return CalculerId(idDataGrid, CalculerIdPage(idPage));
		}

		static public byte[] ToJson(object o, DataContractJsonSerializer dataContractJsonSerializer)
		{
			MemoryStream stream2 = new MemoryStream();
			dataContractJsonSerializer.WriteObject(stream2, o);
			byte[] buf = stream2.GetBuffer();
			byte[] bufa = new byte[stream2.Length];
			Array.Copy(buf, bufa, stream2.Length);
			return bufa;
		}


		static public string ToJsonString(object o, DataContractJsonSerializer dataContractJsonSerializer, bool avecHtmlEncode)
		{
			UTF8Encoding utf8Encoder = new UTF8Encoding();
			byte[] buf = ToJson(o, dataContractJsonSerializer);
			string s = utf8Encoder.GetString(buf, 0, (int)buf.Length);
			if (avecHtmlEncode)
				s = HttpUtility.HtmlEncode(s);
			return s;
		}



		public string CalculerIdPage(string idPage)
		{
			int niveau = App.StackOfWindows.Count;
			return idPage.ToString() + "_" + niveau.ToString();
		}


	};

	[DataContract]
	public class ListeUshort
	{
		[DataMember]
		public List<ushort> l = new List<ushort>();
	};


	/// <summary>
	/// classe contenant les parametres de lancement trouvés dans la page aspx
	/// Attention, ce sont les memes noms dans la page aspx (JSON)
	/// </summary>
	public class XHtmlParametresLancement
	{
		public string program;
		public string user;
		public string div;
	}


}
