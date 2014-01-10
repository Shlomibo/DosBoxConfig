using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
	public abstract class ConfigSection : IDictionary<string, string>
	{
		#region Consts

		private const string CAT_WARNING = "Warning";
		#endregion

		#region Properties

		public abstract string SectionName { get; }

		internal PropertyCollection Properties { get; private set; }

		public ICollection<string> Values
		{
			get
			{
				return (from prop in this.Properties.Values
						select (string)prop.GetValue(this)).ToArray();
			}
		}

		public string this[string key]
		{
			get { return (string)this.Properties[key].GetValue(this); }
			set { this.Properties[key].SetValue(this, value); }
		}

		public int Count
		{
			get { return this.Properties.Count; }
		}

		bool ICollection<KeyValuePair<string, string>>.IsReadOnly
		{
			get { return false; }
		}
		#endregion

		#region Ctor

		protected ConfigSection()
		{
			this.Properties = new PropertyCollection(GetType());
		}
		#endregion

		#region Methods

		void IDictionary<string, string>.Add(string key, string value)
		{
			throw new NotSupportedException();
		}

		public bool ContainsKey(string key)
		{
			return this.Keys.Contains(key);
		}

		public ICollection<string> Keys
		{
			get { return this.Properties.PropertyNames; }
		}

		bool IDictionary<string, string>.Remove(string key)
		{
			throw new NotSupportedException();
		}

		public bool TryGetValue(string key, out string value)
		{
			value = null;
			bool didSucceeded = false;

			try
			{
				PropertyInfo prop;

				if (this.Properties.TryGetValue(key, out prop))
				{
					value = (string)prop.GetValue(this);

					didSucceeded = true;
				}
			}
			catch (Exception ex)
			{
				value = null;
				Trace.WriteLine(string.Format(
					"Failed to get value for {0}, with exception of {1}: {2}",
					key,
					ex.GetType().FullName,
					ex.Message), CAT_WARNING);
			}

			return didSucceeded;
		}

		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			throw new NotSupportedException();
		}

		void ICollection<KeyValuePair<string, string>>.Clear()
		{
			throw new NotSupportedException();
		}

		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
		{
			return this.ContainsKey(item.Key) &&
				this[item.Key] == item.Value;
		}

		void ICollection<KeyValuePair<string, string>>.CopyTo(
			KeyValuePair<string, string>[] array,
			int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}

			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("array is too small");
			}

			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}

			var items = Enumerable.Zip(
				Enumerable.Range(0, this.Count),
				this.Keys,
				(index, key) => new
				{
					Index = index,
					Item = new KeyValuePair<string, string>(key, this[key]),
				});

			foreach (var item in items)
			{
				array[arrayIndex + item.Index] = item.Item;
			}
		}

		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
		{
			return false;
		}

		IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
		{
			foreach (string key in this.Keys)
			{
				yield return new KeyValuePair<string, string>(key, this[key]);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (this as IEnumerable<KeyValuePair<string, string>>).GetEnumerator();
		}

		protected string GetConfigValue(string key)
		{
			string value = this[key];

			if (value == null)
			{
				value = ConfigKeyAttribute.GetAttribute(this.Properties[key]).NullString;
			}

			return value;
		}

		public override string ToString()
		{
			var sectionBuilder = new StringBuilder();

			sectionBuilder.AppendFormat("[{0}]", this.SectionName);
			sectionBuilder.AppendLine();

			sectionBuilder.AppendLine();

			foreach (string keyName in this.Properties.KeyNames)
			{
				sectionBuilder.AppendLine(GetKeyString(keyName));
			}

			sectionBuilder.AppendLine();

			return sectionBuilder.ToString();
		}

		public virtual string GetKeyString(string keyName)
		{
			keyName = ConfigKeyAttribute.GetAttribute(this.Properties[keyName]).ConfigName ?? keyName;

			return string.Format("{0}={1}", keyName, GetConfigValue(keyName));
		}
		#endregion
	}
}
