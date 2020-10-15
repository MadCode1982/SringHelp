using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using Sring.BusinessModule.ExamManage;

namespace SringHelp
{
    /// <summary>
    /// 考试缓存帮助
    /// </summary>
    public class ExamCacheHelper
    {
        public static readonly string StudentPaperDetailBLL_GetEntity_CacheKey = "StudentPaperDetailBLL_GetEntity_keyValue:{0}";

        public static readonly string StudentPaperDetailBLL_GetQuestionPanelList_CacheKey = "StudentPaperDetailBLL_GetQuestionPanelList_studentPaperId:{0}";

        public static readonly string ExamBLL_GetEntity_CacheKey = "ExamBLL_GetEntity_keyValue:{0}";

        public static readonly string StudentPapersBLL_GetEntity_CacheKey = "StudentPapersBLL_GetEntity_keyValue:{0}";

        public static readonly string StudentPapersBLL_GetEntityNoFormJson_CacheKey = "StudentPapersBLL_GetEntityNoFormJson_keyValue:{0}";
        //读取数据
        //写入缓存


        //读取缓存数据
        //批量写入数据库

        /// <summary>
        /// 读取考试到缓存
        /// </summary>
        public static string GetExamToCache(Guid examId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var examPapersIds = ExamDataHelper.GetExamStudentPaperIds(examId).ToList();  //考试试卷id
            int maxCachePaperCount = int.Parse(ConfigurationManager.GetSection("MaxCachePaperCount")); //配置的一次缓存的数量
            int paperDetailCount = 0;
            for (int startPage = 0; startPage <= (examPapersIds.Count / maxCachePaperCount); startPage++) //
            {
                var pageStudentPaperIds = examPapersIds.Skip(startPage * maxCachePaperCount).Take(maxCachePaperCount).ToArray();
                var studentPaperEntitys = ExamDataHelper.GetStudentPapers(pageStudentPaperIds).ToArray(); //考生试卷
                var studentPaperDetails = ExamDataHelper.GetStudentPaperDetails(pageStudentPaperIds).ToArray(); //考生答题卡

                var paperCacheResult = Parallel.For(0, studentPaperEntitys.Length - 1, d =>
                {
                    string paperCacheKey = string.Format(StudentPapersBLL_GetEntityNoFormJson_CacheKey, studentPaperEntitys[d].StudentPaperId);
                    if (RedisCacheHelper.Get<StudentPapersEntity>(paperCacheKey) == null)  //不存在缓存
                        RedisCacheHelper.Set(paperCacheKey, studentPaperEntitys[d]); //异步缓存
                });
                var detailCacheResult = Parallel.For(0, studentPaperDetails.Length - 1, d =>
                {
                    string detailCacheKey = string.Format(StudentPaperDetailBLL_GetEntity_CacheKey, studentPaperDetails[d].StudentPaperDetailId);
                    if (RedisCacheHelper.Get<StudentPaperDetailEntity>(detailCacheKey) == null) //不存在缓存
                        RedisCacheHelper.Set(detailCacheKey, studentPaperDetails[d]); //异步缓存
                });
                paperDetailCount += studentPaperDetails?.Length ?? 0;

                while (paperCacheResult.IsCompleted == false && detailCacheResult.IsCompleted == false)
                {

                }
            }

            stopwatch.Stop();
            return $"缓存了 【{examPapersIds.Count}】条 考生试卷  【{paperDetailCount}】条答题记录 耗时（毫秒）【{stopwatch.ElapsedMilliseconds}】";
        }

        /// <summary>
        /// 批量写入考试数据到数据库
        /// </summary>
        /// <param name="examId"></param>
        public static async Task SaveExamCacheToDataBase(Guid examId)
        {
            var examPapersIds = ExamDataHelper.GetExamStudentPaperIds(examId).ToList();  //考试试卷id
            int maxSignExamUserCount = int.Parse(ConfigurationManager.GetSection("MaxSignExamUserCount")); //配置的一次报名的数量 
            int paperDetailCount = 0;
            for (int startPage = 0; startPage <= (examPapersIds.Count / maxSignExamUserCount); startPage++) //
            {
                var pageStudentPaperIds = examPapersIds.Skip(startPage * maxSignExamUserCount).Take(maxSignExamUserCount).ToArray();
                var studentPaperEntitys = await GetCacheStudentPaperList(pageStudentPaperIds); //缓存考生试卷
                var studentPaperDetails = await GetCacheStudentPaperDetailListByStudnetPaperId(pageStudentPaperIds); //缓存考生答题卡
                
            }
        }

        /// <summary>
        /// 获取缓存的试卷
        /// </summary>
        /// <param name="studentPaperId"></param>
        /// <returns></returns>
        public static async Task<StudentPapersEntity> GetCacheStudentPaper(Guid studentPaperId)
        {
            return await Task.Factory.StartNew(() => { return RedisCacheHelper.Get<StudentPapersEntity>(string.Format(StudentPapersBLL_GetEntityNoFormJson_CacheKey, studentPaperId)); });
        }

        /// <summary>
        /// 获取一批缓存考试试卷
        /// </summary>
        /// <param name="studentPaperIds"></param>
        /// <returns></returns>
        public static async Task<List<StudentPapersEntity>> GetCacheStudentPaperList(params Guid[] studentPaperIds)
        {
            List<StudentPapersEntity> studentPapers = new List<StudentPapersEntity>(studentPaperIds.Length);
            foreach (var id in studentPaperIds)
            {
                var studentPaper = await GetCacheStudentPaper(id);
                if (studentPaper != null)
                    studentPapers.Add(studentPaper);
            }
            return studentPapers;
        }

        /// <summary>
        /// 获取缓存答题记录
        /// </summary>
        /// <param name="paperDetailId"></param>
        /// <returns></returns>
        public static async Task<StudentPaperDetailEntity> GetCacheStudentPaperDetail(Guid paperDetailId)
        {
            return await Task.Factory.StartNew(() => { return RedisCacheHelper.Get<StudentPaperDetailEntity>(string.Format(StudentPaperDetailBLL_GetEntity_CacheKey, paperDetailId)); });
        }

        /// <summary>
        /// 获取缓存考试答题卡
        /// </summary>
        /// <param name="paperDetailIds"></param>
        /// <returns>StudentPaperDetailEntity</returns>
        public static async Task<List<StudentPaperDetailEntity>> GetCacheStudentPaperDetailList(params Guid[] paperDetailIds)
        {
            List<StudentPaperDetailEntity> studentPapersDetails = new List<StudentPaperDetailEntity>(paperDetailIds.Length);
            foreach (var id in paperDetailIds)
            {
                var studentPaperDetail = await GetCacheStudentPaperDetail(id);
                if (studentPaperDetail != null)
                    studentPapersDetails.Add(studentPaperDetail);
            }
            return studentPapersDetails;
        }

        /// <summary>
        /// 通过试卷获取答题记录id
        /// </summary>
        /// <param name="studentPaperId"></param>
        /// <returns></returns>
        public static async Task<List<StudentPaperDetailEntity>> GetCacheStudentPaperDetailListByStudnetPaperId(params Guid[] studentPaperId)
        {
            var paperDetailIds = ExamDataHelper.GetStudentPaperDetailId(studentPaperId); //试卷答题记录
            return await GetCacheStudentPaperDetailList(paperDetailIds.ToArray());
        }
    }
}
