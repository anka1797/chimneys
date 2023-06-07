using chimneys.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using chimneys_library;
using static chimneys_library.Math_model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using ScottPlot;

namespace chimneys
{
    public partial class Form_Result_temp : Form
    {
        string[] name = new string[12] { "Высота участка, м", "Внутренний диаметр в начале участка, м",
            "Внутренний диаметр в конце участка, м", "Толщина слоя внутренней футеровки, м",
            "Толщина воздушного зазора, м ", "Толщина слоя теплоизоляции, м",
            "Толщина слоя прижимной футеровки, м", "Толщина ствола, м",
            "Коэффициент теплопроводности слоя внутренней футеровки, Вт/(м*К)",
            "Коэффициент теплопроводности  слоя теплоизоляции, Вт/(м*К)",
            "Коэффициент теплопроводности прижимной футеровки, Вт/(м*К)", "Коэффициент теплопроводности ствола, Вт/(м*К)"};
        string[] name2 = new string[12] { "Средняя скорость газов на участке при н.у., м/с",
            "Коэффициент конвективной теплоотдачи от газов к внутренней стенке трубы, Вт/(м²⋅К)",
            "Лучистый коэффициент теплоотдачи от газов к внутренней стенке трубы, Вт/(м²⋅К)",
            "Суммарный коэффициент теплоотдачи от газов к внутренней стенке трубы, Вт/(м²⋅К)",
            "Коэффициент теплоотдачи от наружной поверхности трубы к воздуху, Вт/(м²⋅К)",
            "Линейная плотность теплового потока в трубе, Вт/м",
            "Средняя температура газов по участку, °С",
            "Температура на внутренней поверхности футеровки, °С",
            "Температура на внешней поверхности футеровки, °С",
            "Температура на внутренней поверхности прижимной футеровки, °С",
            "Температура на внутренней поверхности основной кладки, °С",
            "Температура наружной поверхности основной кладки, °С" };
        public static TableLayoutPanel GetTable(TableLayoutPanel t, string name, int j)
        {
            Label label = new Label();
            t.Controls.Add(label, 0, j);
            label.Text = name;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            Label label1 = new Label();
            t.Controls.Add(label1, 1, j);
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            return (t);
        }
        public static TableLayoutPanel GetVariants(TableLayoutPanel t, Variants v)
        {
            t.GetControlFromPosition(1, 0).Text = v.h.ToString();
            t.GetControlFromPosition(1, 1).Text = v.d_vn_s.ToString();
            t.GetControlFromPosition(1, 2).Text = v.d_vn_f.ToString();
            t.GetControlFromPosition(1, 3).Text = v.l1.ToString();
            t.GetControlFromPosition(1, 4).Text = v.l2.ToString();
            t.GetControlFromPosition(1, 5).Text = v.l3.ToString();
            t.GetControlFromPosition(1, 6).Text = v.l4.ToString();
            t.GetControlFromPosition(1, 7).Text = v.l5.ToString();
            t.GetControlFromPosition(1, 8).Text = v.y1.ToString();
            t.GetControlFromPosition(1, 9).Text = v.y2.ToString();
            t.GetControlFromPosition(1, 10).Text = v.y3.ToString();
            t.GetControlFromPosition(1, 11).Text = v.y4.ToString();
            return (t);
        }
        public static TableLayoutPanel GetResult(TableLayoutPanel t, ChimneysOut r)
        {
            t.GetControlFromPosition(1, 0).Text = r.V_average_1.ToString("#.##");
            t.GetControlFromPosition(1, 1).Text = r.a_konv.ToString("#.##");
            t.GetControlFromPosition(1, 2).Text = r.a_lk.ToString("#.##");
            t.GetControlFromPosition(1, 3).Text = r.a_sum.ToString("#.##");
            t.GetControlFromPosition(1, 4).Text = r.a_nar.ToString("#.##");
            t.GetControlFromPosition(1, 5).Text = r.Line_p.ToString("#.##");
            t.GetControlFromPosition(1, 6).Text = r.T_average.ToString("#.#");
            t.GetControlFromPosition(1, 7).Text = r.T_in_fut.ToString("#.#");
            t.GetControlFromPosition(1, 8).Text = r.T_outside_fut.ToString("#.#");
            t.GetControlFromPosition(1, 9).Text = r.T_in_fut_prig.ToString("#.#");
            t.GetControlFromPosition(1, 10).Text = r.T_in_cladk.ToString("#.#");
            t.GetControlFromPosition(1, 11).Text = r.T_out_cladk.ToString("#.#");
            return (t);
        }
        public static FormsPlot GetChart(FormsPlot chart, ChimneysOut r, Variants v, int i)
        {
            double[] x = new double[6];
            x[0] = 0;
            x[1] = Math.Round(x[0] + v.l5, 1);
            x[2] = Math.Round(x[1] + v.l4, 1);
            x[3] = Math.Round(x[2] + v.l3 + v.l2, 1);
            x[4] = Math.Round(x[3] + v.l1, 1);
            x[5] = Math.Round(x[4] + ((v.d_vn_s + v.d_vn_f) / 2) / 2, 1);
            double[] y = {
                    Math.Round(r.T_out_cladk, 1),
                    Math.Round(r.T_in_cladk, 1),
                    Math.Round(r.T_in_fut_prig, 1),
                    Math.Round(r.T_outside_fut, 1),
                    Math.Round(r.T_in_fut, 1),
                    Math.Round(r.T_average, 1),
                };

            chart.Plot.AddScatter(x, y);
            chart.Plot.XAxis.Label("Толщина, м");
            chart.Plot.YAxis.Label("Температура, °С");
            chart.Plot.Title("Распределение температур по толщине стенки на участке " + i);
            for (int j = 0; j < 6; j++)
            {
                var tt1 = chart.Plot.AddTooltip(label: "температура:" + y[j] + "\nТолщина:" + x[j], x: x[j], y: y[j]);
                tt1.Font.Size = 10;
                tt1.BorderWidth = 0;
            }
            chart.Refresh();
            return (chart);
        }
        public static FormsPlot ChartDecor(FormsPlot chart, double[] x, double[] y, int wall)
        {
            chart.Plot.YAxis.Label("Высота, м");
            chart.Plot.XAxis.Label("Температура, °С");
            for (int j = 0; j < wall; j++)
            {
                var tt1 = chart.Plot.AddTooltip(label: "температура:" + x[j] + "\nвысота" + y[j], x: x[j], y: y[j]);
                tt1.Font.Size = 12;
                tt1.BorderWidth = 0;
            }

            return (chart);
        }


