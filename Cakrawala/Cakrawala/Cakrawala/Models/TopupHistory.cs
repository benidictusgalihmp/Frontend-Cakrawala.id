using System;
using System.Collections.Generic;
using System.Text;

namespace Cakrawala.Models
{
    public class TopupHistory
    {
        public string topupId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public User from { get; set; }
        public int value { get; set; }
        public string method { get; set; }
        public TransferStatus status { get; set; }

        public TopupHistory(string topupId,
                            DateTime createdAt,
                            DateTime updatedAt,
                            User from,
                            int value,
                            string method,
                            TransferStatus status)
        { 
            this.topupId = topupId;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            this.method = method;
            this.status = status;
            this.from = new User(from);
            this.value = value;
        }

        public TopupHistory(TopupHistory topupHistory) { 
            this.topupId=topupHistory.topupId;
            this.createdAt=topupHistory.createdAt;
            this.status=topupHistory.status;
            this.from = new User(topupHistory.from);
            this.value = topupHistory.value;
            this.updatedAt=topupHistory.updatedAt;
            this.method = topupHistory.method;  
        }
    }
}
