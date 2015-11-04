using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models.Interfaces
{
    public interface IOrderFileRepository
    {
        List<Order> LoadOrders(string Orderdate);

    }
}
