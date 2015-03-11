using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	public sealed class ResolutionParser : ParserBase<Resolution>
	{
		public override Resolution Parse(string str) =>
			Resolution.Parse(str);
	}

	public sealed class PriorityParser : ParserBase<Priority>
	{
		public override Priority Parse(string str) =>
			Priority.Parse(str);
	}

	public sealed class CyclesParser : ParserBase<Cycles>
	{
		public override Cycles Parse(string str) =>
			Cycles.Parse(str);
	}

}
