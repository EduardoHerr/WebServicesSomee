using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication2
{
	/// <summary>
	/// Descripción breve de WebService1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	// [System.Web.Script.Services.ScriptService]
	public class WebService1 : System.Web.Services.WebService
	{
		static string url = "Data Source=PrototipoMovil.mssql.somee.com;Initial Catalog=PrototipoMovil;Persist Security Info=True;User ID=DLPN_SQLLogin_1;Password=zw4fssu4d4";
		/// <summary>
		/// static string url = "workstation id=PrototipoMovil.mssql.somee.com;packet size=4096;user id=DLPN_SQLLogin_1;pwd=zw4fssu4d4;data source=PrototipoMovil.mssql.somee.com;persist security info=False;initial catalog=PrototipoMovil";
		/// </summary>
		SqlConnection con = new SqlConnection(url);
		int rol = 0;


		[WebMethod]
		public string HelloWorld()
		{
			return "Hola a todos";
		}

		[WebMethod]
		public int Ingresar(string user, string pwd)
		{
			string query = "SELECT * FROM TBLUSUARIO WHERE USUUSUARIO = '" + user + "' AND USUCLAVE='" + pwd + "';";
			con.Open();

			SqlCommand cmd = new SqlCommand(query, con);
			SqlDataReader rs = cmd.ExecuteReader();

			while (rs.Read())
			{
				if (rs["USUUSUARIO"].ToString().Equals(user) && rs["USUCLAVE"].ToString().Equals(pwd))
				{
					if (rs["USUESTADO"].ToString().Equals("A"))
					{
						if (rs["USUROL"].ToString().Equals("A"))
						{
							rol = 1;
						}
						else
						{
							rol = 2;
						}
					}
					else
					{
						rol = 0;
					}

				}
				else
				{
					rol = 0;
				}
			}

			rs.Close();
			con.Close();

			return rol;
		}



	}
}
