﻿using Microsoft.AspNetCore.Mvc;
using Quiz.DTO.TestSubjectManagement;
using Quiz.Repository.Model;
using Quiz.UI.ServicesClient;

namespace Quiz.UI.Controllers
{
    public class TestSubjectController : Controller
    {
        private readonly ITestSubjectServiceClient _testSubjectServiceClient;
        private readonly ITestStructureServiceClient _testStructureServiceClient;
        public TestSubjectController(ITestSubjectServiceClient testSubjectServiceClient, ITestStructureServiceClient testStructureServiceClient)
        {
            _testSubjectServiceClient = testSubjectServiceClient;
            _testStructureServiceClient = testStructureServiceClient;
        }
        public async Task<IActionResult> Index(string testStructureId)
        {
            ViewBag.TestStructureId = testStructureId;
            var teststructure = await _testStructureServiceClient.GetTestStructureById(testStructureId);
            ViewBag.ListQuestion = await _testSubjectServiceClient.GetListQuestionOfTest(testStructureId);
            ViewBag.TestStructure = teststructure.ResultObj;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitTest(List<UserAnswerRequest> UserAnswerRequest, string testStructureId)
        {
            
            var userTest = await _testSubjectServiceClient.SaveUserTest(testStructureId);
            if(userTest.UserTestId != null)
            {
                var userTestObj = await _testSubjectServiceClient.SaveUserAnswer(UserAnswerRequest, userTest.UserTestId);
                return RedirectToAction("Score", "TestSubject", new { userTestId = userTestObj.UserTestId });
            }
            return BadRequest("Error");
        }
        public async Task<IActionResult> Score(string userTestId)
        {
            var score = await _testSubjectServiceClient.GetResultUserTest(userTestId); 
            return View(score);
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
