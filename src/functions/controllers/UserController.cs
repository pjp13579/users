using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using MongoDB.Driver;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PP23771.UserService;
using PP23771.Entities;
using PP23771.DTO;
using PP23771.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PP23771.UserController
{
	public class Controller
	{
		private IService service = new Service();

		[FunctionName("GetAllUsers")]
		[OpenApiOperation(operationId: "getAllUsers", tags: new[] { "User" })]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ViewUserDTO>), Description = "Ok response")]
		public IActionResult GetAllUsers(
		    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getallusers")] HttpRequest req,
		    ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				return new OkObjectResult(service.getAllUsers());
			}
			catch (Exception e)
			{
				return new BadRequestObjectResult(e.Message);
			}
		}

		[FunctionName("GetUserById")]
		[OpenApiOperation(operationId: "getUserById", tags: new[] { "User" })]
		[OpenApiParameter(name: "userid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "user id")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ViewUserDTO), Description = "Ok response")]
		[OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "User Not Found response")]
		public IActionResult GetUserById(
			    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getuserbyid/{id}")] HttpRequest req, string id,
			    ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				return new OkObjectResult(service.getUserById(id));
			}
			catch (UserNotFoundException e)
			{
				return new NotFoundObjectResult(e.Message);
			}
			catch (Exception e)
			{
				return new BadRequestObjectResult(e.Message);
			}
		}

		[FunctionName("PostUsers")]
		[OpenApiOperation(operationId: "PostUsers", tags: new[] { "User" })]
		[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<CreateUserDTO>), Required = true, Description = "Request body")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text", bodyType: typeof(bool), Description = "Ok response")]
		public IActionResult PostUser(
			    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "postusers")] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				string requestBody = new StreamReader(req.Body).ReadToEnd();

				List<CreateUserDTO> users = JsonSerializer.Deserialize<List<CreateUserDTO>>(requestBody);

				return new OkObjectResult(service.postUser(users));
			}
			catch (Exception e)
			{
				return new BadRequestObjectResult(e.Message);
			}
		}

		[FunctionName("DeleteUsers")]
		[OpenApiOperation(operationId: "DeleteUsers", tags: new[] { "User" })]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DeleteResult), Description = "Ok response")]
		public IActionResult deleteUser(
			    [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "deleteusers")] HttpRequest req, ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				string requestBody = new StreamReader(req.Body).ReadToEnd();

				List<string> ids = JsonSerializer.Deserialize<List<string>>(requestBody);

				return new OkObjectResult(service.deleteUsers(ids));
			}
			catch (Exception e)
			{
				return new BadRequestObjectResult(e.Message);
			}

		}


	}
}
