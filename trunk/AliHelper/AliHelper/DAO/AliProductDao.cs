using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Soomes;
using System.Data;

namespace AliHelper.DAO
{
    public class AliProductDao
    {
        private SQLiteDBHelper dbHelper;

        public AliProductDao(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliProducts("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Keywords varchar(300) NOT NULL,"
            + "IsKeywords BOOLEAN,"
            + "Status varchar(30),"
            + "GroupId varchar(100) NOT NULL,"
            + "GroupName1 varchar(100),"
            + "GroupName2 varchar(100),"
            + "GroupName3 varchar(100)," 
            + "Subject varchar(300) NOT NULL,"
            + "RedModel varchar(100),"
            + "DetailUrl varchar(500),"
            + "AbsImageUrl varchar(500),"
            + "AbsSummImageUrl varchar(500),"
            + "IsWindowProduct BOOLEAN,"
            + "GmtModified varchar(50),"
            + "Type varchar(50),"
            + "IsDisplay varchar(50),"
            + "OwnerMemberId varchar(50),"
            + "OwnerMemberName varchar(50),"
            + "IsLowScore BOOLEAN(50)"
            + ")");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AliProducts(GroupId);");
        }

        private void UpdateTable()
        {

        }

        public void DeleteProduct4GroupId(int groupId)
        {
            dbHelper.ExecuteNonQuery("delete from AliProducts where GroupId like '%" + groupId.ToString() + "%'");
        }

        public void Insert(List<AliProduct> list)
        {
            string InsSql = @"INSERT INTO AliProducts(Id,Keywords, IsKeywords, Status, GroupId,"
                + " GroupName1,GroupName2,GroupName3, Subject, RedModel, DetailUrl,AbsImageUrl,AbsSummImageUrl,IsWindowProduct,  "
                +"GmtModified, Type, IsDisplay, OwnerMemberId, OwnerMemberName, IsLowScore)"
                + "values(@Id,@Keywords, @IsKeywords, @Status, @GroupId, @GroupName1,@GroupName2,"
                + "@GroupName3, @Subject, @RedModel, @DetailUrl,@AbsImageUrl, @AbsSummImageUrl,@IsWindowProduct,"
                + " @GmtModified, @Type, @IsDisplay, @OwnerMemberId, @OwnerMemberName, @IsLowScore)";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            for (int i = 0; i < list.Count; i++)
            {
                AliProduct item = list[i];
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Id",item.Id),
                    new SQLiteParameter("@Keywords",item.Keywords), 
                    new SQLiteParameter("@IsKeywords",item.IsKeywords),
                    new SQLiteParameter("@Status",item.Status),
                    new SQLiteParameter("@GroupId",item.GroupId),
                    new SQLiteParameter("@GroupName1",item.GroupName1), 
                    new SQLiteParameter("@GroupName2",item.GroupName2), 
                    new SQLiteParameter("@GroupName3",item.GroupName3), 
                    new SQLiteParameter("@Subject",item.Subject),
                    new SQLiteParameter("@RedModel",item.RedModel),
                    new SQLiteParameter("@DetailUrl",item.DetailUrl),
                    new SQLiteParameter("@AbsImageUrl",item.AbsImageUrl), 
                    new SQLiteParameter("@AbsSummImageUrl",item.AbsSummImageUrl),
                    new SQLiteParameter("@IsWindowProduct",item.IsWindowProduct),
                    new SQLiteParameter("@GmtModified",item.GmtModified),
                    new SQLiteParameter("@Type",item.@Type),
                    new SQLiteParameter("@IsDisplay",item.@IsDisplay),
                    new SQLiteParameter("@OwnerMemberId",item.@OwnerMemberId),
                    new SQLiteParameter("@OwnerMemberName",item.@OwnerMemberName),
                    new SQLiteParameter("@IsLowScore",item.@IsLowScore)
                };
                InsertParameters.Add(parameter);
            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }


        public List<AliProduct> GetAliProductList(int GroupId)
        {
            string sql = "SELECT Id,Keywords, IsKeywords, Status, GroupId,"
                + " GroupName1,GroupName2,GroupName3, Subject, RedModel, DetailUrl,AbsImageUrl,AbsSummImageUrl,IsWindowProduct,  "
                + "GmtModified, Type, IsDisplay, OwnerMemberId, OwnerMemberName, IsLowScore from AliProducts where 1 = 1";
            if (GroupId > 0)
            {
                sql = sql + " and GroupId like '%"+ GroupId.ToString() +"%'";
            }
            DataTable dt = dbHelper.ExecuteDataTable(sql , null);
            List<AliProduct> list = new List<AliProduct>();
            foreach (DataRow row in dt.Rows)
            {
                AliProduct kw = new AliProduct();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.Keywords = (string)row["Keywords"];
                kw.IsKeywords = Convert.ToBoolean(row["IsKeywords"]);
                kw.Status = (string)row["Status"];
                kw.GroupName1 = (string)row["GroupName1"];
                kw.GroupName2 = (string)row["GroupName2"];
                kw.GroupName3 = (string)row["GroupName3"];
                kw.Subject = (string)row["Subject"];
                kw.RedModel = (string)row["RedModel"];
                kw.DetailUrl = (string)row["DetailUrl"];
                kw.AbsImageUrl = (string)row["AbsImageUrl"];
                kw.AbsSummImageUrl = (string)row["AbsSummImageUrl"];
                kw.IsWindowProduct = Convert.ToBoolean(row["IsWindowProduct"]);
                kw.GmtModified = (string)row["GmtModified"];
                kw.Type = (string)row["Type"];
                kw.IsDisplay = (string)row["IsDisplay"];
                kw.OwnerMemberId = (string)row["OwnerMemberId"];
                kw.OwnerMemberName = (string)row["OwnerMemberName"];
                kw.IsLowScore = (string)row["IsLowScore"];
                list.Add(kw);
            }
            return list;
        }
    }
}
