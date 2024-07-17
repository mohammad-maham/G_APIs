﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_APIs.Models;

public class ApiResult
{
    public int ResultCode { get; set; }
    public string? Message { get; set; }
    public dynamic? Data { get; set; }

    public ApiResult()
    {
    }

    public ApiResult(int resultCode, string message="",dynamic? data=null)
    {
        this.ResultCode = resultCode;
        this.Message = message;
        this.Data = data;
    }
}