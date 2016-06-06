using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfThreading.Models;

namespace WpfThreading.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IDbGateway dbGateway)
        {
            this.dbGateway = dbGateway;

            Von = new DateTime(2016, 5, 1);
            Bis = new DateTime(2016, 5, 10);
            StarteSucheLabel = sucheStarten;

            dbAbfrageLaeuft = false;

            eintraege = new ObservableCollection<ErweiterterGoodSyncLogEintrag>();
            Eintraege = new ListCollectionView(eintraege);

            StarteSuche = new DelegateCommand<object>(onStarteSuche,
                                                      canStarteSuche)
                                 .ObservesProperty(() => Cid)
                                 .ObservesProperty(() => Von)
                                 .ObservesProperty(() => Bis);
        }

        // Mögliche Beschriftungen für Button StarteSuche.
        private const string sucheStarten = "Suche starten";
        private const string sucheAbbrechen = "Suche abbrechen";

        // Mögliche Beschriftungen der Statusleiste.
        private const string sucheLaeuft = "Suche läuft";
        private const string sucheAbgeschlossen = "Suche abgeschlossen";
        private const string sucheAbgebrochen = "Suche abgebrochen";

        private ObservableCollection<ErweiterterGoodSyncLogEintrag> eintraege;

        public ICollectionView Eintraege { get; private set; }

        private bool dbAbfrageLaeuft;

        private bool canStarteSuche(object arg)
        {
            int cid = 0;
            return int.TryParse(Cid, out cid) && cid > 0 && Von <= Bis;
        }

        private CancellationTokenSource cancellationTokenSourceForSuche = null;

        private async void onStarteSuche(object obj)
        {
            if (dbAbfrageLaeuft)
            {
                // Brich DbAbfrage ab.
                cancellationTokenSourceForSuche.Cancel();
                StarteSucheLabel = sucheStarten;
                dbAbfrageLaeuft = false;
            }
            else
            {
                StarteSucheLabel = sucheAbbrechen;
                StatusBarText = sucheLaeuft;
                dbAbfrageLaeuft = true;
                eintraege.Clear();

                // http://www.heise.de/developer/artikel/Asynchrone-Programmierung-in-NET-4-5-mit-async-und-await-1852797.html
                // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx

                cancellationTokenSourceForSuche = new CancellationTokenSource();

                try
                {
                    var erweiterteGoodSyncLogs =
                        await dbGateway
                                .FetchErweiterteGoodSyncLogsAsync(new List<Aktivitaetszeitraum>(),
                                                                  cancellationTokenSourceForSuche.Token);

                    eintraege.AddRange(erweiterteGoodSyncLogs);
                    StatusBarText = sucheAbgeschlossen;
                }
                catch (TaskCanceledException)
                {
                    StatusBarText = sucheAbgebrochen;
                }
                finally
                {
                    cancellationTokenSourceForSuche.Dispose();
                    StarteSucheLabel = sucheStarten;
                    dbAbfrageLaeuft = false;
                }
            }
        }

        private string cid;
        public string Cid
        {
            get { return cid; }
            set { SetProperty(ref cid, value); }
        }

        private DateTime von;
        public DateTime Von
        {
            get { return von; }
            set { SetProperty(ref von, value); }
        }

        private DateTime bis;
        public DateTime Bis
        {
            get { return bis; }
            set { SetProperty(ref bis, value); }
        }

        private string starteSucheLabel;
        public string StarteSucheLabel
        {
            get { return starteSucheLabel; }
            set { SetProperty(ref starteSucheLabel, value); }
        }

        public ICommand StarteSuche { get; private set; }

        private string statusBarText;
        public string StatusBarText
        {
            get { return statusBarText; }
            set { SetProperty(ref statusBarText, value); }
        }

        private IDbGateway dbGateway;
    }
}
