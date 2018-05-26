using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;

namespace BuildSchool_MVC_Ecommerce.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult Index()
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            if (Request.Cookies["shoppingcar"] == null)
            {
                ViewData["shopping"] = null;
            }
            else
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["shoppingcar"].Value);
                var shopping = JSONSerializer.Deserialize<List<Shopping>>(json);
                ViewData["shopping"] = shopping;
            }
            if(Request.Cookies["user"] != null)
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["user"].Value);
                var user = JSONSerializer.Deserialize<User>(json);
                ViewData["user"] = user.Username;
            }
            return View();
        }
        public ActionResult NoShoppingCar()
        {
            return View();
        }
        public ActionResult PutInCookie(string productid, string color, string size, string Quantity)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            var cookieName = "shoppingcar";
            var productrepository = new ProductRepository();
            var product = productrepository.FindProductFormatByProductID(int.Parse(productid));
            var quantity = product.FirstOrDefault((x) => x.Color == color && x.Size == size);
            var s = new Shopping()
            {
                ProductID = quantity.ProductID,
                ProductName = quantity.ProductName,
                ProductImage = quantity.ProductImage,
                UnitPrice = quantity.UnitPrice,
                Description = quantity.Description,
                StockQuantity = quantity.StockQuantity,
                Size = quantity.Size,
                Color = quantity.Color,
                Quantity = int.Parse(Quantity),
                ProductFormatID = quantity.ProductFormatID
            };
            if (quantity.StockQuantity == 0)
            {
                return View("NoProductQuantity");
            }
            else
            {
                if (Request.Cookies["shoppingcar"] == null)
                {
                    var token = Guid.NewGuid().ToString();
                    var shopping = new List<Shopping>();
                    shopping.Add(s);
                    //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                    string json = JSONSerializer.Serialize(shopping);
                    var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(json))
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(hc);
                }
                else
                {
                    string json = HttpUtility.UrlDecode(Request.Cookies["shoppingcar"].Value);
                    var shopping = JSONSerializer.Deserialize<List<Shopping>>(json);
                    shopping.Add(s);
                    //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                    string jsons = JSONSerializer.Serialize(shopping);
                    var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(jsons))
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(hc);
                }
            }
            return RedirectToAction("Index", "Shopping");
        }
        public ActionResult NoProductQuantity()
        {
            return View();
        }
        public ActionResult Shopping2()
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            if (Request.Cookies["user"] != null)
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["user"].Value);
                var user = JSONSerializer.Deserialize<User>(json);
                ViewData["user"] = user.Username;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Shopping3(string name, string phone, string address, string pay)
        {
            var cookieName = "ShipData";
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            var shipdata = new ShipData()
            {
                ShipName = name,
                ShipPhone = phone,
                ShipAddress = address,
                Pay = pay
            };
            if(Request.Cookies["ShipData"] == null)
            {
                string json = JSONSerializer.Serialize(shipdata);
                var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(json))
                {
                    Expires = DateTime.Now.AddMinutes(10),
                    HttpOnly = true
                };
                Response.Cookies.Add(hc);
            }
            else
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["ShipData"].Value);
                var shopping = JSONSerializer.Deserialize<ShipData>(json);
                shopping = shipdata;
                //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                string jsons = JSONSerializer.Serialize(shopping);
                var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(jsons))
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true
                };
                Response.Cookies.Add(hc);
            }
            return RedirectToAction("Shopping4", "Shopping");
        }
        [HttpPost]
        public ActionResult Shopping5()
        {
            List<Shopping> shopping;
            User user;
            ShipData ship;
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            if (Request.Cookies["shoppingcar"] != null && Request.Cookies["user"] != null && Request.Cookies["ShipData"] != null)
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["shoppingcar"].Value);
                shopping = JSONSerializer.Deserialize<List<Shopping>>(json);
                string json1 = HttpUtility.UrlDecode(Request.Cookies["user"].Value);
                user = JSONSerializer.Deserialize<User>(json1);
                string json2 = HttpUtility.UrlDecode(Request.Cookies["ShipData"].Value);
                ship = JSONSerializer.Deserialize<ShipData>(json2);
                SqlConnection connection = new SqlConnection("data source=.; database=Commerce; integrated security=true");
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var order = new Orders()
                        {
                            MemberID = user.UserID,
                            EmployeeID = 1,
                            OrderDate = DateTime.Now,
                            Discount = 1,
                            ReceiptedDate = null,
                            ShipName = ship.ShipName,
                            ShipPhone = ship.ShipPhone,
                            ShipAddress = ship.ShipAddress,
                            ShippedDate = null,
                            Status = "未出貨"
                        };
                        var orderrepository = new OrdersRepository();
                        orderrepository.Create(order, connection, transaction);
                        var memberrepository = new MemberRepository();
                        var memberorder = memberrepository.GetBuyerOrder(user.UserID, connection, transaction);
                        var lastordeer = memberorder.First();
                        var orderdetailrepository = new OrderDetailsRepository();
                        foreach (var item in shopping)
                        {
                            OrderDetails od = new OrderDetails()
                            {
                                OrderID = lastordeer.OrderID,
                                ProductFormatID = item.ProductFormatID,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice
                            };
                            orderdetailrepository.Create(od, connection, transaction);
                        }                        
                        transaction.Commit();
                        return RedirectToAction("Shopping6", "Shopping");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return RedirectToAction("", "Shopping");
                    }
                    
                }
            }
            else
            {
                return RedirectToAction("", "Shopping");
            }
            //做order跟orderdetail
        }
        public ActionResult Shopping6()
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            if (Request.Cookies["shoppingcar"] == null)
            {

            }
            else
            {
                var cookie1 = Request.Cookies["shoppingcar"];
                cookie1.Expires = DateTime.Now;
                Response.Cookies.Add(cookie1);
            }
            if (Request.Cookies["ShipData"] == null)
            {

            }
            else
            {
                var cookie2 = Request.Cookies["ShipData"];
                cookie2.Expires = DateTime.Now;
                Response.Cookies.Add(cookie2);
            }
            if (Request.Cookies["user"] != null)
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["user"].Value);
                var user = JSONSerializer.Deserialize<User>(json);
                ViewData["user"] = user.Username;
            }
            //新增成功跳轉
            return View();
        }
        public ActionResult Shopping4()
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            if (Request.Cookies["shoppingcar"] == null)
            {
                ViewData["shopping"] = null;
            }
            else
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["shoppingcar"].Value);
                var shopping = JSONSerializer.Deserialize<List<Shopping>>(json);
                ViewData["shopping"] = shopping;
            }
            if (Request.Cookies["user"] != null)
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["user"].Value);
                var user = JSONSerializer.Deserialize<User>(json);
                ViewData["user"] = user.Username;
            }
            if (Request.Cookies["ShipData"] == null)
            {
                ViewData["shipname"] = "";
                ViewData["shipphone"] = "";
                ViewData["shipaddress"] = "";
                ViewData["shippay"] = "";
            }
            else
            {
                string json = HttpUtility.UrlDecode(Request.Cookies["ShipData"].Value);
                var ship = JSONSerializer.Deserialize<ShipData>(json);
                ViewData["shipname"] = ship.ShipName;
                ViewData["shipphone"] = ship.ShipPhone;
                ViewData["shipaddress"] = ship.ShipAddress;
                ViewData["shippay"] = ship.Pay;
            }
            return View();
        }
    }
}