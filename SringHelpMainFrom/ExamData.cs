using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.BulkExtensions.Operations;
using Newtonsoft.Json;
using Sring.BusinessModule.ExamManage;

namespace SringHelpMainFrom
{
    public class ExamData
    {
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public static List<Base_Organize> GetOrganizes()
        {
            return new EduEntities().Base_Organize.ToList();
        }

        /// <summary>
        /// 考试列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Exam_Content> GetExamContents(Func<Exam_Content, bool> predicate)
        {
            if (predicate != null)
                return new EduEntities().Exam_Content.Where(predicate).ToList();
            else
                return new EduEntities().Exam_Content.ToList();
        }

        public static int GetExamLastNum(Guid examId)
        {
            return new EduEntities().Exam_StudentPapers
                .OrderByDescending(d => d.CreatedDate)
                .FirstOrDefault()
                ?.ExamNumber.Value ?? 10000;
        }

        public static string BulkUserToExam(Guid examId, int maxSignCount = 1000, params string[] userIds)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int totalPaperCount = 0;
            int totalPaperDetailCount = 0;
            using (var eduContext = new EduEntities())
            {
                var examPapers = eduContext.Exam_Papers.Where(d => d.ExamId == examId).ToList(); //试卷
                var examPaperJson = new Dictionary<Guid, PaperJson>();  //试卷内容
                foreach (var paper in examPapers)
                {
                    examPaperJson.Add(paper.PaperId, JsonConvert.DeserializeObject<PaperJson>(paper.PaperFormJson));
                }
                var random = new Random(Guid.NewGuid().GetHashCode());
                var lastExamNum = GetExamLastNum(examId); //最后的考号
                for (int pageIndex = 0; pageIndex <= (userIds.Length / maxSignCount); pageIndex++)
                {
                    var pageUser = userIds.Skip(pageIndex * maxSignCount).Take(maxSignCount).ToList(); //每页数量
                    var pageStudentPapers = new List<Exam_StudentPapers>(); //考生试卷
                    var pageStudentPaperDetails = new List<Exam_StudentPaperDetail>(); //考生答题记录
                    foreach (var userId in pageUser)
                    {
                        var selectPaper = examPapers[random.Next(0, examPapers.Count)]; //选中的试卷
                        var studentPaper = new Exam_StudentPapers()
                        {
                            StudentPaperId = Guid.NewGuid(),
                            ExamId = examId,
                            PaperId = selectPaper.PaperId,
                            StudentPaperFormJson = selectPaper.PaperFormJson,
                            UserId = new Guid(userId),
                            CreatedDate = DateTime.Now,
                            ExamNumber = lastExamNum++
                        };
                        pageStudentPapers.Add(studentPaper);
                        pageStudentPaperDetails.AddRange(GetExamStudentPaperDetailsFromPaperJson(studentPaper.StudentPaperId, examPaperJson[selectPaper.PaperId]));
                    }

                    try
                    {
                        totalPaperCount += eduContext.BulkInsert(pageStudentPapers);

                    }
                    catch (Exception e)
                    {
                        throw new Exception($"批量保存试卷出错!{e.Message}");
                    }

                    try
                    {
                        totalPaperDetailCount += eduContext.BulkInsert(pageStudentPaperDetails);

                    }
                    catch (Exception e)
                    {
                        throw new Exception($"批量保存答题记录出错!{e.Message}");
                    }
                }
            }
            stopwatch.Stop();
            return $"共{totalPaperCount}条试卷 共{totalPaperDetailCount}条答题记录 耗时{stopwatch.ElapsedMilliseconds}";
        }

        public static IEnumerable<Exam_StudentPaperDetail> GetExamStudentPaperDetailsFromPaperJson(
            Guid studentPaperId,
            PaperJson paperJson)
        {
            foreach (var questionJson in paperJson.PaperQuestionJsons)
            {
                var paperDetail = new Exam_StudentPaperDetail()
                {
                    StudentPaperDetailId = Guid.NewGuid(),
                    StudentPaperId = studentPaperId,
                    DeleteStatus = false,
                    QuestionId = questionJson.PaperQuestion.QuestionId,
                    QuestionJson = JsonConvert.SerializeObject(questionJson),
                    QuestionType = questionJson.PaperQuestion.QuestionType,
                    Score = questionJson.PaperQuestion._QuestionStrategyScore,
                    Status = 10,
                    CreatedDate = DateTime.Now,
                };
                yield return paperDetail;
            }
        }
    }
}
