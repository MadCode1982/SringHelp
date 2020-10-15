using Sring.BusinessModule.ExamManage;
using SringHelp.ExamEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// 创建答题记录
        /// </summary>
        /// <param name="studentPaperId"></param>
        /// <param name="studentPaperDetail"></param>
        /// <returns></returns>
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
        public static IEnumerable<PapersEntity> GetExamPapers(Guid examId)
        {
            StringBuilder sqlBulider = new StringBuilder();
            sqlBulider.AppendFormat(@"SELECT [PaperId]
                                            ,[PaperFormJson]
                                        FROM [dbo].[Exam_Papers]
                                        WHERE ExamId = '{0}'", examId);
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand()
            {
                Connection = conn,
                CommandText = @"SELECT [PaperId]
                                            ,[PaperFormJson]
                                        FROM [dbo].[Exam_Papers]
                                        WHERE ExamId = @examId"
            };
            sqlCommand.Parameters.Add("@examId", SqlDbType.UniqueIdentifier).Value = examId;

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
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = conn,
                CommandText = @"SELECT TOP (1) [ExamNumber]
                                        FROM [dbo].[Exam_StudentPapers]
                                        WHERE ExamId = @examId
                                        ORDER BY CreatedDate desc"
            };

            sqlCommand.Parameters.Add("@examId", SqlDbType.UniqueIdentifier).Value = examId;

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
            Stopwatch totalTimewatch = new Stopwatch();
            totalTimewatch.Start();

            var examPapers = GetExamPapers(examId).Select(d => new ExamPaper() { ExamId = examId, PaperFormJson = d.PaperFormJson, PaperId = d.PaperId }).ToList(); //考生试卷
            var lastExamNum = GetLastExamNum(examId); //最后答题卡号
            foreach (var paper in examPapers)
            {
                paper.CreateStudnetPaperDeatil(); //创建答题记录
            }

            var joinExamUserIds = GetSignExaUserIds(examId).ToArray();

            userIds = userIds.Where(d => !joinExamUserIds.Contains(d)).ToArray(); //未报名的考生

            var maxSignExamUserCount = int.Parse(ConfigurationManager.GetSection("MaxSignExamUserCount")); //每次报名最大人数
            int paperCount = 0;
            int detailsCount = 0;

            StringBuilder resBuilder = new StringBuilder();
            for (int startPage = 0; startPage <= (userIds.Length / maxSignExamUserCount); startPage++)
            {
                List<StudentPapersEntity> resultPapers = new List<StudentPapersEntity>(userIds.Length); //要保存的试卷
                List<StudentPaperDetailEntity> resultPaperDetails = new List<StudentPaperDetailEntity>(userIds.Length * examPapers.FirstOrDefault().StudentPaperDetails.Count); //要保存的答题记录

                Random random = new Random(Guid.NewGuid().GetHashCode());
                foreach (var user in userIds.Skip(startPage * maxSignExamUserCount).Take(maxSignExamUserCount))
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
                try
                {
                    resBuilder.Append(BulkInsertStudentPapers(resultPapers.ToArray()));

                }
                catch (Exception e)
                {

                    throw new Exception("批量写入试卷出错！" + resultPapers.Count.ToString() + e.Message);
                }
                try
                {
                    resBuilder.Append(BulkInsertStudentPaperDetails(resultPaperDetails.ToArray()));

                }
                catch (Exception e)
                {

                    throw new Exception("批量写入答题记录出错出错！"+ resultPaperDetails.Count.ToString() + e.Message);
                }
                resBuilder.AppendLine();
                paperCount += resultPapers.Count;
                detailsCount += resultPaperDetails.Count;
            }

            totalTimewatch.Stop();
            return $"共创建试卷:{paperCount} 共创建答题记录:{detailsCount} 共耗时（毫秒）：{totalTimewatch.ElapsedMilliseconds} /n {resBuilder}";
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

            return $"Exam_StudentPapers 创建试卷】{studentPapers.Length}】份耗时（毫秒）:{time1} 保存到数据库耗时（毫秒）:{stopwatch.ElapsedMilliseconds}";
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

            return $"Exam_StudentPaperDetail  创建答题记录【{studentPaperDetails.Length}】份耗时（耗时）:{time1} 保存到数据库耗时（毫秒）:{stopwatch.ElapsedMilliseconds}";
        }

        /// <summary>
        /// 试卷数据到datatable
        /// </summary>
        /// <param name="studentPapers"></param>
        /// <returns></returns>
        public static DataTable StudentPaperToDataTable(params StudentPapersEntity[] studentPapers)
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
        public static DataTable PaperDetailToDataTable(params StudentPaperDetailEntity[] studentPaperDetails)
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
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT [OrganizeId] ,[FullName] FROM [dbo].[Base_Organize]", conn);
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

            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT [OrganizeId] ,[FullName] FROM [dbo].[Base_Organize] ", conn);
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                sqlCommand.CommandText += $"WHERE FullName LIKE @searchValue";
                sqlCommand.Parameters.Add("@searchValue", SqlDbType.NVarChar).Value = $"%{searchValue.Trim()}%";
            }
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

        public static DataTable GetExamTable(Guid? orgId, string searchValue)
        {
            var examTable = new DataTable("Exams");
            examTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("ExamId",typeof(Guid)),
                new DataColumn("ExamName",typeof(string))
            });

            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT TOP (30) [ExamId], [ExamName] FROM [dbo].[Exam_Content] WHERE 1=1 ", conn);
            if (orgId.HasValue)
            {
                sqlCommand.CommandText += " AND OrganizeId = @orgId ";
                sqlCommand.Parameters.Add("@orgId", SqlDbType.UniqueIdentifier).Value = orgId;
            }
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                sqlCommand.CommandText += " AND ExamName like @examName ";
                sqlCommand.Parameters.Add("@examName", SqlDbType.VarChar).Value = "%" + searchValue + "%";
            }
            sqlCommand.CommandText += " ORDER BY CreatedDate DESC";
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
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT [UserId] FROM [dbo].[Exam_StudentPapers] WHERE ExamId = @examId ", conn);
            sqlCommand.Parameters.Add("@examId", SqlDbType.UniqueIdentifier).Value = examId;
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

            try
            {
                if (orgId.HasValue)
                {
                    userIds = GetUserIds(orgId.Value).ToArray();
                }
                else
                {
                    userIds = GetUserIds().ToArray();
                }
            }
            catch (Exception e)
            {

                throw new Exception("获取机构用户出错" + e.Message + e.InnerException?.Message + orgId.ToString());
            }
            

            return SignUserToExam(examId, userIds);
        }

        /// <summary>
        /// 批量读取考生试卷
        /// </summary>
        /// <param name="paperIds"></param>
        /// <returns></returns>
        public static IEnumerable<StudentPapersEntity> GetStudentPapers(params Guid[] paperIds)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT [StudentPaperId]
                                ,[ExamId]
                                ,[PaperId]
                                ,[UserId]
                                ,[CreatedDate]
                                ,[ExamNumber]
                            FROM [dbo].[Exam_StudentPapers]
                            WHERE ");
            sqlBuilder.AppendJoin(" OR ", paperIds.Select(d => $" StudentPaperId = '{d}' "));

            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new StudentPapersEntity()
                {
                    StudentPaperId = Guid.Parse(reader["StudentPaperId"].ToString()),
                    ExamId = Guid.Parse(reader["ExamId"].ToString()),
                    PaperId = Guid.Parse(reader["PaperId"].ToString()),
                    UserId = Guid.Parse(reader["UserId"].ToString()),
                    CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                    ExamNumber = int.Parse(reader["ExamNumber"].ToString())
                };
            }
        }

        /// <summary>
        /// 批量读取考试答题记录
        /// </summary>
        /// <param name="paperDetaidIds"></param>
        /// <returns></returns>
        public static IEnumerable<StudentPaperDetailEntity> GetStudentPaperDetails(params Guid[] studentPaperIds)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT [StudentPaperDetailId]
                                ,[StudentPaperId]
                                ,[QuestionId]
                                ,[QuestionType]
                                ,[Score]
                                ,[Status]
                                ,[QuestionJson]
                            FROM [dbo].[Exam_StudentPaperDetail]   
                            WHERE ");
            sqlBuilder.AppendJoin(" OR ", studentPaperIds.Select(d => $" StudentPaperId = '{d}' "));

            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return new StudentPaperDetailEntity()
                {
                    StudentPaperDetailId = Guid.Parse(reader["StudentPaperDetailId"].ToString()),
                    StudentPaperId = Guid.Parse(reader["StudentPaperId"].ToString()),
                    QuestionId = Guid.Parse(reader["QuestionId"].ToString()),
                    QuestionType = int.Parse(reader["QuestionType"].ToString()),
                    Score = double.Parse(reader["Score"].ToString()),
                    DeleteStatus = false,
                    Status = int.Parse(reader["Status"].ToString()),
                    QuestionJson = reader["QuestionJson"].ToString(),
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now
                };
            }
        }

        /// <summary>
        /// 获取考试试卷id
        /// </summary>
        /// <param name="examId"></param>
        /// <returns>StudentPaperId</returns>
        public static IEnumerable<Guid> GetExamStudentPaperIds(Guid examId)
        {
            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT [StudentPaperId] FROM [dbo].[Exam_StudentPapers]  WHERE ExamId = @examId", conn);
            sqlCommand.Parameters.Add("@examId", SqlDbType.UniqueIdentifier).Value = examId;
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return Guid.Parse(reader["StudentPaperId"].ToString());
            }
        }

        /// <summary>
        /// 获取试卷答题记录id
        /// </summary>
        /// <param name="studentPaperId"></param>
        /// <returns>StudentPaperDetailId</returns>
        public static IEnumerable<Guid> GetStudentPaperDetailId(params Guid[] studentPaperId)
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT  [StudentPaperDetailId] FROM [dbo].[Exam_StudentPaperDetail] WHERE ");
            sqlBuilder.AppendJoin(" OR ", studentPaperId.Select(d => $" StudentPaperId = '{d}' "));

            using SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), conn);
            conn.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return Guid.Parse(reader["StudentPaperDetailId"].ToString());
            }
        }

        /// <summary>
        /// 批量更新答题记录
        /// </summary>
        /// <param name="newdata"></param>
        public static int BulkUpdatePaperDetailData(DataTable newdata)
        {
            using SqlConnection conn = new SqlConnection(ConnStr);
            using SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.SelectCommand = new SqlCommand(@"SELECT [StudentPaperDetailId]
                                ,[DeleteStatus]
                                ,[QuestionId]
                                ,[QuestionJson]
                                ,[QuestionType]
                                ,[Score]
                                ,[Status]
                                ,[StudentPaperId]
                                ,[CreatedDate]
                            FROM [dbo].[Exam_StudentPaperDetail]", conn);
            adapter.InsertCommand = commandBuilder.GetUpdateCommand();
            foreach (DataColumn column in newdata.Columns)
            {
            }

            return adapter.Update(newdata);
        }
    }
}