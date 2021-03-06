﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ConfigKeys.Accomodators
{
	public class IllegalEnumValAccomodator<TEnumType> : ConfigValueAccomodator<TEnumType, string>
	{
		#region Consts

		private const int PARAMS_COUNT = 2;
		private const int ACCOMODATOR_INDEX = 0;
		private const int PARSER_INDEX = ACCOMODATOR_INDEX + 1;
		private const string LEGAL_PREFIX = "_";
		
		private const char MIN_LEGAL_VALUE = 'a';
		private const char MAX_LEGAL_VALUE = 'z';

		private static readonly char[] LEGALS_OUT_OF_LEGAL_RANGE = new[]
			{
				'_',
				'@',
			};
		#endregion

		#region Properties

		public IConfigValueAccomodator<string, string> FinalAccomodator { get; private set; }
		public IParser<TEnumType> Parser { get; private set; }
		#endregion

		#region Ctor

		public IllegalEnumValAccomodator() 
		{
			if (!typeof(TEnumType).IsEnum)
			{
				throw new InvalidOperationException("TEnumType must be type of enum");
			}

			this.Parser = new DefaultTypeParser<TEnumType>();
		}

		public IllegalEnumValAccomodator(object parameter)
			: this()
		{
			bool isIllegal = false;

			if ((parameter is Type) &&
				(typeof(IConfigValueAccomodator<string, string>).IsAssignableFrom(parameter as Type)))
			{
				this.FinalAccomodator = (IConfigValueAccomodator<string, string>)Activator.CreateInstance(parameter as Type);
			}
			else if ((parameter is Type) &&
				(typeof(IParser<TEnumType>).IsAssignableFrom(parameter as Type)))
			{
				this.Parser = (IParser<TEnumType>)Activator.CreateInstance(parameter as Type);
			}
			else if (parameter is Type[])
			{
				var typesArr = (Type[])parameter;

				if ((typesArr.Length != PARAMS_COUNT) ||
					!typeof(IConfigValueAccomodator<string, string>).IsAssignableFrom(typesArr[ACCOMODATOR_INDEX]) ||
					!typeof(IParser<TEnumType>).IsAssignableFrom(typesArr[PARSER_INDEX]))
				{
					isIllegal = true;
				}
				else
				{
					this.FinalAccomodator = 
						(IConfigValueAccomodator<string, string>)Activator.CreateInstance(typesArr[ACCOMODATOR_INDEX]);
					this.Parser = (IParser<TEnumType>)Activator.CreateInstance(typesArr[PARSER_INDEX]);
				}
			}
			else
			{
				isIllegal = true;
			}

			if (isIllegal)
			{
				throw new ArgumentException(string.Format(
					"The argument must Type which is assignable from one of the following types: {0}, {1}, or a Type array " +
					"which contains type which {0} is assignable from, as the first item, " +
					"and type which {1} is assignable from, as the second",
					typeof(IConfigValueAccomodator<string, string>).Name,
					typeof(IParser<TEnumType>).Name), "parameter");
			}
		}
		#endregion

		public override string Accomodate(TEnumType value)
		{
			string enumValue = value.ToString();

			if (enumValue.StartsWith(LEGAL_PREFIX))
			{
				enumValue = enumValue.Substring(LEGAL_PREFIX.Length);
			}

			if (this.FinalAccomodator != null)
			{
				enumValue = this.FinalAccomodator.Accomodate(enumValue);
			}

			return enumValue;
		}

		public override TEnumType AccomodateBack(string value)
		{
			if (this.FinalAccomodator != null)
			{
				value = this.FinalAccomodator.AccomodateBack(value);
			}

			if (IsIllegal(value[0]))
			{
				value = LEGAL_PREFIX + value;
			}

			TEnumType parsed = this.Parser.Parse(value);

			return parsed;
		}

		private bool IsIllegal(char @char)
		{
			@char = char.ToLowerInvariant(@char);

			return ((@char < MIN_LEGAL_VALUE) || (@char > MAX_LEGAL_VALUE)) && 
				!LEGALS_OUT_OF_LEGAL_RANGE.Contains(@char);
		}
	}
}
