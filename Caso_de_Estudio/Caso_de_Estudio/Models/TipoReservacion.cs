//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Caso_de_Estudio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoReservacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoReservacion()
        {
            this.Reservacion = new HashSet<Reservacion>();
        }
    
        public int id { get; set; }
        public string nombreTipoR { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> estado { get; set; }
    
        public virtual Estado Estado1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservacion> Reservacion { get; set; }
    }
}
