//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HM13OA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkFlowModel
    {
        public int ModelId { get; set; }
        public string ModelTitle { get; set; }
        public bool IsDelete { get; set; }
        public string Remark { get; set; }
        public int SubBy { get; set; }
        public System.DateTime SubTime { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
