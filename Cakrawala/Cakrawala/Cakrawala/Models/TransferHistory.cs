using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class TransferHistory
    {
        public string transactionId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public User from { get; set; }
        public User to { get; set; }
        public int baseValue { get; set; }
        public TransferStatus status { get; set; }

        public TransferHistory(
            string transactionId,
            DateTime createdAt,
            DateTime updatedAt,
            User from,
            User to,
            int baseValue,
            TransferStatus status)
        { 
            this.status = status;
            this.transactionId = transactionId;
            this.createdAt = createdAt; 
            this.updatedAt = updatedAt;
            this.from = new User(from);
            this.to = new User(to);
            this.baseValue = baseValue;

        }            
    }
}
