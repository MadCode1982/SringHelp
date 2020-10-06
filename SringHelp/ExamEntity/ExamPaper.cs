using Sring.BusinessModule.ExamManage;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SringHelp.ExamEntity
{
    public class ExamPaper : PapersEntity
    {
        public List<StudentPaperDetailEntity> StudentPaperDetails { get; set; } = new List<StudentPaperDetailEntity>();

        public void CreateStudnetPaperDeatil()
        {
            var paperJson = JsonConvert.DeserializeObject<PaperJson>(this.PaperFormJson);
            foreach (var questionJson in paperJson.PaperQuestionJsons)
            {
                var paperDetail = new StudentPaperDetailEntity()
                {
                    DeleteStatus = false,
                    QuestionId = questionJson.PaperQuestion.QuestionId,
                    QuestionJson = JsonConvert.SerializeObject(questionJson),
                    QuestionType = questionJson.PaperQuestion.QuestionType,
                    Score = questionJson.PaperQuestion._QuestionStrategyScore,
                    Status = 10,
                    StudentAnswerText = "",
                };
                StudentPaperDetails.Add(paperDetail);
            }
        }
    }

}
