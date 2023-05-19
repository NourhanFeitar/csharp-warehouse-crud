namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouse_Products
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Warehouse_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Products_ID { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Warehouse_Data Warehouse_Data { get; set; }
    }
}
