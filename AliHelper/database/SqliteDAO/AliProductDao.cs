﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using Soomes;
using System.Data;

namespace Database
{
    public class AliProductDao : IAliProductDao
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
            + "Id integer NOT NULL PRIMARY KEY UNIQUE,"
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
            + "IsLowScore varchar(50)"
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

        public void InsertOrUpdate(List<AliProduct> list)
        {
            string InsSql = @"INSERT INTO AliProducts(Id, Keywords, IsKeywords, Status, GroupId,"
                + " GroupName1,GroupName2,GroupName3, Subject, RedModel, DetailUrl,AbsImageUrl,AbsSummImageUrl,IsWindowProduct,  "
                +"GmtModified, Type, IsDisplay, OwnerMemberId, OwnerMemberName, IsLowScore)"
                + "values(@Id,@Keywords, @IsKeywords, @Status, @GroupId, @GroupName1,@GroupName2,"
                + "@GroupName3, @Subject, @RedModel, @DetailUrl,@AbsImageUrl, @AbsSummImageUrl,@IsWindowProduct,"
                + " @GmtModified, @Type, @IsDisplay, @OwnerMemberId, @OwnerMemberName, @IsLowScore)";

            string UpdSql = @"Update AliProducts SET Keywords = @Keywords, IsKeywords=@IsKeywords, Status = @Status, GroupId=@GroupId,"
                + " GroupName1=@GroupName1,GroupName2=@GroupName2,GroupName3=@GroupName3, Subject=@Subject, RedModel=@RedModel,"
                + " DetailUrl=@DetailUrl,AbsImageUrl=@AbsImageUrl,AbsSummImageUrl=@AbsSummImageUrl,IsWindowProduct=@IsWindowProduct,  "
                + "GmtModified=@GmtModified, Type=@Type, IsDisplay=@IsDisplay, OwnerMemberId = @OwnerMemberId,"
                + "OwnerMemberName = @OwnerMemberName, IsLowScore = @IsLowScore WHERE Id = @Id";

            string ExistRecordSql = "SELECT count(1) FROM AliProducts WHERE Id = ";

            List<SQLiteParameter[]> InsertParameters = new List<SQLiteParameter[]>();
            List<SQLiteParameter[]> UpdateParameters = new List<SQLiteParameter[]>();
            foreach (AliProduct item in list)
            {
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

                int record = Convert.ToInt32(dbHelper.ExecuteScalar(ExistRecordSql + item.Id, null));
                if (record == 0)
                {
                    InsertParameters.Add(parameter);
                }
                else {

                    UpdateParameters.Add(parameter);
                }
            }
            if (InsertParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(InsSql, InsertParameters);
            }
            if (UpdateParameters.Count > 0)
            {
                dbHelper.ExecuteNonQuery(UpdSql, UpdateParameters);
            }
        }


        public QueryObject<AliProduct> GetAliProductList(QueryObject<AliProduct> query)
        {
            string sql = "SELECT Id,Keywords, IsKeywords, Status, GroupId,"
                + " GroupName1,GroupName2,GroupName3, Subject, RedModel, DetailUrl,AbsImageUrl,AbsSummImageUrl,IsWindowProduct,  "
                + "GmtModified, Type, IsDisplay, OwnerMemberId, OwnerMemberName, IsLowScore from AliProducts where 1 = 1";
            int GroupId = Convert.ToInt32(query.Condition.GroupId);
            if (GroupId > 0)
            {
                sql = sql + " and GroupId like '%" + GroupId.ToString() + "%'";
            }
            else {
                bool IsWindowProduct = query.Condition.IsWindowProduct;
                if (IsWindowProduct)
                {
                    sql = sql + " and IsWindowProduct = 1";
                }
            }

            query.RecordCount = dbHelper.GetItemCount(sql, null);
            sql = sql + " order by GmtModified desc limit " + query.Start + ", " + query.PageSize;
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
            query.Result = list;
            return query;
        }


        public AliProduct GetAliProduct(int Id)
        {
            string sql = "SELECT Id,Keywords, IsKeywords, Status, GroupId,"
                + " GroupName1,GroupName2,GroupName3, Subject, RedModel, DetailUrl,AbsImageUrl,AbsSummImageUrl,IsWindowProduct,  "
                + "GmtModified, Type, IsDisplay, OwnerMemberId, OwnerMemberName, IsLowScore from AliProducts where id = " + Id;
            DataTable dt = dbHelper.ExecuteDataTable(sql, null);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
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
                return kw;
            }
            return null;
        }

        public bool IsNeedUpdateDetail(int id)
        {
            string sql = "SELECT p.id FROM AliProducts p left join AliProductDetail d on p.id = d.pid  "
                         + "where p.id = " + id + "  and ( p.GmtModified >  d.gmtModified or d.pid is null ) ";
            return !Convert.IsDBNull(dbHelper.ExecuteScalar(sql, null));
        }
    }
}
