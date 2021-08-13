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
		public void registrarProducto(string idcategoria, string codigo, string nombre,
			string descripcion, string fechaelab, string fechaexp, string cantidad) {
			string sql = "INSERT INTO TBLPRODUCTO(IDCATEGORIAPRODUCTO,PRODCODIGO,PRODNOMBRE,PRODDESC,PRODFRECHAELAB," +
				"PRODFECHAEXP,PRODCANTIDAD,PRODESTADO) " +
				"VALUES (@1,@2,@3,@4,@5,@6,'A') ";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.AddWithValue("@1", idcategoria);
			insertar.Parameters.AddWithValue("@2", codigo);
			insertar.Parameters.AddWithValue("@3", nombre);
			insertar.Parameters.AddWithValue("@4", descripcion);
			insertar.Parameters.AddWithValue("@5", fechaelab);
			insertar.Parameters.AddWithValue("@6", fechaexp);
			insertar.Parameters.AddWithValue("@7", cantidad);
			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}


		[WebMethod]
public void modificarProducto(string idcate, string codigo, string nombre,
	string descripcion, string fechaelab, string fechaexp, int cantidad, string estado,int id)
		{
			string sql = "UPDATE  TBLPRODUCTO SET IDCATEGORIAPRODUCTO=@1, PRODCODIGO = @2, PRODNOMBRE=@3,PRODDESC=@4,PRODFRECHAELAB=@5, " +
				"PRODFECHAEXP=@6, PRODCANTIDAD=@7 WHERE IDPRODUCTO=@id";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.AddWithValue("@1", idcate);
			insertar.Parameters.AddWithValue("@2", codigo);
			insertar.Parameters.AddWithValue("@3", nombre);
			insertar.Parameters.AddWithValue("@4", descripcion);
			insertar.Parameters.AddWithValue("@5", fechaelab);
			insertar.Parameters.AddWithValue("@6", fechaexp);
			insertar.Parameters.AddWithValue("@7", cantidad);
			insertar.Parameters.AddWithValue("@id", id);

			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}

		[WebMethod]
		public DataSet cargarDatosProducto(string codigo)
		{

			con.Open();
			string query = "SELECT * FROM viewProCat WHERE PRODCODIGO='" + codigo + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}


		[WebMethod]
		public void eliminarProducto(string id)
		{
			string sql = "DELETE FROM TBLPRODUCTO WHERE IDPRODUCTO=@id";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.Parameters.AddWithValue("@id", id);

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
			insertar.Parameters.AddWithValue("@1", tipo);
			insertar.Parameters.AddWithValue("@2", descripcion);



			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}

		[WebMethod]
		public void actualizarCategoria(string tipo, string descripcion,int id)
		{
			string sql = "UPDATE  TBLCATEGORIAPRODUCTO SET CATTIPO =@1, CATDESCRIPCION=@2 WHERE IDCATEGORIAPRODUCTO=@id";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.AddWithValue("@1", tipo);
			insertar.Parameters.AddWithValue("@2", descripcion);
			insertar.Parameters.AddWithValue("@id", id);


			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}



		[WebMethod]
		public void eliminarCategoria(int id)
		{
			string sql = "DELETE FROM TBLCATEGORIAPRODUCTO" +
				" WHERE IDCATEGORIAPRODUCTO=@id";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.Parameters.AddWithValue("@id", id);
			comando1.ExecuteNonQuery();
			con.Close();

		}

		[WebMethod]
		public DataSet cargarDatosCategoria(string id)
		{

			con.Open();
			string query = "SELECT * FROM TBLCATEGORIAPRODUCTO WHERE IDCATEGORIAPRODUCTO='" + id + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
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
			insertar.Parameters.AddWithValue("@1", nombre);
			insertar.Parameters.AddWithValue("@2", ruc);
			insertar.Parameters.AddWithValue("@3", direccion);
			insertar.Parameters.AddWithValue("@4", telefono);
			insertar.Parameters.AddWithValue("@5", correo);






			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}

		[WebMethod]
		public void actualizarProveedor( string nombre, string ruc, string direccion, string telefono, string correo,int id)
		{
			string sql = "UPDATE  TBLPROVEEODR SET PROVNOMBRE =@1, PROVRUC=@2,PROVDIRECCION=@3,PROVTELEFONO=@4,PROVCORREO=@5 " +
				"WHERE IDPROVEEDOR = @id";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			insertar.Parameters.AddWithValue("@1", nombre);
			insertar.Parameters.AddWithValue("@2", ruc);
			insertar.Parameters.AddWithValue("@3", direccion);
			insertar.Parameters.AddWithValue("@4", telefono);
			insertar.Parameters.AddWithValue("@5", correo);
			insertar.Parameters.AddWithValue("@id", id);

			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();

		}

		[WebMethod]
		public DataSet cargarDatosProveedor(string ruc)
		{

			con.Open();
			string query = "SELECT * FROM TBLPROVEEODR WHERE PROVRUC='" + ruc + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}




		[WebMethod]
		public void eliminarProveedor(int id)
		{
			string sql = "DELETE FROM TBLPROVEEDOR" +
				" WHERE IDPROVEEDOR=@id";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.Parameters.AddWithValue("@id", id);

			comando1.ExecuteNonQuery();
			con.Close();

		}



		[WebMethod]
		public void eliminarventa(int id)
		{
			string sql = "DELETE FROM TBLVENTA" +
				" WHERE IDVENTA= @id";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.Parameters.AddWithValue("@id", id);

			comando1.ExecuteNonQuery();
			con.Close();

		}


		[WebMethod]
		public void eliminarcompra(int id)
		{
			string sql = "DELETE FROM TBLCOMPRA" +
				" WHERE IDCOMPRA=@id";
			con.Open();
			SqlCommand comando1 = new SqlCommand(sql, con);
			comando1.Parameters.AddWithValue("@id", id);

			comando1.ExecuteNonQuery();
			con.Close();

		}


		#region Usuario

		[WebMethod]
		public void RegistrarUsuario(string nombre, string apellido, string cedula, string correo, string clave, char rol)
		{
			try
			{
				string query = "INSERT INTO TBLUSUARIO VALUES(@nombre,@apellido,@cedula,@correo,@clave,@rol,'A')";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);

				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@cedula", cedula);
				cmd.Parameters.AddWithValue("@correo", correo);
				cmd.Parameters.AddWithValue("@clave", clave);
				cmd.Parameters.AddWithValue("@rol", rol);

				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

			con.Close();
		}

		[WebMethod]
		public void actualizarCliente(string nombre, string apellido, string cedula, string correo, string clave, char rol, int id)
		{
			try
			{
				string query = "UPDATE TBLUSUARIO SET USUNOMBRE=@nombre,USUAPELLIDO=@apellido,USUCEDULA=@cedula,USUUSUARIO=@correo,USUCLAVE=@clave,USUROL=@rol WHERE IDUSUARIO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@cedula", cedula);
				cmd.Parameters.AddWithValue("@correo", correo);
				cmd.Parameters.AddWithValue("@clave", clave);
				cmd.Parameters.AddWithValue("@rol", rol);
				cmd.Parameters.AddWithValue("@id", id);

				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

			con.Close();

		}

		[WebMethod]
		public void eliminarUsuario(int id)
		{
			try
			{
				string query = "UPDATE TBLUSUARIO SET USUESTADO='I' WHERE IDUSUARIO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}
			con.Close();
		}

		[WebMethod]
		public DataSet cargarDatosUsuario(string ci)
		{

			con.Open();
			string query = "SELECT * FROM TBLUSUARIO WHERE USUCEDULA='" + ci + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}

		#endregion

		#region Cliente
		[WebMethod]
		public void RegistrarCliente(string nombre, string apellido, string cedula, string direccion, string telefono)
		{
			try
			{
				string query = "INSERT INTO TBLCLIENTE VALUES(@nombre,@apellido,@cedula,@direccion,@telf,'A')";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);

				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@cedula", cedula);
				cmd.Parameters.AddWithValue("@direccion", direccion);
				cmd.Parameters.AddWithValue("@telf", telefono);


				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

			con.Close();
		}

		[WebMethod]
		public void modificarCliente(string nombre, string apellido, string cedula, string direccion, string telefono, int id)
		{
			try
			{
				string query = "UPDATE TBLCLIENTE SET CLINOMBRE=@nombre,CLIAPELLIDO=@apellido,CLICEDULA=@cedula,CLIDIRECCION=@dir,CLITELEFONO=@telf WHERE IDCLIENTE=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@cedula", cedula);
				cmd.Parameters.AddWithValue("@dir", direccion);
				cmd.Parameters.AddWithValue("@telf", telefono);
				cmd.Parameters.AddWithValue("@id", id);

				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

			con.Close();

		}

		[WebMethod]
		public void eliminarCliente(int id)
		{
			try
			{
				string query = "UPDATE TBLCLIENTE SET CLIESTADO='I' WHERE IDCLIENTE=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}
			con.Close();
		}


		[WebMethod]
		public DataSet cargarDatosCliente(string ci)
		{

			con.Open();
			string query = "SELECT * FROM TBLCLIENTE WHERE CLICEDULA='" + ci + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}
		#endregion

	}



}
