using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme.DHTransportLC;
using System.Runtime.InteropServices;
using Divalto.Systeme;

namespace Divaltohtml
{
	public class XHtmlOutils
	{
		public static unsafe byte[] AjouterEnteteAuDVBuffer(DVBuffer buf)
		{
			EnteteTrame entete = new EnteteTrame();
			entete.Longueur = (uint)buf.NbEcrits;

			byte[] tout = new byte[entete.Longueur + Marshal.SizeOf(typeof(EnteteTrame))];

			fixed (uint* depart = &entete.MagicNumber)
			{
				fixed (byte* arrivee = &tout[0])
				{
					fixed (byte* adressebuf = &buf.LeBuffer[0])
					{
						HWConversion.Copier(arrivee, (byte*)depart, Marshal.SizeOf(typeof(EnteteTrame)));
						HWConversion.Copier(arrivee + Marshal.SizeOf(typeof(EnteteTrame)),
										(byte*)adressebuf, buf.NbEcrits);
					}
				}
			}
			return tout;
		}
	}
}