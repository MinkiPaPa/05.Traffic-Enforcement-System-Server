using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Collections;

namespace TRA.iTOPS.Contracts
{
    [DataContract]
    public class TTOPSUserDef
    {
        [DataMember]
        public string NAME { get; set; }
        [DataMember]
        public string USER_ID { get; set; }
        [DataMember]
        public string USER_PWD { get; set; }
        [DataMember]
        public string USER_NAME { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public int LOGIN_FALSE_COUNT { get; set; }
        [DataMember]
        public string US_YN { get; set; }
        [DataMember]
        public string CTRL_AREA { get; set; }
        [DataMember]
        public string PRIVILEGE { get; set; }
    }
}
