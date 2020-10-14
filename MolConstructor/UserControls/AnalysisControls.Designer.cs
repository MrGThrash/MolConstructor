namespace MolConstructor
{
    partial class AnalysisControls
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisControls));
            this.cmbHalfes = new System.Windows.Forms.ComboBox();
            this.chbAllFolders = new System.Windows.Forms.CheckBox();
            this.tbMGArea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chbTwoPhaseDense = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbCoreBead = new System.Windows.Forms.TextBox();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCoord = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chbAccSepMols = new System.Windows.Forms.CheckBox();
            this.chbByProb = new System.Windows.Forms.CheckBox();
            this.chbHasBonds = new System.Windows.Forms.CheckBox();
            this.chbInBulk = new System.Windows.Forms.CheckBox();
            this.chbByVolume = new System.Windows.Forms.CheckBox();
            this.chbByCylinder = new System.Windows.Forms.CheckBox();
            this.chbAllFiles = new System.Windows.Forms.CheckBox();
            this.tbChainLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.tbEpsilon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeOfResults = new System.Windows.Forms.ComboBox();
            this.gbShiftValues = new System.Windows.Forms.GroupBox();
            this.chbAutoCenterZ = new System.Windows.Forms.CheckBox();
            this.chbWithAutoCenter = new System.Windows.Forms.CheckBox();
            this.tbShiftY = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.tbShiftX = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.tbShiftZ = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.chbWithShift = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnGetData = new System.Windows.Forms.Button();
            this.tbTimeStep = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDirectoryPath = new System.Windows.Forms.TextBox();
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.dgvDataFromFolder = new System.Windows.Forms.DataGridView();
            this.StepTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xsize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ysize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MeanRadiusXY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zsize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GyrRad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLong2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HEad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorkerAggNumber = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerBondLength = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerCatalRate = new System.ComponentModel.BackgroundWorker();
            this.bgWorker2DHeatMap = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerRadialDensity = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerFilmProfile = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerSolv = new System.ComponentModel.BackgroundWorker();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerLarina = new System.ComponentModel.BackgroundWorker();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.bgWorker2Dorder = new System.ComponentModel.BackgroundWorker();
            this.gbOptions.SuspendLayout();
            this.gbShiftValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFromFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbHalfes
            // 
            this.cmbHalfes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHalfes.FormattingEnabled = true;
            this.cmbHalfes.Items.AddRange(new object[] {
            "\"Вниз\"",
            "\"Вверх\""});
            this.cmbHalfes.Location = new System.Drawing.Point(669, 161);
            this.cmbHalfes.Name = "cmbHalfes";
            this.cmbHalfes.Size = new System.Drawing.Size(65, 24);
            this.cmbHalfes.TabIndex = 63;
            this.cmbHalfes.Visible = false;
            // 
            // chbAllFolders
            // 
            this.chbAllFolders.AutoSize = true;
            this.chbAllFolders.Location = new System.Drawing.Point(639, 30);
            this.chbAllFolders.Name = "chbAllFolders";
            this.chbAllFolders.Size = new System.Drawing.Size(95, 20);
            this.chbAllFolders.TabIndex = 62;
            this.chbAllFolders.Text = "Все папки";
            this.chbAllFolders.UseVisualStyleBackColor = true;
            // 
            // tbMGArea
            // 
            this.tbMGArea.Location = new System.Drawing.Point(118, 207);
            this.tbMGArea.Name = "tbMGArea";
            this.tbMGArea.Size = new System.Drawing.Size(100, 23);
            this.tbMGArea.TabIndex = 61;
            this.tbMGArea.Text = "60.0";
            this.tbMGArea.Visible = false;
            this.tbMGArea.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 16);
            this.label4.TabIndex = 60;
            this.label4.Text = "Координата:";
            this.label4.Visible = false;
            // 
            // chbTwoPhaseDense
            // 
            this.chbTwoPhaseDense.AutoSize = true;
            this.chbTwoPhaseDense.Location = new System.Drawing.Point(567, 159);
            this.chbTwoPhaseDense.Name = "chbTwoPhaseDense";
            this.chbTwoPhaseDense.Size = new System.Drawing.Size(96, 36);
            this.chbTwoPhaseDense.TabIndex = 59;
            this.chbTwoPhaseDense.Text = " двухфаз. \r\nплотность";
            this.chbTwoPhaseDense.UseVisualStyleBackColor = true;
            this.chbTwoPhaseDense.Visible = false;
            this.chbTwoPhaseDense.Click += new System.EventHandler(this.chbTwoPhaseDense_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(247, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 58;
            this.label7.Text = "Тип бидов ядер:";
            this.label7.Visible = false;
            // 
            // tbCoreBead
            // 
            this.tbCoreBead.Location = new System.Drawing.Point(250, 212);
            this.tbCoreBead.Name = "tbCoreBead";
            this.tbCoreBead.Size = new System.Drawing.Size(107, 23);
            this.tbCoreBead.TabIndex = 57;
            this.tbCoreBead.Text = "2";
            this.tbCoreBead.Visible = false;
            this.tbCoreBead.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Items.AddRange(new object[] {
            "lammpstrj",
            "mol2",
            "xyzr"});
            this.cmbFormat.Location = new System.Drawing.Point(522, 30);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(104, 24);
            this.cmbFormat.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 55;
            this.label6.Text = "Формат файлов:";
            // 
            // cmbCoord
            // 
            this.cmbCoord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoord.FormattingEnabled = true;
            this.cmbCoord.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cmbCoord.Location = new System.Drawing.Point(669, 119);
            this.cmbCoord.Name = "cmbCoord";
            this.cmbCoord.Size = new System.Drawing.Size(64, 24);
            this.cmbCoord.TabIndex = 54;
            this.cmbCoord.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(573, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 16);
            this.label13.TabIndex = 53;
            this.label13.Text = "Координата";
            this.label13.Visible = false;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chbAccSepMols);
            this.gbOptions.Controls.Add(this.chbByProb);
            this.gbOptions.Controls.Add(this.chbHasBonds);
            this.gbOptions.Controls.Add(this.chbInBulk);
            this.gbOptions.Controls.Add(this.chbByVolume);
            this.gbOptions.Controls.Add(this.chbByCylinder);
            this.gbOptions.Location = new System.Drawing.Point(404, 63);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(157, 167);
            this.gbOptions.TabIndex = 52;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Доп. опции ";
            // 
            // chbAccSepMols
            // 
            this.chbAccSepMols.AutoSize = true;
            this.chbAccSepMols.Enabled = false;
            this.chbAccSepMols.Location = new System.Drawing.Point(11, 143);
            this.chbAccSepMols.Name = "chbAccSepMols";
            this.chbAccSepMols.Size = new System.Drawing.Size(126, 20);
            this.chbAccSepMols.TabIndex = 28;
            this.chbAccSepMols.Text = "Отд. молекулы";
            this.chbAccSepMols.UseVisualStyleBackColor = true;
            // 
            // chbByProb
            // 
            this.chbByProb.AutoSize = true;
            this.chbByProb.Enabled = false;
            this.chbByProb.Location = new System.Drawing.Point(11, 118);
            this.chbByProb.Name = "chbByProb";
            this.chbByProb.Size = new System.Drawing.Size(122, 20);
            this.chbByProb.TabIndex = 27;
            this.chbByProb.Text = "По вероятн-ти";
            this.chbByProb.UseVisualStyleBackColor = true;
            // 
            // chbHasBonds
            // 
            this.chbHasBonds.AutoSize = true;
            this.chbHasBonds.Enabled = false;
            this.chbHasBonds.Location = new System.Drawing.Point(11, 93);
            this.chbHasBonds.Name = "chbHasBonds";
            this.chbHasBonds.Size = new System.Drawing.Size(105, 20);
            this.chbHasBonds.TabIndex = 26;
            this.chbHasBonds.Text = "Учет связей";
            this.chbHasBonds.UseVisualStyleBackColor = true;
            // 
            // chbInBulk
            // 
            this.chbInBulk.AutoSize = true;
            this.chbInBulk.Enabled = false;
            this.chbInBulk.Location = new System.Drawing.Point(11, 19);
            this.chbInBulk.Name = "chbInBulk";
            this.chbInBulk.Size = new System.Drawing.Size(107, 20);
            this.chbInBulk.TabIndex = 21;
            this.chbInBulk.Text = "По толщине";
            this.chbInBulk.UseVisualStyleBackColor = true;
            // 
            // chbByVolume
            // 
            this.chbByVolume.AutoSize = true;
            this.chbByVolume.Enabled = false;
            this.chbByVolume.Location = new System.Drawing.Point(11, 44);
            this.chbByVolume.Name = "chbByVolume";
            this.chbByVolume.Size = new System.Drawing.Size(98, 20);
            this.chbByVolume.TabIndex = 19;
            this.chbByVolume.Text = "По объему";
            this.chbByVolume.UseVisualStyleBackColor = true;
            // 
            // chbByCylinder
            // 
            this.chbByCylinder.AutoSize = true;
            this.chbByCylinder.Enabled = false;
            this.chbByCylinder.Location = new System.Drawing.Point(11, 69);
            this.chbByCylinder.Name = "chbByCylinder";
            this.chbByCylinder.Size = new System.Drawing.Size(113, 20);
            this.chbByCylinder.TabIndex = 20;
            this.chbByCylinder.Text = "По цилиндру";
            this.chbByCylinder.UseVisualStyleBackColor = true;
            // 
            // chbAllFiles
            // 
            this.chbAllFiles.AutoSize = true;
            this.chbAllFiles.Location = new System.Drawing.Point(639, 6);
            this.chbAllFiles.Name = "chbAllFiles";
            this.chbAllFiles.Size = new System.Drawing.Size(101, 20);
            this.chbAllFiles.TabIndex = 51;
            this.chbAllFiles.Text = "Все файлы";
            this.chbAllFiles.UseVisualStyleBackColor = true;
            // 
            // tbChainLength
            // 
            this.tbChainLength.Location = new System.Drawing.Point(250, 165);
            this.tbChainLength.Name = "tbChainLength";
            this.tbChainLength.Size = new System.Drawing.Size(107, 23);
            this.tbChainLength.TabIndex = 50;
            this.tbChainLength.Text = "40";
            this.tbChainLength.Visible = false;
            this.tbChainLength.TextChanged += new System.EventHandler(this.TextBox_TextChangedInt);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 16);
            this.label5.TabIndex = 49;
            this.label5.Text = "Длина цепей:";
            this.label5.Visible = false;
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(247, 56);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(142, 16);
            this.lblStep.TabIndex = 48;
            this.lblStep.Text = "Шаг по координате:";
            // 
            // tbEpsilon
            // 
            this.tbEpsilon.Location = new System.Drawing.Point(250, 75);
            this.tbEpsilon.Name = "tbEpsilon";
            this.tbEpsilon.Size = new System.Drawing.Size(78, 23);
            this.tbEpsilon.TabIndex = 47;
            this.tbEpsilon.Text = "1";
            this.tbEpsilon.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(594, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Тип результатов:";
            // 
            // cmbTypeOfResults
            // 
            this.cmbTypeOfResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeOfResults.FormattingEnabled = true;
            this.cmbTypeOfResults.Items.AddRange(new object[] {
            "Размеры молекулы",
            "Смещение молекулы",
            "Набухание пленки",
            "Концен-ия вдоль оси ",
            "Распределение концов",
            "Растворитель в микрогеле ",
            "Радиальная плотность",
            "2D heatmap",
            "Агрегационное число",
            "Структурный фактор",
            "2D Ориентационный порядок",
            "Плотность микрогеля",
            "Распределение длин связей",
            "Скорость катализа",
            "Контурная длина "});
            this.cmbTypeOfResults.Location = new System.Drawing.Point(567, 78);
            this.cmbTypeOfResults.Name = "cmbTypeOfResults";
            this.cmbTypeOfResults.Size = new System.Drawing.Size(167, 24);
            this.cmbTypeOfResults.TabIndex = 45;
            this.cmbTypeOfResults.SelectedIndexChanged += new System.EventHandler(this.cmbTypeOfResults_SelectedIndexChanged);
            // 
            // gbShiftValues
            // 
            this.gbShiftValues.Controls.Add(this.chbAutoCenterZ);
            this.gbShiftValues.Controls.Add(this.chbWithAutoCenter);
            this.gbShiftValues.Controls.Add(this.tbShiftY);
            this.gbShiftValues.Controls.Add(this.label59);
            this.gbShiftValues.Controls.Add(this.tbShiftX);
            this.gbShiftValues.Controls.Add(this.label58);
            this.gbShiftValues.Controls.Add(this.tbShiftZ);
            this.gbShiftValues.Controls.Add(this.label61);
            this.gbShiftValues.Enabled = false;
            this.gbShiftValues.Location = new System.Drawing.Point(12, 91);
            this.gbShiftValues.Name = "gbShiftValues";
            this.gbShiftValues.Size = new System.Drawing.Size(228, 95);
            this.gbShiftValues.TabIndex = 44;
            this.gbShiftValues.TabStop = false;
            this.gbShiftValues.Text = "Смещения по координатам";
            // 
            // chbAutoCenterZ
            // 
            this.chbAutoCenterZ.AutoSize = true;
            this.chbAutoCenterZ.Enabled = false;
            this.chbAutoCenterZ.Location = new System.Drawing.Point(148, 65);
            this.chbAutoCenterZ.Name = "chbAutoCenterZ";
            this.chbAutoCenterZ.Size = new System.Drawing.Size(67, 20);
            this.chbAutoCenterZ.TabIndex = 100;
            this.chbAutoCenterZ.Text = "И по Z";
            this.chbAutoCenterZ.UseVisualStyleBackColor = true;
            this.chbAutoCenterZ.Click += new System.EventHandler(this.chbAutoCenterZ_CheckedChanged);
            // 
            // chbWithAutoCenter
            // 
            this.chbWithAutoCenter.AutoSize = true;
            this.chbWithAutoCenter.Location = new System.Drawing.Point(11, 65);
            this.chbWithAutoCenter.Name = "chbWithAutoCenter";
            this.chbWithAutoCenter.Size = new System.Drawing.Size(131, 20);
            this.chbWithAutoCenter.TabIndex = 99;
            this.chbWithAutoCenter.Text = "Автоцентровка";
            this.chbWithAutoCenter.UseVisualStyleBackColor = true;
            this.chbWithAutoCenter.Click += new System.EventHandler(this.chbWithAutoCenter_CheckedChanged);
            // 
            // tbShiftY
            // 
            this.tbShiftY.Location = new System.Drawing.Point(81, 35);
            this.tbShiftY.Name = "tbShiftY";
            this.tbShiftY.Size = new System.Drawing.Size(58, 23);
            this.tbShiftY.TabIndex = 95;
            this.tbShiftY.Text = "0";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(78, 19);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(16, 16);
            this.label59.TabIndex = 97;
            this.label59.Text = "Y";
            // 
            // tbShiftX
            // 
            this.tbShiftX.Location = new System.Drawing.Point(18, 35);
            this.tbShiftX.Name = "tbShiftX";
            this.tbShiftX.Size = new System.Drawing.Size(58, 23);
            this.tbShiftX.TabIndex = 93;
            this.tbShiftX.Text = "0";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(145, 19);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(15, 16);
            this.label58.TabIndex = 98;
            this.label58.Text = "Z";
            // 
            // tbShiftZ
            // 
            this.tbShiftZ.Location = new System.Drawing.Point(148, 35);
            this.tbShiftZ.Name = "tbShiftZ";
            this.tbShiftZ.Size = new System.Drawing.Size(58, 23);
            this.tbShiftZ.TabIndex = 96;
            this.tbShiftZ.Text = "0";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(15, 19);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(17, 16);
            this.label61.TabIndex = 94;
            this.label61.Text = "X";
            // 
            // chbWithShift
            // 
            this.chbWithShift.AutoSize = true;
            this.chbWithShift.Location = new System.Drawing.Point(17, 65);
            this.chbWithShift.Name = "chbWithShift";
            this.chbWithShift.Size = new System.Drawing.Size(92, 20);
            this.chbWithShift.TabIndex = 43;
            this.chbWithShift.Text = "Сместить";
            this.chbWithShift.UseVisualStyleBackColor = true;
            this.chbWithShift.Click += new System.EventHandler(this.chbWithShift_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(665, 566);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(49, 566);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(601, 23);
            this.pBar.TabIndex = 41;
            this.pBar.Visible = false;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(584, 207);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(150, 23);
            this.btnGetData.TabIndex = 40;
            this.btnGetData.Text = "Получить данные";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // tbTimeStep
            // 
            this.tbTimeStep.Location = new System.Drawing.Point(250, 119);
            this.tbTimeStep.Name = "tbTimeStep";
            this.tbTimeStep.Size = new System.Drawing.Size(78, 23);
            this.tbTimeStep.TabIndex = 39;
            this.tbTimeStep.Text = "100";
            this.tbTimeStep.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "Шаг по времени:";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Укажите путь к папке с файлами:";
            // 
            // tbDirectoryPath
            // 
            this.tbDirectoryPath.Location = new System.Drawing.Point(12, 30);
            this.tbDirectoryPath.Name = "tbDirectoryPath";
            this.tbDirectoryPath.Size = new System.Drawing.Size(392, 23);
            this.tbDirectoryPath.TabIndex = 36;
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.Location = new System.Drawing.Point(415, 30);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(89, 23);
            this.btnChoosePath.TabIndex = 35;
            this.btnChoosePath.Text = "Выбрать";
            this.btnChoosePath.UseVisualStyleBackColor = true;
            this.btnChoosePath.Click += new System.EventHandler(this.btnChoosePath_Click);
            // 
            // dgvDataFromFolder
            // 
            this.dgvDataFromFolder.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvDataFromFolder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataFromFolder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StepTime,
            this.Xsize,
            this.Ysize,
            this.MeanRadiusXY,
            this.Zsize,
            this.GyrRad,
            this.ColumnLong2,
            this.HEad1,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column4});
            this.dgvDataFromFolder.Location = new System.Drawing.Point(13, 244);
            this.dgvDataFromFolder.Name = "dgvDataFromFolder";
            this.dgvDataFromFolder.Size = new System.Drawing.Size(723, 307);
            this.dgvDataFromFolder.TabIndex = 34;
            // 
            // StepTime
            // 
            this.StepTime.HeaderText = "Время, шаги";
            this.StepTime.Name = "StepTime";
            // 
            // Xsize
            // 
            this.Xsize.HeaderText = "Размер по X";
            this.Xsize.Name = "Xsize";
            // 
            // Ysize
            // 
            this.Ysize.HeaderText = "Размер по Y";
            this.Ysize.Name = "Ysize";
            // 
            // MeanRadiusXY
            // 
            this.MeanRadiusXY.HeaderText = "Средний г/д радиус по XY";
            this.MeanRadiusXY.Name = "MeanRadiusXY";
            // 
            // Zsize
            // 
            this.Zsize.HeaderText = "Размер по Z";
            this.Zsize.Name = "Zsize";
            // 
            // GyrRad
            // 
            this.GyrRad.HeaderText = "Радиус инерции";
            this.GyrRad.Name = "GyrRad";
            // 
            // ColumnLong2
            // 
            this.ColumnLong2.HeaderText = "Асферичность абс.";
            this.ColumnLong2.Name = "ColumnLong2";
            // 
            // HEad1
            // 
            this.HEad1.HeaderText = "Асферичность относ.";
            this.HEad1.Name = "HEad1";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Пустая колонка";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Пустая колонка";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Пустая колонка";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Пустая колонка";
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Пустая колонка";
            this.Column4.Name = "Column4";
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // bgWorkerAggNumber
            // 
            this.bgWorkerAggNumber.WorkerReportsProgress = true;
            this.bgWorkerAggNumber.WorkerSupportsCancellation = true;
            this.bgWorkerAggNumber.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerAggNumber_DoWork);
            this.bgWorkerAggNumber.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerAggNumber_ProgressChanged);
            this.bgWorkerAggNumber.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerAggNumber_RunWorkerCompleted);
            // 
            // bgWorkerBondLength
            // 
            this.bgWorkerBondLength.WorkerReportsProgress = true;
            this.bgWorkerBondLength.WorkerSupportsCancellation = true;
            this.bgWorkerBondLength.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerBondLength_DoWork);
            this.bgWorkerBondLength.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerBondLength_ProgressChanged);
            this.bgWorkerBondLength.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerBondLength_RunWorkerCompleted);
            // 
            // bgWorkerCatalRate
            // 
            this.bgWorkerCatalRate.WorkerReportsProgress = true;
            this.bgWorkerCatalRate.WorkerSupportsCancellation = true;
            this.bgWorkerCatalRate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerCatalRate_DoWork);
            this.bgWorkerCatalRate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerCatalRate_ProgressChanged);
            this.bgWorkerCatalRate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerCatalRate_RunWorkerCompleted);
            // 
            // bgWorker2DHeatMap
            // 
            this.bgWorker2DHeatMap.WorkerReportsProgress = true;
            this.bgWorker2DHeatMap.WorkerSupportsCancellation = true;
            this.bgWorker2DHeatMap.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker2DHeatMap_DoWork);
            this.bgWorker2DHeatMap.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker2DHeatMap_ProgressChanged);
            this.bgWorker2DHeatMap.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker2DHeatMap_RunWorkerCompleted);
            // 
            // bgWorkerRadialDensity
            // 
            this.bgWorkerRadialDensity.WorkerReportsProgress = true;
            this.bgWorkerRadialDensity.WorkerSupportsCancellation = true;
            this.bgWorkerRadialDensity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerRadialDensity_DoWork);
            this.bgWorkerRadialDensity.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerRadialDensity_ProgressChanged);
            this.bgWorkerRadialDensity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerRadialDensity_RunWorkerCompleted);
            // 
            // bgWorkerFilmProfile
            // 
            this.bgWorkerFilmProfile.WorkerReportsProgress = true;
            this.bgWorkerFilmProfile.WorkerSupportsCancellation = true;
            this.bgWorkerFilmProfile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerFilmProfile_DoWork);
            this.bgWorkerFilmProfile.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerFilmProfile_ProgressChanged);
            this.bgWorkerFilmProfile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkeFilmProfile_RunWorkerCompleted);
            // 
            // bgWorkerSolv
            // 
            this.bgWorkerSolv.WorkerReportsProgress = true;
            this.bgWorkerSolv.WorkerSupportsCancellation = true;
            this.bgWorkerSolv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerSolv_DoWork);
            this.bgWorkerSolv.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerSolv_ProgressChanged);
            this.bgWorkerSolv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerSolv_RunWorkerCompleted);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // bgWorkerLarina
            // 
            this.bgWorkerLarina.WorkerReportsProgress = true;
            this.bgWorkerLarina.WorkerSupportsCancellation = true;
            this.bgWorkerLarina.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLarina_DoWork);
            this.bgWorkerLarina.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerLarina_ProgressChanged);
            this.bgWorkerLarina.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerLarina_RunWorkerCompleted);
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowInfo.FlatAppearance.BorderSize = 0;
            this.btnShowInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowInfo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnShowInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnShowInfo.Image")));
            this.btnShowInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShowInfo.Location = new System.Drawing.Point(2, 563);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(41, 40);
            this.btnShowInfo.TabIndex = 64;
            this.btnShowInfo.UseVisualStyleBackColor = false;
            this.btnShowInfo.Click += new System.EventHandler(this.btnShowInfo_Click);
            // 
            // bgWorker2Dorder
            // 
            this.bgWorker2Dorder.WorkerReportsProgress = true;
            this.bgWorker2Dorder.WorkerSupportsCancellation = true;
            this.bgWorker2Dorder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker2Dorder_DoWork);
            this.bgWorker2Dorder.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker2Dorder_ProgressChanged);
            this.bgWorker2Dorder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker2Dorder_RunWorkerCompleted);
            // 
            // AnalysisControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btnShowInfo);
            this.Controls.Add(this.cmbHalfes);
            this.Controls.Add(this.chbAllFolders);
            this.Controls.Add(this.tbMGArea);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chbTwoPhaseDense);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbCoreBead);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbCoord);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.chbAllFiles);
            this.Controls.Add(this.tbChainLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.tbEpsilon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTypeOfResults);
            this.Controls.Add(this.gbShiftValues);
            this.Controls.Add(this.chbWithShift);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.tbTimeStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDirectoryPath);
            this.Controls.Add(this.btnChoosePath);
            this.Controls.Add(this.dgvDataFromFolder);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnalysisControls";
            this.Size = new System.Drawing.Size(749, 606);
            this.Load += new System.EventHandler(this.AnalysisControl_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.gbShiftValues.ResumeLayout(false);
            this.gbShiftValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataFromFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbHalfes;
        private System.Windows.Forms.CheckBox chbAllFolders;
        private System.Windows.Forms.TextBox tbMGArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chbTwoPhaseDense;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCoreBead;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCoord;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chbAccSepMols;
        private System.Windows.Forms.CheckBox chbByProb;
        private System.Windows.Forms.CheckBox chbHasBonds;
        private System.Windows.Forms.CheckBox chbByVolume;
        private System.Windows.Forms.CheckBox chbByCylinder;
        private System.Windows.Forms.CheckBox chbAllFiles;
        private System.Windows.Forms.TextBox tbChainLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.TextBox tbEpsilon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTypeOfResults;
        private System.Windows.Forms.GroupBox gbShiftValues;
        private System.Windows.Forms.CheckBox chbAutoCenterZ;
        private System.Windows.Forms.CheckBox chbWithAutoCenter;
        private System.Windows.Forms.TextBox tbShiftY;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox tbShiftX;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox tbShiftZ;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.CheckBox chbWithShift;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox tbTimeStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDirectoryPath;
        private System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.DataGridView dgvDataFromFolder;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ErrorProvider ep;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker bgWorkerAggNumber;
        private System.ComponentModel.BackgroundWorker bgWorkerBondLength;
        private System.ComponentModel.BackgroundWorker bgWorkerCatalRate;
        private System.ComponentModel.BackgroundWorker bgWorker2DHeatMap;
        private System.ComponentModel.BackgroundWorker bgWorkerRadialDensity;
        private System.ComponentModel.BackgroundWorker bgWorkerFilmProfile;
        private System.ComponentModel.BackgroundWorker bgWorkerSolv;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.ComponentModel.BackgroundWorker bgWorkerLarina;
        private System.Windows.Forms.Button btnShowInfo;
        private System.ComponentModel.BackgroundWorker bgWorker2Dorder;
        private System.Windows.Forms.CheckBox chbInBulk;
        private System.Windows.Forms.DataGridViewTextBoxColumn StepTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Xsize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ysize;
        private System.Windows.Forms.DataGridViewTextBoxColumn MeanRadiusXY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zsize;
        private System.Windows.Forms.DataGridViewTextBoxColumn GyrRad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLong2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HEad1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}
