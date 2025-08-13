using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers.Member
{
    public class ResponceData
    {
        public string Remain_Id { get; set; }
        public string UrlData { get; set; }
    }
    public class MemberController : Controller
    {
        public DataTable Dt = null;
        private static Random random = new Random();

        #region Get System IP

        public string SystemIP()
        {
            string Ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(Ipaddress))
            {
                Ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            return Ipaddress;
        }

        #endregion

        #region My Team
        [CookiesExpireFilter]
        public ActionResult MyTeam()
        {
            IHomeManager homeManager = new HomeManager();
            MemberModel Model = new MemberModel();
            Model.List_Registration_Businesses_Obj = homeManager.GetMyTeam(CookiesStateManager.Cookies_Logged_Token_Id);
            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult MyGenerology()
        {
            IHomeManager homeManager = new HomeManager();
            MemberModel Model = new MemberModel();

            return View();
        }
        #endregion

        #region Tree
        public ActionResult GetTreeNodeRec(string Token_Id, string Position)
        {
            //DataRow[] dataRows = Dt.Select("Parent_ID= '" + Token_Id + "'");
            DataRow[] dataRows = Dt.Select("Parent_Id = '" + Token_Id + "' and Position =' " + @Position + "'");
            string msg = "";
            if (dataRows.Count() == 0)
            {
                msg += GetBlankTreeNode(Token_Id, Position);
            }
            else
            {
                msg += "<ul>";
            }
            return View();
        }
        [CookiesExpireFilter]
        public string GetTreeNode(DataRow data, DataTable dt)
        {
            string msg = "";
            var style = "";
            var img = "";
            var Gender = data["Gender"].ToString();
            if (Gender == "Male")
            {
                style = "style = 'color: green;'";
                img = "/Content/assets/images/Male_Icon.png";
            }
            else
            {
                style = "style = 'color: red;'";
                img = "/Content/assets/images/Female_Icon.png";
            }
            msg += $"<li><a class='over' onmouseover=MouseOverEvent('{data["Sponsor_Id"]}','{data["Sponsor_Name"]}','{data["Status_Name"]}','{data["TeamLevel"]}','{data["IsGoleComplete"]}',event) " +
             $"onmouseout=MouseOutEvent() onclick=GetTreeData('{data["Token_ID"]}') >" +
             $"<img src='{img}' alt=''> <p {style} > {data["Full_Name"]} </p>{data["Token_ID"]} ({data["Position"]})";

            msg += "</a>";
            var dr3 = dt.Select("Parent_ID= '" + data["Token_ID"] + "'");
            if (dr3.Length > 0)
                msg += "<ul></ul>";
            msg += "</li>";
            return msg;
        }
        [CookiesExpireFilter]
        public string GetTreeNode2(DataRow data, DataTable dt)
        {
            string msg = "";
            var style = "";
            var img = "";
            var Gender = data["Gender"].ToString();
            if (Gender == "Male")
            {
                style = "style = 'color: green;'";
                img = "/Content/assets/images/Male_Icon.png";
            }
            else
            {
                style = "style = 'color: red;'";
                img = "/Content/assets/images/Female_Icon.png";
            }
            msg += $"<li><a class='over' onmouseover=MouseOverEvent('{data["Sponsor_Id"]}','{data["Sponsor_Name"]}','{data["Status_Name"]}','{data["TeamLevel"]}','{data["IsGoleComplete"]}',event) " +
                $" onmouseout=MouseOutEvent() onclick=GetTreeData('{data["Token_ID"]}') ><img src='{img}' alt=''> <p {style}> {data["Full_Name"]} </p>{data["Token_ID"]}({data["Position"]})";
            msg += "</a>";
            return msg;
        }
        [CookiesExpireFilter]
        public string GetBlankTreeNode(string position, string Parent_Id)
        {
            string msg = "";
            string style = "style = 'color: Blue;'";
            string img = "/Content/assets/images/Blank_Icon.png";
            msg += "<li><a class='over'" +
                " onclick=RedirectToRegistration('" + CookiesStateManager.Cookies_Logged_Token_Id + "," + position + "," + Parent_Id + "') >" +
                "<img src='" + img + "' alt=''> <p " + style + "> " + "XXXXXXXX" + " </p>" + "XXXXXXX(" + position + ")";
            msg += "</a>";
            msg += "</li>";
            return msg;
        }

        [HttpPost]
        [CookiesExpireFilter]
        public ActionResult UserTreeView(string Token_Id)
        {
            try
            {
                if (Token_Id == null || Token_Id == "")
                {
                    Token_Id = CookiesStateManager.Cookies_Logged_Token_Id;
                }
                DataRow[] dr;
                DataRow[] dr1;
                DataRow[] dr2;
                string msg = "";
                string style = "";
                string Position = "";

                #region Top Parent
                IHomeManager homeManager = new HomeManager();
                DataTable dt = homeManager.GetRegistrationDataTable(Token_Id, CookiesStateManager.Cookies_Logged_Token_Id);
                Dt = dt;
                DataRow dataRow = dt.Select("Token_Id= '" + Token_Id + "'") != null ? dt.Select("Token_Id= '" + Token_Id + "'")[0] : null;
                if (dataRow != null)
                {
                    string img = "";
                    string Gender = "";
                    if (dataRow["Gender"].ToString().Equals("Male"))
                    {
                        style = "style = 'color: green;'";
                        img = "/Content/assets/images/Male_Icon.png";
                    }
                    else
                    {
                        style = "style = 'color: red;'";
                        img = "/Content/assets/images/Female_Icon.png";
                    }
                    msg += "<ul style='width: 1400px;'>";
                    msg += $"<li><a class='over' onmouseover=MouseOverEvent('{dataRow["Sponsor_Id"].ToString()}','{dataRow["Sponsor_Name"].ToString()}'," +
                        $"'{dataRow["Status_Name"].ToString()}','{dataRow["TeamLevel"].ToString()}','{dataRow["IsGoleComplete"].ToString()}',event)" +
                        $" onmouseout=MouseOutEvent() onclick=GetTreeData('{Token_Id}') ><img src='{img}' alt=''> <p>{dataRow["Full_Name"].ToString()}</p>{Token_Id}";
                    msg += "</a><ul>";
                }
                else
                {
                    Registration_Business registration = homeManager.GetRegistration(0, 0, 0, Token_Id, null, null, null, null).FirstOrDefault();

                    string img = "";
                    string Gender = "";
                    if (registration.Gender == "Male")
                    {
                        style = "style = 'color: green;'";
                        img = "/Content/assets/images/Male_Icon.png";
                    }
                    else
                    {
                        style = "style = 'color: red;'";
                        img = "/Content/assets/images/Female_Icon.png";
                    }
                    msg += "<ul style='width: 1400px;'>";
                    msg += $"<li><a class='over' onmouseover=MouseOverEvent('{registration.Sponsor_Id}','{registration.Sponsor_Name}','{registration.Status_Name}','{registration.TeamLevel}','{registration.IsGoleComplete}',event)" +
                        $" onmouseout=MouseOutEvent() onclick=GetTreeData('{Token_Id}') ><img src='{img}' alt=''> <p>{registration.Full_Name}</p>{Token_Id}";
                    msg += "</a><ul>";
                }
                    #endregion

                    dr = dt.Select("Parent_ID= '" + Token_Id + "'");
                if (dr.Length == 2)
                {
                    foreach (DataRow row in dr)
                    {
                        msg += GetTreeNode2(row, dt);
                        dr1 = dt.Select("Parent_ID= '" + row["Token_ID"] + "'");
                        msg += "<ul>";
                        if (dr1.Length == 2)
                        {
                            foreach (DataRow row1 in dr1)
                            {
                                msg += GetTreeNode2(row1, dt);
                                dr2 = dt.Select("Parent_ID= '" + row1["Token_ID"] + "'");
                                msg += "<ul>";

                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data["Parent_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data["Parent_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (row1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", row1["Token_ID"].ToString());
                                        msg += GetBlankTreeNode("Right", row1["Token_ID"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 1)
                        {
                            var data1 = dr1[0];
                            if (data1["Position"].ToString() == "Left")
                            {
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data["Parent_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data["Parent_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_ID"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_ID"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                                msg += GetBlankTreeNode("Right", row["Token_ID"].ToString());
                            }
                            else
                            {
                                msg += GetBlankTreeNode("Left", row["Token_ID"].ToString());
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data["Parent_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data["Parent_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_ID"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_ID"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 0)
                        {
                            if (row["FK_Reg_Status_Id"].ToString().Equals("2"))
                            {
                                msg += GetBlankTreeNode("Left", row["Token_ID"].ToString());
                                msg += GetBlankTreeNode("Right", row["Token_ID"].ToString());
                            }
                        }
                        msg += "</ul>";
                        msg += "</li>";
                    }
                }
                else if (dr.Length == 1)
                {
                    if (dt.Rows[0]["Position"].ToString() == "Left")
                    {
                        #region Level 2
                        dr = dt.Select("Parent_ID= '" + Token_Id + "'");
                        var data2 = dr[0];
                        msg += GetTreeNode2(data2, dt);
                        dr1 = dt.Select("Parent_ID= '" + data2["Token_ID"] + "'");
                        msg += "<ul>";
                        if (dr1.Length == 2)
                        {
                            foreach (DataRow row1 in dr1)
                            {
                                msg += GetTreeNode2(row1, dt);
                                dr2 = dt.Select("Parent_ID= '" + row1["Token_ID"] + "'");
                                msg += "<ul>";

                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", row1["Token_ID"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", row1["Token_ID"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (row1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", row1["Token_ID"].ToString());
                                        msg += GetBlankTreeNode("Right", row1["Token_ID"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 1)
                        {
                            var data1 = dr1[0];
                            if (data1["Position"].ToString() == "Left")
                            {
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                                msg += GetBlankTreeNode("Right", data2["Token_Id"].ToString());
                            }
                            else
                            {
                                msg += GetBlankTreeNode("Left", data2["Token_Id"].ToString());
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {

                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 0)
                        {
                            if (data2["FK_Reg_Status_Id"].ToString().Equals("2"))
                            {
                                msg += GetBlankTreeNode("Left", data2["Token_Id"].ToString());
                                msg += GetBlankTreeNode("Right", data2["Token_Id"].ToString());
                            }
                        }
                        msg += "</ul>";
                        #endregion
                        msg += GetBlankTreeNode("Right", Token_Id);
                        msg += "</li>";
                        msg += "</ul>";
                    }
                    else
                    {
                        msg += GetBlankTreeNode("Left", Token_Id);
                        #region Level 2
                        dr = dt.Select("Parent_ID= '" + Token_Id + "'");
                        var data2 = dr[0];
                        msg += GetTreeNode2(data2, dt);
                        dr1 = dt.Select("Parent_ID= '" + data2["Token_ID"] + "'");
                        msg += "<ul>";
                        if (dr1.Length == 2)
                        {
                            foreach (DataRow row1 in dr1)
                            {
                                msg += GetTreeNode2(row1, dt);
                                dr2 = dt.Select("Parent_ID= '" + row1["Token_ID"] + "'");
                                msg += "<ul>";

                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", row1["Token_ID"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", row1["Token_ID"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (row1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", row1["Token_ID"].ToString());
                                        msg += GetBlankTreeNode("Right", row1["Token_ID"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 1)
                        {
                            var data1 = dr1[0];
                            if (data1["Position"].ToString() == "Left")
                            {
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                                msg += GetBlankTreeNode("Right", data2["Token_Id"].ToString());
                            }
                            else
                            {
                                msg += GetBlankTreeNode("Left", data2["Token_Id"].ToString());
                                msg += GetTreeNode2(data1, dt);
                                dr2 = dt.Select("Parent_ID= '" + data1["Token_ID"] + "'");
                                msg += "<ul>";
                                #region Third Level Tree
                                if (dr2.Length == 2)
                                {
                                    foreach (DataRow row2 in dr2)
                                    {
                                        msg += GetTreeNode(row2, dt);
                                    }
                                }
                                else if (dr2.Length == 1)
                                {
                                    var data = dr2[0];
                                    if (data["Position"].ToString() == "Left")
                                    {
                                        #region Face Generation
                                        msg += GetTreeNode(data, dt);
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Face Generation
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetTreeNode(data, dt);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    if (data1["FK_Reg_Status_Id"].ToString().Equals("2"))
                                    {
                                        msg += GetBlankTreeNode("Left", data1["Token_Id"].ToString());
                                        msg += GetBlankTreeNode("Right", data1["Token_Id"].ToString());
                                    }
                                }
                                msg += "</ul>";
                                msg += "</li>";
                                #endregion
                            }
                        }
                        else if (dr1.Length == 0)
                        {
                            msg += GetBlankTreeNode("Left", data2["Token_Id"].ToString());
                            msg += GetBlankTreeNode("Right", data2["Token_Id"].ToString());
                        }
                        msg += "</ul>";
                        msg += "</li>";
                        #endregion
                    }
                }
                else
                {
                    if (dataRow["FK_Reg_Status_Id"].ToString().Equals("2"))
                    {
                        msg += GetBlankTreeNode("Left", Token_Id);
                        msg += GetBlankTreeNode("Right", Token_Id);
                    }
                }
                msg += "</ul>";
                msg += "</li>";
                msg += "</ul>";
                return Json(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [CookiesExpireFilter]
        public ActionResult GetUserData(string Token_Id)
        {
            IHomeManager homeManager = new HomeManager();
            Registration_Business Registration_Business = homeManager.GetRegistration(0, 0, 0, Token_Id, null, null, null, null).FirstOrDefault();
            return Json(Registration_Business);
        }

        [CookiesExpireFilter]
        public ActionResult GetIncreptedUrl(string UrlData)
        {
            UrlData = EncryptionEngine.Base64Encode(UrlData);
            return Json(UrlData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Registration Approval
        [CookiesExpireFilter]
        public ActionResult PendingRegistration()
        {
            IHomeManager homeManager = new HomeManager();
            MemberModel Model = new MemberModel();
            Model.List_Registration_Businesses_Obj = homeManager.GetRegistration(0, 0, 0, null, null, null, null, null);
            Model.List_Registration_Businesses_Obj = Model.List_Registration_Businesses_Obj
                .Where(u => !String.Equals(u.Token_Id, "US123456")).OrderBy(u => u.FK_Reg_Status_Id).ToList();
            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult UpdateResStatus(int Registration_Id, int Status_Id)
        {
            IHomeManager homeManager = new HomeManager();
            Registration registration = homeManager.GetRegistration(Registration_Id, 0, 0, null, null, null, null, null).FirstOrDefault();
            int oldStatus = registration.FK_Reg_Status_Id;
            if (Status_Id == 2)
            {
                registration.FK_Reg_Status_Id = 2; //Approved
                registration.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                registration.IsRegFeeApproved = true;
                registration.ActiveDate = DateTime.Now;
            }
            else
            {
                registration.FK_Reg_Status_Id = 3; //Rejected
                registration.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                registration.IsRegFeeApproved = false;
            }
            registration.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
            registration.Modified_IP = SystemIP();
            registration.Modified_On = DateTime.Now;
            int Id = homeManager.UpdateRegistration(registration);
            if (Id > 0)
            {
                // Send status change email notification
                string to = registration.Email;
                string subject = "Your Registration Status Updated - Ujjawal Sanskar";
                string statusText = registration.FK_Reg_Status_Id == 2 ? "APPROVED" : "REJECTED";
                string body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='UTF-8'>
                        <title>Welcome to Ujjawal Sanskar</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; background: #f4f4f4; margin: 0; padding: 0; }}
                            .container {{ max-width: 600px; margin: 40px auto; background: #fff; border-radius: 10px; box-shadow: 0 2px 8px #e0e0e0; padding: 30px; }}
                            .header {{ background: #1976d2; color: #fff; padding: 20px 0; border-radius: 10px 10px 0 0; text-align: center; }}
                            .content {{ padding: 20px; }}
                            .status {{ font-size: 18px; font-weight: bold; color: {(registration.FK_Reg_Status_Id == 2 ? "#388e3c" : "#d84315")}; }}
                            .footer {{ text-align: center; color: #888; font-size: 13px; margin-top: 30px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Registration Status Update</h1>
                            </div>
                            <div class='content'>
                                <p>Dear <b>{registration.Full_Name}</b>,</p>
                                <p>Your registration status for <b>Ujjawal Sanskar</b> has been updated by the admin.</p>
                                <p class='status'>Current Status: {statusText}</p>
                                {(registration.FK_Reg_Status_Id == 2 ? "<p>Congratulations! Your account is now active. You can log in and start using all features.</p>" :
                                "<p>We regret to inform you that your registration has been rejected. For more information, please contact support.</p>")}
                            </div>
                            <div class='footer'>
                                &copy; {DateTime.Now.Year} Ujjawal Sanskar. All rights reserved.
                            </div>
                        </div>
                    </body>
                    </html>";

                // Use HomeController's SendMail
                new Web.Controllers.HomeController().SendMail("prince.project1901@gmail.com", to, subject, body);
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Registration Status Updated Successfully !";
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Failed to Update Registration Status !";
            }
            return RedirectToAction("PendingRegistration");
        }

        #endregion

        #region Registration Kyc
        [CookiesExpireFilter]
        public ActionResult PendingKyc()
        {
            IHomeManager homeManager = new HomeManager();
            MemberModel Model = new MemberModel();
            Model.List_Registration_Businesses_Obj = homeManager.GetRegistration(0, 0, 2, null, null, null, null, null)
                .Where(u => !u.Is_KYC_Approved && !String.Equals(u.Token_Id, "US123456")).ToList();
            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult UpdateResKyc(int Registration_Id, int IsKycApproved)
        {
            IHomeManager homeManager = new HomeManager();
            Registration registration = homeManager.GetRegistration(Registration_Id, 0, 0, null, null, null, null, null).FirstOrDefault();
            if (IsKycApproved == 2)
            {
                registration.Is_KYC_Approved = true; //Approved
                registration.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                registration.IsRegFeeApproved = true;
            }
            else
            {
                registration.Is_KYC_Approved = false; //Rejected
                registration.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                registration.IsRegFeeApproved = false;
            }
            int Id = homeManager.UpdateRegistration(registration);
            if (Id > 0)
            {
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "KYC Updated Successfully !";
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Failed to Update User KYC !";
            }
            return RedirectToAction("PendingKyc");
        }
        #endregion

        #region User Profile
        [CookiesExpireFilter]
        public ActionResult UserProfile(int? Registration_Id)
        {
            IHomeManager homeManager = new HomeManager();
            MemberModel Model = new MemberModel();
            if (Registration_Id != null && Registration_Id > 0)
                Model.Registration_Obj = homeManager.GetRegistration(Registration_Id, 0, 0, null, null, null, null, null).FirstOrDefault();
            else
                Model.Registration_Obj = homeManager.GetRegistration(0, Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id), 0, null, null, null, null, null).FirstOrDefault();
            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult UpdateProfile(MemberModel Model)
        {
            HomeManager homeManager = new HomeManager();
            int No = 0;
            if (Model.ImageFile != null)
            {
                string fullPath = Request.MapPath("/Upload/Member/ProfileImage/");
                string[] files = System.IO.Directory.GetFiles(fullPath, (Model.Registration_Obj.Token_Id + "*"));
                foreach (string f in files)
                {
                    No += 1;
                }
                string extension = System.IO.Path.GetExtension(Model.ImageFile.FileName);
                Model.ImageFile.SaveAs(Server.MapPath("~/Upload/Member/ProfileImage/" + Model.Registration_Obj.Token_Id + "_" + No + extension));
                string FilePathForPhoto = "~/Upload/Member/ProfileImage/" + Model.Registration_Obj.Token_Id + "_" + No + extension;
                Model.Registration_Obj.Img_File = FilePathForPhoto;
            }
            Model.Registration_Obj.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
            Model.Registration_Obj.Modified_IP = SystemIP();
            Model.Registration_Obj.Modified_On = DateTime.Now;

            int Id = homeManager.UpdateRegistration(Model.Registration_Obj);
            if (Id != 0 && Id > 0)
            {
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Profile updated Successfully !";
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Sorry, Failed to Update Profile !";
            }
            return RedirectToAction("UserProfile", "Member", new { Registration_Id = Model.Registration_Obj.Registration_Id });
        }
        #endregion

        #region RoleCompletedList
        public ActionResult UserGoleComList()
        {
            MemberModel Model=new MemberModel();
            HomeManager homeManager = new HomeManager();
            Model.List_Registration_Businesses_Obj = homeManager.GetRegistration(null, null, null, null, null, null, null, null, true);            
            return View(Model);
        }
        #endregion
    }
}