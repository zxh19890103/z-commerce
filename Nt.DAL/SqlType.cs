using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.DAL
{
    public enum SqlType : int
    {
        TinyInt,
        SmallInt,
        Int,
        BigInt,
        Money,
        SmallMoney,
        Decimal,
        Float,
        Numeric,
        Real,

        Char,
        VarChar,
        Text,
        NChar,
        NVarchar,
        NText,
        Xml,

        UniqueIdentifier,

        Time,
        TimeStamp,
        SmallDateTime,
        Date,
        DateTime,
        DateTimeOffset,

        Sql_Variant,

        Binary,
        VarBinary,
        Bit,
        HierarchyId,

        Geography,
        Geometry,
        Image
    }
}
