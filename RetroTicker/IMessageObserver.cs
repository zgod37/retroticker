using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTicker {
    public interface IMessageObserver {
        void displayMessages(List<Message> messages);
    }
}
