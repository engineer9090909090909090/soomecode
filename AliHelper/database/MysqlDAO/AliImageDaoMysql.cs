using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public class AliImageDaoMysql
    {
        private SQLiteDBHelper dbHelper;

        public AliImageDaoMysql(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliImages("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "GroupId integer,"
            + "Width integer,"
            + "Height integer,"
            + "FileSize integer,"
            + "ReferenceCount integer,"
            + "MemberSeq integer,"
            + "CompanyId integer,"
            + "MemberId varchar(200),"
            + "MemberName varchar(200),"
            + "Status varchar(50),"
            + "HashCode varchar(50),"
            + "FileName varchar(500),"
            + "DisplayNameUtf8 varchar(200),"
            + "Url varchar(500),"
            + "LocationUrl varchar(500))");
            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AliImages(GroupId);");
        }


        private void UpdateTable()
        {

        }

        
        public QueryObject<ImageInfo> GetAliImageList(QueryObject<ImageInfo> query)
        {
            string sql = "select Status, Width, HashCode, FileSize, Id, GroupId, Height, ReferenceCount, ";
            sql = sql + "MemberSeq, FileName, MemberId, MemberName, CompanyId, DisplayNameUtf8, Url, LocationUrl ";
            sql = sql + "FROM AliImages where 1 = 1 ";
            if (query.Condition != null)
            {
                if (query.Condition.GroupId != -1)
                {
                    sql = sql + "and GroupId = " + query.Condition.GroupId + " ";
                }
                if (!string.IsNullOrEmpty(query.Condition.DisplayNameUtf8))
                {
                    sql = sql + "and DisplayNameUtf8 like '%" + query.Condition.DisplayNameUtf8 + "%' ";
                }
            }

            query.RecordCount = dbHelper.GetItemCount(sql, null);

            sql = sql + "limit " + query.Page + ", " + query.PageSize;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            List<ImageInfo> list = new List<ImageInfo>();
            foreach (DataRow row in dt.Rows)
            {
                ImageInfo info = new ImageInfo();
                info.Id = Convert.ToInt32(row["Id"]);
                info.Status = (string)row["Status"];
                info.Width = Convert.ToInt32(row["Width"]);
                info.HashCode = (string)row["HashCode"];
                info.FileSize = Convert.ToInt32(row["FileSize"]);
                info.GroupId = Convert.ToInt32(row["GroupId"]);
                info.Height = Convert.ToInt32(row["Height"]);
                info.ReferenceCount = Convert.ToInt32(row["ReferenceCount"]);
                info.MemberSeq = Convert.ToInt32(row["MemberSeq"]);
                info.FileName = (string)row["FileName"];
                info.MemberId = (string)row["MemberId"];
                info.MemberName = (string)row["MemberName"];
                info.CompanyId = Convert.ToInt32(row["CompanyId"]);
                info.DisplayNameUtf8 = (string)row["DisplayNameUtf8"];
                info.Url = (string)row["Url"];
                info.LocationUrl = (string)row["LocationUrl"];
                list.Add(info);
            }
            query.Result = list;
            return query;
        }


        public void Insert(List<ImageInfo> list)
        {
            string InsSql = @"INSERT INTO AliImages(Status, Width, HashCode, FileSize, Id, GroupId, Height, ReferenceCount, " +
             "MemberSeq, FileName, MemberId, MemberName, CompanyId, DisplayNameUtf8, Url, LocationUrl)"
                            + "values(@Status, @Width, @HashCode, @FileSize, @Id, @GroupId, @Height, @ReferenceCount, " +
             "@MemberSeq, @FileName, @MemberId, @MemberName, @CompanyId, @DisplayNameUtf8, @Url, @LocationUrl)";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            for (int i = 0; i < list.Count; i++)
            {
                ImageInfo item = list[i];
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Status",item.Status),
                    new SQLiteParameter("@Width",item.Width),
                    new SQLiteParameter("@HashCode",item.HashCode),
                    new SQLiteParameter("@FileSize",item.FileSize),
                    new SQLiteParameter("@Id",item.Id),
                    new SQLiteParameter("@GroupId",item.GroupId),
                    new SQLiteParameter("@Height",item.Height),
                    new SQLiteParameter("@ReferenceCount",item.ReferenceCount),
                    new SQLiteParameter("@MemberSeq",item.MemberSeq),
                    new SQLiteParameter("@FileName",item.FileName),
                    new SQLiteParameter("@MemberId",item.MemberId),
                    new SQLiteParameter("@MemberName",item.MemberName), 
                    new SQLiteParameter("@CompanyId",item.CompanyId),
                    new SQLiteParameter("@DisplayNameUtf8",item.DisplayNameUtf8),
                    new SQLiteParameter("@Url",item.Url),
                    new SQLiteParameter("@LocationUrl",item.LocationUrl)
                };
                InsertParameters.Add(parameter);
            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }

        public void DeleteAllImages()
        {
            dbHelper.ExecuteNonQuery("delete from AliImages");
        }

    }
}
