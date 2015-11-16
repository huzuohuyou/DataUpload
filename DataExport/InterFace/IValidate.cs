using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataExport
{
    public interface IValidate
    {
        string ValidateData(string p_strTableName);

        void ValidateAll(DataTable p_dt);
    }
}
