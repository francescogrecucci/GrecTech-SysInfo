using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace SysInfo
{
    public partial class DataForm : Form

        
    {
        string data_value;

        public DataForm()
        {
            InitializeComponent();
        }

        public void setFormName(string form_name)
        {
            this.Text = form_name;
        }

        public void setDataClass(string data_class)
        {
            data_value = data_class;
        }


        private void DataForm_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher data = new ManagementObjectSearcher("select * from " + data_value);
            try
            {
                foreach (ManagementObject share in data.Get())
                {

                    ListViewGroup grp;
                    try
                    {
                        grp = lst.Groups.Add(share["Name"].ToString(), share["Name"].ToString());
                    }
                    catch
                    {
                        grp = lst.Groups.Add(share.ToString(), share.ToString());
                    }

                    if (share.Properties.Count <= 0)
                    {
                        MessageBox.Show("No Information Available", "No Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    foreach (PropertyData PC in share.Properties)
                    {

                        ListViewItem item = new ListViewItem(grp);
                        if (lst.Items.Count % 2 != 0)
                            item.BackColor = Color.White;
                        else
                            item.BackColor = Color.WhiteSmoke;

                        item.Text = PC.Name;

                        if (PC.Value != null && PC.Value.ToString() != "")
                        {
                            switch (PC.Value.GetType().ToString())
                            {
                                case "System.String[]":
                                    string[] str = (string[])PC.Value;

                                    string str2 = "";
                                    foreach (string st in str)
                                        str2 += st + " ";

                                    item.SubItems.Add(str2);

                                    break;
                                case "System.UInt16[]":
                                    ushort[] shortData = (ushort[])PC.Value;


                                    string tstr2 = "";
                                    foreach (ushort st in shortData)
                                        tstr2 += st.ToString() + " ";

                                    item.SubItems.Add(tstr2);

                                    break;

                                default:
                                    item.SubItems.Add(PC.Value.ToString());
                                    break;
                            }
                        }
                        else
                        {
                           /* if (!DontInsertNull)
                                item.SubItems.Add("No Information available");
                            else
                                continue;*/
                        }
                        lst.Items.Add(item);
                    }
                }
            }

            catch(Exception exp)
            {
                MessageBox.Show("Can't get any " + this.Text);
            }
        }
    }
}
