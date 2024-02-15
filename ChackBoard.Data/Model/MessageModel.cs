using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChackBoard.Data.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
