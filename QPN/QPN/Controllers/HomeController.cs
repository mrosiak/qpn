using System.Web.Mvc;
using System.Security.Cryptography;
using QPN.Models;
using System.Text;
using QPN.Database.Repositories;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System.Linq;
using System;
using QPN.Helpers;

namespace QPN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
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
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string ExportData)
        {
            var dataModel = JsonConvert.DeserializeObject<PdfModel[]>(ExportData);
            var answersSuffix = new[] { "a. ", "b. ", "c. ", "d. ", };
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //StringReader reader = new StringReader(ExportData);
                Document PdfFile = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                PdfFile.Add(new Paragraph("QPN"));
                int i = 1;
                foreach (var data in dataModel.OrderBy(x => x.Difficulty))
                {

                    PdfFile.Add(new Paragraph(i + ". " + data.Question));
                    var answers = new[] { data.CorrectAnswer, data.Wrong1Answer, data.Wrong2Answer, data.Wrong3Answer };
                    var rng = new Random();
                    rng.Shuffle(answers);

                    for (int j = 0; j < answers.Length; j++)
                    {
                        PdfFile.Add(new Paragraph(answersSuffix[j] + answers[j]));
                    }
                    PdfFile.Add(new Chunk("\n"));
                    PdfFile.Add(new Paragraph(" "));
                    i++;
                }
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close();
                return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
            }
        }
    }
}