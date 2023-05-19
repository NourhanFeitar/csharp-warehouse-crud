namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_Req_Products
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Request_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        public int? Quantity { get; set; }

        public int? Supplier_ID { get; set; }

        public int? Client_ID { get; set; }

        public virtual Client_Request Client_Request { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
