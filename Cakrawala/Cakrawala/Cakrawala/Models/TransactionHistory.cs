using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cakrawala.Models
{
    public class TransactionHistory
    {
        public int transactionId { get; set; }
        public TransactionType transactionType { get; set; }
        public string transactionNote { get; set; }
        public String urlImage { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedAt { get; set; }
        public User from { get; set; }
        public int amount { get; set; }
        public TransferStatus status { get; set; }

        // TOP UP 
        public string topupMethod { get; set; }

        // TRANSFER
        public User to { get; set; }

        // TOP UP 
        public TransactionHistory(
            int transactionId,
            TransactionType transactionType,
            string transactionNote,
            String urlImage,
            DateTime createdDate,
            DateTime updatedAt,
            User from,
            int amount,
            string topupMethod,
            TransferStatus status
            )
        {
            this.transactionId = transactionId;
            this.transactionType = transactionType;
            this.transactionNote = transactionNote;
            this.urlImage = urlImage;
            this.createdDate = createdDate;
            this.updatedAt = updatedAt;
            this.from = from;
            this.to = null;
            this.amount = amount;
            this.topupMethod = topupMethod;
            this.status = status;
        }

        // TRANSFER
        public TransactionHistory(
            int transactionId,
            TransactionType transactionType,
            string transactionNote,
            String urlImage,
            DateTime createdDate,
            DateTime updatedAt,
            User from,
            User to,
            int amount,
            TransferStatus status
            )
        {
            this.transactionId = transactionId;
            this.transactionType = transactionType;
            this.transactionNote = transactionNote;
            this.urlImage= urlImage;
            this.createdDate = createdDate;
            this.updatedAt = updatedAt;
            this.from = from;
            this.to = to;
            this.amount = amount;
            this.topupMethod = "";
            this.status = status;
        }
    }
}
