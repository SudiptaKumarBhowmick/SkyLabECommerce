using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Service.Error;
using System.Net;

namespace Service.Validations
{
    public static class ModelStateValidation
    {
        public static void ModelStateValidationService(this IServiceCollection services)
        {
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return CustomModelStateValidationMessage(actionContext);
                };
            });
        }

        private static BadRequestObjectResult CustomModelStateValidationMessage(ActionContext actionContext)
        {
            return new BadRequestObjectResult(actionContext.ModelState
                .Where(modelError => modelError.Value != null && modelError.Value.Errors.Count > 0)
                .Select(modelError => new ApiException(Convert.ToInt32(HttpStatusCode.BadRequest))
                {
                    Message = modelError.Value?.Errors?.First().ErrorMessage ?? ""
                }).First());
        }
    }
}
