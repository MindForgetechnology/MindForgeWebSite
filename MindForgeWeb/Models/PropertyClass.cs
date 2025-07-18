using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace MindForgeWeb.Models
{
    public class PropertyClass
    {

        public static List<SelectListItem> BindDDL(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        } 

        public static List<SelectListItem> BindDDLService(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }

        public static List<SelectListItem> BindDDLpft(DataTable dt)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = Convert.ToString(item[1]),
                        Value = Convert.ToString(item[0])
                    });
                }
            }
            else
            {
                lst.Add(new SelectListItem() { Text = "--none--", Value = "" });
            }
            return lst;
        }

    }
}
