﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPICore.Response
{
    public static class ResponseCustomization
    {
        public static IActionResult ToHttpResponse<TModel>(this IListModelResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null)
            {
                status = HttpStatusCode.NoContent;
            }

            return new ObjectResult(response) { StatusCode = (Int32)status };
        }
        public static IActionResult ToHttpResponse<TModel>(this ISingleModelResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (response.Model == null)
            {
                status = HttpStatusCode.NotFound;
            }

            return new ObjectResult(response) { StatusCode = (Int32)status };
        }
    }
}
