using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfThreading.Entities;
using WpfThreading.Services;

namespace WpfThreading.ViewModels
{
    public class ErweiterteGoodSyncLogsViewModel : BindableBase
    {
        public ErweiterteGoodSyncLogsViewModel(IReportGenerator reportGenerator)
        {
            this.reportGenerator = reportGenerator;

            Cid = 1000;
            Von = new DateTime(2016, 1, 1);
            Bis = new DateTime(2016, 5, 10);
            StarteSucheLabel = Properties.Resources.SucheStarten;

            dbAbfrageLaeuft = false;

            aktivitaetszeitraeume = new ObservableCollection<Aktivitaetszeitraum>();
            Aktivitaetszeitraeume = new ListCollectionView(aktivitaetszeitraeume);

            erweiterteGoodSyncLogs = new ObservableCollection<ErweiterterGoodSyncLogEintrag>();
            ErweiterteGoodSyncLogs = new ListCollectionView(erweiterteGoodSyncLogs);

            AddAktivitaetszeitraum = new DelegateCommand<object>(onAddAktivitaetszeitraum,
                                                                 canAddAktivitaetszeitraum)
                                          .ObservesProperty(() => Cid)
                                          .ObservesProperty(() => Von)
                                          .ObservesProperty(() => Bis);

            StarteSuche = new DelegateCommand<object>(onStarteSuche);
        }

        private bool canAddAktivitaetszeitraum(object arg)
        {
            return Cid > 0 && Von <= Bis;
        }

        private void onAddAktivitaetszeitraum(object obj)
        {
            aktivitaetszeitraeume.Add(new Aktivitaetszeitraum()
            {
                Cid = Cid,
                Von = Von,
                Bis = Bis,
            });
        }

        private ObservableCollection<Aktivitaetszeitraum> aktivitaetszeitraeume;
        public ICollectionView Aktivitaetszeitraeume { get; private set; }

        private ObservableCollection<ErweiterterGoodSyncLogEintrag> erweiterteGoodSyncLogs;
        public ICollectionView ErweiterteGoodSyncLogs { get; private set; }

        private bool dbAbfrageLaeuft;

        private CancellationTokenSource cancellationTokenSourceForSuche = null;

        private async void onStarteSuche(object obj)
        {
            if (dbAbfrageLaeuft)
            {
                // Brich DbAbfrage ab.
                cancellationTokenSourceForSuche.Cancel();
                cancellationTokenSourceForSuche.Dispose();
                StarteSucheLabel = Properties.Resources.SucheStarten;
                dbAbfrageLaeuft = false;
            }
            else
            {
                StarteSucheLabel = Properties.Resources.SucheAbbrechen;
                StatusBarText = Properties.Resources.SucheLaeuft;
                dbAbfrageLaeuft = true;
                erweiterteGoodSyncLogs.Clear();

                // http://www.heise.de/developer/artikel/Asynchrone-Programmierung-in-NET-4-5-mit-async-und-await-1852797.html
                // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx

                cancellationTokenSourceForSuche = new CancellationTokenSource();

                try
                {
                    var erweiterteGoodSyncLogs =
                        await reportGenerator
                                .ErweiterteGoodSyncLogsAsync(aktivitaetszeitraeume,
                                                             cancellationTokenSourceForSuche.Token);

                    this.erweiterteGoodSyncLogs.AddRange(erweiterteGoodSyncLogs);
                    StatusBarText = Properties.Resources.SucheAbgeschlossen;
                }
                catch (TaskCanceledException)
                {
                    StatusBarText = Properties.Resources.SucheAbgebrochen;
                }
                finally
                {
                    cancellationTokenSourceForSuche.Dispose();
                    StarteSucheLabel = Properties.Resources.SucheStarten;
                    dbAbfrageLaeuft = false;
                }
            }
        }

        private int cid;
        public int Cid
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

        public ICommand AddAktivitaetszeitraum { get; private set; }

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

        private IReportGenerator reportGenerator;
    }
}
