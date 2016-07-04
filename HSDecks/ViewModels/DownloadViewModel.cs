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
            // default not ready
            this.IsDownloadVisible = true;
            this.IsDeleteVisible = false;
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

        public bool _isDownloadVisible;
        public bool IsDownloadVisible
        {
            get { return _isDownloadVisible; }
            set
            {
                SetProperty(ref _isDownloadVisible, value);
            }
        }

        public bool _isDeleteVisible;
        public bool IsDeleteVisible
        {
            get { return _isDeleteVisible; }
            set
            {
                SetProperty(ref _isDeleteVisible, value);
            }
        }
    }
}
