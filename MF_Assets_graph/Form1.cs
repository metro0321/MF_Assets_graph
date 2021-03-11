using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // フォームをロードするときの処理
            DateTime today = DateTime.Now;

            UpdateGraph(DateTime.Now);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //データを取り込む
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            List<string> lists_input_line = new List<string>();
            StreamReader sr = new StreamReader(files[0]);
            {
                bool over_list_head = false;
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string read_line = sr.ReadLine();
                    if (read_line != null)
                    {
                        if (over_list_head)
                        {
                            bool duplicate = false;
                            // 読み込みたいCSVファイルのパスを指定して開く
                            StreamReader data_reader = new StreamReader(@"data.csv");
                            {
                                // 末尾まで繰り返す
                                while (!data_reader.EndOfStream)
                                {
                                    // CSVファイルの一行を読み込む
                                    string dup_check_string = data_reader.ReadLine();
                                    if (dup_check_string != null)
                                    {
                                        if (dup_check_string.CompareTo(read_line.Replace("\"", "")) == 0)
                                        {
                                            duplicate = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            data_reader.Close();
                            data_reader.Dispose();

                            if (duplicate == false)
                            {
                                string line = read_line.Replace("\"", "");
                                lists_input_line.Add(line);
                            }
                        }
                        over_list_head = true;
                    }
                }
            }
            sr.Close();


            lists_input_line.Sort((a1, b1) => b1.CompareTo(a1));

            for (var count = 0; count < lists_input_line.Count(); count++)
            {
                File.AppendAllText(@"data.csv", lists_input_line[(count)] + Environment.NewLine);
            }

            UpdateGraph(DateTime.Now);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // マウスポインター形状変更
            //
            // DragDropEffects
            //  Copy  :データがドロップ先にコピーされようとしている状態
            //  Move  :データがドロップ先に移動されようとしている状態
            //  Scroll:データによってドロップ先でスクロールが開始されようとしている状態、あるいは現在スクロール中である状態
            //  All   :上の3つを組み合わせたもの
            //  Link  :データのリンクがドロップ先に作成されようとしている状態
            //  None  :いかなるデータもドロップ先が受け付けようとしない状態

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void UpdateGraph(DateTime disp_date)
        {
            // フォームをロードするときの処理
            chart1.Series.Clear();  // ← 最初からSeriesが1つあるのでクリアします
            chart1.ChartAreas.Clear();


            // ChartにChartAreaを追加します
            string chart_area1 = "資産推移";
            chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea(chart_area1));
            chart1.ChartAreas[0].AxisX.Title = "日付";
            chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 90;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;            // ChartにSeriesを追加します
            string legend_total = "合計";
            string legend_cash = "預金・現金・仮想通貨";
            string legend_stocks = "株式(現物)";
            string legend_invest = "投資信託";
            string legend_points = "ポイント";
            chart1.Series.Add(legend_total);
            chart1.Series.Add(legend_cash);
            chart1.Series.Add(legend_stocks);
            chart1.Series.Add(legend_invest);
            chart1.Series.Add(legend_points);
            // グラフの種別を指定
            chart1.Series[legend_total].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // 折れ線グラフを指定してみます
            chart1.Series[legend_total].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square; // 折れ線グラフを指定してみます
            chart1.Series[legend_total].BorderWidth = 3;
            chart1.Series[legend_cash].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // 折れ線グラフを指定してみます
            chart1.Series[legend_cash].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square; // 折れ線グラフを指定してみます
            chart1.Series[legend_cash].BorderWidth = 3;
            chart1.Series[legend_stocks].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // 折れ線グラフを指定してみます
            chart1.Series[legend_stocks].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square; // 折れ線グラフを指定してみます
            chart1.Series[legend_stocks].BorderWidth = 3;
            chart1.Series[legend_invest].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // 折れ線グラフを指定してみます
            chart1.Series[legend_invest].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square; // 折れ線グラフを指定してみます
            chart1.Series[legend_invest].BorderWidth = 3;
            chart1.Series[legend_points].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // 折れ線グラフを指定してみます
            chart1.Series[legend_points].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square; // 折れ線グラフを指定してみます
            chart1.Series[legend_points].BorderWidth = 3;

            List<string> lists_date = new List<string>();
            List<int> lists_total = new List<int>();
            List<int> lists_cash = new List<int>();
            List<int> lists_stocks = new List<int>();
            List<int> lists_invest = new List<int>();
            List<int> lists_points = new List<int>();

            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new StreamReader(@"data.csv");
            {
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        string[] values = line.Split(',');
                        if (values.Length == 6)
                        {
                            int ret;
                            if (Int32.TryParse(values[1], out ret))
                            {
                                lists_total.Add(ret);
                            }
                            if (Int32.TryParse(values[2], out ret))
                            {
                                lists_cash.Add(ret);
                            }
                            if (Int32.TryParse(values[3], out ret))
                            {
                                lists_stocks.Add(ret);
                            }
                            if (Int32.TryParse(values[4], out ret))
                            {
                                lists_invest.Add(ret);
                            }
                            if (Int32.TryParse(values[5], out ret))
                            {
                                lists_points.Add(ret);
                            }
                            if (lists_date.Count() < lists_total.Count())
                            {
                                lists_date.Add(values[0]);
                            }
                        }
                    }
                }
            }

            // データをシリーズにセットします
            for (int i = 0; i < lists_total.Count; i++)
            {
                string disp_date_string = disp_date.ToString("yyyy/MM");
                if (lists_date[i].StartsWith(disp_date_string))
                {
                    chart1.Series[legend_total].Points.AddXY(lists_date[i], lists_total[i]);
                    chart1.Series[legend_cash].Points.AddY(lists_cash[i]);
                    chart1.Series[legend_stocks].Points.AddY(lists_stocks[i]);
                    chart1.Series[legend_invest].Points.AddY(lists_invest[i]);
                    chart1.Series[legend_points].Points.AddY(lists_points[i]);
                }
            }

            sr.Close();
            sr.Dispose();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            UpdateGraph(dateTimePicker1.Value);
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);
            chart1.ChartAreas[0].CursorX.Interval = 0.001;
            chart1.ChartAreas[0].CursorY.Interval = 0.001;

            try
            {

            }
            catch
            {
                //pass
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(-1);
            UpdateGraph(dateTimePicker1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(1);
            UpdateGraph(dateTimePicker1.Value);

        }
    }
}
