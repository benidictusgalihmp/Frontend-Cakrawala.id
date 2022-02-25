using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class TransactionHistory
    {
        public string transactionId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public User from { get; set; }
        public User to { get; set; }
        public int baseValue { get; set; }
        public Voucher voucher { get; set; }
        public TransferStatus status { get; set; }

        public TransactionHistory(
            string transactionId,
            DateTime createdAt,
            DateTime updatedAt,
            User from,
            User to,
            int baseValue,
            Voucher voucher,
            TransferStatus status)
        { 
            this.status = status;
            this.transactionId = transactionId;
            this.createdAt = createdAt; 
            this.updatedAt = updatedAt;
            this.voucher = new Voucher(voucher);
            this.from = new User(from);
            this.to = new User(to);
            this.baseValue = baseValue;

        }

        public TransactionHistory(TransactionHistory th) { 
            this.transactionId = th.transactionId;
            this.createdAt = th.createdAt;
            this.updatedAt = th.updatedAt;  
            this.voucher = new Voucher(th.voucher);
            this.status = th.status;
            this.baseValue= th.baseValue;
            this.from = new User(th.from);
            this.to = new User(th.to);
        }
            
    }
}
