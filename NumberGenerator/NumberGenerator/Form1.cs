using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using NumberGenerator;

namespace NumberGenerator
{
    public partial class Main : Form
    {

        // СТРУКТУРА ГРИДА С РЕЗУЛЬТАТАМИ ГЕНЕРАЦИИ //
        public struct Grid {
            public string error;
            public string step;
            public bool cancel;

            private DataGridView grid;
            private Main parent;
            private int[] arr;
            private double grid_firstColW;
            private double grid_secondColW;

            //создание
            public Grid(DataGridView g, Main p) {
                this.parent = p;
                this.error = this.step = "";
                this.arr = new int[0];
                this.grid_firstColW = 0.15;
                this.grid_secondColW = 0.75;
                this.cancel = false;

                this.grid = g;

                this.commonUpdate();
            }

            //обновление данных
            public void update(Object d, int index = 0) {
                this.parent.Grid_startLoad();
                if (index == 0) {
                    this.commonUpdate();
                }

                this.arr = d as int[];

                for (int i = index; i < this.arr.Length; i++)
                {
                    if (this.cancel) {
                        this.parent.refreshTitle("Генерация закончена. Загрузка чисел прервана на " + Main.formatNumber(i+1) + " из " + Main.formatNumber(this.arr.Length));
                        this.parent.Grid_stopLoad();
                        break;
                    }
                    //this.data.Rows.Add(i + 1, Main.formatNumber(this.arr[i]));
                    this.grid.Rows.Add(i + 1, Main.formatNumber(this.arr[i]));
                    this.step = "Генерация закончена. Загружено чисел в таблицу: " + Main.formatNumber(i+1) + " / " + Main.formatNumber(this.arr.Length);
                    this.parent.refreshTitle(this.step);
                    Application.DoEvents();
                }
                this.grid.Columns[0].Width = (int)(this.grid.Width * this.grid_firstColW);
                this.grid.Columns[1].Width = (int)(this.grid.Width * this.grid_secondColW);
                if (!this.cancel) {
                    this.parent.Grid_endLoad();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //обновление дизайна
            public void commonUpdate() {
                this.grid.DataSource = null;
                this.grid.Rows.Clear();
                this.grid.Columns.Clear();
                this.grid.Columns.Add("ID", "#");
                this.grid.Columns.Add("VALUE", "VALUE");

                this.grid.RowHeadersVisible = false;
                this.grid.ColumnHeadersVisible = true;
                this.grid.AllowUserToAddRows = false;
                this.grid.AllowUserToDeleteRows = false;
                this.grid.MultiSelect = false;
                this.grid.ReadOnly = true;

                this.grid.Columns[0].Width = (int)(this.grid.Width * this.grid_firstColW);
                this.grid.Columns[1].Width = (int)(this.grid.Width * this.grid_secondColW);

                DataGridViewCellStyle styleCells = new DataGridViewCellStyle();
                styleCells.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                styleCells.ForeColor = System.Drawing.SystemColors.WindowText;
                this.grid.Columns[0].DefaultCellStyle = styleCells;

                styleCells.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                this.grid.Columns[1].DefaultCellStyle = styleCells;

                DataGridViewCellStyle styleHeaders = new DataGridViewCellStyle();
                styleHeaders.BackColor = Color.LightGray;
                styleHeaders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                styleHeaders.ForeColor = System.Drawing.SystemColors.WindowText;
                styleHeaders.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

                this.grid.ColumnHeadersDefaultCellStyle = styleHeaders;
            }

            //остановка загрузки данных в таблицу
            public void stop() {
                this.cancel = true;
            }

            //продолжение загрузки данных в таблицу
            public void continueLoad(Object d) {
                int index = this.grid.RowCount;
                this.cancel = false;
                this.update(d, index);
            }

            public void clear() {
                this.cancel = false;
                this.commonUpdate();
            }
        }

        // СТРУКТУРА ТЕКСТОВОЙ ОБЛАСТИ С РЕЗУЛЬТАТАМИ ВЫЧИСЛЕНИЯ //
        public struct Memo {

            private RichTextBox box;
            private Main parent;
            private string endLine;
            private NumberGenerator.Result resultGenerate;
            private NumberGenerator.Result resultCalculate;
            private string countNumbersAfterPoint;

            public Memo(RichTextBox b, Main p) {
                this.box = b;
                this.parent = p;
                this.endLine = "\r\n";
                this.countNumbersAfterPoint = "F3";
                this.resultCalculate = new NumberGenerator.Result();
                this.resultGenerate = new NumberGenerator.Result();
                this.clear();
            }

            public void update(NumberGenerator.Result rG, NumberGenerator.Result rC) {
                this.resultGenerate = rG;
                this.resultCalculate = rC;

                this.box.Clear();

                this.newLine("------ Данные по генерации ------");
                this.newLine("Общая генерация составила " + ((double)this.resultGenerate.MainDiff/1000).ToString(this.countNumbersAfterPoint) + " сек.");
                this.newLine("");
                this.newLine("Генерация внутри потоков:");
                for (int i = 0; i < this.resultGenerate.ThreadDiffs.Length; i++) {
                    string key = (i + 1).ToString();
                    string sec = ((double)this.resultGenerate.ThreadDiffs[i] / 1000).ToString(this.countNumbersAfterPoint);
                    this.newLine(key + "-ый поток - " + sec + " сек.");
                }
                if (this.resultCalculate.data != null) {
                    this.newLine("");
                    this.newLine("------ Данные по вычислению ------");
                    string max = Main.formatNumber((int)this.resultCalculate.data);
                    this.newLine("Максимальное число в выборке - " + max);
                    this.newLine("Общая генерация составила " + ((double)this.resultCalculate.MainDiff / 1000).ToString(this.countNumbersAfterPoint) + " сек.");
                    this.newLine("");
                    this.newLine("Генерация данных внутри потоков:");
                    for (int i = 0; i < this.resultCalculate.ThreadDiffs.Length; i++)
                    {
                        string key = (i + 1).ToString();
                        string sec = ((double)this.resultCalculate.ThreadDiffs[i] / 1000).ToString(this.countNumbersAfterPoint);
                        this.newLine(key + "-ый поток - " + sec + " сек.");
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            public void clear() {
                this.box.Clear();
                this.newLine("Данные отсутствуют");
            }

            private void newLine(string str)
            {
                this.box.AppendText(str + this.endLine);
            }
        }



        /*************** СТАТИЧНЫЕ ПЕРЕМЕННЫЕ ***************/
        private static string numberDelimeter = " ";
        private static string mainTitle = "Генератор чисел";

        private static string text_StopLoadGrid = "Остановить загрузку";
        private static string text_ContinueLoadGrid = "Продолжить загрузку";

        private static string text_Generate = "Сгенерировать";
        private static string text_SearchMaxValue = "Поиск максимального значения";

        private static string text_labelCountNumbers = "Кол-во генерируемых чисел: ";
        private static string text_labelCountThreads_Generate = "Кол-во потоков для генерации";
        private static string text_labelCountThreads_MaxValue = "Кол-во потоков для поиска";

        private static int number_counNumbers = (int)Math.Pow(10, 8);
        private static int number_countThreadsGenerate = 8;
        private static int number_countThreadsCalculate = 4;

        /*************** ПЕРЕМЕННЫЕ ***************/
        NumberGenerator ng;
        Grid DG;
        Memo MB;
        int countNumbers;
        int countGenerateThreads;
        int countCalculateThreads;
        int maxThreads = Environment.ProcessorCount;

        Panel panel_Generate;
        Panel panel_Result;
        Panel panel_Grid;
        Panel panel_GenerateStopContinue;
        Panel panel_SerachMaxValue;

        Button btn_Generate;
        Button btn_LoadStopContinue;
        Button btn_SearchMaxValue;

        Label l_CountNumbers;
        Label l_CountThreads_Generate;
        Label l_CountThreads_MaxValue;

        TextBox t_CountNumbers;
        RichTextBox rt_Result;
        ComboBox cmb_CountThreads_Generate;
        ComboBox cmb_CountThreads_MaxValue;

        /*************** ИНИЦИАЛИЗАЦИЯ ***************/
        public Main()
        {
            InitializeComponent();

            //свойства формы
            this.Text = Main.mainTitle;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            //переменные объектов
            this.DG = new Grid(this.DataGrid, this);
            this.MB = new Memo(this.rtResult, this);

            this.panel_Generate = this.panelGenerate;
            this.panel_Result = this.panelResult;
            this.panel_Grid = this.panelGrid;
            this.panel_GenerateStopContinue = this.panelGenerateStopContinue;
            this.panel_SerachMaxValue = this.panelSearchMaxValue;

            this.btn_Generate = this.btnGenerate;
            this.btn_LoadStopContinue = this.btnLoadStopContinue;
            this.btn_SearchMaxValue = this.btnSearchMaxValue;

            this.l_CountNumbers = this.lCountNumbers;
            this.l_CountThreads_Generate = this.lCountThreads_Generate;
            this.l_CountThreads_MaxValue = this.lCountThreads_MaxValue;

            this.t_CountNumbers = this.tCountNumbers;

            this.rt_Result = this.rtResult;

            this.cmb_CountThreads_Generate = this.cmbCountThreads_Generate;
            this.cmb_CountThreads_MaxValue = this.cmbCountThreads_MaxValue;

            //свойства объектов
            this.panel_Result.Enabled =
                this.panel_Grid.Enabled =
                this.panel_GenerateStopContinue.Enabled =
                this.panel_SerachMaxValue.Enabled = false;

            this.btn_Generate.Text = Main.text_Generate;
            this.btn_LoadStopContinue.Text = Main.text_StopLoadGrid;
            this.btn_SearchMaxValue.Text = Main.text_SearchMaxValue;

            this.l_CountNumbers.Text = Main.text_labelCountNumbers;
            this.l_CountThreads_Generate.Text = Main.text_labelCountThreads_Generate;
            this.l_CountThreads_MaxValue.Text = Main.text_labelCountThreads_MaxValue;

            this.cmb_CountThreads_Generate.RightToLeft =
                this.cmb_CountThreads_MaxValue.RightToLeft = RightToLeft.Yes;

            this.t_CountNumbers.TextAlign = HorizontalAlignment.Right;
            this.t_CountNumbers.Text = Main.number_counNumbers.ToString();

            for (int i = 1; i <= this.maxThreads; i++) {
                this.cmb_CountThreads_Generate.Items.Add(i.ToString());
                this.cmb_CountThreads_MaxValue.Items.Add(i.ToString());
            }
            this.cmb_CountThreads_Generate.Text = Main.number_countThreadsGenerate.ToString();
            this.cmb_CountThreads_MaxValue.Text = Main.number_countThreadsCalculate.ToString();

            this.rt_Result.ReadOnly = true;
        }


        /*************** ФУНКЦИИ ***************/
        private void generate() {
            if (Int32.TryParse(this.t_CountNumbers.Text, out this.countNumbers)
            && Int32.TryParse(this.cmb_CountThreads_Generate.Text, out this.countGenerateThreads))
            {
                this.btn_LoadStopContinue.Text = Main.text_StopLoadGrid;
                DG.clear();
                loader_show();
                ng = new NumberGenerator(this.countNumbers, this.countGenerateThreads);
                while (true)
                {
                    if (!ng.isGenerate)
                    {
                        MB.update(ng.generateResult, ng.calculateResult);
                        DG.update(ng.generateResult.data);
                        break;
                    }
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
            }
            else
            {
                this.setError("Error1");
            }
        }

        private void getMax() {
            if (Int32.TryParse(this.cmb_CountThreads_MaxValue.Text, out this.countCalculateThreads)) {
                if (ng != null && ng.generateResult.data != null)
                {
                    loader_show();
                    ng.foundMax(this.countCalculateThreads);
                    while (true)
                    {
                        if (!ng.isCalculate)
                        {
                            MB.update(ng.generateResult, ng.calculateResult);
                            loader_hide(true);
                            break;
                        }
                        Application.DoEvents();
                        Thread.Sleep(1);
                    }
                }
                else
                {
                    this.setError("Error3");
                }
            }
            else
            {
                this.setError("Error2");
            }


        }

        public void Grid_startLoad() {
            this.panel_Generate.Enabled = false;
            this.panel_Result.Enabled =
                this.panel_Grid.Enabled =
                this.panel_GenerateStopContinue.Enabled =
                this.panel_SerachMaxValue.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        public void Grid_stopLoad() {
            this.panel_Generate.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        public void Grid_endLoad() {
            this.btn_LoadStopContinue.Text = Main.text_StopLoadGrid;
            this.panel_GenerateStopContinue.Enabled = false;
            this.panel_Generate.Enabled = true;
        }

        private void loader_show()
        {
            this.panel_Generate.Enabled =
                this.panel_Result.Enabled =
                this.panel_Grid.Enabled =
                this.panel_GenerateStopContinue.Enabled =
                this.panel_SerachMaxValue.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
        }

        private void loader_hide(bool max = false) {
            this.panel_Result.Enabled =
                this.panel_Grid.Enabled =
                this.panel_SerachMaxValue.Enabled = true;
            if (max) {
                this.panel_GenerateStopContinue.Enabled = true;
            }
            else
            {
                this.btn_LoadStopContinue.Text = Main.text_StopLoadGrid;
                this.panel_GenerateStopContinue.Enabled = false;  
            }
            if (this.btn_LoadStopContinue.Text == Main.text_StopLoadGrid) {
                this.panel_Generate.Enabled = false;
            }
            else if(this.btn_LoadStopContinue.Text == Main.text_ContinueLoadGrid)
            {
                this.panel_Generate.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void GUI_return() {
            this.panel_Generate.Enabled = true;
            this.panel_Result.Enabled =
                this.panel_Grid.Enabled =
                this.panel_GenerateStopContinue.Enabled =
                this.panel_SerachMaxValue.Enabled = false;
        }

        /*************** СТАТИЧНЫЕ ФУНКЦИИ ***************/
        private static string formatNumber(int number) {
            string strNumber = number.ToString();
            string newNumber = "";
            int j = 0;
            for (int i = strNumber.Length -1; i >= 0; i--) {
                newNumber += strNumber[i];
                j++;
                if (j == 3 && i != 0) {
                    newNumber += Main.numberDelimeter;
                    j = 0;
                }
            }
            return Main.reverseString(newNumber);
        }

        private static string reverseString(string str) {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public void setError(string str) {
            this.GUI_return();
            this.Text = Main.mainTitle + " : " + str;
        }

        public void refreshTitle(string str) {
            this.Text = Main.mainTitle + " [" + str + "]";
        }


        /*************** СОБЫТИЯ НАЖАТИЯ КНОПОК ***************/
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Text = "Очистка мусора";
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Text = "Генерация";
            this.generate();
        }

        private void loadStop_Click(object sender, EventArgs e)
        {
            if (!DG.cancel)
            {
                DG.stop();
                this.btnLoadStopContinue.Text = Main.text_ContinueLoadGrid;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                this.btnLoadStopContinue.Text = Main.text_StopLoadGrid;
                DG.continueLoad(ng.generateResult.data);
            } 
        }

        private void btnSearchMaxValue_Click(object sender, EventArgs e)
        {
            this.Text = "Очистка мусора";
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Text = "Поиск максимального значения";
            this.getMax();
        }
    }
}
