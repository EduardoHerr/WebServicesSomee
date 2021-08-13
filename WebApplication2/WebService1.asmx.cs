using System;
using System.Collections.Generic;
using System.Data;
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

		[WebMethod]
		public void registrarProducto(string codigo, string nombre,
			string descripcion, string fechaelab, string fechaexp, int cantidad, string estado) {
			string sql = "INSERT INTO TBLPRODUCTO(PRODCODIGO,PRODNOMBRE,PRODDESC,PRODFRECHAELAB," +
				"PRODFECHAEXP,PRODCANTIDAD,PRODESTADO) " +
				"VALUES (@1,@2,@3,@4,@5,@6,'A') ";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = codigo;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = nombre;
			insertar.Parameters.Add("@3", SqlDbType.VarChar, 100).Value = descripcion;
			insertar.Parameters.Add("@4", SqlDbType.VarChar, 12).Value = fechaelab;
			insertar.Parameters.Add("@5", SqlDbType.VarChar, 12).Value = fechaexp;
			insertar.Parameters.Add("@6", SqlDbType.Int).Value = cantidad;
			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}


		[WebMethod]
		public void modificarProducto(int id, string codigo, string nombre,
	string descripcion, string fechaelab, string fechaexp, int cantidad, string estado)
		{
			string sql = "UPDATE  TBLPRODUCTO SET PRODCODIGO = @1, PRODNOMBRE=@2,PRODDESC=@3,PRODFRECHAELAB=@4, " +
				"PRODFECHAEXP=@5, PRODCANTIDAD=@6 WHERE IDPRODUCTO='" + id + "'";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = codigo;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = nombre;
			insertar.Parameters.Add("@3", SqlDbType.VarChar, 100).Value = descripcion;
			insertar.Parameters.Add("@4", SqlDbType.VarChar, 12).Value = fechaelab;
			insertar.Parameters.Add("@5", SqlDbType.VarChar, 12).Value = fechaexp;
			insertar.Parameters.Add("@6", SqlDbType.Int).Value = cantidad;
			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}




		[WebMethod]
		public void eliminarProducto(string id)
		{
			string sql = "DELETE FROM TBLPRODUCTO WHERE IDPRODUCTO='" + id + "'";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.ExecuteNonQuery();
			con.Close();

		}

		[WebMethod]
		public void registrarCategoria(string tipo, string descripcion)
		{
			string sql = "INSERT INTO TBLCATEGORIAPRODUCTO(CATTIPO,CATDESCRIPCION, CATESTADO) " +
				"VALUES (@1,@2,'A') ";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = tipo;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = descripcion;

			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}

		[WebMethod]
		public void actualizarCategoria(int id, string tipo, string descripcion)
		{
			string sql = "UPDATE  TBLCATEGORIAPRODUCTO SET CATTIPO =@1, CATDESCRIPCION=@2 WHERE IDCATEGORIAPRODUCTO='" + id + "'";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = tipo;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = descripcion;

			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}



		[WebMethod]
		public void eliminarCategoria(int idcategoria)
		{
			string sql = "DELETE FROM TBLCATEGORIAPRODUCTO" +
				" WHERE IDCATEGORIAPRODUCTO='" + idcategoria + "'";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.ExecuteNonQuery();
			con.Close();

		}

		[WebMethod]
		public void registrarProveedor(string nombre, string ruc, string direccion, string telefono, string correo)
		{
			string sql = "INSERT INTO TBLPROVEEDOR" +
				"(PROVNOMBRE,PROVRUC,PROVDIRECCION,PROVTELEFONO," +
				"PROVCORREO,PROVESTADO) " +
				"VALUES (@1,@2,@3,@4,@5,'A') ";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = nombre;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = ruc;
			insertar.Parameters.Add("@3", SqlDbType.VarChar, 100).Value = direccion;
			insertar.Parameters.Add("@4", SqlDbType.VarChar, 12).Value = telefono;
			insertar.Parameters.Add("@5", SqlDbType.VarChar, 12).Value = correo;
			
			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}

		[WebMethod]
		public void actualizarProveedor(int id, string nombre, string ruc, string direccion, string telefono,string correo)
		{
			string sql = "UPDATE  TBLPROVEEODR SET PROVNOMBRE =@1, PROVRUC=@2,PROVDIRECCION=@3,PROVTELEFONO=@4,PROVCORREO=@5 " +
				"WHERE IDPROVEEDOR='" + id + "'";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.Add("@1", SqlDbType.VarChar, 100).Value = nombre;
			insertar.Parameters.Add("@2", SqlDbType.VarChar, 100).Value = ruc;
			insertar.Parameters.Add("@3", SqlDbType.VarChar, 100).Value = direccion;
			insertar.Parameters.Add("@4", SqlDbType.VarChar, 100).Value = direccion;
			insertar.Parameters.Add("@5", SqlDbType.VarChar, 100).Value = correo;

			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}


		[WebMethod]
		public void eliminarProveedor(int id)
		{
			string sql = "DELETE FROM TBLPROVEEDOR" +
				" WHERE IDPROVEEDOR='" + id + "'";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.ExecuteNonQuery();
			con.Close();

		}

		[WebMethod]
		public void eliminarventa(int id)
		{
			string sql = "DELETE FROM TBLVENTA" +
				" WHERE IDVENTA='" + id + "'";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.ExecuteNonQuery();
			con.Close();

		}


		[WebMethod]
		public void eliminarcompra(int id)
		{
			string sql = "DELETE FROM TBLCOMPRA" +
				" WHERE IDCOMPRA='" + id + "'";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.ExecuteNonQuery();
			con.Close();

		}



	}



}
