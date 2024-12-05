using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentWebAppPortal.Models;
using X.PagedList.Extensions;

namespace StudentWebAppPortal.Controllers
{

    public class StudentController1 : Controller
    {
        private string url = "https://localhost:7052/api/Students/";
        private HttpClient client = new HttpClient();

        [Route("Student")]
        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 10; // Number of items per page
            int pageNumber = page ?? 1; // Default to the first page

            List<Student> students = new List<Student>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);

                if (data != null)
                {
                    students = data;
                }
            }

            // Apply pagination using X.PagedList
            var paginatedStudents = students.ToPagedList(pageNumber, pageSize);

            return View(paginatedStudents);
         
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std); 
            StringContent stringContent = new StringContent(data,Encoding.UTF8,"application/json");  
            HttpResponseMessage response = client.PostAsync(url, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added......";
                return RedirectToAction("Index");
            }

            return View();
        }
      
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            Student std = new Student();
            HttpResponseMessage msg = client.GetAsync(url + id).Result;
            if (msg.IsSuccessStatusCode)
            {
                string result = msg.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if(data != null)
                {
                    std = data;
                  
                }
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + std.id, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student Updated......";
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Details(int id)
        {

            Student std = new Student();
            HttpResponseMessage msg = client.GetAsync(url + id).Result;
            if (msg.IsSuccessStatusCode)
            {
                string result = msg.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;

                }
            }
            return View(std);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            Student std = new Student();
            HttpResponseMessage msg = client.GetAsync(url + id).Result;
            if (msg.IsSuccessStatusCode)
            {
                string result = msg.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;

                }
            }
            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
        
            HttpResponseMessage msg = client.DeleteAsync(url + id).Result;
            if (msg.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student Deleted......";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
