using API.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Response
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse<TModel>(this ISingleResponseModel<TModel> response)
        {
            var status = HttpStatusCode.OK;
            response.Message = "Successful";

            if (response.Model == null)
            {
                response.Message = "No data found";
                status = HttpStatusCode.NotFound;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpListResponse<TModel>(this IListResponseModel<TModel> response)
        {
            var status = HttpStatusCode.OK;
            response.Message = "Successful";

            if (response.Model == null)
            {
                response.Message = "No Data Found";
                status = HttpStatusCode.NotFound;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpCreatedResponse<TModel>(this ISingleResponseModel<TModel> response)
        {
            var status = HttpStatusCode.Created;
            response.Message = "Successful";

            if (Convert.ToInt32(response.Model) == 0)
            {
                response.Message = "Failed to save data";
                status = HttpStatusCode.BadRequest;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpUpdatedResponse<TModel>(this ISingleResponseModel<TModel> response)
        {
            var status = HttpStatusCode.NoContent;

            if (Convert.ToInt32(response.Model) == 0)
            {
                response.Message = "Failed to update data";
                status = HttpStatusCode.BadRequest;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpDeletedResponse<TModel>(this ISingleResponseModel<TModel> response)
        {
            var status = HttpStatusCode.NoContent;

            if (Convert.ToInt32(response.Model) == 0)
            {
                response.Message = "Failed to delete data";
                status = HttpStatusCode.BadRequest;
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpErrorResponse(this IResponseModel response)
        {
            var status = HttpStatusCode.BadRequest;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }
}
