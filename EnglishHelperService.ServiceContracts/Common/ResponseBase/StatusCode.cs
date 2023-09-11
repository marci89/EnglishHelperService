namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Status codes
	/// </summary>
	public enum StatusCode
	{
		Ok = 200,
		Created = 201,

		BadRequest = 400,
		Unauthorized = 401,
		NotFound = 404,
		InternalServerError = 500
	}
}
