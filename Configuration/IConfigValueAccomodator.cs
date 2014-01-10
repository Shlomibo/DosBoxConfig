using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
	interface IConfigValueAccomodator
	{
		string Accomodate(string value);
		string AccomodateBack(string value);
	}
}
