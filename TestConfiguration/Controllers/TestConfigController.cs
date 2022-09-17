using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestConfiguration.DAL;
using TestConfiguration.Models;

namespace TestConfiguration.Controllers
{
    public class TestConfigController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _env;
        public TestConfigController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            _env = env;
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
            testConfig.TestConfigDetails.Add(new TestConfigDetail());
            testConfig.TestConfigDetails.Add(new TestConfigDetail());
            testConfig.TestConfigDetails.Add(new TestConfigDetail());

            return View( testConfig);
        }

        [HttpPost]
        public IActionResult Create(TestConfig testConfig)
        {
            //testConfig.TestConfigDetails.RemoveAll(tcd => tcd.SubjectId < 1);
            //string profilePath =  UploadedFile(testConfig.ProfileImage);
            //testConfig.ProfileUrl = profilePath;
            //if (ModelState.IsValid)
            //{
            //    context.Add(testConfig);
            //    context.SaveChanges();
            //}
            TempData["Success"] = "Hello";
            return View(testConfig);
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

        private string UploadedFile(IFormFile image)
        {
            string filePath = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }
            }
            return filePath;
        }

    }
}
