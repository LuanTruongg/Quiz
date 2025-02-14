﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DTO.QuestionManagement
{
    public class GetQuestionResponse
    {
        public string QuestionId { get; set; }
        public string SubjectId { get; set; }
        public string QuestionText { get; init; }
        public string QuestionA { get; set; }
        public string QuestionB { get; set; }
        public string QuestionC { get; set; }
        public string QuestionD { get; set; }
        public string Answer { get; set; }
        public string Difficult { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Image { get; set; }
        public string Audio { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile AudioFile { get; set; }
    }
}
