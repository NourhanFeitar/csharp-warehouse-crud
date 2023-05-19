namespace Warehouse
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Measurments
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public double Length { get; set; }

        public double? Width { get; set; }


        public double? Weight { get; set; }

        public virtual Product Product { get; set; }
    }
}
