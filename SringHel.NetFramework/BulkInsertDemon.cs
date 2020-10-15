using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using EFBulkInsert;
using Newtonsoft.Json;
using Sring.BusinessModule.ExamManage;

namespace SringHel.NetFramework
{
    public class BulkInsertDemon
    {
        public static void Test1<T>(IEnumerable<T> ts)
        {
            eduEntities uEntities = new eduEntities();
            uEntities.BulkInsert(ts);
        }


        /// <summary>
        /// 获取考生试卷
        /// </summary>
        /// <returns></returns>
        public static List<Exam_StudentPapers> GetStudentPapersList(Expression<Func<Exam_StudentPapers, bool>> predicate)
        {
            eduEntities ed = new eduEntities();
            return ed.Exam_StudentPapers.Where(predicate).ToList();
        }

        public static List<Exam_Papers> GetExamPapersList(Expression<Func<Exam_Papers, bool>> predicate)
        {
            var edContext = new eduEntities();
            return edContext.Exam_Papers.Where(predicate).ToList();
        }

        public static List<Exam_Content> GetExamContents(Expression<Func<Exam_Content, bool>> predicate)
        {
            var edContext = new eduEntities();
            return edContext.Exam_Content.Where(predicate).ToList();
        }

        public static List<Base_User> GetUserTotal()
        {
            var eduContext = new eduEntities();
            return eduContext.Base_User.ToList();
        }

        public static string SignExamUser(Guid examId)
        {
            var examPapers = GetExamPapersList(d => d.ExamId == examId); //考试试卷
            var paperDetailsTemp = new Dictionary<Guid, List<Exam_StudentPaperDetail>>();
            foreach (var paper in examPapers)
            {
                var paperJson = JsonConvert.DeserializeObject<PaperJson>(paper.PaperFormJson);
                var paperDetails = new List<Exam_StudentPaperDetail>();
                foreach (var questionJson in paperJson.PaperQuestionJsons)
                {
                    var paperDetail = new Exam_StudentPaperDetail()
                    {
                        DeleteStatus = false,
                        QuestionId = questionJson.PaperQuestion.QuestionId,
                        QuestionJson = JsonConvert.SerializeObject(questionJson),
                        QuestionType = questionJson.PaperQuestion.QuestionType,
                        Score = questionJson.PaperQuestion._QuestionStrategyScore,
                        Status = 10,
                        StudentAnswerText = "",
                    };
                    paperDetails.Add(paperDetail);
                }
                paperDetailsTemp.Add(paper.PaperId,paperDetails);
            }

            var users = GetUserTotal();

            List<Exam_StudentPapers> resultPapers = new List<Exam_StudentPapers>(users.Count); //要保存的试卷
            List<Exam_StudentPaperDetail> resultPaperDetails = new List<Exam_StudentPaperDetail>(users.Count * paperDetailsTemp.FirstOrDefault().Value.Count); //要保存的答题记录
            int lastExamNum = 100000;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            foreach (var user in users)
            {
                var selectExamPaper = examPapers[random.Next(0, examPapers.Count)];
                var studentPaper = CreateStudentPaperFromPaperJson(examId, Guid.Parse(user.UserId), selectExamPaper, lastExamNum++); //创建考生试卷
                resultPapers.Add(studentPaper);
                foreach (var paperDetail in paperDetailsTemp[selectExamPaper.PaperId])
                {
                    var studentPaperDetail = CreateStudentPaperDetailFromDetail(studentPaper.StudentPaperId, paperDetail); //答题记录
                    resultPaperDetails.Add(studentPaperDetail);
                }
            }

            var eduContext = new eduEntities();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            eduContext.BulkInsert(resultPapers);
            stopwatch.Stop();

            var time1 = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            eduContext.BulkInsert(resultPaperDetails);
            stopwatch.Stop();
            var time2 = stopwatch.ElapsedMilliseconds;
            return $"耗时：{time1}_{time2}";
        }

        /// <summary>
        /// 通过考试试卷创建考生试卷
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <param name="paper"></param>
        /// <param name="examNum"></param>
        /// <returns></returns>
        public static Exam_StudentPapers CreateStudentPaperFromPaperJson(Guid examId, Guid userId, Exam_Papers paper, int examNum)
        {
            return new Exam_StudentPapers()
            {
                StudentPaperId = Guid.NewGuid(),
                ExamId = examId,
                UserId = userId,
                ExamNumber = examNum,
                PaperId = paper.PaperId,
                StudentPaperFormJson = paper.PaperFormJson,
                CreatedDate = DateTime.Now,
            };
        }

        /// <summary>
        /// 创建答题记录
        /// </summary>
        /// <param name="studentPaperId"></param>
        /// <param name="studentPaperDetail"></param>
        /// <returns></returns>
        public static Exam_StudentPaperDetail CreateStudentPaperDetailFromDetail(Guid studentPaperId, Exam_StudentPaperDetail studentPaperDetail)
        {
            return new Exam_StudentPaperDetail()
            {
                StudentPaperDetailId = Guid.NewGuid(),
                DeleteStatus = studentPaperDetail.DeleteStatus,
                QuestionId = studentPaperDetail.QuestionId,
                QuestionJson = studentPaperDetail.QuestionJson,
                QuestionType = studentPaperDetail.QuestionType,
                Score = studentPaperDetail.Score,
                Status = studentPaperDetail.Status,
                StudentPaperId = studentPaperId,
                CreatedDate = DateTime.Now
            };
        }


    }
}