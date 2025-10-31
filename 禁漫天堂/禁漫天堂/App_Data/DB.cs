using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 禁漫天堂;
using 禁漫天堂.Models;
using System.Diagnostics;
namespace 禁漫天堂
{
    public class DB//数据库公共类，工具库（提供增删改查操作）
    {
        public SqlConnection Con = new SqlConnection();//连接对象
        public SqlCommand Com = new SqlCommand();//命令对象
        public SqlDataAdapter Da = new SqlDataAdapter();//数据适配器对象
        public DataSet Ds = new DataSet();//内存数据库


        public string GetConnectionString()
        {
            string ConStr;
            ConStr = System.Configuration.ConfigurationManager.AppSettings["Con"];
            return ConStr;//返回获取到的字符串
        }
        public DataSet GetDataTableBySql(string SqlStr)//数据库查询操作
        {
            Con.ConnectionString = GetConnectionString();//连接对象赋值
            Com.Connection = Con;//命令对象赋值
            Com.CommandText = SqlStr;
            Da.SelectCommand = Com;//数据适配器接管控制权


            try
            {
                Con.Open();
                Ds.Clear();
                Da.Fill(Ds);
                Con.Close();
            }
            catch (Exception)
            {

                Con.Close();
            }
            return Ds;//查询到结果
        }
        public DataSet GetDataTableBySql(string SqlStr, SqlParameter[] parameters)
        {
            Con.ConnectionString = GetConnectionString(); // 连接字符串赋值
            Com.Connection = Con; // 命令对象赋值
            Com.CommandText = SqlStr;
            // 如果有参数，添加到命令对象中
            Com.Parameters.Clear();
            if (parameters != null)
            {
                Com.Parameters.AddRange(parameters);
            }

            Da.SelectCommand = Com; // 数据适配器接管控制权
            try
            {
                Con.Open();
                Ds.Clear();
                Da.Fill(Ds); // 执行查询并将结果填充到 DataSet 中
                Con.Close();
            }
            
            catch (Exception)
            {
                Con.Close();
            }

            return Ds; // 返回查询结果
        }
        public Boolean UpdateDataBySql(string SqlStr)
        {
            Con.ConnectionString = GetConnectionString();//连接对象赋值
            Com.Connection = Con;//命令对象赋值
            Com.CommandText = SqlStr;
            try
            {
                Con.Open();
                Com.ExecuteNonQuery();//执行数据更新
                Con.Close();
                return true;//完成更新

            }
            catch (Exception)
            {
                Con.Close();
                return false;
            }
           
        }
        public bool UpdateDataBySql(string SqlStr, SqlParameter[] parameters)
        {
            Con.ConnectionString = GetConnectionString();
            Com.Connection = Con;
            Com.CommandText = SqlStr;

            if (parameters != null)
            {
                Com.Parameters.Clear();
                Com.Parameters.AddRange(parameters);
            }

            try
            {
                Con.Open();
                Com.ExecuteNonQuery();
                Con.Close();
                return true;
            }
            catch (Exception)
            {
                Con.Close();
                return false;
            }
        }

        public Comic GetComicDetails(string Id)
        {
            string sqlStr = "SELECT ComicID, 标题, 封面URL, 作者, 类型或分类, 评分, 简介, 发布日期 , ReadingLink FROM Comics WHERE ComicID = @ComicID";

            Con.ConnectionString = GetConnectionString();
            Com.Connection = Con;
            Com.CommandText = sqlStr;
            Com.Parameters.AddWithValue("@ComicID", Id); // 使用字符串类型的 comicId

            SqlDataReader reader = null;
            Comic comic = null;

            try
            {
                Con.Open();
                reader = Com.ExecuteReader();

                if (reader.Read())
                {
                    comic = new Comic()
                    {
                        ComicID = reader["ComicID"].ToString(),
                        标题 = reader["标题"].ToString(),
                        封面URL = reader["封面URL"].ToString(),
                        作者 = reader["作者"].ToString(),
                        类型或分类 = reader["类型或分类"].ToString(),
                        评分 = (decimal)reader["评分"],
                        简介 = reader["简介"].ToString(),
                        发布日期 = (DateTime)reader["发布日期"],
                        ReadingLink = reader["ReadingLink"].ToString() // 读取 ReadingLink
                    };
                }

                Con.Close();
            }
            catch (Exception)
            {
                Con.Close();
            }

            return comic;
        }
        public bool AddComicToFavorites(int userId, string comicId)
        {
            try
            {
                // 创建 SQL 连接
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    string query = "INSERT INTO ComicFavorites (UserId, ComicID) VALUES (@UserId, @ComicID)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);  // 用户 ID
                    cmd.Parameters.AddWithValue("@ComicID", comicId);  // 漫画 ID

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();  // 执行插入操作
                    conn.Close();

                    return result > 0;  // 返回成功与否
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<string> GetComicPages(string Id)
        {
            List<string> pages = new List<string>();
            string query = "SELECT PageURL FROM Comic页面 WHERE ComicID = @ComicID ORDER BY PageNumber";

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ComicID", Id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    pages.Add(reader["PageURL"].ToString());  // 获取每一页的 URL
                }

                conn.Close();
            }

            return pages;
        }
        public List<string> GetComicTags(string Id)
        {
            List<string> tags = new List<string>();
            string sqlQuery = @"
                SELECT t.标签Name
                FROM Comic标签 ct
                JOIN 标签 t ON ct.标签ID = t.标签ID
                WHERE ct.ComicID = @ComicID";

            SqlParameter[] parameters = {
            new SqlParameter("@ComicID", SqlDbType.NVarChar) { Value = Id }
            };
           
            DataSet ds = GetDataTableBySql(sqlQuery, parameters);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tags.Add(row["标签Name"].ToString());

                }
            }

            return tags;
        }


    }
}