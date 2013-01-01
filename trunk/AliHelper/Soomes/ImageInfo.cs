using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{

    public class ImageInfoJson
    {
        public Dictionary<int,string> UrlMap { set; get; }
        public int Total { set; get; }
        public List<ImageInfo> ImageInfos { set; get; }
        public ImageQuery Query { set; get; }
    }

    public class ImageInfo
    {
        public string Status{ set; get; }
        public int Width { set; get; }
        public string HashCode { set; get; }
        public int FileSize { set; get; }
        public int Id { set; get; }
        public int GroupId { set; get; }
        public int Height { set; get; }
        public int ReferenceCount { set; get; }
        public int MemberSeq { set; get; }
        public string FileName { set; get; }
        public string MemberId { set; get; }
        public string MemberName { set; get; }
        public int CompanyId { set; get; }
        public string DisplayNameUtf8 { set; get; }
        public string Url { set; get; }
        public string LocationUrl { set; get; }
    }

    public class ImageQuery
    { 
        public string SearchImageLocationType{ set; get; }
        public int PageFristItem{ set; get; }
        public int PageLastItem{ set; get; }
        public int Level2{ set; get; }
        public int Level3{ set; get; }
        public int Level1{ set; get; }
        public string Name{ set; get; }
        public bool IsReference{ set; get; }
        public int PreviousPage{ set; get; }
        public string DisplayNameUtf8{ set; get; }
        public bool FirstPage { set; get; }
        public bool LastPage{ set; get; }
        public int NextPage{ set; get; }
        public int PageSize{ set; get; }
        public int EndRow{ set; get; }
        public int StartRow{ set; get; }
        public int TotalItem{ set; get; }
        public int MemberSeq{ set; get; }
        public int CurrentPage{ set; get; }
        public int CompanyId{ set; get; }
        public int TotalPage{ set; get; }
    }
}
