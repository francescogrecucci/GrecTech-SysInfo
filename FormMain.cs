using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.VisualBasic;

namespace SysInfo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            int ok = 0;                                                         //Parameter for checking the avaiability of a device.
            string notAvb = "Press the button to see more detailed infos.";    //Error String

            /* COMPUTER SYSTEM INFORMATIONS*/


            try
            {

                ManagementObjectSearcher computer_system = new ManagementObjectSearcher("select * from " + "Win32_ComputerSystem");
                foreach (ManagementObject data in computer_system.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {

                        computerSystem.Text = data["Caption"].ToString();
                        computerManufacturer.Text = data["Manufacturer"].ToString();
                        computerModel.Text = data["Model"].ToString();
                        computerType.Text = data["SystemType"].ToString();
                    }

                }
            }
            catch
            {
                computerSystem.Text = notAvb;
                computerManufacturer.Text = notAvb;
                computerModel.Text = notAvb;

            }

            /* CPU INFORMATIONS */

            try
            {

                ManagementObjectSearcher cpu = new ManagementObjectSearcher("select * from " + "Win32_Processor");
                foreach (ManagementObject data in cpu.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {

                        processor.Text = data["Name"].ToString();

                        try // for OS that doesn't have Cores and Socket Instances (Windows XP)
                        {
                            processorCores.Text = data["NumberOfCores"].ToString();
                            processorSocket.Text = data["SocketDesignation"].ToString();
                        }
                        catch(Exception)
                        {
                            label1.Visible = false;
                            label6.Visible = false;
                            processorCores.Visible = false;
                            processorSocket.Visible = false;
                        
                        }
                    }

                }
            }
            catch
            {
                processorCores.Text = notAvb;
                processorSocket.Text = notAvb;
            }


            /* RAM INFORMATIONS */

            try
            {
                ManagementObjectSearcher ram = new ManagementObjectSearcher("select * from " + "Win32_PhysicalMemoryArray");
                foreach (ManagementObject data in ram.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {

                        /*KB to MB Conversion (Capacity)*/

                        int mbcapacity;
                        int gbcapacity;

                        mbcapacity = Int32.Parse(data["MaxCapacity"].ToString());
                        mbcapacity = mbcapacity / 1024;
                        gbcapacity = mbcapacity / 1024;

                        maxCapacity.Text = gbcapacity + "GB" + " (" + mbcapacity + "MB" + ")";
                        memoryDevices.Text = data["MemoryDevices"].ToString();
                    }

                }
            }

            catch
            {
                maxCapacity.Text = notAvb;
                memoryDevices.Text = notAvb;
            }

            long installed_mbcapacity;
            long installed_gbcapacity;



            installed_mbcapacity = Int64.Parse(GetTotalMemoryInBytes().ToString());
            installed_mbcapacity = (installed_mbcapacity / 1024) / 1024;
            installed_gbcapacity = installed_mbcapacity / 1000;
            memoryInstalled.Text = installed_gbcapacity + "GB" + " (" + installed_mbcapacity + "MB" + ")";


            /* BIOS INFORMATIONS */
            ManagementObjectSearcher bios = new ManagementObjectSearcher("select * from " + "Win32_BIOS");
            foreach (ManagementObject data in bios.Get())
            {
                foreach (PropertyData PC in data.Properties)
                {

                    biosManufacturer.Text = data["Manufacturer"].ToString();
                    biosVersion.Text = data["Version"].ToString();
                }

            }

            /* VIDEO INFORMATIONS */
            try
            {
             
                ManagementObjectSearcher video = new ManagementObjectSearcher("select * from " + "Win32_VideoController");
                foreach (ManagementObject data in video.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {

                        videoName.Text = data["Name"].ToString();
                        videoVersion.Text = data["DriverVersion"].ToString();
                        videoDate.Text = data["DriverDate"].ToString();
                    }

                }
            }
            catch
            {
                videoName.Text = notAvb;
                videoVersion.Text = notAvb;
                videoDate.Text = notAvb;
            }

            /* OS INFORMATIONS */
            try
            {
                ManagementObjectSearcher opsys = new ManagementObjectSearcher("select * from " + "Win32_OperatingSystem");
                foreach (ManagementObject data in opsys.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        try
                        {
                            osName.Text = data["Caption"].ToString() + " " + data["OSArchitecture"].ToString();
                        }
                        catch
                        {
                            osName.Text = data["Caption"].ToString();   // For OS that doesn't have Architecture Instance (Windows XP)
                        }
                        osDirectory.Text = data["SystemDirectory"].ToString();
                        osVersion.Text = data["Version"].ToString();
                    }

                }
            }
            catch
            {
                osName.Text = notAvb;
                osDirectory.Text = notAvb;
                osVersion.Text = notAvb;
            }

            /* MOTHERBOARD INFORMATIONS */
            try
            {
                ManagementObjectSearcher mobo = new ManagementObjectSearcher("select * from " + "Win32_BaseBoard");
                foreach (ManagementObject data in mobo.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {

                        baseBoardManu.Text = data["Manufacturer"].ToString();
                        baseBoardProd.Text = data["Product"].ToString();
                        baseBoardSerial.Text = data["SerialNumber"].ToString();
                    }

                }
            }
            catch
            {
                baseBoardManu.Text = notAvb;
                baseBoardProd.Text = notAvb;
                baseBoardSerial.Text = notAvb;
            }

            /* BATTERY INFORMATIONS */
            try
            {
                ok = 0;
                ManagementObjectSearcher battery = new ManagementObjectSearcher("select * from " + "Win32_Battery");
                foreach (ManagementObject data in battery.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        batteryEstimated.Text = data["EstimatedChargeRemaining"].ToString();
                        batteryFullCharge.Text = data["FullChargeCapacity"].ToString();
                        batteryStatus.Text = data["Status"].ToString();
                        ok++;
                    }

                }

                if (ok == 0)
                {
                    button15.Enabled = false;
                    batteryEstimated.Visible = false;
                    batteryFullCharge.Visible = false;
                    batteryStatus.Visible = false;
                    label24.Enabled = false;
                    label21.Enabled = false;
                    label22.Enabled = false;
                }
            }
            catch
            {
                button15.Enabled = true;
                batteryEstimated.Visible = false;
                batteryFullCharge.Visible = false;
                batteryStatus.Visible = false;
                label24.Enabled = false;
                label21.Enabled = false;
                label22.Enabled = false;
            }


            /* CD-ROM/DVD-ROM INFORMATIONS */
            try
            {
                ok = 0;
                ManagementObjectSearcher cdrom = new ManagementObjectSearcher("select * from " + "Win32_CDROMDrive");
                foreach (ManagementObject data in cdrom.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        cdLetter.Text = data["Drive"].ToString();
                        cdName.Text = data["Name"].ToString();
                        cdType.Text = data["MediaType"].ToString();
                        ok++;
                    }

                }

                if (ok == 0)
                {
                    button13.Enabled = false;
                    cdLetter.Visible = false;
                    cdName.Visible = false;
                    cdType.Visible = false;
                    label19.Enabled = false;
                    label20.Enabled = false;
                    label25.Enabled = false;
                }
            }
            catch
            {
                cdLetter.Text = notAvb;
                cdName.Text = notAvb;
                cdType.Text = notAvb;
            }

            /* HARD DRIVES INFORMATIONS*/

            try
            {
                ManagementObjectSearcher disk = new ManagementObjectSearcher("select * from " + "Win32_DiskDrive");
                foreach (ManagementObject data in disk.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        diskInterface.Text = data["InterfaceType"].ToString();
                        diskName.Text = data["Caption"].ToString();

                        long mbsize, gbsize;

                        mbsize = Int64.Parse(data["Size"].ToString());
                        mbsize = (mbsize / 1024) / 1024;
                        gbsize = mbsize / 1024;

                        diskSize.Text = gbsize + "GB" + " (" + mbsize + "MB" + ")";
                    }

                }
            }
            catch
            {
                diskInterface.Text = notAvb;
                diskName.Text = notAvb;
            }

            /* AUDIO INFORMATIONS */
            try
            {
                ok = 0;
                ManagementObjectSearcher sound = new ManagementObjectSearcher("select * from " + "Win32_SoundDevice");
                foreach (ManagementObject data in sound.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        soundName.Text = data["Caption"].ToString();
                        soundManu.Text = data["Manufacturer"].ToString();
                        ok++;
                    }

                }

                if (ok == 0)
                {
                    button6.Enabled = false;
                    soundName.Visible = false;
                    soundManu.Visible = false;
                    label26.Enabled = false;
                    label27.Enabled = false;
                }
            }
            catch
            {
                soundName.Text = notAvb;
                soundManu.Text = notAvb;
            }


            /* NETWORK INFORMATIONS */
            try
            {
                ok = 0;
                ManagementObjectSearcher network = new ManagementObjectSearcher("select * from " + "Win32_NetworkAdapter");
                foreach (ManagementObject data in network.Get())
                {
                    foreach (PropertyData PC in data.Properties)
                    {
                        networkName.Text = data["Description"].ToString();
                        networkManu.Text = data["Manufacturer"].ToString();
                        ok++;
                    }

                }

                if (ok == 0)
                {
                    button5.Enabled = false;
                    networkName.Visible = false;
                    networkManu.Visible = false;
                    label32.Enabled = false;
                    label28.Enabled = false;
                }
            }
            catch
            {
                networkName.Text = notAvb;
                networkManu.Text = notAvb;
            }

            this.Text = "System Information on " + computerSystem.Text;
        }


        static ulong GetTotalMemoryInBytes()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            License license = new License();
            license.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button1.Text + " Information");
            datafrm.setDataClass("Win32_ComputerSystem");
            datafrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button2.Text + " Information");
            datafrm.setDataClass("Win32_Processor");
            datafrm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button3.Text + " Information");
            datafrm.setDataClass("Win32_PhysicalMemory");
            datafrm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button7.Text + " Information");
            datafrm.setDataClass("Win32_BIOS");
            datafrm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button13.Text + " Information");
            datafrm.setDataClass("Win32_CDROMDrive");
            datafrm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button14.Text + " Information");
            datafrm.setDataClass("Win32_DiskDrive");
            datafrm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button10.Text + " Information");
            datafrm.setDataClass("Win32_DiskPartition");
            datafrm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button11.Text + " Information");
            datafrm.setDataClass("Win32_LogicalDisk");
            datafrm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button6.Text + " Information");
            datafrm.setDataClass("Win32_SoundDevice");
            datafrm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button5.Text + " Information");
            datafrm.setDataClass("Win32_NetworkAdapter");
            datafrm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button4.Text + " Information");
            datafrm.setDataClass("Win32_VideoController");
            datafrm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button12.Text + " Information");
            datafrm.setDataClass("Win32_BaseBoard");
            datafrm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button9.Text + " Information");
            datafrm.setDataClass("Win32_Account");
            datafrm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button8.Text + " Information");
            datafrm.setDataClass("Win32_OperatingSystem");
            datafrm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button15.Text + " Information");
            datafrm.setDataClass("Win32_Battery");
            datafrm.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button18.Text + " Information");
            datafrm.setDataClass("Win32_Printer");
            datafrm.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button17.Text + " Information");
            datafrm.setDataClass("Win32_Environment");
            datafrm.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button16.Text + " Information");
            datafrm.setDataClass("Win32_PortConnector");
            datafrm.Show();
        }

        private void exitFromSysinfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
    }
}
