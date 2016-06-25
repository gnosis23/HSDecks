using FuckUWP1.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDecks.ViewModels {
    public class DownloadViewModel: BindableBase {

        public DownloadViewModel(string Title) {
            this.Title = Title;
            this.Size = 0;
        }

        double _progress;
        public double Progress {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        ulong _size;
        public ulong Size {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }

        string _status;
        public string Status {
            get { return _status; }
            set { SetProperty(ref _status, value);  }
        }

        public string Title { get; set; }

        public string FileName { get; set; }
        public string FullFileName { get; set; }
        public string Address { get; set; }

        public Guid guid { get; set; }
    }
}
