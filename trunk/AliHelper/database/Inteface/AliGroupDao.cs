using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public interface IAliGroupDao
    {
        List<AliGroup> GetAliGroupList();

        List<int> GetAliGroupIdList();

        void Insert(List<AliGroup> list);

        void Update(List<AliGroup> list);

        void DeleteGroups(List<int> ids);
    }
}
