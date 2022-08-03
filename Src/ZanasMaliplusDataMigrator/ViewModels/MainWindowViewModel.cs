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
        public ICommand MoveToMySQLCommand { get; }
        public ICommand GenerateMigrationReportCommand { get; }
        public ICommand CheckTablesCommand { get; }
        public ICommand TestZanasConnectionCommand { get; }
        public ICommand TestMaliplusConnectionCommand { get; }

        public MainWindowViewModel()
        {
            GenerateMigrationReportCommand = ReactiveCommand.CreateFromTask(GenerateMigrationReport);
            CloseCommand = ReactiveCommand.CreateFromTask<object>(tt => CloseSystem(tt));
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

        private void ShowMessage(string v1, string v2)
        {
            //throw new NotImplementedException();
        }

        private async Task GenerateMigrationReport()
        {
            try
            {
                DbServices dbServices = new();
                var cols = await dbServices.GetColumns(() => this.ZanasConnection, ZanasDbModel.Schema);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowError(Exception ex)
        {
        }
    }
}