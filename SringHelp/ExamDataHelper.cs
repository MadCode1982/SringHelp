using Sring.BusinessModule.ExamManage;
using SringHelp.ExamEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SringHelp
{
    /// <summary>
    /// 考试数据
    /// </summary>
    public class ExamDataHelper
    {
        private static string ConnStr { get; set; }

        static ExamDataHelper()
        {
            ConnStr = ConfigurationManager.GetSection("SqlServerStr");
        }

        /// <summary>
        /// 通过考试试卷创建考生试卷
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <param name="paper"></param>
        /// <param name="examNum"></param>
        /// <returns></returns>
        public static StudentPapersEntity CreateStudentPaperFromPaperJson(Guid examId, Guid userId, PapersEntity paper, int examNum)
        {
            return new StudentPapersEntity()
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

        public static StudentPaperDetailEntity CreateStudentPaperDetailFromDetail(Guid studentPaperId, StudentPaperDetailEntity studentPaperDetail)
        {
            return new StudentPaperDetailEntity()
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

        /// <summary>
        /// 获取考试试卷
        /// </summary>
        public static IEnumerable<PapersEntity> GetExamPapers(string examId)
        {
            StringBuilder sqlBulider = new StringBuilder();
            sqlBulider.AppendFormat(@"SELECT [PaperId]
                                            ,[PaperFormJson]
                                        FROM [dbo].[Exam_Papers]
                                        WHERE ExamId = '{0}'", examId);
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBulider.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new PapersEntity()
                {
                    PaperId = new Guid(reader["PaperId"].ToString()),
                    PaperFormJson = reader["PaperFormJson"].ToString()
                };
            }
        }

        /// <summary>
        /// 获取考试最后考试号
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public static int GetLastExamNum(Guid examId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat(@"SELECT TOP (1) [ExamNumber]
                                        FROM [edu].[dbo].[Exam_StudentPapers]
                                        WHERE ExamId =  '{0}'
                                        ORDER BY CreatedDate desc", examId);
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return int.Parse(reader["ExamNumber"].ToString());
            }
            else
            {
                return 10000;
            }
        }

        //报名 考生id 创建 试卷答题卡 批量保存

        /// <summary>
        /// 报名考生
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        public static string SignUserToExam(Guid examId, params Guid[] userIds)
        {
            var examPapers = GetExamPapers(examId.ToString()).Select(d => new ExamPaper() { ExamId = examId, PaperFormJson = d.PaperFormJson, PaperId = d.PaperId }).ToList(); //考生试卷
            var lastExamNum = GetLastExamNum(examId); //最后答题卡号
            foreach (var paper in examPapers)
            {
                paper.CreateStudnetPaperDeatil(); //创建答题记录
            }

            var joinExamUserIds = GetSignExaUserIds(examId).ToArray();

            userIds = userIds.Where(d => !joinExamUserIds.Contains(d)).ToArray(); //未报名的考生
            List<StudentPapersEntity> resultPapers = new List<StudentPapersEntity>(userIds.Length); //要保存的试卷
            List<StudentPaperDetailEntity> resultPaperDetails = new List<StudentPaperDetailEntity>(userIds.Length * examPapers.FirstOrDefault().StudentPaperDetails.Count); //要保存的答题记录

            Random random = new Random(Guid.NewGuid().GetHashCode());
            foreach (var user in userIds)
            {
                var selectExamPaper = examPapers[random.Next(0, examPapers.Count)];
                var studentPaper = CreateStudentPaperFromPaperJson(examId, user, selectExamPaper, lastExamNum++); //创建考生试卷
                resultPapers.Add(studentPaper);
                foreach (var paperDetail in selectExamPaper.StudentPaperDetails)
                {
                    var studentPaperDetail = CreateStudentPaperDetailFromDetail(studentPaper.StudentPaperId.Value, paperDetail); //答题记录
                    resultPaperDetails.Add(studentPaperDetail);
                }
            }

            string res1 = BulkInsertStudentPapers(resultPapers.ToArray());
            string res2 = BulkInsertStudentPaperDetails(resultPaperDetails.ToArray());

            return $"PaperCount:{resultPapers.Count} DetailCount:{resultPaperDetails.Count} {res1} {res2}";
        }

        /// <summary>
        /// 批量插入试卷
        /// </summary>
        /// <param name="studentPapers"></param>
        public static string BulkInsertStudentPapers(params StudentPapersEntity[] studentPapers)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var studentPaperTable = StudentPaperToDataTable(studentPapers);
            stopwatch.Stop();
            var time1 = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            using SqlConnection conn = new SqlConnection(ConnStr);
            using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn)
            {
                DestinationTableName = "Exam_StudentPapers",
                BatchSize = studentPaperTable.Rows.Count
            };
            foreach (DataColumn column in studentPaperTable.Columns)
            {
                sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }

            conn.Open();
            sqlBulkCopy.WriteToServer(studentPaperTable);
            stopwatch.Stop();

            return $"Exam_StudentPapers time1:{time1} time2:{stopwatch.ElapsedMilliseconds}";
        }

        /// <summary>
        /// 批量插入答题记录
        /// </summary>
        /// <param name="studentPapers"></param>
        public static string BulkInsertStudentPaperDetails(params StudentPaperDetailEntity[] studentPaperDetails)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var studentPaperDetailsTable = PaperDetailToDataTable(studentPaperDetails);
            stopwatch.Stop();
            var time1 = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            using SqlConnection conn = new SqlConnection(ConnStr);
            using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn)
            {
                DestinationTableName = "Exam_StudentPaperDetail",
                BatchSize = studentPaperDetailsTable.Rows.Count
            };
            foreach (DataColumn column in studentPaperDetailsTable.Columns)
            {
                sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }

            conn.Open();
            sqlBulkCopy.WriteToServer(studentPaperDetailsTable);
            stopwatch.Stop();

            return $"Exam_StudentPaperDetail  time1:{time1} time2:{stopwatch.ElapsedMilliseconds}";
        }

        /// <summary>
        /// 试卷数据到datatable
        /// </summary>
        /// <param name="studentPapers"></param>
        /// <returns></returns>
        private static DataTable StudentPaperToDataTable(params StudentPapersEntity[] studentPapers)
        {
            DataTable dataTable = new DataTable("Exam_StudentPapers");
            dataTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("StudentPaperId"){ DataType = typeof(Guid) },
                new DataColumn("ExamId"){ DataType = typeof(Guid) },
                new DataColumn("PaperId"){ DataType = typeof(Guid) },
                new DataColumn("StudentPaperFormJson"){ DataType = typeof(string) },
                new DataColumn("UserId"){ DataType = typeof(Guid) },
                new DataColumn("CreatedDate"){ DataType = typeof(DateTime) },
                new DataColumn("ExamNumber"){ DataType = typeof(int) }
            });

            foreach (var paper in studentPapers)
            {
                var row = dataTable.NewRow();
                row["StudentPaperId"] = paper.StudentPaperId.Value;
                row["ExamId"] = paper.ExamId.Value;
                row["PaperId"] = paper.PaperId.Value;
                row["StudentPaperFormJson"] = paper.StudentPaperFormJson;
                row["UserId"] = paper.UserId.Value;
                row["CreatedDate"] = paper.CreatedDate.Value;
                row["ExamNumber"] = paper.ExamNumber.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        /// <summary>
        /// 答题记录到datatable
        /// </summary>
        /// <param name="studentPaperDetails"></param>
        /// <returns></returns>
        private static DataTable PaperDetailToDataTable(params StudentPaperDetailEntity[] studentPaperDetails)
        {
            DataTable dataTable = new DataTable("Exam_StudentPaperDetail");
            dataTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("StudentPaperDetailId"){ DataType = typeof(Guid) },
                new DataColumn("DeleteStatus"){ DataType = typeof(bool) },
                new DataColumn("QuestionId"){ DataType = typeof(Guid) },
                new DataColumn("QuestionJson"){ DataType = typeof(string) },
                new DataColumn("QuestionType"){ DataType = typeof(int) },
                new DataColumn("Score"){ DataType = typeof(double) },
                new DataColumn("Status"){ DataType = typeof(int) },
                new DataColumn("StudentPaperId"){ DataType = typeof(Guid) },
                new DataColumn("CreatedDate"){ DataType = typeof(DateTime) },
            });

            foreach (var detail in studentPaperDetails)
            {
                var row = dataTable.NewRow();
                row["StudentPaperDetailId"] = detail.StudentPaperDetailId.Value;
                row["DeleteStatus"] = detail.DeleteStatus.Value;
                row["QuestionId"] = detail.QuestionId.Value;
                row["QuestionJson"] = detail.QuestionJson;
                row["QuestionType"] = detail.QuestionType.Value;
                row["Score"] = detail.Score.Value;
                row["Status"] = detail.Status.Value;
                row["StudentPaperId"] = detail.StudentPaperId.Value;
                row["CreatedDate"] = detail.CreatedDate.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Guid> GetUserIds(params Guid[] orgIds)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT  [UserId] FROM [dbo].[Base_User] ");
            if (orgIds != null && orgIds.Length > 0)
            {
                sqlBuilder.Append($" WHERE {string.Join(" OR ", orgIds.Select(d => $" OrganizeId = '{d}' "))}");
            }
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new Guid(reader["UserId"].ToString());
            }
        }

        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<(Guid OrgId, string OrgName)> GetOrgIdNames()
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT [OrganizeId] ,[FullName] FROM [dbo].[Base_Organize] ");
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return (new Guid(reader["OrganizeId"].ToString()), reader["FullName"].ToString());
            }
        }

        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <returns>OrganizeId,FullName</returns>
        public static DataTable GetOrgIdNamesTable(string searchValue)
        {
            var orgTable = new DataTable("Orgs");
            orgTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("OrganizeId",typeof(Guid)),
                new DataColumn("FullName",typeof(string))
            });
            orgTable.DefaultView.Sort = "FullName ASC ";

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT [OrganizeId] ,[FullName] FROM [dbo].[Base_Organize] ");
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                sqlBuilder.Append($"WHERE FullName LIKE '%{searchValue.Trim()}%'");
            }
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var row = orgTable.NewRow();
                row[0] = new Guid(reader["OrganizeId"].ToString());
                row[1] = reader["FullName"].ToString();
                orgTable.Rows.Add(row);
            }
            return orgTable;
        }

        public static DataTable GetExamTable(string orgId)
        {
            var examTable = new DataTable("Exams");
            examTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("ExamId",typeof(Guid)),
                new DataColumn("ExamName",typeof(string))
            });
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT TOP (30) [ExamId], [ExamName] FROM [dbo].[Exam_Content] ");
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sqlBuilder.Append($" WHERE OrganizeId ='{orgId}' ");
            }
            sqlBuilder.Append(" ORDER BY CreatedDate DESC ");
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var row = examTable.NewRow();
                row[0] = new Guid(reader["ExamId"].ToString());
                row[1] = reader["ExamName"].ToString();
                examTable.Rows.Add(row);
            }
            return examTable;
        }

        public static IEnumerable<Guid> GetUserIdsByAccount(params string[] accounts)
        {
            if (accounts == null || accounts.Length == 0)
                yield break;
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"SELECT [UserId] FROM [dbo].[Base_User] WHERE {string.Join(" OR ", accounts.Select(d => $" Account = '{d}' "))} ");
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new Guid(reader["UserId"].ToString());
            }
        }

        /// <summary>
        /// 获取已报名的考生id
        /// </summary>
        /// <param name="examId">examId</param>
        /// <returns></returns>
        private static IEnumerable<Guid> GetSignExaUserIds(Guid examId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append($"SELECT [UserId] FROM [dbo].[Exam_StudentPapers] WHERE ExamId = '{examId}'");
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new Guid(reader["UserId"].ToString());
            }
        }

        /// <summary>
        /// 报名excel考生
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="fileName"></param>
        public static string SignExcelUser(Guid examId, string fileName)
        {
            var userAccounts = ExcelHelper.GetWorkBookFromFile(fileName).GetUserAccounts().ToArray();
            var userIds = GetUserIdsByAccount(userAccounts).ToArray();
            return SignUserToExam(examId, userIds);
        }

        /// <summary>
        /// 报名机构用户
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static string SignOrgUser(Guid examId, Guid? orgId)
        {

            Guid[] userIds;
            if (orgId.HasValue)
            {
                userIds = GetUserIds(orgId.Value).ToArray();
            }
            else
            {
                userIds = GetUserIds().ToArray();
            }

            return SignUserToExam(examId, userIds);
        }
    }
}