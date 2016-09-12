using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.MODEL.Enum
{
    /// <summary>
    /// 删除标记枚举
    /// </summary>
    public enum DelFlagEnum
    {
        /// <summary>
        /// 未删除
        /// </summary>
        Normarl=0,

        /// <summary>
        /// 逻辑删除（软删除）
        /// </summary>
        LogicDelete=1,

        /// <summary>
        /// 物理删除（从数据库中删除）
        /// </summary>
        PhyicDelte=2
    }
}
