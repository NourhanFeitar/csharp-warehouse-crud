namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client_Request()
        {
            Client_Req_Products = new HashSet<Client_Req_Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Request_ID { get; set; }

        public int? Warehouse_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? Client_ID { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_Req_Products> Client_Req_Products { get; set; }

        public virtual Warehouse_Data Warehouse_Data { get; set; }
    }
}
