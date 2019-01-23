using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.VisualBasic;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace SysInfo


{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

        }

        /* LANGUAGE FROM MS WINDOWS */
        private static ResourceManager rm = new ResourceManager("SysInfo." + CultureInfo.CurrentUICulture.TwoLetterISOLanguageName + "_local", Assembly.GetExecutingAssembly());
        // FOR TEST        private static ResourceManager rm = new ResourceManager("SysInfo.sq_local", Assembly.GetExecutingAssembly());

        private void Label_Loading()
        {
            /* LABEL VALUES FROM LANGUAGE STRINGS */

            try
            {
                button1.Text = rm.GetString("computer");
                label2.Text = rm.GetString("system_type");
                label3.Text = rm.GetString("manufacturer");
                label4.Text = rm.GetString("model");

                button2.Text = rm.GetString("cpu");
                label5.Text = rm.GetString("cpu");
                label1.Text = rm.GetString("cores");
                label6.Text = rm.GetString("socket");

                button3.Text = rm.GetString("ram");
                label33.Text = rm.GetString("installed");
                label8.Text = rm.GetString("max_available");
                label7.Text = rm.GetString("slots");

                button7.Text = rm.GetString("bios");
                label11.Text = rm.GetString("manufacturer");
                label10.Text = rm.GetString("version");

                button13.Text = rm.GetString("optical_drives");
                label19.Text = rm.GetString("name");
                label20.Text = rm.GetString("letter");
                label25.Text = rm.GetString("type");

                button14.Text = rm.GetString("hard_disks");
                label31.Text = rm.GetString("name");
                label30.Text = rm.GetString("capacity");
                label29.Text = rm.GetString("_interface");

                button6.Text = rm.GetString("sound_devices");
                label27.Text = rm.GetString("name");
                label26.Text = rm.GetString("manufacturer");

                button5.Text = rm.GetString("network_adapters");
                label32.Text = rm.GetString("name");
                label28.Text = rm.GetString("manufacturer");

                button4.Text = rm.GetString("video");
                label9.Text = rm.GetString("name");
                label12.Text = rm.GetString("driver_version");
                label13.Text = rm.GetString("driver_date");

                button12.Text = rm.GetString("motherboard");
                label16.Text = rm.GetString("manufacturer");
                label17.Text = rm.GetString("model");

                button8.Text = rm.GetString("operating_system");
                label15.Text = rm.GetString("directory");
                label14.Text = rm.GetString("version");

                button9.Text = rm.GetString("accounts");
                button10.Text = rm.GetString("disk_partitions");
                button11.Text = rm.GetString("logical_disks");
                button17.Text = rm.GetString("environment_variables");
                button18.Text = rm.GetString("printers");
                button20.Text = rm.GetString("shared_printers");

                button15.Text = rm.GetString("battery");
                label24.Text = rm.GetString("status");
                label21.Text = rm.GetString("rated_charge");
                label22.Text = rm.GetString("full_charge");

                fileToolStripMenuItem.Text = rm.GetString("file");
                exitFromSysinfoToolStripMenuItem.Text = rm.GetString("exit");
                toolStripMenuItem1.Text = rm.GetString("_help");
                licenseToolStripMenuItem.Text = rm.GetString("license");
                informationToolStripMenuItem.Text = rm.GetString("about");

                printReportButton.Text = rm.GetString("print_report");
            }
            catch(MissingManifestResourceException)
            {

            }

        }

        private void Label_Clean()
        {
            computerSystem.Text = "";
            computerType.Text = "";
            computerManufacturer.Text = "";
            computerModel.Text = "";
            processor.Text = "";
            processorCores.Text = "";
            processorSocket.Text = "";
            memoryInstalled.Text = "";
            maxCapacity.Text = "";
            memoryDevices.Text = "";
            biosManufacturer.Text = "";
            biosVersion.Text = "";
            cdName.Text = "";
            cdLetter.Text = "";
            cdType.Text = "";
            diskName.Text = "";
            diskSize.Text = "";
            diskInterface.Text = "";
            soundName.Text = "";
            soundManu.Text = "";
            networkName.Text = "";
            networkManu.Text = "";
            videoName.Text = "";
            videoVersion.Text = "";
            videoDate.Text = "";
            baseBoardManu.Text = "";
            baseBoardProd.Text = "";
            baseBoardSerial.Text = "";
            osDirectory.Text = "";
            osVersion.Text = "";
            batteryStatus.Text = "";
            batteryEstimated.Text = "";
            batteryFullCharge.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)

        {
            int ok = 0;                                                             //Parameter for checking the availability of a device.
            Label_Clean();
            Label_Loading();

            string notAvb;
            try
            {
                notAvb = rm.GetString("press_buttons");                      //Error String
                toolStripStatusLabel1.Text = notAvb;
            }
            catch(MissingManifestResourceException)
            {
                notAvb = "Press the buttons for detailed informations";
                toolStripStatusLabel1.Text = notAvb;
            }

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
                        batteryStatus.Text = data["Status"].ToString();
                        batteryEstimated.Text = data["EstimatedChargeRemaining"].ToString();
                        batteryFullCharge.Text = data["FullChargeCapacity"].ToString();
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
                batteryStatus.Text = notAvb;
                batteryEstimated.Text = notAvb;
                batteryFullCharge.Text = notAvb;
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
                //networkName.Text = notAvb;
                //networkManu.Text = notAvb;
            }

            this.Text = "System Information: " + computerSystem.Text;
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
            datafrm.setFormName(button1.Text);
            datafrm.setDataClass("Win32_ComputerSystem");
            datafrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button2.Text);
            datafrm.setDataClass("Win32_Processor");
            datafrm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button3.Text);
            datafrm.setDataClass("Win32_PhysicalMemory");
            datafrm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button7.Text);
            datafrm.setDataClass("Win32_BIOS");
            datafrm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button13.Text);
            datafrm.setDataClass("Win32_CDROMDrive");
            datafrm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button14.Text);
            datafrm.setDataClass("Win32_DiskDrive");
            datafrm.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button10.Text);
            datafrm.setDataClass("Win32_DiskPartition");
            datafrm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button11.Text);
            datafrm.setDataClass("Win32_LogicalDisk");
            datafrm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button6.Text);
            datafrm.setDataClass("Win32_SoundDevice");
            datafrm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button5.Text);
            datafrm.setDataClass("Win32_NetworkAdapter");
            datafrm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button4.Text);
            datafrm.setDataClass("Win32_VideoController");
            datafrm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button12.Text);
            datafrm.setDataClass("Win32_BaseBoard");
            datafrm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button9.Text);
            datafrm.setDataClass("Win32_Account");
            datafrm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button8.Text);
            datafrm.setDataClass("Win32_OperatingSystem");
            datafrm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button15.Text);
            datafrm.setDataClass("Win32_Battery");
            datafrm.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button18.Text);
            datafrm.setDataClass("Win32_Printer");
            datafrm.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button17.Text);
            datafrm.setDataClass("Win32_Environment");
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

        private void button20_Click(object sender, EventArgs e)
        {
            DataForm datafrm = new DataForm();
            datafrm.setFormName(button20.Text);
            datafrm.setDataClass("Win32_PrinterShare");
            datafrm.Show();
        }
        private void printReportButton_Click(object sender, EventArgs e)
        {
            string[] lines = { "System Information Report - " + DateAndTime.DateString,
                               "=============================================",
                               "",
                               button1.Text,
                               button1.Text + ": " + computerSystem.Text,
                               label2.Text + ": " + computerType.Text,
                               label3.Text + ": " + computerManufacturer.Text,
                               label4.Text + ": " + computerModel.Text,
                               "",
                               button2.Text,
                               label5.Text + ": " + processor.Text,
                               label1.Text + ": " + processorCores.Text,
                               label6.Text + ": " + processorSocket.Text,
                               "",
                               button3.Text,
                               label33.Text + ": " + memoryInstalled.Text,
                               label8.Text + ": " + maxCapacity.Text,
                               label7.Text + ": " + memoryDevices.Text,
                               "",
                               button7.Text,
                               label11.Text + ": " + biosManufacturer.Text,
                               label10.Text + ": " + biosVersion.Text,
                               "",
                               button13.Text,
                               label19.Text + ": " + cdName.Text,
                               label20.Text + ": " + cdLetter.Text,
                               label25.Text + ": " + cdType.Text,
                               "",
                               button14.Text,
                               label31.Text + ": " + diskName.Text,
                               label30.Text + ": " + diskSize.Text,
                               label29.Text + ": " + diskInterface.Text,
                               "",
                               button6.Text,
                               label27.Text + ": " + soundName.Text,
                               label26.Text + ": " + soundManu.Text,
                               "",
                               button5.Text,
                               label32.Text + ": " + networkName.Text,
                               label28.Text + ": " + networkManu.Text,
                               "",
                               button4.Text,
                               label9.Text + ": " + videoName.Text,
                               label12.Text + ": " + videoVersion.Text,
                               label13.Text + ": " + videoDate.Text,
                               "",
                               button12.Text,
                               label16.Text + ": " + baseBoardManu.Text,
                               label17.Text + ": " + baseBoardProd.Text,
                               label18.Text + ": " + baseBoardSerial.Text,
                               "",
                               button8.Text,
                               osName.Text,
                               label15.Text + ": " + osDirectory.Text,
                               label14.Text + ": " + osVersion.Text,
                               "",
                               button15.Text,
                               label24.Text + ": " + batteryStatus.Text,
                               label21.Text + ": " + batteryEstimated.Text,
                               label22.Text + ": " + batteryFullCharge.Text
                            
                                                        
                                };
            System.IO.File.WriteAllLines(@"SysInfo - " + computerSystem.Text + ".txt", lines);
            System.Diagnostics.Process.Start("notepad.exe", @"SysInfo - " + computerSystem.Text + ".txt");
        }
    }
}
