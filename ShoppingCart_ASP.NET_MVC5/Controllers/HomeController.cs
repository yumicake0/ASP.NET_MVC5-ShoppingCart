﻿using ShoppingCart_ASP.NET_MVC5.Dao;
using ShoppingCart_ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_ASP.NET_MVC5.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Home(string username, string password)
        {
            Debug.WriteLine("HttpPost Method");

            if (username == null)
                return View();

            Customer customer = CustomerDetails.GetCustomer(username);
            if (customer.password != CalculateMD5Hash(password).ToLower())
                return View();

            List<Product> prolist = ProductDetails.callme();
            ViewData["pl"] = prolist;
            string sessionId = SessionManagement.CreateSession(customer.customer_id);
            ViewData["sessionId"] = sessionId;
            ViewBag.a = 1;
            ViewBag.customer = customer.customer_id;
            return View();

        }


        public string CalculateMD5Hash(string input)

        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


    }
}