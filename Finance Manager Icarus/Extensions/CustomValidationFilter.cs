using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Manager_Icarus.Extensions;

public class CustomValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("ModelState inválido detectado na requisição para {Path}", context.HttpContext.Request.Path);

            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            context.Result = new UnprocessableEntityObjectResult(new
            {
                mensagem = "Não foi possível processar a solicitação: revise os dados informados e tente novamente.",
                erros = errors
            });
        }
    }
}
