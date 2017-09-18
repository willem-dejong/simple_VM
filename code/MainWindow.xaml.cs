//Willem DeJong
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace VM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//main window class
    {
        private string pathname;//string for the file location and name
        public MainWindow()
        {
            InitializeComponent();
        }
        static public int bitoint(string bi)//converts a string representing a binary number to an integer assuming unsigned
        {
            int val = 0;
            for (int x = bi.Length - 1; x >= 0; x--)
            {
                if (bi.Substring(bi.Length - 1 - x, 1) == "1")
                    val = val + (int)Math.Pow(2, x);
            }
            return val;
        }
        static public string hextobi(string hex, int bilength)//converts a string representing a hex number to a binarry number of length bilength if bilength is too short the lowwer digits are cut off
        {
            int i = 0;
            for (int x = hex.Length - 1; x >= 0; x--)
            {
                switch (hex[hex.Length - 1 - x])
                {
                    case '0':
                        break;
                    case '1':
                        i = i + (int)Math.Pow(16, x);
                        break;
                    case '2':
                        i = i + 2 * (int)Math.Pow(16, x);
                        break;
                    case '3':
                        i = i + 3 * (int)Math.Pow(16, x);
                        break;
                    case '4':
                        i = i + 4 * (int)Math.Pow(16, x);
                        break;
                    case '5':
                        i = i + 5 * (int)Math.Pow(16, x);
                        break;
                    case '6':
                        i = i + 6 * (int)Math.Pow(16, x);
                        break;
                    case '7':
                        i = i + 7 * (int)Math.Pow(16, x);
                        break;
                    case '8':
                        i = i + 8 * (int)Math.Pow(16, x);
                        break;
                    case '9':
                        i = i + 9 * (int)Math.Pow(16, x);
                        break;
                    case 'A':
                        i = i + 10 * (int)Math.Pow(16, x);
                        break;
                    case 'B':
                        i = i + 11 * (int)Math.Pow(16, x);
                        break;
                    case 'C':
                        i = i + 12 * (int)Math.Pow(16, x);
                        break;
                    case 'D':
                        i = i + 13 * (int)Math.Pow(16, x);
                        break;
                    case 'E':
                        i = i + 14 * (int)Math.Pow(16, x);
                        break;
                    case 'F':
                        i = i + 15 * (int)Math.Pow(16, x);
                        break;
                }
            }
            return itobi(i, bilength);
        }
        static private string inttohex(int x)//converts an integer to a string representing an hex number
        {
            string bi = "";
            while (x > 0)
            {
                int y = x % 16;
                x = x / 16;
                switch (y)
                {
                    case 0:
                        bi = "0" + bi;
                        break;
                    case 1:
                        bi = "1" + bi;
                        break;
                    case 2:
                        bi = "2" + bi;
                        break;
                    case 3:
                        bi = "3" + bi;
                        break;
                    case 4:
                        bi = "4" + bi;
                        break;
                    case 5:
                        bi = "5" + bi;
                        break;
                    case 6:
                        bi = "6" + bi;
                        break;
                    case 7:
                        bi = "7" + bi;
                        break;
                    case 8:
                        bi = "8" + bi;
                        break;
                    case 9:
                        bi = "9" + bi;
                        break;
                    case 10:
                        bi = "A" + bi;
                        break;
                    case 11:
                        bi = "B" + bi;
                        break;
                    case 12:
                        bi = "C" + bi;
                        break;
                    case 13:
                        bi = "D" + bi;
                        break;
                    case 14:
                        bi = "E" + bi;
                        break;
                    case 15:
                        bi = "F" + bi;
                        break;
                }
            }
            return bi;
        }
        static public string bitohex(string bi)
        {//converts bi (which represents a binary number) to a string rep hex
            int y = bi.IndexOf('1') / 4;//allows for bi 00000001 to be represented as hex 01 this is so the 16 bit binary can be represented as xxxx, 0xxx,00xx,000x, and 0000 instead of xxxx, xxx, xx, and x for convience but 001000 will be just 8 not 08.
            int x = bitoint(bi);
            string hex = "";
            for (; y > 0; y--)
                hex = "0" + hex;
            return hex + inttohex(x);
        }
        static public string itobi(int i, int length)//length is num of bits, 2's comp. converts an int to a 2's comp binary number (i think technially it will also convert high enough positives into 
        {//if length is too short the lower end digits are cut off
            string bi = "";
            if (i < 0)
            {
                i = Math.Abs(i) - 1;
                for (int x = length; x > 0; i = i / 2, x--)
                {
                    if (i % 2 == 1)
                        bi = "0" + bi;
                    else
                        bi = "1" + bi;
                }
            }
            else
            {
                for (int x = length; x > 0; i = i / 2, x--)
                {
                    if (i % 2 == 1)
                        bi = "1" + bi;
                    else
                        bi = "0" + bi;
                }
            }
            return bi;
        }
        public static string translate(string comand)// assumes language format of "command arg1 arg2 arg3 ..." Ex("ADD R3 R2 R1" or "ADD R3 R2 7") assumes no reg above R7 as input
        {
            string bi;
            int i = comand.IndexOf(' ');
            int ii = comand.IndexOf(' ', i + 1);
            string comandname = comand.Substring(0, i);
            switch (comandname)
            {
                case "ADD":
                    bi = "0001";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(i + 2, 1)), 3);
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(ii + 2, 1)), 3);
                    if (comand.IndexOf('R', ii + 2) < 0)
                    {
                        int iii = int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)) % 16;// restricts the number to +/-15
                        bi = bi + "1" + MainWindow.itobi(iii, 5);
                    }
                    else
                        bi = bi + "000" + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf('R') + 1)), 3);
                    return bi;
                case "AND":
                    bi = "0101";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(i + 2, 1)), 3);
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(ii + 2, 1)), 3);
                    if (comand.IndexOf('R', ii + 2) < 0)
                        bi = bi + "1" + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 5);
                    else
                        bi = bi + "000" + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf('R') + 1)), 3);
                    return bi;
                case "NOT":
                    bi = "1001";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(i + 2, 1)), 3);
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(ii + 2, 1)), 3);
                    bi = bi + "111111";
                    return bi;
                case "BR":
                    bi = "0000";
                    if (comand.IndexOf('n') >= 0)
                        bi = bi + "1";
                    else
                        bi = bi + "0";
                    if (comand.IndexOf('z') >= 0)
                        bi = bi + "1";
                    else
                        bi = bi + "0";
                    if (comand.IndexOf('p') >= 0)
                        bi = bi + "1";
                    else
                        bi = bi + "0";
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 9);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 9);
                    return bi;
                case "LD":
                    bi = "0010";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.IndexOf('R') + 1, 1)), 3);
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 9);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 9);
                    return bi;
                case "LDI":
                    bi = "1010";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.IndexOf('R') + 1, 1)), 3);
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 9);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 9);
                    return bi;
                case "ST":
                    bi = "0011";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.IndexOf('R') + 1, 1)), 3);
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 9);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 9);
                    return bi;
                case "STI":
                    bi = "1011";
                    bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.IndexOf('R') + 1, 1)), 3);
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 9);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 9);
                    return bi;
                case "TRAP":
                    bi = "1111";
                    if (comand.IndexOf('x') < 0)
                        bi = bi + MainWindow.itobi(int.Parse(comand.Substring(comand.LastIndexOf(' ') + 1)), 12);
                    else
                        bi = bi + hextobi(comand.Substring(comand.LastIndexOf('x') + 1), 12);
                    return bi;

            }
            return "";
        }

        private void compb_Click(object sender, RoutedEventArgs e)
        {
            string[] toCompile = File.ReadAllLines(pathboxtocomp.Text);
            string[] hex = new string[toCompile.Length];
            for (int x = 0; x < toCompile.Length; x++)
            {
                toCompile[x] = translate(toCompile[x]);
                string temp = bitohex(toCompile[x]);
                while (temp.Length < 4)
                    temp = "0" + temp;
                hex[x] = temp;
            }
            pathname = pathboxtocomp.Text.Substring(0, pathboxtocomp.Text.IndexOf('.') + 1);
            File.WriteAllLines(pathname + "lc2b", toCompile);
            File.WriteAllLines(pathname + "lc2h", hex);
        }
        public static void dump(string[] toDump, string pathname)
        {
            File.WriteAllLines(pathname + "out", toDump);
        }
    }
    partial class Memline16//represents a single address of memory
    {
        private string mem;//string contaianing the string rep of the binary
        public Memline16()
        {
            mem = "0000000000000000";
        }
        public Memline16(string data)
        {
            mem = data;
        }
        public void store(string data)//stores data to this line of mem
        {
            mem = data;
        }
        public string retreive()//retreives this line of mem
        {
            return mem;
        }
    }
    partial class Reg16bit : Grid//represents a 16 bit reg
    {
        private TextBox label;//textbox for name of reg
        private TextBox reg;//textbox for the 16 bit 
        private bool regb;//bool for visuals, represents if the register is highlighted
        public Reg16bit()
        {
            regb = false;
            this.Focusable = false;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Height = 22;
            Width = 142;
            label = new TextBox();
            label.Text = "Ins";
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Height = 22;
            label.Width = 28;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.IsReadOnly = true;
            label.Focusable = false;
            this.Children.Add(label);
            reg = new TextBox();
            reg.Text = "0000000000000000";
            reg.BorderBrush = new SolidColorBrush(Colors.Black);
            reg.Height = 22;
            reg.Width = 114;
            reg.VerticalAlignment = VerticalAlignment.Top;
            reg.HorizontalAlignment = HorizontalAlignment.Left;
            reg.Margin = new Thickness(28, 0, 0, 0);
            reg.Focusable = false;
            reg.IsReadOnly = true;
            this.Children.Add(reg);
        }
        public Reg16bit(string lab)
        {
            regb = false;
            this.Focusable = false;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Height = 22;
            Width = 142;
            label = new TextBox();
            label.Text = lab;
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Height = 22;
            label.Width = 28;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.IsReadOnly = true;
            label.Focusable = false;
            this.Children.Add(label);
            reg = new TextBox();
            reg.Text = "0000000000000000";
            reg.BorderBrush = new SolidColorBrush(Colors.Black);
            reg.Height = 22;
            reg.Width = 114;
            reg.VerticalAlignment = VerticalAlignment.Top;
            reg.HorizontalAlignment = HorizontalAlignment.Left;
            reg.Margin = new Thickness(28, 0, 0, 0);
            reg.Focusable = false;
            reg.IsReadOnly = true;
            this.Children.Add(reg);
        }
        public void store(string data)//stores data to this reg
        {
            reg.Text = data;
        }
        public string retrieve()//retrieves data from this reg
        {
            return reg.Text;
        }
        public void highlight(int inn)//highlights this reg with the option inn
        {
            regb = true;
            switch (inn)
            {
                case 0:
                    reg.Background = new SolidColorBrush(Colors.Blue);
                    return;
                case 1:
                    reg.Background = new SolidColorBrush(Colors.Red);
                    return;
                case 2:
                    reg.Background = new SolidColorBrush(Colors.Purple);
                    return;
            }
        }
        public void dehighlight()//dehighlights this reg
        {
            reg.Background = null;
            regb = false;
        }
        public bool ishighlight()// returns bool representing if ThemeDictionaryExtension reg is highlighted
        {
            return regb;
        }
    }
    partial class Reg3bit : Grid//represents a 3 bit reg
    {
        private TextBox label;//textbox for reg label
        private TextBox reg;//textbox for reg data
        private bool regb;//bool representing if highlighted
        public Reg3bit()
        {
            regb = false;
            this.Focusable = false;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Height = 22;
            Width = 60;
            label = new TextBox();
            label.Text = "NZP";
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Height = 22;
            label.Width = 30;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.IsReadOnly = true;
            label.Focusable = false;
            this.Children.Add(label);
            reg = new TextBox();
            reg.Text = "010";
            reg.BorderBrush = new SolidColorBrush(Colors.Black);
            reg.Height = 22;
            reg.Width = 30;
            reg.VerticalAlignment = VerticalAlignment.Top;
            reg.HorizontalAlignment = HorizontalAlignment.Left;
            reg.Margin = new Thickness(30, 0, 0, 0);
            reg.Focusable = false;
            reg.IsReadOnly = true;
            this.Children.Add(reg);
        }
        public void store(string data)//stores data to this reg
        {
            reg.Text = data;
        }
        public string retrieve()//retreives the binary string of this reg
        {
            return reg.Text;
        }
        public void highlight(int inn)//highlights this reg with option inn
        {
            regb = true;
            switch (inn)
            {
                case 0:
                    reg.Background = new SolidColorBrush(Colors.Blue);
                    return;
                case 1:
                    reg.Background = new SolidColorBrush(Colors.Red);
                    return;
                case 2:
                    reg.Background = new SolidColorBrush(Colors.Purple);
                    return;
            }
        }
        public void dehighlight()//dehighlights this reg
        {
            reg.Background = null;
            regb = false;
        }
        public bool ishighlight()//returns whether this reg is highlighted
        {
            return regb;
        }
    }
    partial class ALU : Grid//represents the part of the cpu that decodes and performs the logic circuts/ commands
    {
        private TextBox label;//label for box that shows the binary instruction currently fetched
        private TextBox reg;//textbox for the binary instruction currently fetched
        //private bool regb;//bool for if instructions is highlighted
        public ALU()
        {
            //regb = false;
            this.Focusable = false;
            VerticalAlignment = VerticalAlignment.Bottom;
            HorizontalAlignment = HorizontalAlignment.Left;
            Height = 22;
            Width = 145;
            label = new TextBox();
            label.Text = "Ins";
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Height = 22;
            label.Width = 28;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.IsReadOnly = true;
            label.Focusable = false;
            this.Children.Add(label);
            reg = new TextBox();
            reg.Text = "0000000000000000";
            reg.VerticalAlignment = VerticalAlignment.Top;
            reg.HorizontalAlignment = HorizontalAlignment.Left;
            reg.Margin = new Thickness(28, 0, 0, 0);
            reg.BorderBrush = new SolidColorBrush(Colors.Black);
            reg.Focusable = false;
            reg.IsReadOnly = true;
            this.Children.Add(reg);
        }
        public ALU(string lab)
        {
            //regb = false;
            this.Focusable = false;
            VerticalAlignment = VerticalAlignment.Bottom;
            HorizontalAlignment = HorizontalAlignment.Left;
            Height = 22;
            Width = 145;
            label = new TextBox();
            label.Text = lab;
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Height = 22;
            label.Width = 28;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.IsReadOnly = true;
            label.Focusable = false;
            this.Children.Add(label);
            reg = new TextBox();
            reg.Text = "0000000000000000";
            reg.VerticalAlignment = VerticalAlignment.Top;
            reg.HorizontalAlignment = HorizontalAlignment.Left;
            reg.Margin = new Thickness(28, 0, 0, 0);
            reg.BorderBrush = new SolidColorBrush(Colors.Black);
            reg.Focusable = false;
            reg.IsReadOnly = true;
            this.Children.Add(reg);
        }
        public void store(string data)//stores data to instructions 
        {
            reg.Text = data;
        }
        public string retreive()//returns the intructions(mostlikly not currently used)
        {
            return reg.Text;
        }
        private void updatecond(string data)//logic to determine what to update the nzp reg to
        {
            if (data[0] == '1')
                ((Control)((CPU)this.Parent).Parent).updateCC("100");
            else
            {
                string cond = "010";
                for (int x = 1; x < 16; x++)
                {
                    if (data[x] == '1')
                    {
                        cond = "001";
                        break;
                    }
                }
                ((Control)((CPU)this.Parent).Parent).updateCC(cond);
            }
        }
        public bool swtch()//decodes which 'circutry' to use (ie. ADD, AND, etc)
        {
            string ins = reg.Text.Substring(0, 4);
            string data = reg.Text.Substring(4);
            switch (ins)
            {
                case "0001":
                    add(data);
                    //((Control)((CPU)this.Parent).Parent).incrementIP();
                    return true;
                case "0101":
                    and(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
                case "1001":
                    not(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
                case "0000":
                    return br(data);
                //break;
                case "0010":
                    ld(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
                case "0011":
                    st(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
                case "1111":
                    return trap(data);
                //break;
                case "1010":
                    ldi(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
                case "1011":
                    sti(data);
                    return true;
                //((Control)((CPU)this.Parent).Parent).incrementIP();
                //break;
            }
            return true;
        }
        private void add(string data)//takes in the 12 bit string representing the argument of the command and performs add
        {
            int sra = MainWindow.bitoint(data.Substring(3, 3));//grabs the 3 bits representing the first or only source register this is used for the visual changes not for the machine representation
            int srb = -1;//initializes the second 
            string data1 = ((CPU)this.Parent).retrieve(data.Substring(3, 3));//retrieves the binary string from the corresponding register this is for the simulated machine circut
            string data2;
            if (data[6] == '0')//checks to see which add operation to do
            {
                data2 = ((CPU)this.Parent).retrieve(data.Substring(9, 3));//if the two source reg opperation grabs the corrisponding 16 bit data
                srb = MainWindow.bitoint(data.Substring(9, 3));// the int rep for the reg for the visualization changes
            }
            else//if the 1 source reg add operation converts the 5 bit 2's comp number and sign extends it
            {
                data2 = data.Substring(7);
                if (data2[0] == '0')
                    data2 = "00000000000" + data2;
                else
                    data2 = "11111111111" + data2;
            }
            char carry = '0';//for carrying bit
            for (int x = 16; x > 0; x--)//goes though each bit and adds the two and goes through the logic 'circut' to add the two 16 bit binary numbers overflow ignored/tossed out
            {
                if (carry == '1')
                {
                    if (data1[x - 1] == '0')
                    {
                        data1 = data1.Substring(0, x - 1) + "1" + data1.Substring(x);
                        carry = '0';
                    }
                    else
                        data1 = data1.Substring(0, x - 1) + "0" + data1.Substring(x);
                }
                if (data2[x - 1] == '1')
                {
                    if (data1[x - 1] == '1')
                    {
                        data1 = data1.Substring(0, x - 1) + "0" + data1.Substring(x);
                        data2 = data2.Substring(0, x - 1) + "0" + data2.Substring(x);
                        carry = '1';
                    }
                    else
                    {
                        data1 = data1.Substring(0, x - 1) + "1" + data1.Substring(x);
                        data2 = data2.Substring(0, x - 1) + "1" + data2.Substring(x);
                    }
                }
                else
                {
                    if (data1[x - 1] == '1')
                    {
                        data1 = data1.Substring(0, x - 1) + "1" + data1.Substring(x);
                        data2 = data2.Substring(0, x - 1) + "1" + data2.Substring(x);
                    }
                    else
                    {
                        data1 = data1.Substring(0, x - 1) + "0" + data1.Substring(x);
                        data2 = data2.Substring(0, x - 1) + "0" + data2.Substring(x);
                    }
                }
            }
            ((CPU)this.Parent).store(data.Substring(0, 3), data1);//stores the resulting 16 bit binary number to the dr register
            int dr = MainWindow.bitoint(data.Substring(0, 3));//int rep of the dr reg for visualization changes 
            //the following logic structure determins which and does highlight corresponding registers with the 
            //color representing whether the reg is used as a sr(blue), dr(red), or both(purple)
            if (srb > -1)
            {
                if (sra == dr)
                {
                    ((CPU)(this.Parent)).highlight(sra, 2);
                    if (srb != sra)
                        ((CPU)(this.Parent)).highlight(srb, 0);
                }
                else
                {
                    ((CPU)(this.Parent)).highlight(sra, 0);
                    if (srb == dr)
                        ((CPU)(this.Parent)).highlight(srb, 2);
                    else
                    {
                        if (srb != sra)
                            ((CPU)(this.Parent)).highlight(srb, 0);
                        ((CPU)(this.Parent)).highlight(dr, 1);
                    }
                }
            }
            else
            {
                if (sra == dr)
                    ((CPU)(this.Parent)).highlight(sra, 2);
                else
                {
                    ((CPU)(this.Parent)).highlight(sra, 0);
                    ((CPU)(this.Parent)).highlight(dr, 1);
                }
            }
            updatecond(data1);//updates nzp reg
        }
        private void and(string data)//takes in the 12 bit string representing the argument of the command and performs and
        {
            int sra = MainWindow.bitoint(data.Substring(3, 3));//grabs the 3 bits representing the first or only source register this is used for the visual changes not for the machine representation
            int srb = -1;//initializes the second 
            string data1 = ((CPU)this.Parent).retrieve(data.Substring(3, 3));//retrieves the binary string from the corresponding register this is for the simulated machine circut
            string data2 = "";
            string data3 = "";
            if (data[6] == '0')//checks to see which and operation to do
            {
                data2 = ((CPU)this.Parent).retrieve(data.Substring(9, 3));//if the two source reg opperation grabs the corrisponding 16 bit data
                srb = MainWindow.bitoint(data.Substring(9, 3));// the int rep for the reg for the visualization changes
            }
            else//if the 1 source reg add operation converts the 5 bit 2's comp number and sign extends it
            {
                data2 = data.Substring(7);//string from index 7 of the 12 bit to end
                if (data2[0] == '0')
                    data2 = "00000000000" + data2;
                else
                    data2 = "11111111111" + data2;
            }
            for (int x = 0; x < 16; x++)//goes through each bit and checks if both '1'like an 'and' logic circut would do. 
            {
                if (data1[x] == '1' && data2[x] == '1')
                    data3 = data3 + "1";
                else
                    data3 = data3 + "0";
            }
            ((CPU)this.Parent).store(data.Substring(0, 3), data3);//stores the result to dr register
            int dr = MainWindow.bitoint(data.Substring(0, 3));// int rep of dr reg for visual changes
            //logic structer that controls/ decides which registers to highlight and what color
            if (srb > -1)
            {
                if (sra == dr)
                {
                    ((CPU)(this.Parent)).highlight(sra, 2);
                    if (srb != sra)
                        ((CPU)(this.Parent)).highlight(srb, 0);
                }
                else
                {
                    ((CPU)(this.Parent)).highlight(sra, 0);
                    if (srb == dr)
                        ((CPU)(this.Parent)).highlight(srb, 2);
                    else
                    {
                        if (srb != sra)
                            ((CPU)(this.Parent)).highlight(srb, 0);
                        ((CPU)(this.Parent)).highlight(dr, 1);
                    }
                }
            }
            else
            {
                if (sra == dr)
                    ((CPU)(this.Parent)).highlight(sra, 2);
                else
                {
                    ((CPU)(this.Parent)).highlight(sra, 0);
                    ((CPU)(this.Parent)).highlight(dr, 1);
                }
            }
            updatecond(data3);//updates nzp reg
        }
        private void not(string data)//takes in the 12 bit argument and nots sr
        {
            int sr = MainWindow.bitoint(data.Substring(3, 3));//int rep of sr reg for visual changes
            int dr = MainWindow.bitoint(data.Substring(0, 3));//int rep of dr reg for visual changes
            string data1 = ((CPU)this.Parent).retrieve(data.Substring(3, 3));//binary string representation of the data in sr reg for not operation
            string data2 = "";
            for (int x = 0; x < 16; x++)//goes through each bit and nots it
                if (data1[x] == '1')
                    data2 = data2 + "0";
                else
                    data2 = data2 + "1";
            ((CPU)this.Parent).store(data.Substring(0, 3), data2);//stores results to dr directory
            if (sr != dr)//logic structure for which reg and what color for highlighting
            {
                ((CPU)(this.Parent)).highlight(sr, 0);
                ((CPU)(this.Parent)).highlight(dr, 1);
            }
            else
                ((CPU)(this.Parent)).highlight(sr, 2);
            updatecond(data2);//updates nzp reg
        }
        private void ld(string data)//loads data into reg from mem
        {
            string address = ((Control)((CPU)this.Parent).Parent).retreiveIP().Substring(0, 7) + data.Substring(3);//creates string representing the binary memory address 
            ((CPU)this.Parent).store(data.Substring(0, 3), ((Control)((CPU)this.Parent).Parent).ram.retrieve(address));//stores to corresponding reg the data from mem location address
            int dr = MainWindow.bitoint(data.Substring(0, 3));//int rep for visual changes
            ((CPU)(this.Parent)).highlight(dr, 1);//highlights the dr register
            ((Control)((CPU)this.Parent).Parent).ram.scrollto(address);//moves the scroll window to view where in memery we are accessing
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(address, false);//adds "<<" infront of the memery line to show where we got it from
            updatecond(((CPU)this.Parent).retrieve(data.Substring(0, 3)));//updates nzp reg
        }
        private void ldi(string data)//loads data into reg from mem using an indirect address
        {
            string ad = ((Control)((CPU)this.Parent).Parent).retreiveIP().Substring(0, 7) + data.Substring(3);//address of mem that contaians the address
            string address = ((Control)((CPU)this.Parent).Parent).ram.retrieve(ad);//address for data to load to reg
            ((CPU)this.Parent).store(data.Substring(0, 3), ((Control)((CPU)this.Parent).Parent).ram.retrieve(address));//stores to corresponding reg the data from mem location address
            int dr = MainWindow.bitoint(data.Substring(0, 3));//int rep for visual change
            ((CPU)(this.Parent)).highlight(dr, 1);//highlights dr reg red
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(ad, false);// adds "<<" to the address that contained the address of mem used
            ((Control)((CPU)this.Parent).Parent).ram.scrollto(address);//scrolls to address of data grabed
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(address, false);//adds "<<" to the mem line ocontaining data used
            updatecond(((CPU)this.Parent).retrieve(data.Substring(0, 3)));//updates nzp reg
        }
        private void st(string data)//stores from reg to mem
        {
            string data1 = ((CPU)this.Parent).retrieve(data.Substring(0, 3));//string representing binary data in corresponding reg
            string address = ((Control)((CPU)this.Parent).Parent).retreiveIP().Substring(0, 7) + data.Substring(3);//address of where to store data
            ((Control)((CPU)this.Parent).Parent).ram.store(address, data1);////stores the data from reg to address
            int sr = MainWindow.bitoint(data.Substring(0, 3));//int rep of sr reg
            ((CPU)(this.Parent)).highlight(sr, 0);//highlights source reg blue
            ((Control)((CPU)this.Parent).Parent).ram.scrollto(address);//scrolls to location of where data was stored in mem
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(address, true);// adds ">>" infront of the mem line that data was stored in
        }
        private void sti(string data)//stores data from reg to an indirect address
        {
            string data1 = ((CPU)this.Parent).retrieve(data.Substring(0, 3));//string representing the binary data in sr reg
            string ad = ((Control)((CPU)this.Parent).Parent).retreiveIP().Substring(0, 7) + data.Substring(3);//address of the adress where we want to store data
            string address = ((Control)((CPU)this.Parent).Parent).ram.retrieve(ad);//address of where we want to store data
            ((Control)((CPU)this.Parent).Parent).ram.store(address, data1);//stores data to the indirect address
            int sr = MainWindow.bitoint(data.Substring(0, 3));//int rep of sr reg for visual changes
            ((CPU)(this.Parent)).highlight(sr, 0);//highlights the sr reg blue
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(ad, false);//adds "<<" to the address of address
            ((Control)((CPU)this.Parent).Parent).ram.scrollto(address);//scrolls to the address where we stored data
            ((Control)((CPU)this.Parent).Parent).ram.arrowline(address, true);//adds ">>" to mem line where we stored data
        }
        private bool br(string data)//checks conditions if they are met changes IP to point to new location
        {
            string temp = ((Control)((CPU)this.Parent).Parent).retrieveCC();//string rep of 3 bit binary nzp condition
            if ((temp[0] == '1' && data[0] == '1') || (temp[1] == '1' && data[1] == '1') || (temp[2] == '1' && data[2] == '1'))//checks to see if it meets any of the check criteria
            {//if criteria is met changes IP to point to the specified location
                temp = ((Control)((CPU)this.Parent).Parent).retreiveIP().Substring(0, 7);
                for (int x = 3; x < 12; x++)
                    temp = temp + data[x];
                ((Control)((CPU)this.Parent).Parent).storeIP(temp);
                return false;
            }
            return true;
        }
        private bool trap(string data)// currently only trap x25 is supported which is halt
        {
            string temp = MainWindow.bitohex("0000" + data);
            if (temp == "0025")//"0025"==x25==37
            {//if trap x25 it halts the fecth,decode&excute cycle
                ((Control)((CPU)this.Parent).Parent).halt();
                return true;
            }
            return false;
        }
        public void highlight(int inn)//takes int used for decision
        {
            //regb = true;//sets the bool representing if it is highlight to true
            switch (inn)
            {
                case 0://inn==0 means highlight blue which represents being a sr(data from it was used).
                    reg.Background = new SolidColorBrush(Colors.Blue);
                    return;
                case 1://inn==1 means highlight red which represents being a dr(data changed/inserted in/to it)
                    reg.Background = new SolidColorBrush(Colors.Red);
                    return;
                case 2://inn==2 means highlight purple which represents being both dr and sr (data used and then data changed/inserted in/to it)
                    reg.Background = new SolidColorBrush(Colors.Purple);
                    return;
            }
        }
        public void dehighlight()//unhighlights reg
        {
            reg.Background = null;
            //regb = false;
        }
    }
    partial class CPU : Grid
    {
        private Reg16bit[] regs = new Reg16bit[8];
        private ALU alu;
        public CPU()
        {
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Border bbb = new Border();
            bbb.VerticalAlignment = VerticalAlignment.Stretch;
            bbb.HorizontalAlignment = HorizontalAlignment.Stretch;
            bbb.BorderBrush = new SolidColorBrush(Colors.Black);
            bbb.BorderThickness = new Thickness(5);
            Children.Add(bbb);
            this.Focusable = false;
            TextBox label = new TextBox();
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.Background = new SolidColorBrush(Colors.Transparent);
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Text = "CPU";
            label.Width = 142;
            label.Height = 22;
            label.TextAlignment = TextAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.HorizontalAlignment = HorizontalAlignment.Stretch;
            label.Margin = new Thickness(0, 0, 0, 0);
            this.Children.Add(label);
            string temp;
            for (int x = 0; x < 8; x++)
            {
                temp = "R" + x;
                regs[x] = new Reg16bit(temp);
                regs[x].Margin = new Thickness(5, 24 + 21 * x, 0, 0);
                Children.Add(regs[x]);
            }
            alu = new ALU("Ins");
            alu.Margin = new Thickness(5, 0, 0, 10);
            Children.Add(alu);
        }
        public string retrieve(string address)
        {
            int ad = MainWindow.bitoint(address);
            return regs[ad].retrieve();
        }
        public void store(string address, string data)
        {
            int ad = MainWindow.bitoint(address);
            regs[ad].store(data);
        }
        public void pushIns(string ins)//sets instructions
        {
            alu.store(ins);
        }
        public bool excute()//decodes and exicutes instructions
        {
            return alu.swtch();
        }
        public void clear()//clears the 8 reg and the instruction box
        {
            dehighlight();
            alu.dehighlight();
            for (int x = 0; x < 8; x++)
                regs[x].store("0000000000000000");
            alu.store("0000000000000000");
        }
        public void highlight(int regn, int inn)//highlights regn
        {
            regs[regn].highlight(inn);
        }
        public void highlightalu(int inn)//highlight instruction box
        {
            alu.highlight(inn);
        }
        public void dehighlightalu()//dehighlight instruction box
        {
            alu.dehighlight();
        }
        public void dehighlight()//dehighlight cpu regs
        {
            for (int x = 0; x < 8; x++)
            {
                if (regs[x].ishighlight())
                    regs[x].dehighlight();
            }
        }
    }
    partial class Mempage : Grid
    {
        private Memline16[] lines = new Memline16[512];
        private TextBlock page;
        public Mempage()
        {
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Right;
            this.Height = 512 * 16 + 10;
            this.Width = 152;
            TextBox label = new TextBox();
            label.Width = 142;
            label.Height = 22;
            label.Text = "page ";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.TextAlignment = TextAlignment.Center;
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.Children.Add(label);
            Border border = new Border();
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            Children.Add(border);
            page = new TextBlock();
            page.HorizontalAlignment = HorizontalAlignment.Left;
            page.VerticalAlignment = VerticalAlignment.Top;
            page.Margin = new Thickness(5, 22, 0, 0);
            page.Text = "";
            for (int x = 0; x < 512; x++)
            {
                lines[x] = new Memline16();
                page.Text = page.Text + x + ":" + lines[x].retreive() + "\n";
            }
            Children.Add(page);
        }
        public Mempage(int pagen)
        {
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.Height = 512 * 16 + 10;
            this.Width = 152;
            TextBox label = new TextBox();
            label.Width = 142;
            label.Height = 22;
            label.Text = "page " + pagen;
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.TextAlignment = TextAlignment.Center;
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.Children.Add(label);
            Border border = new Border();
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            Children.Add(border);
            page = new TextBlock();
            page.HorizontalAlignment = HorizontalAlignment.Left;
            page.VerticalAlignment = VerticalAlignment.Top;
            page.Margin = new Thickness(5, 22, 0, 0);
            page.Text = "";
            for (int x = 0; x < 512; x++)
            {
                lines[x] = new Memline16();
                string temp = "";
                if (x < 10)
                    temp = "    ";
                else if (x < 100)
                    temp = "  ";
                page.Text = page.Text + temp + x + ":" + lines[x].retreive() + "\n";
            }
            Children.Add(page);
        }
        public string retreive(int address)//retreives 16 bit data from line address
        {
            if (address < 0 || address >= 512)
                return "";
            return lines[address].retreive();
        }
        public string fastdump()//used for fast full 65536 lines of mem
        {
            return page.Text;
        }
        public void store(int address, string data)//stores data to line address
        {
            if (address < 0 || address >= 512)
                return;
            lines[address].store(data);
            refresh(address);
        }
        private void refresh(int address)//updates the visuals when line address is changed
        {
            string ad1 = "" + address + ":";
            if (address < 10)
                ad1 = "    " + ad1;
            else if (address < 100)
                ad1 = "  " + ad1;
            string ad2 = "" + (address + 1) + ":";
            if (address + 1 < 10)
                ad2 = "    " + ad2;
            else if (address + 1 < 100)
                ad2 = "  " + ad2;
            page.Text = page.Text.Substring(0, page.Text.IndexOf(ad1)) + ad1 + lines[address].retreive() + "\n" + page.Text.Substring(page.Text.IndexOf(ad2));
        }
        public void clear()//clears each mem line on page
        {
            page.Text = "";
            for (int x = 0; x < 512; x++)
            {
                lines[x].store("0000000000000000");
                string temp = "";
                if (x < 10)
                    temp = "    ";
                else if (x < 100)
                    temp = "  ";
                page.Text = page.Text + temp + x + ":" + lines[x].retreive() + "\n";
            }
        }
        public void starline(int address)//stars the line adress used for visually representing where in memory instruction was fetched
        {
            string ad1 = "" + address + ":";
            if (address < 10)
                ad1 = "    " + ad1;
            else if (address < 100)
                ad1 = "  " + ad1;
            string ad2 = "" + (address + 1) + ":";
            if (address + 1 < 10)
                ad2 = "    " + ad2;
            else if (address + 1 < 100)
                ad2 = "  " + ad2;
            int tempi = page.Text.IndexOf(ad1);
            page.Text = page.Text.Substring(0, tempi) + ad1 + lines[address].retreive() + "*" + "\n" + page.Text.Substring(page.Text.IndexOf(ad2, tempi));
        }
        public void arrowline(int address, bool inn) //arrows line address. for visually representin when memery is accessed or inserted by an instruction
        {
            string ad1 = "" + address + ":";
            if (address < 10)
                ad1 = "    " + ad1;
            else if (address < 100)
                ad1 = "  " + ad1;
            int tempi = page.Text.IndexOf(ad1);
            string arr;
            if (inn)
                arr = ">>";
            else
                arr = "<<";
            page.Text = page.Text.Substring(0, tempi) + arr + page.Text.Substring(tempi);
        }
        public void destarline(int address)//used to remove the star at the line address
        {
            string ad1 = "" + address + ":";
            if (address < 10)
                ad1 = "    " + ad1;
            else if (address < 100)
                ad1 = "  " + ad1;
            string ad2 = "" + (address + 1) + ":";
            if (address + 1 < 10)
                ad2 = "    " + ad2;
            else if (address + 1 < 100)
                ad2 = "  " + ad2;
            int tempi = page.Text.IndexOf(ad1);
            page.Text = page.Text.Substring(0, tempi) + ad1 + lines[address].retreive() + "\n" + page.Text.Substring(page.Text.IndexOf(ad2, tempi));
        }
        public void dearrowline(int address)//used to remove the arrows from the line address
        {
            string ad1 = "" + address + ":";
            if (address < 10)
                ad1 = "    " + ad1;
            else if (address < 100)
                ad1 = "  " + ad1;
            int tempi = page.Text.IndexOf(ad1);
            page.Text = page.Text.Substring(0, tempi - 2) + page.Text.Substring(tempi);
        }
    }
    partial class RAM : Grid
    {
        private Mempage[] pages = new Mempage[128];
        private ScrollViewer viewer;
        private string arrowin;
        private string arrowout;
        public RAM()
        {
            arrowin = "";
            arrowout = "";
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            this.Height = 300;
            this.Width = 500;
            TextBox label = new TextBox();
            label.Text = "RAM";
            label.Height = 22;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.HorizontalAlignment = HorizontalAlignment.Stretch;
            label.TextAlignment = TextAlignment.Center;
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Children.Add(label);
            Border border = new Border();
            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            Children.Add(border);
            viewer = new ScrollViewer();
            viewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            viewer.Margin = new Thickness(5, 25, 5, 5);
            viewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            viewer.Content = new Grid();
            ((Grid)(viewer.Content)).Height = 512 * 16 + 10;
            ((Grid)(viewer.Content)).Width = 150 * 128;
            for (int x = 0; x < 128; x++)
            {
                pages[x] = new Mempage(x);
                pages[x].Margin = new Thickness(150 * x, 0, 0, 0);
                ((Grid)(viewer.Content)).Children.Add(pages[x]);
            }
            Children.Add(viewer);
            //    pages[x] = new Mempage(x);
            //    pages[x].Margin = new Thickness(x * 150, 0, 0, 0);
            //    grid.Children.Add(pages[x]);
            //}
            //pages[0] = new Mempage(0);
            //tabs.Items.Add(pages[0]);
            //for (int x = 0; x < 32; x++)
            //{
            //    tabs[x]=new TabItem();
            //    tabs[x].Header=""+x+"-"+x+3;
            //    ScrollViewer veiwer=new ScrollViewer();
            //    veiwer.HorizontalAlignment=HorizontalAlignment.Left;

            //    for(int y=0;y<4;y++)
            //    {

            //        pages[x*4+y] = new Mempage(x*4+y);
            //    tabc.Items.Add(tabs[x]);
            //}
            //Children.Add(tabs);
        }
        public void store(string bi16, string data)//stores data to the address of memory represented by the binary string bi16
        {
            int pag = MainWindow.bitoint(bi16.Substring(0, 7));//gets the int rep of the page for the array index
            pages[pag].store(MainWindow.bitoint(bi16.Substring(7, 9)), data);//stores data to the line repressented by te remaining end of the string (represented as an int from here on out) on page pag
        }
        public string retrieve(string bi16)//retreives the data from address represented by bi16
        {
            int pag = MainWindow.bitoint(bi16.Substring(0, 7));//gets the int rep of the page for the array index
            return pages[pag].retreive(MainWindow.bitoint(bi16.Substring(7, 9)));//retrieves the data from mem line represented as as an int on page pag
        }
        public string[] fastdump()//used for fast dumping all 65536 lines of mem
        {
            string[] ss = new string[65536];
            for (int x = 0; x < 65536; x++)
            {
                ss[x] = pages[x / 512].retreive(x % 512);
            }
            return ss;
        }
        public void clear()//clears each page in mem
        {
            for (int x = 0; x < 128; x++)
                pages[x].clear();
        }
        public void starline(string bi16)//stars line of mem represented by the string representing the 16 bits
        {
            int pag = MainWindow.bitoint(bi16.Substring(0, 7));//gets page number
            pages[pag].starline(MainWindow.bitoint(bi16.Substring(7, 9)));//stars the line on page pag
        }
        public void destarline(string bi16)//destars the line of mem specified by the string bi16
        {
            int pag = MainWindow.bitoint(bi16.Substring(0, 7));
            pages[pag].destarline(MainWindow.bitoint(bi16.Substring(7, 9)));
        }
        public void arrowline(string bi16, bool inn)//arrows line represented by bi16. inn used to decide if <<(false) or >>(true)
        {
            if (inn)//for storing the line which was arrowed so it can later be easily and quickly dearrowed
                arrowin = bi16;
            else
                arrowout = arrowout + bi16;
            int pag = MainWindow.bitoint(bi16.Substring(0, 7));
            pages[pag].arrowline(MainWindow.bitoint(bi16.Substring(7, 9)), inn);
        }
        public void dearrowline()//dearrows both types of arrows if they exist.
        {
            if (arrowin != "")
            {
                int pag = MainWindow.bitoint(arrowin.Substring(0, 7));
                pages[pag].dearrowline(MainWindow.bitoint(arrowin.Substring(7, 9)));
                arrowin = "";
            }
            if (arrowout != "")
            {
                int pag = MainWindow.bitoint(arrowout.Substring(0, 7));
                pages[pag].dearrowline(MainWindow.bitoint(arrowout.Substring(7, 9)));
                if (arrowout.Length > 16)
                {
                    pag = MainWindow.bitoint(arrowout.Substring(16, 7));
                    pages[pag].dearrowline(MainWindow.bitoint(arrowout.Substring(23, 9)));
                }
                arrowout = "";
            }
        }
        public void scrollto(string bi)//scrolls to specifide line in mem
        {
            int horr = MainWindow.bitoint(bi.Substring(0, 7));
            int vert = MainWindow.bitoint(bi.Substring(7));
            viewer.ScrollToVerticalOffset(vert * 16 - 10);
            viewer.ScrollToHorizontalOffset(150 * (horr - 1));
        }
    }
    partial class Controlbox : Grid
    {
        private Button loadb;
        private Button dumpb;
        private Button clearb;
        private Button autorunb;
        private Button manualrunb;
        private TextBox pathbox;
        public Controlbox(Reg16bit IP, Reg3bit CC)
        {
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Height = 215;
            this.Width = 155;
            TextBox label = new TextBox();
            label.Text = "Control";
            label.Height = 22;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.HorizontalAlignment = HorizontalAlignment.Stretch;
            label.TextAlignment = TextAlignment.Center;
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.Children.Add(label);
            Border bb = new Border();
            bb.HorizontalAlignment = HorizontalAlignment.Stretch;
            bb.VerticalAlignment = VerticalAlignment.Stretch;
            bb.BorderBrush = new SolidColorBrush(Colors.Black);
            bb.BorderThickness = new Thickness(5);
            this.Children.Add(bb);
            pathbox = new TextBox();
            pathbox.Text = "file path";
            pathbox.Height = 22;
            pathbox.VerticalAlignment = VerticalAlignment.Top;
            pathbox.Margin = new Thickness(8, 23, 8, 0);
            this.Children.Add(pathbox);
            loadb = new Button();
            loadb.Height = 22;
            loadb.Content = "Load";
            loadb.VerticalAlignment = VerticalAlignment.Top;
            loadb.Margin = new Thickness(8, 46, 8, 0);
            loadb.Click += loadclick;
            this.Children.Add(loadb);
            dumpb = new Button();
            dumpb.Height = 22;
            dumpb.Content = "Dump";
            dumpb.VerticalAlignment = VerticalAlignment.Top;
            dumpb.Margin = new Thickness(8, 69, 8, 0);
            dumpb.Click += dumpclick;
            this.Children.Add(dumpb);
            clearb = new Button();
            clearb.Height = 22;
            clearb.Content = "Clear";
            clearb.VerticalAlignment = VerticalAlignment.Top;
            clearb.Margin = new Thickness(8, 92, 8, 0);
            clearb.Click += clearclick;
            this.Children.Add(clearb);
            autorunb = new Button();
            autorunb.Height = 22;
            autorunb.Content = "Auto Run";
            autorunb.VerticalAlignment = VerticalAlignment.Top;
            autorunb.Margin = new Thickness(8, 115, 8, 0);
            autorunb.Click += autoclick;
            this.Children.Add(autorunb);
            manualrunb = new Button();
            manualrunb.Height = 22;
            manualrunb.Content = "Manual Run";
            manualrunb.VerticalAlignment = VerticalAlignment.Top;
            manualrunb.Margin = new Thickness(8, 138, 8, 0);
            manualrunb.Click += manualclick;
            this.Children.Add(manualrunb);
            IP.Margin = new Thickness(5, 161, 0, 0);
            this.Children.Add(IP);
            CC.Margin = new Thickness(48, 184, 0, 0);
            this.Children.Add(CC);
        }
        public void enablerunbs(bool b)
        {
            manualrunb.IsEnabled = b;
            autorunb.IsEnabled = b;
        }
        private void loadclick(object sender, RoutedEventArgs e)
        {
            ((Control)((Controlbox)((Button)(sender)).Parent).Parent).load(pathbox.Text);
        }
        private void dumpclick(object sender, RoutedEventArgs e)
        {
            ((Control)((Controlbox)((Button)(sender)).Parent).Parent).dump();
        }
        private void clearclick(object sender, RoutedEventArgs e)
        {
            ((Control)((Controlbox)((Button)(sender)).Parent).Parent).clear();
        }
        private void autoclick(object sender, RoutedEventArgs e)
        {
            ((Control)((Controlbox)((Button)(sender)).Parent).Parent).autorun();
        }
        private void manualclick(object sender, RoutedEventArgs e)
        {
            autorunb.IsEnabled = false;
            ((Control)((Controlbox)((Button)(sender)).Parent).Parent).manualrun();

        }
        public string pathfrompathbox()
        {
            return pathbox.Text;
        }
    }
    partial class Control : Grid
    {
        private int manualcntr;
        private Reg16bit IP;
        private Reg3bit CC;
        public RAM ram;
        public CPU cpu;
        private Controlbox box;
        private bool loop = true;
        private string last;
        public Control()
        {
            manualcntr = 0;
            last = "0000000000000000";
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            TextBox label = new TextBox();
            label.Text = "Machine";
            label.Height = 22;
            label.VerticalAlignment = VerticalAlignment.Top;
            label.HorizontalAlignment = HorizontalAlignment.Stretch;
            label.TextAlignment = TextAlignment.Center;
            label.Focusable = false;
            label.IsReadOnly = true;
            label.Margin = new Thickness(0, 0, 0, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            Children.Add(label);
            cpu = new CPU();
            cpu.Height = 230;
            cpu.Width = 155;
            cpu.Margin = new Thickness(15, 255, 0, 0);
            this.Children.Add(cpu);
            ram = new RAM();
            ram.Height = 450;
            ram.Width = 680;
            ram.Margin = new Thickness(200, 35, 0, 0);
            this.Children.Add(ram);
            IP = new Reg16bit("IP");
            CC = new Reg3bit();
            box = new Controlbox(IP, CC);
            box.Margin = new Thickness(15, 35, 0, 0);
            this.Children.Add(box);
            //incrementIP();
        }
        public void setlast(string nwlast)
        {
            last = nwlast;
        }
        public void halt()
        {
            loop = false;
            box.enablerunbs(false);
            ram.destarline(last);
        }
        public string retrieveCC()
        {
            CC.highlight(0);
            return CC.retrieve();
        }
        public void updateCC(string data)
        {
            CC.highlight(1);
            CC.store(data);
        }
        public void load(string path)
        {
            if (path.Substring(path.IndexOf('.') + 1) != "lc2b")
                return;
            string[] toload = File.ReadAllLines(path);
            IP.store("0000000000000000");
            manualcntr = 0;
            last = "0000000000000000";
            box.enablerunbs(true);
            loop = true;
            string address = "";
            for (int x = 0; x < toload.Length; x++)
            {
                address = MainWindow.itobi(x, 16);
                //toload[x] =MainWindow.translate(toload[x]);
                ram.store(address, toload[x]);
            }
        }
        public void clear()
        {
            ram.clear();
            cpu.clear();
            IP.store("0000000000000000");
            CC.store("010");
            CC.dehighlight();
            IP.dehighlight();
        }
        public void storeIP(string data)
        {
            IP.store(data);
        }
        public string retreiveIP()
        {
            return IP.retrieve();
        }
        public void incrementIP()
        {
            char carry = '1';
            char[] nw = IP.retrieve().ToCharArray();
            for (int x = 15; carry == '1' && x >= 0; x--)
            {
                if (nw[x] == '1')
                    nw[x] = '0';
                else
                {
                    nw[x] = '1';
                    carry = '0';
                }
            }
            IP.store(new string(nw));
        }
        public void fetch()
        {
            cpu.pushIns(ram.retrieve(IP.retrieve()));
        }
        public bool excute()
        {
            return cpu.excute();
        }
        public void autorun()
        {
            IP.store("0000000000000000");
            while (loop)
            {
                fetch();
                cpu.dehighlight();
                ram.dearrowline();
                if (excute())
                    incrementIP();
            }
            if (CC.ishighlight())
                CC.dehighlight();
        }
        public void dump(int upperlim)
        {
            string[] toDump = new string[upperlim];
            IP.store("0000000000000000");
            for (int x = 0; x < upperlim; x++)
            {
                toDump[x] = ram.retrieve(IP.retrieve());
                incrementIP();
            }
            MainWindow.dump(toDump, box.pathfrompathbox());
        }
        public void dump()
        {
            //    string[] toDump = new string[65536];
            //    IP.store("0000000000000000");
            //    for (int x = 0; x < 65536; x++)
            //    {
            //        toDump[x] = ram.retrieve(IP.retrieve());
            //        incrementIP();
            //    }
            string[] todump = ram.fastdump();
            MainWindow.dump(todump, box.pathfrompathbox());
        }
        public void manualrun()
        {
            if (loop)
            {
                switch (manualcntr % 2)
                {
                    case 0:
                        IP.dehighlight();
                        CC.dehighlight();
                        if (manualcntr > 0)
                        {
                            ram.destarline(last);
                            last = IP.retrieve();
                        }
                        ram.starline(IP.retrieve());
                        fetch();
                        ram.scrollto(IP.retrieve());
                        cpu.highlightalu(1);
                        cpu.dehighlight();
                        ram.dearrowline();
                        break;
                    case 1:
                        cpu.dehighlightalu();
                        if (excute())
                        {
                            incrementIP();
                        }
                        if (loop)
                            IP.highlight(1);
                        break;
                }
                manualcntr++;
            }
        }



    }


}
