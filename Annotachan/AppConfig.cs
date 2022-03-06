using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Annotachan {
    public enum DatabaseType {
        [EnumText("Oracle")]
        Oracle,
        [EnumText("SQLite")]
        SQLite,
    }
    public class AppConfig {

        private static AppConfig _singleton = null;
        private AppConfig() {

        }
        public static AppConfig GetInstance() {
            if (_singleton == null) {
                _singleton = new AppConfig();
                _singleton.Load();
            }
            return _singleton;
        }
        public string ConfigPath {
            get {
                return System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
            }
        }
        public void Load() {

            var doc = XDocument.Load(this.ConfigPath);
            var root = doc.Element("Configurations");
            this.ImageFilters = root.Element("Image").Attribute("Filter").Value.Split(';');
        }
        public IEnumerable<string> ImageFilters {
            get;
            private set;
        }
    }
}
