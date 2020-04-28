using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class GeographyEntity
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }

    //public class GeographyEntityDetails
    //{
    //    public OrderEntity oEntity { get; set; }
    //    public List<OrderEntity> lstOrderEntity { get; set; }
    //    public int totalcount { get; set; }
    //    public OrderEntityDetails()
    //    {
    //        oEntity = new OrderEntity();
    //        lstOrderEntity = new List<OrderEntity>();
    //    }
    //}

}
