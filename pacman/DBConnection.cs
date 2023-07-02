using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{
    public static class  DBConnection
    {
        public static string stringconn()
        {
            string stringconnection = " server = localhost; database = cruddb; uid = root; pwd='";
            return stringconnection;
        }
    }
}
