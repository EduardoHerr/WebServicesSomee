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

        #region Producto

        [WebMethod]
		public void registrarProducto( string codigo, string nombre,
			string descripcion, string fechaelab, string fechaexp, string cantidad) {
			string sql = "INSERT INTO TBLPRODUCTO(PRODCODIGO,PRODNOMBRE,PRODDESC,PRODFRECHAELAB," +
				"PRODFECHAEXP,PRODCANTIDAD,PRODESTADO) " +
				"VALUES (@1,@2,@3,@4,@5,'A') ";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			
			insertar.Parameters.AddWithValue("@1", codigo);
			insertar.Parameters.AddWithValue("@2", nombre);
			insertar.Parameters.AddWithValue("@3", descripcion);
			insertar.Parameters.AddWithValue("@4", fechaelab);
			insertar.Parameters.AddWithValue("@5", fechaexp);
			insertar.Parameters.AddWithValue("@6", cantidad);
			insertar.CommandType = CommandType.Text;
			insertar.ExecuteReader();
			con.Close();



		}


		[WebMethod]
public void modificarProducto( string codigo, string nombre,
	string descripcion, string fechaelab, string fechaexp, string cantidad,int id)
		{
			string sql = "UPDATE  TBLPRODUCTO SET  PRODCODIGO = @2, PRODNOMBRE=@3,PRODDESC=@4,PRODFRECHAELAB=@5, " +
				"PRODFECHAEXP=@6, PRODCANTIDAD=@7 WHERE IDPRODUCTO=@id";
			con.Open();
			SqlCommand insertar = new SqlCommand(sql, con);
			
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
		public DataSet cargarDatosProducto(string id)
		{

			con.Open();
			string query = "SELECT * FROM viewProCat WHERE IDPRODUCTO='" + id + "' AND PROESTADO='A'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}


		[WebMethod]
		public void eliminarProducto(int id)
		{
			try
			{
				string query = "UPDATE TBLPRODUCTO SET PRODESTADO='I' WHERE IDPRODUCTO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

		}
        #endregion

        #region Categoria
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
			try
			{
				string query = "UPDATE TBLCATEGORIAPRODUCTO SET CATESTADO='I' WHERE IDCATEGORIAPRODUCTO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}
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

        #endregion

        #region Proveedor
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
			string sql = "UPDATE  TBLPROVEEDOR SET PROVNOMBRE =@1, PROVRUC=@2,PROVDIRECCION=@3,PROVTELEFONO=@4,PROVCORREO=@5 " +
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
			string query = "SELECT * FROM TBLPROVEEDOR WHERE PROVRUC='" + ruc + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}




		[WebMethod]
		public void eliminarProveedor(int id)
		{
			try
			{
				string query = "UPDATE TBLPROVEEDOR SET PROVESTADO='I' WHERE IDPROVEEDOR=@id";
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


        #endregion

        #region venta


		[WebMethod]
		public void registrarVenta(string idproducto, string idcliente, string vntcodigo, int vntcantidad, float vntcostoventa, string vntfecha)
		{
			try
			{
				string sql = "INSERT INTO TBLVENTA" +
					"(IDPRODUCTO,IDCLIENTE,VNTCODIGO,VNTCANTIDAD,VNTCOSTOVENTA," +
					"VNTFECHA,VNTESTADO) " +
					"VALUES (@1,@2,@3,@4,@5,@6,'A') ";
				con.Open();
				SqlCommand insertar = new SqlCommand(sql, con);
				insertar.Parameters.AddWithValue("@1", idproducto);
				insertar.Parameters.AddWithValue("@2", idcliente);
				insertar.Parameters.AddWithValue("@3", vntcodigo);
				insertar.Parameters.AddWithValue("@4", vntcantidad);
				insertar.Parameters.AddWithValue("@5", vntcostoventa);
				insertar.Parameters.AddWithValue("@6", vntfecha);
				insertar.CommandType = CommandType.Text;
				insertar.ExecuteReader();
			}
			catch (Exception)
			{
				throw;
			}

			con.Close();



		}




		[WebMethod]
		public void eliminarventa(int id)
		{
			try
			{
				string query = "UPDATE TBLVENTA SET VNTESTADO='I' WHERE IDVENTA=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

		}

		[WebMethod]
		public DataSet verVenta(int id)
		{

			con.Open();
			string query = "SELECT * FROM VIEWVENTA WHERE IDVENTA='" + id + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}


		#endregion

		#region CompraVenta
		[WebMethod]
		public void AumentarProducto(int cant , int id) {
			try
			{
				string query = "UPDATE TBLPRODUCTO SET PRODCANTIDAD= PRODCANTIDAD + '" + cant + "' WHERE IDPRODUCTO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();

			}
			catch (Exception)
			{
				throw;
			}


		}

		[WebMethod]
		public void RestarProducto(int cant, int id)
		{
			try
			{
				string query = "UPDATE TBLPRODUCTO SET PRODCANTIDAD= PRODCANTIDAD - '" + cant + "' WHERE IDPRODUCTO=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();

			}
			catch (Exception)
			{
				throw;
			}


		}
		#endregion


		#region compra


		[WebMethod]
		public void registrarCompra(string idproveedor, string idproducto, string comcodigo, int comcantidad, float comcostocompra, string comfecha)
		{
			try
			{
				string sql = "INSERT INTO TBLCOMPRA" +
					"(IDPROVEEDOR,IDPRODUCTO,COMCODIGO,COMCANTIDAD,COMCOSTOCOMPRA," +
					"COMFECHA,COMESTADO) " +
					"VALUES (@1,@2,@3,@4,@5,@6,'A') ";
				con.Open();
				SqlCommand insertar = new SqlCommand(sql, con);
				insertar.Parameters.AddWithValue("@1", idproveedor);
				insertar.Parameters.AddWithValue("@2", idproducto);
				insertar.Parameters.AddWithValue("@3", comcodigo);
				insertar.Parameters.AddWithValue("@4", comcantidad);
				insertar.Parameters.AddWithValue("@5", comcostocompra);
				insertar.Parameters.AddWithValue("@6", comfecha);
				insertar.CommandType = CommandType.Text;
				insertar.ExecuteReader();
			}
			catch (Exception)
			{
				throw;
			}

			con.Close();



		}

		[WebMethod]
		public DataSet verCompra(int id)
		{

			con.Open();
			string query = "SELECT * FROM VIEWVERCOMPRA WHERE IDCOMPRA='" + id + "'";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}

		[WebMethod]
		public void eliminarCompra(int id)
		{
			
			try
			{
				string query = "UPDATE TBLCOMPRA SET COMESTADO='I' WHERE IDCOMPRA=@id";
				con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception)
			{

				throw;
			}

		}

        #endregion


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
			string query = "SELECT * FROM TBLCLIENTE WHERE CLICEDULA='" + ci + "' ";



			SqlDataAdapter sda = new SqlDataAdapter(query, con);
			DataSet ds = new DataSet();

			sda.Fill(ds);

			con.Close();

			return ds;
		}
		#endregion

	}



}
