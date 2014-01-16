using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	public class SmallLettersAccomodator : IConfigValueAccomodator<object, string>
	{
		#region Properties

		public IParser Parser { get; private set; } 
		#endregion

		#region Ctor

		public SmallLettersAccomodator(Type parserType)
		{
			this.Parser = (IParser)Activator.CreateInstance(parserType);
		} 
		#endregion

		#region Methods

		public string Accomodate(object value)
		{
			return value.ToString().ToLowerInvariant();
		}

		public object AccomodateBack(string value)
		{
			return this.Parser.Parse(value);
		} 

		object IConfigValueAccomodator.Accomodate(object value)
		{
			return Accomodate(value);
		}

		object IConfigValueAccomodator.AccomodateBack(object value)
		{
			return AccomodateBack((string)value);
		}
		#endregion

	}
}
