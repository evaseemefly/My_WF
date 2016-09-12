using lshOA.MODEL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lshOA.UI.Models
{

    public class IndexContent:SearchResultViewModel
    {
        /// <summary>
        /// 在索引队列中是添加还是删除
        /// </summary>
        public LuceneEnumType LuceneEnumType { get; set; }
    }
}