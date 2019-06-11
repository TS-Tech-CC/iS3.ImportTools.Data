using System;
using System.Data;
using iS3.ImportTools.Core.Interface;
using iS3.ImportTools.Core.Models;

namespace iS3.ImportTools.DataVerification
{
    public class DataVerification_A : IDataVerification
    {
        public DataCarrier Verification(DataCarrier aimDC)
        {
            foreach (DataRow itemDR in aimDC.DataContainer.Rows)
            {
                if (itemDR["BoreholeID"] == DBNull.Value)
                {
                    itemDR["BoreholeID"] = 0;
                }

                //DateTime dt = DateTime.Now;
                //if (!DateTime.TryParse(itemDR["BoreholeTime"].ToString(), out dt))
                //{
                //    itemDR["BoreholeTime"] = DBNull.Value;
                //}
            }

            return aimDC;
        }
    }
}
