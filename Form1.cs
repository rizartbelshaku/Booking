using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RezervimHoteli
{
    public partial class Form1 : Form
    {
        private string emriRezervimit;
        private DateTime dataRezervimit;
        private DateTime dataeLeshitmit;
        private List<string> dhomatHotelit = new List<string>();
        private List<string> persona = new List<string>();
        private List<Rezervimi> listaRezervimeve = new List<Rezervimi>();
        private List<DhomatRezervuara> dhomatRezervuara = new List<DhomatRezervuara>();
        

        public Form1()
        {
            InitializeComponent();
            InicializoDhomat();
            InicializoPersona();
           

        }
       
        

        private struct DhomatRezervuara
        {
            public DateTime DataRezervimit;
            public DateTime DataLargimit;
            public List<string> Dhomat;

        }

        private void InicializoDhomat()
        {
            checkedListBox1.Items.Add("Dhoma 101 / 50$");
            checkedListBox1.Items.Add("Dhoma 102 / 70$");
            checkedListBox1.Items.Add("Dhoma 103/80$");
            checkedListBox1.Items.Add("Dhoma 104/ 90$");
            checkedListBox1.Items.Add("Dhoma 105 /120$");
            checkedListBox1.Items.Add("Dhoma 106 / 1404");
        }
        private void InicializoPersona()
        {
            checkedListBox2.Items.Add(" 1 Person");
            checkedListBox2.Items.Add(" 2 Person");
            checkedListBox2.Items.Add(" 3 Person");
            checkedListBox2.Items.Add(" 4 Person");
            checkedListBox2.Items.Add(" 5 Person");
        }

        private void label1_Click(object sender, EventArgs e)
        {

            label1.BackColor = Color.Transparent;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            emriRezervimit = textBox1.Text;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataRezervimit = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dataeLeshitmit = dateTimePicker2.Value;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            dhomatHotelit.Clear();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                dhomatHotelit.Add(item.ToString());
            }
        }

        private void checkedListBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            persona.Clear();
            foreach (var item in checkedListBox2.CheckedItems)
            {
                persona.Add(item.ToString());
            }
        }
      

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string teksti = textBox2.Text;
        }


        public class Rezervimi
        {
            public string Emri { get; set; }
            public DateTime DataRezervimit { get; set; }
            public DateTime DataLargimit { get; set; }
            public List<string> Dhomat { get; set; }
            public List<string> Persona { get; set; }

            public Rezervimi(string emri, DateTime dataRezervimit, DateTime dataLargimit, List<string> dhomat, List<string> persona)
            {
                Emri = emri;
                DataRezervimit = dataRezervimit;
                DataLargimit = dataLargimit;
                Dhomat = dhomat;
                Persona = persona;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0 || checkedListBox2.CheckedItems.Count == 0 || string.IsNullOrEmpty(emriRezervimit))
            {
                MessageBox.Show("Ju lutemi zgjidhni një dhomë, një numër personash dhe plotësoni emrin para se të kryeni rezervimin.");
                return;
            }

            else
            {
                dhomatHotelit.Clear();
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    dhomatHotelit.Add(item.ToString());
                }

                persona.Clear();
                foreach (var item in checkedListBox2.CheckedItems)
                {
                    persona.Add(item.ToString());
                }
            }
         
            if (DhomatErezervuara(dhomatHotelit))
            {
                MessageBox.Show("Dhoma e zgjedhur është e rezervuar nga personi i parë. Ju lutemi zgjidhni një dhomë të lirë.");
                return;
            }
            else
            {
               
                Rezervimi rezervimi = new Rezervimi(emriRezervimit, dataRezervimit, dataeLeshitmit, dhomatHotelit, persona);
                listaRezervimeve.Add(rezervimi);
                DhomatRezervuara dhomatRezervuaraPersonit = new DhomatRezervuara
                {
                    DataRezervimit = dataRezervimit,
                    DataLargimit = dataeLeshitmit,
                    Dhomat = new List<string>(dhomatHotelit)
                };
                dhomatRezervuara.Add(dhomatRezervuaraPersonit);
            }

            MessageBox.Show($"Rezervimi për {emriRezervimit} u krye me sukses!\n\nDhoma e zgjedhur: {string.Join(", ", dhomatHotelit)}\nNumri i personave: {string.Join(", ", persona)}\nData e rezervimit: {dataRezervimit}\nData e largimit: {dataeLeshitmit}");
        }

        private bool DhomatErezervuara(List<string> dhomatHotelit)
        {
            foreach (var dhomatRezervuaraPersonit in dhomatRezervuara)
            {
                if (dhomatRezervuaraPersonit.Dhomat.Intersect(dhomatHotelit).Any())
                {
                    return true; 
                }
            }
            return false;
        }

      
    }
}

    

    
    








