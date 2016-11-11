using System;
using System.Collections.Generic;
using System.Linq;
using SendGrid.Models;
using SendGrid.Models.Error;

namespace SendGrid.Exceptions
{
	public class SendGridApiException
		: Exception
	{
		public SendGridApiException(ErrorResponse error)
			: base(error.Errors.First().Message)
		{
			Errors = error.Errors;
		}

		public IEnumerable<ErrorDetail> Errors { get; private set; }
	}
}
