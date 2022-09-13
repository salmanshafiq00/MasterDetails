using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestConfiguration.DAL;
using TestConfiguration.Models;

namespace TestConfiguration.Controllers
{
    public class TestConfigController : Controller
    {
        private readonly ApplicationDbContext context;

        public TestConfigController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.TestConfigs.ToList());
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            return View(context.TestConfigs.Include(tc => tc.TestConfigDetails).Where(tc => tc.Id == Id).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult Create()
        {
            TestConfig testConfig = new TestConfig();
            testConfig.TestConfigDetails.Add(new TestConfigDetail());
            return View(testConfig);
        }

        [HttpPost]
        public IActionResult Create(TestConfig testConfig)
        {
            testConfig.TestConfigDetails.RemoveAll(tcd => tcd.SubjectId < 1);

            if (ModelState.IsValid)
            {
                context.Add(testConfig);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var result =   context.TestConfigs.Include(tcd => tcd.TestConfigDetails).Where(tc => tc.Id == id).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(TestConfig testConfig)
        {

            if (ModelState.IsValid)
            {
                
                context.Entry(testConfig).State = EntityState.Modified;
                context.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
           var result =  context.TestConfigs.FirstOrDefault(tc => tc.Id == id);
            if (result == null)
            {
                return View("Data not found");
            }
            else
            {
                context.TestConfigs.Remove(result);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
