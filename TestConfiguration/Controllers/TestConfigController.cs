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
            //for (int i = 0; i < testConfig.TestConfigDetails.Count; i++)
            //{
            //    if (testConfig.TestConfigDetails[i].SubjectId < 1)
            //    {
            //        testConfig.TestConfigDetails.Remove(testConfig.TestConfigDetails[i]);
            //        i--;
            //    }
            //}
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
            var childNumber = result.TestConfigDetails.Count;
            var iterationNumber = 6 - childNumber;
            for (int i = 0; i < iterationNumber; i++)
            {
                result.TestConfigDetails.Add(new TestConfigDetail());
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(TestConfig testConfig)
        {
            for (int i = 0; i < testConfig.TestConfigDetails.Count; i++)
            {
                if (testConfig.TestConfigDetails[i].Id > 0 && testConfig.TestConfigDetails[i].SubjectId < 1)
                {
                    if (context.TestConfigDetails.Any(tcd => tcd.Id == testConfig.TestConfigDetails[i].Id))
                    {
                        context.Remove(context.TestConfigDetails.FirstOrDefault(tcd => tcd.Id == testConfig.TestConfigDetails[i].Id));
                        testConfig.TestConfigDetails.Remove(testConfig.TestConfigDetails[i]);
                        i--;
                    }
                   
                }
                else if (testConfig.TestConfigDetails[i].SubjectId < 1)
                {
                    testConfig.TestConfigDetails.Remove(testConfig.TestConfigDetails[i]);
                    i--;
                }

            }
            if (ModelState.IsValid)
            {
                context.Update(testConfig);
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
