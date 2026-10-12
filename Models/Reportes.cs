using System.Data;

namespace Integradora.Models
{
    public class Reportes: Conexion{
        public int id_reporte { get; set; }  
        public int id_usuario { get; set; }
        public int id_categoria { get; set; }
        public int id_estado { get; set; } 
        public string titulo { get; set; }
        public string descripcion { get; set; }       
        public double latitud {  get; set; }    
        public double longitud { get; set; }
        public DateOnly fecha_creacion { get; set; }    
        public DateOnly fecha_actualizacion { get; set; }   

        public Reportes() { }

        public Reportes(int id_reporte, int id_usuario, int id_categoria, int id_estado, string titulo, string descripcion, double latitud, double longitud, DateOnly fecha_creacion, DateOnly fecha_actualizacion)
        {
            this.id_reporte = id_reporte;
            this.id_usuario = id_usuario;
            this.id_categoria = id_categoria;
            this.id_estado = id_estado;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.latitud = latitud;
            this.longitud = longitud;
            this.fecha_creacion = fecha_creacion;
            this.fecha_actualizacion = fecha_actualizacion;
        }

        public List<Reportes> GetAllReportes()
        {
            const string sql = "Select * from usuario; ";
            List<Reportes> lstReportes = new List<Reportes>();
            DataTable tabla = GetQuery(sql);
            if (tabla.Rows.Count < 1) return lstReportes;
            foreach (DataRow row in tabla.Rows)
            {
                lstReportes.Add(new Reportes((int)row["id_reporte"],
                                             (int)row["id_usuario"],
                                             (int)row["id_categoria"],
                                             (int)row["id_estado"],
                                             (string)row["titulo"],
                                             (string)row["descripcion"],
                                             (double)row["latitud"],
                                             (double)row["lonitud"],
                                             (DateOnly)row["fecha_creacion"],
                                             (DateOnly)row["fecha_actualizacion"]));

            }
            return lstReportes;
        }

        private DataTable GetQuery(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
