using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotachan {
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnumTextAttribute : Attribute {
        public EnumTextAttribute(string text) {
            this.Text = text;
        }
        public string Text {
            get;
            private set;
        }
    }

    public static class EnumExtension {
        public static string GetEnumText(this System.Enum value) {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attrs = fieldInfo.GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[];
            if (attrs != null && attrs.Length > 0) {
                return attrs[0].Text;
            } else {
                return value.ToString();
            }
        }
    }
}
