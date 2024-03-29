﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HideEndpoint.Filters;

public class SwaggerDocumentFilter : IDocumentFilter
{
	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
	{
		swaggerDoc.Paths.Remove("/WeatherForecast/GetMethodFour");
	}
}