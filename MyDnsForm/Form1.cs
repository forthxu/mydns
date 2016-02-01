using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyDnsPackage;
namespace MyDnsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Type myenum = typeof(QueryType);
            Array ar= Enum.GetValues(myenum);
           QueryType qtype=  (QueryType ) ar.GetValue(cbType.SelectedIndex);
            MyDns mydns = new MyDns();
            if (!mydns.Search(txtDomain.Text.Trim(), qtype, txtDns.Text.Trim(), null ))
            {

                MessageBox.Show(mydns.header.RCODE.ToString());
                return;
            }
            txtInfo.Clear();
            txtInfo.AppendText (string.Format ("回复记录数：{0}\n",mydns.header.ANCOUNT) );
            txtInfo.AppendText(string.Format("回复额外记录数：{0}\n", mydns.header.ARCOUNT ));
            txtInfo.AppendText(string.Format("回复权威记录数：{0}", mydns.header.NSCOUNT ));

            txtContent.Clear();
            foreach (MyDnsRecord item in mydns.record.Records)
            {
                txtContent.AppendText(item.QType.ToString() + "   " + item.RDDate.ToString()+"\n");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Type myenum = typeof(QueryType);
            Array qt = Enum.GetNames(myenum);
            foreach (var name in qt)
                cbType.Items.Add(name);
            cbType.SelectedIndex = 0;
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
