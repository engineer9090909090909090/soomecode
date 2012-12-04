using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Soomes;

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
            + "GroupId integer NOT NULL,"
            + "GroupName1 varchar(100)," 
            + "Subject varchar(300) NOT NULL,"
            + "RedModel varchar(100),"
            + "DetailUrl varchar(500),"
            + "AbsImageUrl varchar(500),"
            + "AbsSummImageUrl varchar(500),"
            + "IsWindowProduct BOOLEAN,"
            + "GmtModified datetime)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AliProducts(GroupId);");
        }

        private void UpdateTable()
        {

        }

        public void DeleteProduct4GroupId(int groupId)
        {
            dbHelper.ExecuteNonQuery("delete from AliProducts where GroupId = " + groupId.ToString());
        }

        public void Insert(List<AliProduct> list)
        {
            string InsSql = @"INSERT INTO AliProducts(Id,Keywords, IsKeywords, Status, GroupId, GroupName1, Subject, RedModel, DetailUrl,AbsImageUrl, AbsSummImageUrl,IsWindowProduct, GmtModified)"
                            + "values(@Id,@Keywords, @IsKeywords, @Status, @GroupId, @GroupName1, @Subject, @RedModel, @DetailUrl,@AbsImageUrl, @AbsSummImageUrl,@IsWindowProduct, @GmtModified)";

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
                    new SQLiteParameter("@Subject",item.Subject),
                    new SQLiteParameter("@RedModel",item.RedModel),
                    new SQLiteParameter("@DetailUrl",item.DetailUrl),
                    new SQLiteParameter("@AbsImageUrl",item.AbsImageUrl), 
                    new SQLiteParameter("@AbsSummImageUrl",item.AbsSummImageUrl),
                    new SQLiteParameter("@IsWindowProduct",item.IsWindowProduct),
                    new SQLiteParameter("@GmtModified",item.GmtModified)
                };
                InsertParameters.Add(parameter);
            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }
    }
}
