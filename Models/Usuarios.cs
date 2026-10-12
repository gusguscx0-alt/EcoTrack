using Npgsql;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Integradora.Models
{
    public class Usuarios:Conexion
    {
       

        public int id_usuarios { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno {  get; set; }   
        public string apellido_materno {  get; set; }   
        public string correo { get; set; }
        public string password_hash { get; set; }
        public string rol { get; set; }
        public int puntos_totales { get; set; }
        public DateOnly fecha_registro { get; set; }

        public Usuarios() { }

        public Usuarios(int id, string nom, string apPat, string apMat, string corr, string pass, string roles, int puntosTotales, DateOnly fechaRegistro)
        {
            id_usuarios = id;
            nombre = nom;
            apellido_paterno = apPat;
            apellido_materno = apMat;
            correo = corr;
            password_hash = pass;
            rol = roles;
            puntos_totales = puntosTotales;
            fecha_registro = fechaRegistro;
        }


        //Metodo para consultar todos usuarios en la base de datos 
        public List<Usuarios> GetAllUsuarios()
        {
            const string sql = "Select * FROM usuarios;";
            DataTable tabla = GetQuery(sql);

            List<Usuarios> lstUsuarios = new List<Usuarios>();
            if(tabla.Rows.Count > 1) return lstUsuarios;

            foreach(DataRow row in tabla.Rows)
            {
                lstUsuarios.Add(new Usuarios(Convert.ToInt32(row["id_usuario"]),
                                                             row["nombre"].ToString(),
                                                             row["apellido_paterno"].ToString(),
                                                             row["apellido_materno"].ToString(),
                                                             row["correo"].ToString(),
                                                             row["password_hash"].ToString(),
                                                             row["rol"].ToString(),
                                                             Convert.ToInt32(row["puntos_totales"]),
                                                             DateOnly.FromDateTime(Convert.ToDateTime(row["fecha_registro"]))
                                                             ));
            }
            return lstUsuarios; 
        }

        //Refistrar un usuario

        public int AddUsuario(Usuarios usuario){
            const string sql = @"INSERT INTO usuarios (nombre,apellido_paterno,apellido_materno,correo,password_hash,rol,puntos_totales)
                               VALUES(:nom, :apPat, :apMat, :corr, :pass, :rol, :puntos_totales);";
            List<NpgsqlParameter> Lista = new List<NpgsqlParameter>
            {
                new NpgsqlParameter(":nom", usuario.nombre),
                new NpgsqlParameter(":apPat", usuario.apellido_paterno),
                new NpgsqlParameter(":apMat", usuario.apellido_materno),
                new NpgsqlParameter(":corr", usuario.correo),
                new NpgsqlParameter(":pass", usuario.password_hash),
                new NpgsqlParameter(":rol", string.IsNullOrEmpty(usuario.rol)),
                new NpgsqlParameter(":puntos_totales", usuario.puntos_totales),

            };
            return ExecuteQuery(sql, Lista);
        }

        //Inicio de sesion

        public Usuarios IniciarSesion(string correo, string pass){
            const string sql = "SELECT * FROM usuarios WHERE correo = :correo and password_hash = :pass";
            List<NpgsqlParameter> param = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@correo",correo ?? ""),
                new NpgsqlParameter("@pass",pass ?? "")
            };

            DataTable tabla = GetQuery(sql, param);
            if (tabla.Rows.Count < 1) return null;

            DataRow row = tabla.Rows[0];
            return new Usuarios(
                Convert.ToInt32(row["id_usuario"]),
                row["nombre"].ToString(),
                row["apellido_paterno"].ToString(),
                row["apellido_materno"].ToString(),
                row["correo"].ToString(),
                "",
                row["rol"].ToString(),
                Convert.ToInt32(row["puntos_totales"]),
                DateOnly.FromDateTime(Convert.ToDateTime(row["fecha_registro"]))
                );
        }

        //Buscar por id
        public Usuarios GetUserById(int id){
            const string sql = "SELECT * FROM usuarios WHERE id_usuario = :id;";
            List<NpgsqlParameter> lstParameter = new List<NpgsqlParameter>();
            lstParameter.Add(new NpgsqlParameter("id",id));

            DataTable table = GetQuery(sql, lstParameter);
            if (table.Rows.Count < 1) return null;
            
            DataRow row = table.Rows[0];
            return new Usuarios(
             Convert.ToInt32(row["id_usuario"]),
             row["nombre"].ToString(),
             row["apellido_paterno"].ToString(),
             row["apellido_materno"].ToString(),
             row["correo"].ToString(),
             "",
             row["rol"].ToString(),
             Convert.ToInt32(row["puntos_totales"]),
             DateOnly.FromDateTime(Convert.ToDateTime(row["fecha_registro"]))
             );
        }
    }
}




