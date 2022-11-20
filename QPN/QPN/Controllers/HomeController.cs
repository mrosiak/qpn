using System.Web.Mvc;
using System.Security.Cryptography;
using QPN.Models;
using System.Text;
using QPN.Database.Repositories;

namespace QPN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new QuizModel();
            return View(model);
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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var usersRepository = new UsersRepository();
                var user = usersRepository.GetUser(userModel.Login, MD5Hash(userModel.Password));
                if (user != null)
                {                    
                    Session["UserName"] = user.Login;
                    return RedirectToAction("Index");
                }
            }
            return View(userModel);
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}