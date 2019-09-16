using ControleDApi.DAL;
using ControleDApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace importarDados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            //string text = System.IO.File.ReadAllText(@"C:\Users\thiago.nsilva.ABNOTE\Desktop\alimentos.json");

            var caminho = "C:/Users/thiago.nsilva.ABNOTE/Desktop/alimentos.json";
            string text = System.IO.File.ReadAllText(caminho);

            System.IO.StreamReader file =
    new System.IO.StreamReader(caminho);

            var line = "";

            //while ((line = file.ReadLine()) != null)
            //{
            //    System.Console.WriteLine(line);
            //    // counter++;
            //}


            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            List<Alimento> alimentos = jsonSerializer.Deserialize<List<Alimento>>(text);

            var context = new Context();

            context.Alimento.AddRange(alimentos);

            context.SaveChanges();



            //var contador = 0; 
            //foreach (string line in lines)
            //{
            //    MessageBox.Show(line);
            //    if (contador > 3)
            //        return;
            //    contador += 1;
            //}


            //

        }
    }
}
