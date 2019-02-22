using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class Test
    {

        DataTable dt = DatabaseUtilities.ExecuteQuery("Select * from table where 1=1 ");
    }

    public class TestModel
    {
        public int ID { get; set; }
        public int Name { get; set; }
    }
}
