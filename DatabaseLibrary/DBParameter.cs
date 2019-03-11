using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;

namespace DatabaseLibrary
{
    public class DBParameter
    {
        public DBParameter(string Name, object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType Type {
            get
            {
                if (Value == null)
                {
                    return SqlDbType.VarChar;
                }
                else if (Value is string)
                {
                    return SqlDbType.VarChar;
                }
                else if (Value is decimal)
                {
                    return SqlDbType.Decimal;
                }
                else if (Value is int)
                {
                    return SqlDbType.Int;
                }
                else if (Value is DateTime)
                {
                    return SqlDbType.DateTime;
                }
                else if (Value is Byte[])
                {
                    return SqlDbType.VarBinary;
                }
                else if (Value is Guid)
                {
                    return SqlDbType.UniqueIdentifier;
                }

                return SqlDbType.VarChar;
            }
        }
    }
}
