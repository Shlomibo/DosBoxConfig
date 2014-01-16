using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	public class ResolutionParser : ParserBase<Resolution>
	{
		public override Resolution Parse(string str)
		{
			return Resolution.Parse(str);
		}
	}

	public class PriorityParser : ParserBase<Priority>
	{
		public override Priority Parse(string str)
		{
			return Priority.Parse(str);
		}
	}

	public class CyclesParser : ParserBase<Cycles>
	{
		public override Cycles Parse(string str)
		{
			return Cycles.Parse(str);
		}
	}

}
