using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.OrderDetail
{
    public class OrderDetailModel
    {
        public Guid OrderId { get; set; }

        public Guid SaplingId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
    }
}
