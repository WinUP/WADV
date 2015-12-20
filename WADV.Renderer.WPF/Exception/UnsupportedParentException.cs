using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WADV.WPF.Renderer.Exception
{
    /// <summary>
    /// 表示尝试将元素挂载到不受支持的父元素下的异常
    /// </summary>
    class UnsupportedParentException : System.Exception
    {
        public UnsupportedParentException()
            : base("无法挂载到当前父元素下，因为父元素不是面板或不受支持。")
        {

        }
    }
}
