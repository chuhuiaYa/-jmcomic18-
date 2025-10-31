using System;

namespace 禁漫天堂.Models
{
    public class Comic
    {
        public string ComicID { get; set; }
        public string 标题 { get; set; }
        public string 封面URL { get; set; }
        public string 作者 { get; set; }
        public string 类型或分类 { get; set; }
        public decimal 评分 { get; set; }
        public string 简介 { get; set; }
        public DateTime 发布日期 { get; set; }
        public string ReadingLink { get; set; } // 新增字段，用于存储“开始阅读”链接
    }

}
