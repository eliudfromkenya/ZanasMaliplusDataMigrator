using ReactiveUI;
using System.Threading;
using System.Windows.Input;
using ZanasMaliplusDataMigrator.Views;

namespace ZanasMaliplusDataMigrator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand CloseCommand { get; }
        public ICommand MigrateDataCommand { get; }
        public ICommand ReplicateToMySQLCommand { get; }
        public ICommand PreMigrationReportCommand { get; }
        public ICommand PostMigrationCommand { get; }
        public ICommand TestZanasConnectionCommand { get; }
        public ICommand TestMaliplusConnectionCommand { get; }

        public MainWindowViewModel()
        {
            PreMigrationReportCommand = ReactiveCommand.CreateFromTask(PreMigrationReport);
            CloseCommand = ReactiveCommand.CreateFromTask<object>(tt => CloseSystem(tt));
            MigrateDataCommand = ReactiveCommand.CreateFromTask<object>(tt => MigrateData(tt));
            ReplicateToMySQLCommand = ReactiveCommand.CreateFromTask<object>(tt => ReplicateToMySQL(tt));
            PostMigrationCommand = ReactiveCommand.CreateFromTask<object>(tt => PostMigration(tt));
            TestZanasConnectionCommand = ReactiveCommand.CreateFromTask<object>(tt => TestZanasConnection(tt));
            TestMaliplusConnectionCommand = ReactiveCommand.CreateFromTask<object>(tt => TestMaliplusConnection(tt));
        }

        private async Task TestMaliplusConnection(object tt)
        {
            try 
            {
                await MaliplusConnection.OpenAsync();
                ShowMessage("Success connected to Maliplus database", "Maliplus Connection Test");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async Task TestZanasConnection(object tt)
        {
            try
            {
                await ZanasConnection.OpenAsync();
                ShowMessage("Success connected to Zanas database", "Zanas Connection Test");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async Task PostMigration(object tt)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async Task ReplicateToMySQL(object tt)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async Task MigrateData(object tt)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private async Task CloseSystem(object form)
        {
            try
            {
                ShowMessage("System is Closing", "System Closed");
                Thread.Sleep(3000);

                if (form is MainWindow page)
                    page.Close();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowMessage(string message, string title)
        {
            MessageError = string.Empty;
            Message= message;
            MessageTitle = title;   
        }

        private async Task PreMigrationReport()
        {
            try
            {
                DbServices dbServices = new();
                var zanasDbColumns = await dbServices.GetColumns(() => this.ZanasConnection, ZanasDbModel.Schema);
                var zanasDbRelationship = await dbServices.GetRelationship(() => this.ZanasConnection, ZanasDbModel.Schema);
                var maliplusDbColumns = await dbServices.GetColumns(() => this.MaliplusConnection, MaliplusDbModel.Schema);
                var maliplusDbRelationship = await dbServices.GetRelationship(() => this.MaliplusConnection, MaliplusDbModel.Schema);
                new MsExcelReportService().GeneratePreMigrationReport(zanasDbColumns.ToArray(), maliplusDbColumns.ToArray(), zanasDbRelationship.ToArray(), maliplusDbRelationship.ToArray());
                ShowMessage("Successfully generated the report", "Maliplus Report");
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
            MessageError = ex.Message;
            Message = string.Empty;
            MessageTitle = "An Error";
        }
    }
}