        private readonly ApplicationContextDb _context;
        public Form_Result_temp()
        {
            InitializeComponent();
            _context = new ApplicationContextDb();
            //заполнение таблиц ячейками и заголовками
            for (int j = 0; j < 12; j++)
            {
                GetTable(table_wall1, name[j], j);
                GetTable(table_wall2, name[j], j);
                GetTable(table_wall3, name[j], j);
                GetTable(table_wall4, name[j], j);
                GetTable(table_wall5, name[j], j);
                GetTable(table_wall6, name[j], j);
                GetTable(table_wall7, name[j], j);
                GetTable(table_wall8, name[j], j);
                GetTable(table_wall9, name[j], j);
                GetTable(table_wall10, name[j], j);
                GetTable(table_wall11, name[j], j);
                GetTable(table_wall12, name[j], j);
                GetTable(table_wall13, name[j], j);
                GetTable(table_wall14, name[j], j);
                GetTable(table_wall15, name[j], j);

                GetTable(table_wall1_1, name2[j], j);
                GetTable(table_wall2_2, name2[j], j);
                GetTable(table_wall3_3, name2[j], j);
                GetTable(table_wall4_4, name2[j], j);
                GetTable(table_wall5_5, name2[j], j);
                GetTable(table_wall6_6, name2[j], j);
                GetTable(table_wall7_7, name2[j], j);
                GetTable(table_wall8_8, name2[j], j);
                GetTable(table_wall9_9, name2[j], j);
                GetTable(table_wall10_10, name2[j], j);
                GetTable(table_wall11_11, name2[j], j);
                GetTable(table_wall12_12, name2[j], j);
                GetTable(table_wall13_13, name2[j], j);
                GetTable(table_wall14_14, name2[j], j);
                GetTable(table_wall15_15, name2[j], j);

            }

            //выгрузка варианта из бд и нахождение значений
            List<Variants> variant = new List<Variants>(15);
            List<ChimneysOut> result = new List<ChimneysOut>(15);
            double[] temp = new double[16];
            for (int i = 0; i < 15; i++)
            {
                variant.Add(_context.Variants.FirstOrDefault(x => x.Name_var == Program.name_variant && x.num_wall == i + 1));

                if (variant[i] != null)
                {
                    temp[0] = variant[0].T;
                    var input = new ChimneysInput
                    {
                        h = variant[i].h,
                        d_vn_s = variant[i].d_vn_s,
                        d_vn_f = variant[i].d_vn_f,
                        l1 = variant[i].l1,
                        l2 = variant[i].l2,
                        l3 = variant[i].l3,
                        l4 = variant[i].l4,
                        l5 = variant[i].l5,
                        y1 = variant[i].y1,
                        y2 = variant[i].y2,
                        y3 = variant[i].y3,
                        y4 = variant[i].y4,
                        L = variant[i].L,
                        T = temp[i],
                        C_co2 = variant[i].C_co2,
                        C_h2o = variant[i].C_h2o,
                        T_okr = variant[i].T_okr,
                        V = variant[i].V,
                        h_2 = Program.input_h2,
                    };

                    var lib = new Math_model(input);
                    result.Add(lib.calc());
                    temp[i + 1] = result[i].T;
                };
            }


            int wall = 0;
            for (int i = 0; i < 15; i++)
            {
                if (variant[i] != null)
                {
                    wall = wall + 1;
                }
            }
            GetVariants(table_wall1, variant[0]);
            GetVariants(table_wall2, variant[1]);
            GetVariants(table_wall3, variant[2]);
            GetVariants(table_wall4, variant[3]);
            GetVariants(table_wall5, variant[4]);
            GetVariants(table_wall6, variant[5]);
            GetVariants(table_wall7, variant[6]);
            GetVariants(table_wall8, variant[7]);
            GetVariants(table_wall9, variant[8]);
            GetVariants(table_wall10, variant[9]);
            GetVariants(table_wall11, variant[10]);
            GetVariants(table_wall12, variant[11]);
            GetVariants(table_wall13, variant[12]);
            GetVariants(table_wall14, variant[13]);
            GetVariants(table_wall15, variant[14]);

            GetResult(table_wall1_1, result[0]);
            GetResult(table_wall2_2, result[1]);
            GetResult(table_wall3_3, result[2]);
            GetResult(table_wall4_4, result[3]);
            GetResult(table_wall5_5, result[4]);
            GetResult(table_wall6_6, result[5]);
            GetResult(table_wall7_7, result[6]);
            GetResult(table_wall8_8, result[7]);
            GetResult(table_wall9_9, result[8]);
            GetResult(table_wall10_10, result[9]);
            GetResult(table_wall11_11, result[10]);
            GetResult(table_wall12_12, result[11]);
            GetResult(table_wall13_13, result[12]);
            GetResult(table_wall14_14, result[13]);
            GetResult(table_wall15_15, result[14]);

            GetChart(chart1, result[0], variant[0], 1);
            GetChart(chart2, result[1], variant[1], 2);
            GetChart(chart3, result[2], variant[2], 3);
            GetChart(chart4, result[3], variant[3], 4);
            GetChart(chart5, result[4], variant[4], 5);
            GetChart(chart6, result[5], variant[5], 6);
            GetChart(chart7, result[6], variant[6], 7);
            GetChart(chart8, result[7], variant[7], 8);
            GetChart(chart9, result[8], variant[8], 9);
            GetChart(chart10, result[9], variant[9], 10);
            GetChart(chart11, result[10], variant[10], 11);
            GetChart(chart12, result[11], variant[11], 12);
            GetChart(chart13, result[12], variant[12], 13);
            //GetChart(chart14, result[13], variant[13]);
            //GetChart(chart15, result[14], variant[14]);
            double[] y = new double[13];
            y[0] = variant[0].h;
            for (int i = 1; i < 13; i++)
            {
                y[i] = y[i - 1] + variant[i].h;
            }

            double[] x = new double[13];
            for (int i = 0; i < 13; i++)
            {
                x[i] = Math.Round(result[i].T_average, 1);
            }

            double[] q = new double[13];
            for (int i = 0; i < 13; i++)
            {
                q[i] = Math.Round(result[i].T_in_fut, 1);
            }

            double[] w = new double[13];
            for (int i = 0; i < 13; i++)
            {
                w[i] = Math.Round(result[i].T_in_cladk, 1);
            }

            double[] e = new double[13];
            for (int i = 0; i < 13; i++)
            {
                e[i] = Math.Round(result[i].T_out_cladk, 1);
            }

            chartT1.Plot.AddScatter(x, y);
            chartT1.Plot.Title("Температура газов в трубе");
            ChartDecor(chartT1, x, y, 13);
            chartT1.Refresh();

            chartT2.Plot.AddScatter(q, y);
            chartT2.Plot.Title("Температура на внутренней поверхности футеровки");
            ChartDecor(chartT2, q, y, 13);
            chartT2.Refresh();

            chartT3.Plot.AddScatter(w, y);
            chartT3.Plot.Title("Температура внутренней поверхности ж/б ствола");
            ChartDecor(chartT3, w, y, 13);
            chartT3.Refresh();

            chartT4.Plot.AddScatter(e, y);
            chartT4.Plot.Title("Температура наружной поверхности ствола ж/б трубы");
            ChartDecor(chartT4, e, y, 13);
            chartT4.Refresh();

        }
    }
}
