using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;

namespace AliHelper
{
    class AliGroupDao
    {
        private SQLiteDBHelper dbHelper;

        public AliGroupDao(SQLiteDBHelper dbHelper)
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
            + "HasChildren BOOLEAN default 0,"   
            + "ChildrenCount integer NOT NULL,"
            + "ParentId integer NOT NULL,"
            + "Level integer");

            dbHelper.ExecuteNonQuery("Create Index  IF NOT EXISTS Index_key on AliGroups(ParentId);");
        }

        private void UpdateTable()
        {

        }


        public void Insert(List<AliGroup> list)
        {
            string InsSql = @"INSERT INTO RankInfo(Id,Name, HasChildren, ChildrenCount, Level,ParentId)"
                            + "values(@Id,@Name, @HasChildren, @ChildrenCount, @Level,@ParentId)";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
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
        }
    }
}
