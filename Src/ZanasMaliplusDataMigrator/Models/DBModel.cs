using System;
using ReactiveUI;

namespace ZanasMaliplusDataMigrator.Models
{
    public class DBModel : ReactiveObject
    {
        private string? host;
        private string? schema;
        private string? port;
        private string? user;
        private string? password;
        private string? database;

        public string? Host { get => host; set => this.RaiseAndSetIfChanged(ref host, value); }
        public string? Schema { get => schema; set => this.RaiseAndSetIfChanged(ref schema, value); }
        public string? Port { get => port; set => this.RaiseAndSetIfChanged(ref port, value); }
        public string? User { get => user; set => this.RaiseAndSetIfChanged(ref user, value); }
        public string? Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        public string? Database { get => database; set => this.RaiseAndSetIfChanged(ref database, value); }
    }
}