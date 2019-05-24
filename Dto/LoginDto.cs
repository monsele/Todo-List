﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DavidProject.Dto
{
	public class LoginDto
	{
		public string Username { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }

		
	}
}
