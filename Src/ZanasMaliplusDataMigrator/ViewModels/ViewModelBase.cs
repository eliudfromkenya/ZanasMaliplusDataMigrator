using ReactiveUI;

namespace ZanasMaliplusDataMigrator.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private DBModel maliplusDbModel = new() { Database = "KfaMain", Host = "192.168.1.239", Password = "HQisd2022", Port = "52232", Schema = "kfaltd", User = "kfaltd" };
        private DBModel zanasDbModel = new() { Database = "MOISB", Host = "192.168.1.10", Password = "Pa55word", Port = "50000", Schema = "ZANAS", User = "maliplus" };

        public string Greeting => "ZANAS TO MALIPLUS DATA MIGRATION";
        public DBModel MaliplusDbModel { get => maliplusDbModel; set => maliplusDbModel = value; }
        public DBModel ZanasDbModel { get => zanasDbModel; set => zanasDbModel = value; }

        public IBM.Data.DB2.Core.DB2Connection MaliplusConnection => new IBM.Data.DB2.Core.DB2Connection($"Server={MaliplusDbModel.Host}:{MaliplusDbModel.Port};Database={MaliplusDbModel.Database};UID={MaliplusDbModel.User};PWD={MaliplusDbModel.Password};");
        public IBM.Data.DB2.Core.DB2Connection ZanasConnection => new IBM.Data.DB2.Core.DB2Connection($"Server={ZanasDbModel.Host}:{ZanasDbModel.Port};Database={ZanasDbModel.Database};UID={ZanasDbModel.User};PWD={ZanasDbModel.Password};");
    }
}