using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.api.Helpers {
    public static class Extensions {
        public static void AddApplicationError (this HttpResponse response, string message) {
            response.Headers.Add ("Application-Error", message);
            response.Headers.Add ("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add ("Access-Control-Allow-Origin", "*");
        }
    
        public static int GetAge (this DateTime theBrithDay) {
            var age = DateTime.Today.Year - theBrithDay.Year;
            if (theBrithDay.AddYears (age) > DateTime.Today) {
                age--;
            }
            return age;
        }
            public static void AddPagintainHeader (this HttpResponse response, int currentPage, int itemPerPage, int totalPages, int totalItems) {
            var paginationHeader = new PaginationHeader (currentPage, itemPerPage, totalPages, totalItems);
            var jsonSetting =new JsonSerializerSettings();
            jsonSetting.ContractResolver =new CamelCasePropertyNamesContractResolver();
            response.Headers.Add ("pagination", JsonConvert.SerializeObject(paginationHeader,jsonSetting));
            response.Headers.Add ("Access-Control-Expose-Headers", "pagination");
        }

    }
}