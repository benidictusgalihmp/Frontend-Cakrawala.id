﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cakrawala.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Bank), "bank")]
    [QueryProperty(nameof(BankNumber), "bankNumber")]
    [QueryProperty(nameof(TimeLimit), "timeLimit")]
    [QueryProperty(nameof(Nominal), "nominal")]
    public partial class TopupBankStepPage : ContentPage
    {

        private string bank;
        public string Bank
        {
            get { return bank; }
            set 
            {
                bank = value;
                this.bankLabel.Text = value;
            }
        }

        private string bankNumber;
        public string BankNumber
        {
            get { return bankNumber; }
            set
            {
                bankNumber = value;
                this.bankNumberLabel.Text = value;
            }
        }

        private string timeLimit;
        public string TimeLimit
        {
            get { return timeLimit;}
            set
            {
                timeLimit = value;
                timeLimitLabel.Text = value;
            }
        }

        private string nominal;
        public string Nominal
        {
            get { return nominal;}
            set
            {
                nominal = value;
                nominalLabel.Text = $"Rp {value}";
            }
        }

        public TopupBankStepPage()
        {
            InitializeComponent();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//dashboard");
        }
    }
}