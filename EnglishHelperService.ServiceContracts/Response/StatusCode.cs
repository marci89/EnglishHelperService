namespace EnglishHelperService.ServiceContracts
{
	public enum StatusCode
	{
		Ok = 200,
		Created = 201,

		BadRequest = 400,
		Unauthorized = 401,
		NotFound = 403,
		InternalServerError = 500
	}
}
