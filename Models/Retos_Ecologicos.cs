namespace Integradora.Models
{
    public class Retos_Ecologicos: Conexion {

        public int id_reto {  get; set; }   
        public int id_reporte_origen { get; set; }  
        public string titulo { get; set; }  
        public string descripion { get; set; }  
        public DateOnly fecha_inicio { get; set; }  
        public DateOnly fecha_fin {  get; set; }    
        public int puntos_recompensa { get; set; }  
        public string estado { get; set; }  
        
        public Retos_Ecologicos() { }

        public Retos_Ecologicos(int id_reto, int id_reporte_origen, string titulo, string descripion, DateOnly fecha_inicio, DateOnly fecha_fin, int puntos_recompensa, string estado)
        {
            this.id_reto = id_reto;
            this.id_reporte_origen = id_reporte_origen;
            this.titulo = titulo;
            this.descripion = descripion;
            this.fecha_inicio = fecha_inicio;
            this.fecha_fin = fecha_fin;
            this.puntos_recompensa = puntos_recompensa;
            this.estado = estado;
        }


    }
}
