using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CreditSuisseTrades
{
    public enum ENUMCATEGORIES
    {
        [EnumMember]
        DEFAULTED,

        [EnumMember]
        HIGHRISK,

        [EnumMember]
        MEDIUMRISK
        
    }
}
