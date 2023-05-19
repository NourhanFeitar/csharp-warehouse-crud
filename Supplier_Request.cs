namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier_Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier_Request()
        {
            Sup_Req_Products = new HashSet<Sup_Req_Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Request_ID { get; set; }

        public int? Warehouse_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? Supplier_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sup_Req_Products> Sup_Req_Products { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Warehouse_Data Warehouse_Data { get; set; }
    }
}
