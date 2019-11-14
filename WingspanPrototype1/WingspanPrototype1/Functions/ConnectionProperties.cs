using System;
using System.Collections.Generic;
using System.Text;

namespace WingspanPrototype1.Functions
{
    class ConnectionProperties
    {
        protected string connectionString = "mongodb://localhost:27017";

        public string GetConnectionProperties()
        {
            return connectionString;
        }
    }
}
