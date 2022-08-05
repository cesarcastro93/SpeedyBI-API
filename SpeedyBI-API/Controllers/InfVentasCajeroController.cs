using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Cors;

namespace SpeedyBI_API.Controllers
{

    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [RoutePrefix("api/InformesVentas")]
    public class InfVentasCajeroController : ApiController
    {
        SqlConnection conexionConnection = new SqlConnection("data source =VIRTUALSERVER01 ; initial catalog = SpeedyAnalitica; user id = sa; password = Sql4dmin;");
        

        [Route("VentasCajero")]
        [HttpGet]
        public IHttpActionResult GetVentasCajero(string FechaInicial, string FechaFinal)
        {
            using (conexionConnection)
            {
                if (conexionConnection != null)
                {
                    try
                    {
                        string CommandQuery = "Select * from ventasxCajero where Fecha between  @FechaInicial  and @FechaFinal order by fecha asc";
                        conexionConnection.Open();
                        SqlCommand Command = new SqlCommand();
                        Command.Connection = conexionConnection;
                        Command.CommandText = CommandQuery;
                        Command.CommandType = CommandType.Text;
                        Command.Parameters.Add(new SqlParameter("@FechaInicial",FechaInicial));
                        Command.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                        Command.ExecuteNonQuery();
                        SqlDataAdapter adapter = new SqlDataAdapter(Command);
                        DataSet dataSetVentasCajero = new DataSet();
                        adapter.Fill(dataSetVentasCajero);
                        conexionConnection.Close();
                        return Json(new { status = 200, isSuccess = true, message = "Conectado a base de datos exitosamente", dataSetVentasCajero});
                    }
                    catch (Exception e)
                    {
                        return Ok(e.Message);
                    }
                }
                else
                {
                    return Ok(new { status = 401, isSuccess = false, message = "error de conexion a base de datos", });
                } 
            }
        }


        [Route("VentasCajeroNoAfil")]
        [HttpGet]
        public IHttpActionResult GetVentasCajeroNoAfil(string FechaInicial, string FechaFinal)
        {
            using (conexionConnection)
            {
                if (conexionConnection != null)
                {
                    try
                    {
                        string CommandQuery = "Select * from ventasxCajeroNoAfiliados where Fecha between  @FechaInicial  and @FechaFinal order by fecha asc";
                        conexionConnection.Open();
                        SqlCommand Command = new SqlCommand();
                        Command.Connection = conexionConnection;
                        Command.CommandText = CommandQuery;
                        Command.CommandType = CommandType.Text;
                        Command.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                        Command.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                        Command.ExecuteNonQuery();
                        SqlDataAdapter adapter = new SqlDataAdapter(Command);
                        DataSet dataSetVentasCajeroNoAfil = new DataSet();
                        adapter.Fill(dataSetVentasCajeroNoAfil);
                        conexionConnection.Close();
                        return Json(new { status = 200, isSuccess = true, message = "Conectado a base de datos exitosamente", dataSetVentasCajeroNoAfil });
                    }
                    catch (Exception e)
                    {
                        return Ok(e.Message);
                    }
                }
                else
                {
                    return Ok(new { status = 401, isSuccess = false, message = "error de conexion a base de datos", });
                }
            }
        }
        [Route("MultiInforme")]
        [HttpGet]
        public IHttpActionResult GetVentasConvenios(string FechaInicial, string FechaFinal) 
        {
            using (conexionConnection) 
            {
                if (conexionConnection != null)
                {
                    try
                    {
                        string CommandQuery = "MultiInformeBI";
                        conexionConnection.Open();
                        SqlCommand Command = new SqlCommand();
                        Command.Connection = conexionConnection;
                        Command.CommandText = CommandQuery;
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@FechaInicial", FechaInicial));
                        Command.Parameters.Add(new SqlParameter("@FechaFinal", FechaFinal));
                        Command.ExecuteNonQuery();
                        SqlDataAdapter adapter = new SqlDataAdapter(Command);
                        DataSet dataSetMultiInforme = new DataSet();
                        adapter.Fill(dataSetMultiInforme);
                        conexionConnection.Close();
                        return Json(new { status = 200, isSuccess = true, message = "Conectado a base de datos exitosamente", dataSetMultiInforme });
                    }
                    catch (Exception e)
                    {
                        return Ok(e.Message);
                    }
                }
                else
                {
                    return Ok(new { status = 401, isSuccess = false, message = "error de conexion a base de datos", });
                }
            }
               
        }
    }
}

