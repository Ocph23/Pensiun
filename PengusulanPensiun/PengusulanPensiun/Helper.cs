using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengusulanPensiun.ViewModels;
using DAL;
using PengusulanPensiun.Models;

namespace PengusulanPensiun
{
    public class Helper
    {
        internal static string ValidateModel<T>(T model,string colName, string Error)
        {
            Error = null;
            var props = typeof(T).GetProperties().Where(O => O.Name == colName).FirstOrDefault();
            if(props!=null)
            {
              var result=  props.GetValue(model);
                if (result != null)
                    switch (props.PropertyType.Name)
                    {
                        case "String":
                            Error= string.IsNullOrEmpty(result.ToString()) ? "Not Valid" : null;
                            break;
                        default:
                            Error= "Not Valid";
                            break;
                    }
              
                else
                Error= "Not Valid";
            }

            return Error;

        }

        internal static periode GetLastPeriode()
        {
            using (var db = new OcphDbContext())
            {
               return db.Periodes.GetLastItem();
            }
        }
    }
}
