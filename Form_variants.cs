
using System.Data.Common;
using chimneys.Data;
using chimneys_library;
using Microsoft.Data.Sqlite;

using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace chimneys
{
    public partial class Form_variants : Form
    {

        string[] name = new string[13] { "Исходные данные","Высота участка, м", "Внутренний диаметр в начале участка, м",
            "Внутренний диаметр в конце участка, м", "Толщина слоя внутренней футеровки, м",
            "Толщина воздушного зазора, м ", "Толщина слоя теплоизоляции, м",
            "Толщина слоя прижимной футеровки, м", "Толщина ствола, м",
            "Коэффициент теплопроводности слоя внутренней футеровки, Вт/(м*К)",
            "Коэффициент теплопроводности  слоя теплоизоляции, Вт/(м*К)",
            "Коэффициент теплопроводности прижимной футеровки, Вт/(м*К)", "Коэффициент теплопроводности ствола, Вт/(м*К)"};
        private readonly ApplicationContextDb _context;
        public Form_variants()
        {

            InitializeComponent();

            _context = new ApplicationContextDb();

            //var connection = new SqliteConnection("Data Source=chimneys.db");
            //SqliteCommand command = new SqliteCommand(@"select * from Name_var", connection);
            //connection.Open();
            //DbDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //    comboBox1.Items.Add(reader["Name_var"]);         //СтолбецТаблицы
            //connection.Close();
            for (int j = 0; j < 13; j++)
            {
                Label label = new Label();
                table_variants.Controls.Add(label, 0, j);
                label.Dock = DockStyle.Fill;
                label.BackColor = Color.LightGray;
                label.Text = name[j].ToString();
                label.TextAlign = ContentAlignment.MiddleLeft;
            }
            for (int i = 1; i < 14; i++)
            {
                Label label = new Label();
                table_variants.Controls.Add(label, i, 0);
                label.Dock = DockStyle.Fill;
                label.BackColor = Color.LightGray;
                label.TextAlign = ContentAlignment.MiddleLeft;

                label.Text = i.ToString();
            }
            for (int i = 1; i < 14; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    TextBox textBox = new TextBox();
                    table_variants.Controls.Add(textBox, i, j);

                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = Color.LightGray;
                    textBox.Dock = DockStyle.Fill;
                }
            }
            
            
        }

        private void button_calc_Click(object sender, EventArgs e)
        {
            Control c;
            for (int j = 1; j < 14; j++)
            {

                c = table_variants.GetControlFromPosition(j, 1);
                if (c.Text != "")
                {
                    Program.cell++;
                }
            }
            var input = new ChimneysInput
            {
                Name_var = textBox_name_var.Text,
                L = Convert.ToDouble(textBox_L.Text),
                T = Convert.ToDouble(textBox_T.Text),
                C_co2 = Convert.ToDouble(textBox_Cco2.Text),
                C_h2o = Convert.ToDouble(textBox_Ch2o.Text),
                T_okr = Convert.ToDouble(textBox_Tokr.Text),
                V = Convert.ToDouble(textBox_V.Text),
            };
            c = table_variants.GetControlFromPosition(11, 1);
            if (c.Text == "")
                input.h_2 = 0;
            else
            {
                input.h_2 = Convert.ToDouble(c.Text);
                Program.input_h2 = Convert.ToDouble(c.Text);
            }
                
            for (int i = 1; i <= Program.cell; i++)
            {
                input.num_wall = i;
                input.h = Convert.ToDouble(table_variants.GetControlFromPosition(i, 1).Text);
                input.d_vn_s = Convert.ToDouble(table_variants.GetControlFromPosition(i, 2).Text);
                input.d_vn_f = Convert.ToDouble(table_variants.GetControlFromPosition(i, 3).Text);
                input.l1 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 4).Text);
                input.l2 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 5).Text);
                input.l3 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 6).Text);
                input.l4 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 7).Text);
                input.l5 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 8).Text);
                input.y1 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 9).Text);
                input.y2 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 10).Text);
                input.y3 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 11).Text);
                input.y4 = Convert.ToDouble(table_variants.GetControlFromPosition(i, 12).Text);
                var existVariant = _context.Variants.FirstOrDefault(x => x.Name_var == input.Name_var && x.num_wall == input.num_wall);
                if (existVariant != null)
                {
                    // Обновление варианта
                    existVariant.num_wall = input.num_wall;
                    existVariant.h = input.h;
                    existVariant.d_vn_s = input.d_vn_s;
                    existVariant.d_vn_f = input.d_vn_f;
                    existVariant.l1 = input.l1;
                    existVariant.l2 = input.l2;
                    existVariant.l3 = input.l3;
                    existVariant.l4 = input.l4;
                    existVariant.l5 = input.l5;
                    existVariant.y1 = input.y1;
                    existVariant.y2 = input.y2;
                    existVariant.y3 = input.y3;
                    existVariant.y4 = input.y4;
                    existVariant.L = input.L;
                    existVariant.T = input.T;
                    existVariant.C_co2 = input.C_co2;
                    existVariant.C_h2o = input.C_h2o;
                    existVariant.T_okr = input.T_okr;
                    existVariant.V = input.V;

                    _context.Variants.Update(existVariant);
                    _context.SaveChanges();
                }
                else
                {
                    //добавление варианта
                    var variant = new Variants
                    {
                        Name_var = input.Name_var,
                        num_wall = input.num_wall,
                        h = input.h,
                        d_vn_s = input.d_vn_s,
                        d_vn_f = input.d_vn_f,
                        l1 = input.l1,
                        l2 = input.l2,
                        l3 = input.l3,
                        l4 = input.l4,
                        l5 = input.l5,
                        y1 = input.y1,
                        y2 = input.y2,
                        y3 = input.y3,
                        y4 = input.y4,
                        L = input.L,
                        T = input.T,
                        C_co2 = input.C_co2,
                        C_h2o = input.C_h2o,
                        T_okr = input.T_okr,
                        V = input.V,
                    };
                    _context.Variants.Add(variant);
                    _context.SaveChanges();
                }
            }
            
            Form new_form = new Form_Result_temp();
            new_form.Show(); // отображаем Form_Result_temp

        }
    }
}
