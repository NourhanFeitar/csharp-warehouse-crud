namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sup_Req_Products
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

        [Column(TypeName = "date")]
        public DateTime? Exp_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Manu_Date { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier_Request Supplier_Request { get; set; }
    }
}
