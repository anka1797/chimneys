
using System.Data.Common;
using chimneys.Data;
using chimneys_library;
using Microsoft.Data.Sqlite;

using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Linq;

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

        public static TableLayoutPanel GetTable(TableLayoutPanel t, string name, int j)
        {
            Label label = new Label();
            t.Controls.Add(label, 0, j);
            label.Text = name;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            return (t);
        }
        public static TableLayoutPanel GetTable_wall(TableLayoutPanel t, int i)
        {
            Label label1 = new Label();
            t.Controls.Add(label1, i, 0);
            label1.Text = i.ToString();
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            return (t);
        }
        public static TableLayoutPanel GetVariants(TableLayoutPanel t, Variants v, int j)
        {
            t.GetControlFromPosition(j, 1).Text = v.h.ToString();
            t.GetControlFromPosition(j, 2).Text = v.d_vn_s.ToString();
            t.GetControlFromPosition(j, 3).Text = v.d_vn_f.ToString();
            t.GetControlFromPosition(j, 4).Text = v.l1.ToString();
            t.GetControlFromPosition(j, 5).Text = v.l2.ToString();
            t.GetControlFromPosition(j, 6).Text = v.l3.ToString();
            t.GetControlFromPosition(j, 7).Text = v.l4.ToString();
            t.GetControlFromPosition(j, 8).Text = v.l5.ToString();
            t.GetControlFromPosition(j, 9).Text = v.y1.ToString();
            t.GetControlFromPosition(j, 10).Text = v.y2.ToString();
            t.GetControlFromPosition(j, 11).Text = v.y3.ToString();
            t.GetControlFromPosition(j, 12).Text = v.y4.ToString();
            return (t);
        }
        private readonly ApplicationContextDb _context;
        public Form_variants()
        {

            InitializeComponent();

            //настройка выбора варианта
            _context = new ApplicationContextDb();
            comboBox1.Items.Add("Выберите вариант");
            var names_variants = _context.Variants.Select(x => x.Name_var).Distinct().ToList();
            for (int i = 0; i < names_variants.Count; i++)
            {
                comboBox1.Items.Add(names_variants[i]);
            }

            //заполнение заголовков таблицы
            for (int j = 0; j < 13; j++) //строки
            {
                GetTable(table_variants, name[j], j);
            }
            for (int i = 1; i < 16; i++) //столбцы
            {
                GetTable_wall(table_variants, i);
            }

            //создание текстбоксов таблицы
            for (int j = 1; j < 13; j++)
            {
                for (int i = 1; i < 16; i++)
                {
                    TextBox textBox = new TextBox();
                    table_variants.Controls.Add(textBox, i, j);
                    textBox.Text = "0";
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = Color.FromArgb(240, 240, 240);
                    textBox.Dock = DockStyle.Fill;
                }
            }

        }

        private void button_calc_Click(object sender, EventArgs e)
        {
            Control c;
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
            if (c.Text == "0")
                input.h_2 = 0;
            else
            {
                input.h_2 = Convert.ToDouble(c.Text);
                Program.input_h2 = Convert.ToDouble(c.Text);
            }

            for (int i = 1; i <= 15; i++)
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
            this.Hide();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.name_variant = comboBox1.Text;
            List<Variants> variant = new List<Variants>(15);
            //variant.Add(_context.Variants.FirstOrDefault(x => x.Name_var == Program.name_variant));
            for (int j = 1; j < 14; j++)
            {

                variant.Add(_context.Variants.FirstOrDefault(x => x.Name_var == Program.name_variant && x.num_wall == j));
                GetVariants(table_variants, variant[j - 1], j);
                textBox_name_var.Text = Program.name_variant;
                textBox_L.Text = Convert.ToString(variant[j - 1].L);
                textBox_T.Text = Convert.ToString(variant[j - 1].T);
                textBox_Cco2.Text = Convert.ToString(variant[j - 1].C_co2);
                textBox_Ch2o.Text = Convert.ToString(variant[j - 1].C_h2o);
                textBox_Tokr.Text = Convert.ToString(variant[j - 1].T_okr);
                textBox_V.Text = Convert.ToString(variant[j - 1].V);
            }
        }
    }
}
