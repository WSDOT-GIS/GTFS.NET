﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gtfs.Contract
{
	public class CalendarDate
	{
		[Required]
		public string service_id { get; set; }

		[Required]
		public DateTime date { get; set; }

		[Required]
		public ExceptionType exception_type { get; set; }
	}
}