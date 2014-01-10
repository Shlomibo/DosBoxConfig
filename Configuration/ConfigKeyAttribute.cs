using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	sealed class ConfigKeyAttribute : Attribute
	{
		#region Properties

		/// <summary>
		/// Gets or sets the string that whould be printed if the property value is null
		/// </summary>
		public string NullString { get; set; }

		/// <summary>
		/// Gets or set the name of the key in the 
		/// </summary>
		public string ConfigName { get; set; }

		/// <summary>
		/// Gets or sets an accomodator for the values
		/// </summary>
		public Type Accomodator { get; set; }
		#endregion

		#region Ctor

		public ConfigKeyAttribute()
		{
			this.NullString = "";
		}

		public ConfigKeyAttribute(string configName)
			: this()
		{
			this.ConfigName = configName;
		}

		public ConfigKeyAttribute(string configName, string nullString)
			: this(configName)
		{
			this.NullString = nullString;
		}
		#endregion

		#region Methods

		public static ConfigKeyAttribute GetAttribute(PropertyInfo property)
		{
			return (ConfigKeyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigKeyAttribute));
		}
		#endregion
	}
}
