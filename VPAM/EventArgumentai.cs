using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPAM
{
    public class EventArgumentai: EventArgs
    {
        private string tekstas;
        private int skaicius;
     
        public string tekstasInfo
        {
            get
            {
                return tekstas;
            }
            set
            {
                if (this.tekstas != value)
                {
                    this.tekstas = value;
                }

            }
        }

        public int skaiciusInfo
        {
            get
            {
                return skaicius;
            }
            set
            {
                if (this.skaicius != value)
                {
                    this.skaicius = value;
                }

            }
        }


    }
}
