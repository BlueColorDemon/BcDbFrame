﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BcDbFrame.Common
{
    /// <summary>
    /// 系统管理模块友好的提示信息
    /// </summary>
    public enum SysMessage
    {
        LoadSuccess,
        LoadError,
        OperateSuccess,
        OperateError,
        AddSuccess,
        AddError,
        UpdateSuccess,
        UpdateError,
        DeleteSuccess,
        DeleteError,
        UnkownError,
        ParamError
    }
}
