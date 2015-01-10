using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.SettingsExtensions.Enums
{
    /// <summary>
    /// 信息面板的位置
    /// </summary>
    public enum WindowViewType
    {
        /// <summary>
        /// 底部
        /// </summary>
        Bottom = 0,
        /// <summary>
        /// 顶部
        /// </summary>
        Top = 1,
        /// <summary>
        /// 左边
        /// </summary>
        Left = 2,
        /// <summary>
        /// 右边
        /// </summary>
        Right = 3,
        /// <summary>
        /// 拆分
        /// </summary>
        Split = 4,
        /// <summary>
        /// 选项卡
        /// </summary>
        Tabs = 5
    }
}
//Bottom
//M3.083,7.333v16.334h24.833V7.333H3.083zM24.915,16.833H6.083v-6.501h18.833L24.915,16.833L24.915,16.833z
//Top
//M27.916,23.667V7.333H3.083v16.334H27.916zM24.915,20.668H6.083v-6.501h18.833L24.915,20.668L24.915,20.668z
//Left
//M3.084,7.333v16.334h24.832V7.333H3.084z M11.667,10.332h13.251v10.336H11.667V10.332z
//Right
//M3.083,7.333v16.334h24.833V7.333H3.083z M19.333,20.668H6.083V10.332h13.25V20.668z
//Split
//M5.896,5.333V21.25h23.417V5.333H5.896zM26.312,18.25H8.896V8.334h17.417V18.25L26.312,18.25zM4.896,9.542H1.687v15.917h23.417V22.25H4.896V9.542z