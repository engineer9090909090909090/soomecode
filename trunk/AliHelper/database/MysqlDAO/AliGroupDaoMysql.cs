using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public class AliGroupDaoMysql
    {
        private SQLiteDBHelper dbHelper;

        public AliGroupDaoMysql(SQLiteDBHelper dbHelper)
        { 
            this.dbHelper = dbHelper;
            CreateTable();
            UpdateTable();
        }

        private void CreateTable()
        {

            dbHelper.ExecuteNonQuery(
              "CREATE TABLE IF NOT EXISTS AliGroups("
            + "Id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,"
            + "Name varchar(300) NOT NULL,"
            + "HasChildren BOOLEAN,"   
            + "ChildrenCount integer default 0,"
            + "ParentId integer NOT NULL,"
            + "Level integer default 0)");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AliGroups(ParentId);");
        }

        private void UpdateTable()
        {

        }


        public List<AliGroup> GetAliGroupList()
        {
            DataTable dt = dbHelper.ExecuteDataTable(
                  "SELECT Id, Name, HasChildren, ChildrenCount, Level, ParentId FROM AliGroups", null);
            List<AliGroup> list = new List<AliGroup>();
            foreach (DataRow row in dt.Rows)
            {
                AliGroup kw = new AliGroup();
                kw.Id = Convert.ToInt32(row["Id"]);
                kw.Name = (string)row["Name"];
                kw.HasChildren = Convert.ToBoolean(row["HasChildren"]);
                kw.ChildrenCount = Convert.ToInt32(row["ChildrenCount"]);
                kw.Level = Convert.ToInt32(row["Level"]);
                kw.ParentId = Convert.ToInt32(row["ParentId"]);
                list.Add(kw);
            }
            return list;
        }

        public List<int> GetAliGroupIdList()
        {
            List<int> ids = new List<int>();
            DataTable dt = dbHelper.ExecuteDataTable("SELECT Id FROM AliGroups", null);
            List<AliGroup> list = new List<AliGroup>();
            foreach (DataRow row in dt.Rows)
            {
                ids.Add(Convert.ToInt32(row["Id"]));
            }
            return ids;
        }

        public void Insert(List<AliGroup> list)
        {
            string InsSql = @"INSERT INTO AliGroups(Id,Name, HasChildren, ChildrenCount, Level,ParentId)"
                            + "values(@Id,@Name, @HasChildren, @ChildrenCount, @Level,@ParentId)";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            for (int i = 0; i < list.Count; i++)
            {
                AliGroup item = list[i];
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Id",item.Id),
                    new SQLiteParameter("@Name",item.Name), 
                    new SQLiteParameter("@HasChildren",item.HasChildren),
                    new SQLiteParameter("@ChildrenCount",item.ChildrenCount),
                    new SQLiteParameter("@Level",item.Level),
                    new SQLiteParameter("@ParentId",item.ParentId)
                };
                InsertParameters.Add(parameter);
            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
        }

        public void Update(List<AliGroup> list)
        {
            string UpdSql = @"Update AliGroups SET Name = @Name, HasChildren = @HasChildren, ChildrenCount = @ChildrenCount, "
                   + "Level = @Level, ParentId = @ParentId WHERE Id = @Id";
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            List<int> existGroupIds = GetAliGroupIdList();
            foreach (AliGroup item in list)
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Name",item.Name), 
                    new SQLiteParameter("@HasChildren",item.HasChildren),
                    new SQLiteParameter("@ChildrenCount",item.ChildrenCount),
                    new SQLiteParameter("@Level",item.Level),
                    new SQLiteParameter("@ParentId",item.ParentId),
                    new SQLiteParameter("@Id",item.Id)
                };
                UpdateParameters.Add(parameter);

            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(UpdSql, UpdateParameters);
            }
        }

        public void DeleteGroups(List<int> ids)
        {
            string DeleteSql = @"delete from AliGroups WHERE Id = @Id";
            List<SQLiteParameter[]> DeleteParameters = new List<SQLiteParameter[]>();
            List<int> existGroupIds = GetAliGroupIdList();
            foreach (int item in ids)
            {
                SQLiteParameter[] parameter = new SQLiteParameter[]
                {
                    new SQLiteParameter("@Id",item)
                };
                DeleteParameters.Add(parameter);

            }
            if (DeleteParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(DeleteSql, DeleteParameters);
            }
        }
    }
}
