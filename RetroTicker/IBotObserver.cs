using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public interface IBotObserver {

        void setBotStatus(String status);

    }
}
