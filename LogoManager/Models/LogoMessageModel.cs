using System;

namespace LogoManager.Models
{
    public class LogoMessageModel
    {
        public string message { get; set; }
        public DateTime time { get; set; }
        public string clientInfo { get; set; }
        public override string ToString()
        {
            return $"{time} [{clientInfo}] {message}";
        }
    }
}
