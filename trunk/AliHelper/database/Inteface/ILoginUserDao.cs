using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public interface ILoginUserDao
    {
        List<LoginUser> GetLoginUserList();

        LoginUser GetUserByUserId(string userId);

        void Insert(LoginUser item);

        void Update(LoginUser item);

        void DeleteGroups(int id);
    }
}
