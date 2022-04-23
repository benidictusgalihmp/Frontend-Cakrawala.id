using Cakrawala.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cakrawala.Models
{
    public class TransactionHistory
    {
        public string transactionId { get; set; }
        public TransactionType transactionType { get; set; }
        public string transactionNote { get; set; }
        public string urlImage { get; set; }
        public string headerDate { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedAt { get; set; }
        public User from { get; set; }
        public int amount { get; set; }
        public TransferStatus? status { get; set; }

        // TOP UP 
        public string topupMethod { get; set; }

        // TRANSFER
        public User to { get; set; }

        // TOP UP 
        public TransactionHistory(
            string transactionId,
            TransactionType transactionType,
            string transactionNote,
            String urlImage,
            String headerDate,
            DateTime createdDate,
            DateTime updatedAt,
            int amount,
            string topupMethod,
            TransferStatus? status
            )
        {
            this.transactionId = transactionId;
            this.transactionType = transactionType;
            this.transactionNote = transactionNote;
            this.urlImage = urlImage;
            this.headerDate = headerDate;
            this.createdDate = createdDate;
            this.updatedAt = updatedAt;
            this.from = null;
            this.to = null;
            this.amount = amount;
            this.topupMethod = topupMethod;
            this.status = status;
        }

        // TRANSFER
        public TransactionHistory(
            string transactionId,
            TransactionType transactionType,
            string transactionNote,
            String urlImage,
            String headerDate,
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
            this.headerDate = headerDate;
            this.createdDate = createdDate;
            this.updatedAt = updatedAt;
            this.from = new User(from);
            this.to = new User(to);
            this.amount = amount;
            this.topupMethod = "";
            this.status = status;
        }

        public TransactionHistory()
        {
            this.transactionId = "";
            this.transactionType = TransactionType.ISIULANG;
            this.transactionNote = "";
            this.urlImage = "";
            this.headerDate = "";
            this.createdDate = new DateTime();
            this.updatedAt = new DateTime();
            this.from = new User();
            this.to = new User();
            this.amount = 0;
            this.topupMethod = "";
            this.status = TransferStatus.PENDING;
        }
    }
}
