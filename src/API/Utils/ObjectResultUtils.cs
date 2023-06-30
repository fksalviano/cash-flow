using Microsoft.AspNetCore.Mvc;

namespace Application.Commons.Utils;

public static class ObjectResultUtils
{
    public static ObjectResult InternalServerError(object? error) => new(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError
    };
}
