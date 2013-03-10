using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soomes;
using System.Data.SQLite;
using System.Data;

namespace Database
{
    public interface IAliImageDao
    {
        QueryObject<ImageInfo> GetAliImageList(QueryObject<ImageInfo> query);

        void Insert(List<ImageInfo> list);

        void DeleteAllImages();

    }
}
