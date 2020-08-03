using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailHandler
{
    public interface Iemailsender
    {

        Task SendMailAsync(Message message);
    }
}
