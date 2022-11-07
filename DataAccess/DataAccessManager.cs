using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace DataAccess
{
    public class DataAccessManager
    {
        public DataAccessContext DataAccessContext { get; private set; }

        public DataAccessManager()
        {
            this.DataAccessContext = new DataAccessContext();
        }
    }
}
