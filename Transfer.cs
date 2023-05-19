namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transfer")]
    public partial class Transfer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transfer()
        {
            Transfer_Products = new HashSet<Transfer_Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Transfer_ID { get; set; }

        public int? From_WH_ID { get; set; }

        public int? To_WH_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfer_Products> Transfer_Products { get; set; }

        public virtual Warehouse_Data Warehouse_Data { get; set; }

        public virtual Warehouse_Data Warehouse_Data1 { get; set; }
    }
}
