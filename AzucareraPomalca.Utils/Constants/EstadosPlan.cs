namespace AzucareraPomalca.Utils.Constants
{
    /// <summary>
    /// Catálogo "Estado del Plan de Capacitación" en tabla_comun (id_tabla = 6).
    /// Las filas se siembran en SqlScripts/20260522_plan_capacitacion.sql.
    /// El id surrogate (id_tabla_comun) NO se hardcodea: se resuelve por (ID_TABLA, FILA_*).
    /// </summary>
    public class EstadosPlan
    {
        public const int ID_TABLA = 6;

        public const int FILA_BORRADOR = 1;
        public const int FILA_APROBADO = 2;
        public const int FILA_EN_EJECUCION = 3;
        public const int FILA_CERRADO = 4;

        public const string BORRADOR = "Borrador";
        public const string APROBADO = "Aprobado";
        public const string EN_EJECUCION = "En ejecución";
        public const string CERRADO = "Cerrado";
    }
}
