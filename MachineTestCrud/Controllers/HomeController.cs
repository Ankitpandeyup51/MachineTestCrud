using MachineTestCrud.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;

namespace MachineTestCrud.Controllers
{
    public class HomeController : Controller
    {
        // string cs = ("Data Source=DESKTOP-J2P85F6\\SQLEXPRESS;Initial Catalog=demo1;Integrated Security=True;");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public ActionResult index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
       
         public ActionResult Registration(Student model)
        {
            SqlCommand cmd=new SqlCommand("insert into Student values(@Name,@Department,@Email,@Password,@Dob,@Address)",con);
            cmd.Parameters.AddWithValue("@Id",model.Id);
            cmd.Parameters.AddWithValue("Name",model.Name);
            cmd.Parameters.AddWithValue("@Department", model.Department);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Dob", model.Dob);
            cmd.Parameters.AddWithValue("@Address", model.Address);
            con.Open();
            int res=cmd.ExecuteNonQuery();
            con.Close();
            if (res>0)
            {
                return Content("<script>alert('Data insert Successfully');location.href='/Home/Registration';</script>");
            }
            else
            {
                return Content("<script>alert('Registration Failed');location.href='/Home/Registration';</script>");
            }
        }

        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(StudentModel model)
        {
            SqlCommand cmd = new SqlCommand("Select * from Student Where Email=@Email and Password=@Password",con);
            cmd.Parameters.AddWithValue("@Email",model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                return Content("<script>alert('Login Successfully');location.href='/Home/Index';</script>");
            }
            else
            {
                con.Close();
                return Content("<script>alert('Invalid Email or Password');location.href='/Home/Index';</script>");
            }
            
        }

        public ActionResult StudentList()
        {
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            return View(dt);
        }

        public ActionResult Delete(int? Id)
        {
            SqlCommand cmd = new SqlCommand("delete from Student where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id",Id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res>0)
            {
                return Content("<script>alert('Student Deleted Successful');location.href='/Home/StudentList';</script>");

            }
            else
            {
                return Content("<script>alert('No Record Found');location.href='/Home/StudentList';</script>");
            }
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            SqlCommand cmd = new SqlCommand("select * from Student Where id=@id", con);
            cmd.Parameters.AddWithValue("@id", Id);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);

            return View(dt);

        }

      //  [HttpPost]
        /*  public ActionResult Update(Student model)
          {
              SqlCommand cmd = new SqlCommand("Update Student set Name=@Name,Department=@Department,Email=@Email,Password=@Password,Dob=@Dob,Address=@Address Where id=@id",con);
              cmd.Parameters.AddWithValue("@Id", model.Id);
              cmd.Parameters.AddWithValue("Name", model.Name);
              cmd.Parameters.AddWithValue("@Department", model.Department);
              cmd.Parameters.AddWithValue("@Email", model.Email);
              cmd.Parameters.AddWithValue("@Password", model.Password);
              cmd.Parameters.AddWithValue("@Dob", model.Dob);
              cmd.Parameters.AddWithValue("@Address", model.Address);
              con.Open();
              int res = cmd.ExecuteNonQuery();
              con.Close();
              if(res > 0)
              {
                  return Content("<script>alert('Data Inserted Successfully');location.href='/Home/Registration';</script>");
              }
              else
              {
                  return Content("<script>alert('Registration Failed');location.href='/Home/Registration';</script>");
              }
          }

          */

        [HttpPost]
        public ActionResult Update(Student model)
        {
            SqlCommand cmd = new SqlCommand(
                "UPDATE Student SET Name=@Name, Department=@Department, Email=@Email, Password=@Password, Dob=@Dob, Address=@Address WHERE Id=@Id",
                con);

            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Department", model.Department);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Dob", model.Dob);
            cmd.Parameters.AddWithValue("@Address", model.Address);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            if (res > 0)
            {
                return Content("<script>alert('Data Updated Successfully');location.href='/Home/StudentList';</script>");
            }
            else
            {
                return Content("<script>alert('Update Failed: Record not found');location.href='/Home/StudentList';</script>");
            }
        }
    }
}