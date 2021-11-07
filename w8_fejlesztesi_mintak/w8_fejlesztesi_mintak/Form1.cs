﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using w8_fejlesztesi_mintak.Abstractions;
using w8_fejlesztesi_mintak.Entities;

namespace w8_fejlesztesi_mintak
{
    public partial class Form1 : Form
    {
        private Toy _nextToy;

        private List<Toy> _toys = new List<Toy>();
       
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set 
            { _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();

            Factory = new CarFactory();// töltsd fel a Factory változót egy BallFactory példánnyal
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();//Factory CreateNew metódusát felhasználva hozz létre egy Ball példányt.
            _toys.Add(toy);
           toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition =0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (maxPosition< item.Left)
                {
                    maxPosition = item.Left;
                }
            }

            if (maxPosition>=100)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy!= null)//nem üres
            {
                Controls.Remove(_nextToy);
            }

            _nextToy = Factory.CreateNew();//töltsdfel _nextToy értékét CN metóddal
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);

        }
    }
}
