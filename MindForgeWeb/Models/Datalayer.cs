


using Microsoft.Data.SqlClient;
using MindForgeWeb.Models;
using System.Data;

namespace MindForgeWeb.Data
{
    public class Datalayer
    {
        private readonly dbHelper _dbHelper;

        public Datalayer(dbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public DataTable UpdateUserDataById(UserProfileDto user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", user.Action),
                new SqlParameter("@UserId", user.UserId),
                new SqlParameter("@Mobile", user.MobileNu),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@UserName", user.UserName)
            };
            return _dbHelper.ExecProcDataTable("ManageAdminLogin", parameters);
        }
        public DataTable GetUserByUsername(string mobile)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Mobile", mobile),
                new SqlParameter("@Action", "4")
              
                //new SqlParameter("@OTP", otp)
            };
            return _dbHelper.ExecProcDataTable("ManageAdminLogin", parameters);
        }
        public DataTable GetUserById(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {new SqlParameter("@UserId", userId),
             new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManageAdminLogin", parameters);
        }
        public DataTable GetUserByUpdateUserLogin(int userid)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userid),
                new SqlParameter("@Action", "5"),
                new SqlParameter("@IsLogin", 1),
                
            };
            return _dbHelper.ExecProcDataTable("ManageAdminLogin", parameters);
        }
        public DataTable GetUserByUpdateUserLogout(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Action", "6")

            };
            return _dbHelper.ExecProcDataTable("ManageAdminLogin", parameters);
        }
        
        public DataTable ResetPassword(string oldPassword, string newPassword, string employeeCode)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@oldPassword", oldPassword),
                new SqlParameter("@newPassword", newPassword),
                new SqlParameter("@employeeCode", employeeCode)
            };
            return _dbHelper.ExecProcDataTable("USP_ResetPassword", parameters);
        } 

        public bool DeleteService(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@ServiceId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageService", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int InsertUpdateService(ServiceDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@ServiceId", model.ServiceId),
                new SqlParameter("@ServiceName", model.ServiceName)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManageService", parameters);
            return dt.Rows.Count;
        }

        public bool GetNameserviceData(string name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "6"),
            new SqlParameter("@ServiceName", name)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageService", parameters);

                if (result != null && result.Rows.Count > 0)
                {
                    var status = result.Rows[0]["Status"].ToString();
                    return status == "Success";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public DataTable GetServiceById(int id)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@ServiceId", id)
            };

            return _dbHelper.ExecProcDataTable("ManageService", parameters);
        }

        public DataTable GetServiceList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManageService", parameters);
        }
        public DataTable GetServiceDropdown()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "4")
            };
            return _dbHelper.ExecProcDataTable("ManageService", parameters);
        }

        public bool GetNameserviceDetailsData(int ServiceId,string name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "6"),
            new SqlParameter("@ServiceId", name),
            new SqlParameter("@ServiceTittle", ServiceId)

                };

                DataTable result = _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);

                if (result != null && result.Rows.Count > 0)
                {
                    var status = result.Rows[0]["Status"].ToString();
                    return status == "Success";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public int InsertUpdateServiceDetails(ServiceDetailsDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@ServicedId", model.ServicedId),
                new SqlParameter("@ServiceId", model.ServiceId),
                new SqlParameter("@ServiceTittle", model.ServiceTittle),
                new SqlParameter("@ServiceDescription", model.ServiceDescription), 
                new SqlParameter("@FilePath", model.Filename)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);
            return dt.Rows.Count;
        }
        public bool DeleteServiceDetails(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@ServicedId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetServiceDetailsList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);
        }


        public DataTable GetServicedetailsById(int id)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@ServicedId", id)
            };

            return _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);
        }

        public bool GetNameBlogDetailsData(string name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "6"),
            new SqlParameter("@BlogTittle", name)

                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);

                if (result != null && result.Rows.Count > 0)
                {
                    var status = result.Rows[0]["Status"].ToString();
                    return status == "Success";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public int InsertUpdateBlogDetails(BlogDetailsDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@BlogId", model.BlogId),
                new SqlParameter("@BlogTittle", model.BlogTittle),
                new SqlParameter("@BlogDescription", model.BlogDescription),
                new SqlParameter("@PostDate", model.PostDate),
                new SqlParameter("@FilePath", model.Filename)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);
            return dt.Rows.Count;
        }
        public bool DeleteBlogDetails(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@BlogId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetBlogDetailsList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);
        }
        public DataTable GetBlogdetailsById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@BlogId", id)
            };
            return _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);
        }
        public DataTable GetCountAllPftDetailsData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "5")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }
        public DataTable GetCountMaspftData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "6")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }
        public DataTable GetCountServiceData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "1")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }
        public DataTable GetCountCommentData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "4")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }
        public DataTable GetCountServiceDetailsData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "2")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }
        public DataTable GetCountallBlogData()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionType", "3")
            };
            return _dbHelper.ExecProcDataTable("ManageDashboardSP", parameters);
        }

        public bool DeleteComments(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "8"),
            new SqlParameter("@CommentId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageBlogComment", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetBlogCommentDetailsList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "4")
            };
            return _dbHelper.ExecProcDataTable("ManageBlogComment", parameters);
        }

        public bool BlogCommentHideshow(int CommentId, bool Checked)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "9"),
            new SqlParameter("@CommentId", CommentId),
            new SqlParameter("@IsDeleted", Checked)

                };
                DataTable result = _dbHelper.ExecProcDataTable("ManageBlogComment", parameters);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeletePftMaster(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@PftId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int InsertUpdatePftMaster(PftMasterDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@PftId", model.PftId),
                new SqlParameter("@PftName", model.PftName)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);
            return dt.Rows.Count;
        }

        public bool GetNamePftMasterData(string name)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "6"),
            new SqlParameter("@PftName", name)
                };

                DataTable result = _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);

                if (result != null && result.Rows.Count > 0)
                {
                    var status = result.Rows[0]["Status"].ToString();
                    return status == "Success";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public DataTable GetPftMasterById(int id)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@PftId", id)
            };

            return _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);
        }

        public DataTable GetPftMasterList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);
        }
        public DataTable GetPftMasterDropdown()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "4")
            };
            return _dbHelper.ExecProcDataTable("sp_ManagePftMaster", parameters);
        }


        public int InsertUpdatePftDetails(PftDetailsDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@PftDId", model.PftDId),
                new SqlParameter("@PftId", model.PftId), 
                new SqlParameter("@FilePath", model.Filename)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManagePftDetails", parameters);
            return dt.Rows.Count;
        }
        public bool DeletePftDetails(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@ServicedId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManagePftDetails", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetPftDetailsList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManagePftDetails", parameters);
        }


        public DataTable GetPftdetailsById(int id)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@ServicedId", id)
            };

            return _dbHelper.ExecProcDataTable("ManagePftDetails", parameters);
        }
        public int InsertUpdateComments(CommentBlogsDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@CommentId", model.CommentId),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@Message", model.Message),
                new SqlParameter("@BlogId", model.BlogId)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManageBlogComment", parameters);
            return dt.Rows.Count;
        }
        public DataTable GetBlogDetailsListbyid(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "8"),
         new SqlParameter("@BlogId", id)
            };
            return _dbHelper.ExecProcDataTable("ManageBlogsDetails", parameters);
        }
        public DataTable GetBlogCommentDetailsListbyid(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
new SqlParameter("@Action", "10"),
 new SqlParameter("@BlogId", id)
            };
            return _dbHelper.ExecProcDataTable("ManageBlogComment", parameters);
        }
        public DataTable GetServiceDetailsListbyid(int id)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "8"),
         new SqlParameter("@ServicedId", id)
            };

            return _dbHelper.ExecProcDataTable("ManagespServiceDetails", parameters);
        }
        public bool GetNameOurTeamDetailsData(string name,string designation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "6"),
            new SqlParameter("@Name", name),
            new SqlParameter("@Designation", designation)

                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageOurTeamDetails", parameters);

                if (result != null && result.Rows.Count > 0)
                {
                    var status = result.Rows[0]["Status"].ToString();
                    return status == "Success";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public int InsertUpdateOurTeamDetails(TeamDetailsDto model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", model.Action),
                new SqlParameter("@TeamsId", model.TeamsId),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Designation", model.Designation),
                new SqlParameter("@FilePath", model.Filename)
            };
            DataTable dt = _dbHelper.ExecProcDataTable("ManageOurTeamDetails", parameters);
            return dt.Rows.Count;
        }
        public bool DeleteOurTeamDetails(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Action", "3"),
            new SqlParameter("@TeamsId", id)
                };

                DataTable result = _dbHelper.ExecProcDataTable("ManageOurTeamDetails", parameters);
                return result != null && result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0]["RowsAffected"]) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetOurTeamDetailsList()
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "7")
            };
            return _dbHelper.ExecProcDataTable("ManageOurTeamDetails", parameters);
        }
        public DataTable GetOurTeamdetailsById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "5"),
         new SqlParameter("@TeamsId", id)
            };
            return _dbHelper.ExecProcDataTable("ManageOurTeamDetails", parameters);
        }
    }
}
