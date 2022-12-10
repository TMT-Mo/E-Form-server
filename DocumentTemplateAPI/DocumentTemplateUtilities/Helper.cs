using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DocumentTemplateUtilities
{
    public static class ConvertEDMXToDetail
    {
        //public static string CheckWhiteListedParams(List<String> whiteListedParams)
        //{
        //    HttpContext context;
        //    if (context.Request != null)
        //    {
        //        var queries = context.Request.QueryString.Value;
        //        foreach (var item in whiteListedParams)
        //        {
        //            if (!item.Contains(queries))
        //            {
        //                return item;
        //            }
        //        }

        //        //foreach (string key in queries)
        //        //{
        //        //    if (!whiteListedParams.Contains(key))
        //        //    {
        //        //        return key;
        //        //    }
        //        //}
        //    }

        //    return null;
        //}
        public class PaginationParams
        {
            public int Page { get; set; }
            public int Size { get; set; }
        }
        public static PaginationParams ParsePaginationParams(HttpRequest queries)
        {
            int size = 10;
            int page = 0;

            var sizeStr = queries.Query["_size"].ToString();
            if (!String.IsNullOrEmpty(sizeStr))
            {
                size = int.Parse(sizeStr);
            }

            var pageStr = queries.Query["_page"].ToString();
            if (!String.IsNullOrEmpty(pageStr))
            {
                page = int.Parse(pageStr);
            }

            return new PaginationParams
            {
                Size = size,
                Page = page
            };
        }
        public static string GetErrorMessagesFromModalState(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            var errorMessages = "";

            foreach (var state in modelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        errorMessages += error.ErrorMessage + " ";
                    }
                    else if (!string.IsNullOrEmpty(error.Exception.Message))
                    {
                        errorMessages += error.Exception.Message + " ";
                    }

                }
            }

            return errorMessages;
        }
    }
}
