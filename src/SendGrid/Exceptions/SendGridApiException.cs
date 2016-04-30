using System;
using SendGrid.Models;

namespace SendGrid.Exceptions
{
	public class SendGridApiException
		: Exception
	{
		public SendGridApiException(ErrorResponse error)
			: base(error.Message)
		{
			Errors = error.Errors;
		}

		public string[] Errors { get; private set; }
	}
}
