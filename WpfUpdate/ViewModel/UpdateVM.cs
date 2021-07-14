using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update.ViewModel
{
    class UpdateVM : INotifyPropertyChanged
    {

        public UpdateVM(string[] agrs)
        {

        }

        public UpdateVM()
        {

        }


        public event PropertyChangedEventHandler PropertyChanged;

        #region 数据库
        /// <summary>
        /// 数据库
        /// </summary>
        public double _progressValuelue = 0.0;

        public double ProgressValue
        {
            get
            {
                return _progressValuelue;
            }
            set
            {
                _progressValuelue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ProgressValue"));
                }
            }
        }
        #endregion

    }
}
