using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	public sealed class SmallLettersAccomodator : IConfigValueAccomodator<object, string>
	{
		#region Properties

		public IParser Parser { get; } 
		#endregion

		#region Ctor

		public SmallLettersAccomodator(Type parserType)
		{
			if (parserType == null)
			{
				throw new NullReferenceException(nameof(parserType));
			}

			this.Parser = (IParser)Activator.CreateInstance(parserType);
		} 
		#endregion

		#region Methods

		public string Accomodate(object value) =>
			value.ToString().ToLowerInvariant();

		public object AccomodateBack(string value) =>
			this.Parser.Parse(value);

		object IConfigValueAccomodator.Accomodate(object value) =>
			Accomodate(value);

		object IConfigValueAccomodator.AccomodateBack(object value) =>
			AccomodateBack((string)value);
		#endregion

	}
}
