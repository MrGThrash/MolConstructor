namespace MolConstructor.UserControls
{
    partial class VisualControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualControl));
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tControlVisualize = new System.Windows.Forms.TabControl();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.btnShowMovie = new System.Windows.Forms.Button();
            this.tbTrimPerc = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.chbTrim = new System.Windows.Forms.CheckBox();
            this.tbMovieOutName = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.cmbFormat_Page1 = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnWriteMovieFile = new System.Windows.Forms.Button();
            this.tbFilesToWrite = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.tbFileCount = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.gbShiftValues = new System.Windows.Forms.GroupBox();
            this.tbShiftY = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.chbHasWalls_Page1 = new System.Windows.Forms.CheckBox();
            this.tbShiftX = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.tbShiftZ = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.chbWithShift = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.tbDirectoryPath = new System.Windows.Forms.TextBox();
            this.btnChoosePath_Page1 = new System.Windows.Forms.Button();
            this.tbPageDrop = new System.Windows.Forms.TabPage();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label78 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.bgWorkerCenter = new System.ComponentModel.BackgroundWorker();
            this.tControlVisualize.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.gbShiftValues.SuspendLayout();
            this.tbPageDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // tControlVisualize
            // 
            this.tControlVisualize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tControlVisualize.Controls.Add(this.tabPage13);
            this.tControlVisualize.Controls.Add(this.tbPageDrop);
            this.tControlVisualize.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tControlVisualize.Location = new System.Drawing.Point(0, 0);
            this.tControlVisualize.Name = "tControlVisualize";
            this.tControlVisualize.SelectedIndex = 0;
            this.tControlVisualize.Size = new System.Drawing.Size(749, 606);
            this.tControlVisualize.TabIndex = 4;
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage13.Controls.Add(this.label1);
            this.tabPage13.Controls.Add(this.cmbTaskType);
            this.tabPage13.Controls.Add(this.btnShowMovie);
            this.tabPage13.Controls.Add(this.tbTrimPerc);
            this.tabPage13.Controls.Add(this.label88);
            this.tabPage13.Controls.Add(this.chbTrim);
            this.tabPage13.Controls.Add(this.tbMovieOutName);
            this.tabPage13.Controls.Add(this.label54);
            this.tabPage13.Controls.Add(this.label53);
            this.tabPage13.Controls.Add(this.cmbFormat_Page1);
            this.tabPage13.Controls.Add(this.btnCancel);
            this.tabPage13.Controls.Add(this.pBar);
            this.tabPage13.Controls.Add(this.btnWriteMovieFile);
            this.tabPage13.Controls.Add(this.tbFilesToWrite);
            this.tabPage13.Controls.Add(this.label41);
            this.tabPage13.Controls.Add(this.tbFileCount);
            this.tabPage13.Controls.Add(this.label40);
            this.tabPage13.Controls.Add(this.gbShiftValues);
            this.tabPage13.Controls.Add(this.chbWithShift);
            this.tabPage13.Controls.Add(this.label39);
            this.tabPage13.Controls.Add(this.tbDirectoryPath);
            this.tabPage13.Controls.Add(this.btnChoosePath_Page1);
            this.tabPage13.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage13.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabPage13.Location = new System.Drawing.Point(4, 25);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(741, 577);
            this.tabPage13.TabIndex = 0;
            this.tabPage13.Text = "Создание movie-файла";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Тип задачи:";
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Items.AddRange(new object[] {
            "movie-файл",
            "центровка 3D",
            "центровка 2D"});
            this.cmbTaskType.Location = new System.Drawing.Point(23, 89);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(121, 24);
            this.cmbTaskType.TabIndex = 52;
            this.cmbTaskType.SelectedIndexChanged += new System.EventHandler(this.cmbTaskType_SelectedIndexChanged);
            // 
            // btnShowMovie
            // 
            this.btnShowMovie.Enabled = false;
            this.btnShowMovie.Location = new System.Drawing.Point(538, 331);
            this.btnShowMovie.Name = "btnShowMovie";
            this.btnShowMovie.Size = new System.Drawing.Size(162, 41);
            this.btnShowMovie.TabIndex = 51;
            this.btnShowMovie.Text = "Показать";
            this.btnShowMovie.UseVisualStyleBackColor = true;
            this.btnShowMovie.Click += new System.EventHandler(this.btnShowMovie_Click);
            // 
            // tbTrimPerc
            // 
            this.tbTrimPerc.Location = new System.Drawing.Point(208, 291);
            this.tbTrimPerc.Name = "tbTrimPerc";
            this.tbTrimPerc.Size = new System.Drawing.Size(100, 23);
            this.tbTrimPerc.TabIndex = 50;
            this.tbTrimPerc.Text = "10";
            this.tbTrimPerc.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(28, 296);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(177, 16);
            this.label88.TabIndex = 49;
            this.label88.Text = "Процент растворителей:";
            // 
            // chbTrim
            // 
            this.chbTrim.AutoSize = true;
            this.chbTrim.Location = new System.Drawing.Point(28, 262);
            this.chbTrim.Name = "chbTrim";
            this.chbTrim.Size = new System.Drawing.Size(148, 20);
            this.chbTrim.TabIndex = 48;
            this.chbTrim.Text = "Проредить атомы";
            this.chbTrim.UseVisualStyleBackColor = true;
            this.chbTrim.CheckedChanged += new System.EventHandler(this.chbTrim_CheckedChanged);
            // 
            // tbMovieOutName
            // 
            this.tbMovieOutName.Location = new System.Drawing.Point(500, 234);
            this.tbMovieOutName.Name = "tbMovieOutName";
            this.tbMovieOutName.Size = new System.Drawing.Size(100, 23);
            this.tbMovieOutName.TabIndex = 47;
            this.tbMovieOutName.Text = "Movie";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(447, 215);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(160, 16);
            this.label54.TabIndex = 46;
            this.label54.Text = "Имя выходного файла:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(20, 10);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(120, 16);
            this.label53.TabIndex = 45;
            this.label53.Text = "Формат файлов:";
            // 
            // cmbFormat_Page1
            // 
            this.cmbFormat_Page1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat_Page1.FormattingEnabled = true;
            this.cmbFormat_Page1.Items.AddRange(new object[] {
            "xyzr",
            "lammpstrj"});
            this.cmbFormat_Page1.Location = new System.Drawing.Point(23, 28);
            this.cmbFormat_Page1.Name = "cmbFormat_Page1";
            this.cmbFormat_Page1.Size = new System.Drawing.Size(131, 24);
            this.cmbFormat_Page1.TabIndex = 44;
            this.cmbFormat_Page1.SelectedIndexChanged += new System.EventHandler(this.cmbFormat_Page1_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(624, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(25, 390);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(582, 23);
            this.pBar.TabIndex = 42;
            this.pBar.Visible = false;
            // 
            // btnWriteMovieFile
            // 
            this.btnWriteMovieFile.Enabled = false;
            this.btnWriteMovieFile.Location = new System.Drawing.Point(538, 284);
            this.btnWriteMovieFile.Name = "btnWriteMovieFile";
            this.btnWriteMovieFile.Size = new System.Drawing.Size(162, 41);
            this.btnWriteMovieFile.TabIndex = 41;
            this.btnWriteMovieFile.Text = "Записать файл";
            this.btnWriteMovieFile.UseVisualStyleBackColor = true;
            this.btnWriteMovieFile.Click += new System.EventHandler(this.btnWriteMovieFile_Click);
            // 
            // tbFilesToWrite
            // 
            this.tbFilesToWrite.Location = new System.Drawing.Point(500, 171);
            this.tbFilesToWrite.Name = "tbFilesToWrite";
            this.tbFilesToWrite.Size = new System.Drawing.Size(100, 23);
            this.tbFilesToWrite.TabIndex = 40;
            this.tbFilesToWrite.TextChanged += new System.EventHandler(this.TextBox_TextChangedInt);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(443, 154);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(214, 16);
            this.label41.TabIndex = 39;
            this.label41.Text = "Число записываемых файлов: ";
            // 
            // tbFileCount
            // 
            this.tbFileCount.Location = new System.Drawing.Point(312, 171);
            this.tbFileCount.Name = "tbFileCount";
            this.tbFileCount.Size = new System.Drawing.Size(100, 23);
            this.tbFileCount.TabIndex = 38;
            this.tbFileCount.TextChanged += new System.EventHandler(this.TextBox_TextChangedInt);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(309, 154);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(106, 16);
            this.label40.TabIndex = 37;
            this.label40.Text = "Число файлов:";
            // 
            // gbShiftValues
            // 
            this.gbShiftValues.Controls.Add(this.tbShiftY);
            this.gbShiftValues.Controls.Add(this.label36);
            this.gbShiftValues.Controls.Add(this.chbHasWalls_Page1);
            this.gbShiftValues.Controls.Add(this.tbShiftX);
            this.gbShiftValues.Controls.Add(this.label37);
            this.gbShiftValues.Controls.Add(this.tbShiftZ);
            this.gbShiftValues.Controls.Add(this.label38);
            this.gbShiftValues.Enabled = false;
            this.gbShiftValues.Location = new System.Drawing.Point(28, 155);
            this.gbShiftValues.Name = "gbShiftValues";
            this.gbShiftValues.Size = new System.Drawing.Size(228, 100);
            this.gbShiftValues.TabIndex = 36;
            this.gbShiftValues.TabStop = false;
            this.gbShiftValues.Text = "Смещения по координатам";
            // 
            // tbShiftY
            // 
            this.tbShiftY.Location = new System.Drawing.Point(80, 39);
            this.tbShiftY.Name = "tbShiftY";
            this.tbShiftY.Size = new System.Drawing.Size(58, 23);
            this.tbShiftY.TabIndex = 95;
            this.tbShiftY.Text = "0";
            this.tbShiftY.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(77, 23);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(16, 16);
            this.label36.TabIndex = 97;
            this.label36.Text = "Y";
            // 
            // chbHasWalls_Page1
            // 
            this.chbHasWalls_Page1.AutoSize = true;
            this.chbHasWalls_Page1.Location = new System.Drawing.Point(17, 73);
            this.chbHasWalls_Page1.Name = "chbHasWalls_Page1";
            this.chbHasWalls_Page1.Size = new System.Drawing.Size(109, 20);
            this.chbHasWalls_Page1.TabIndex = 24;
            this.chbHasWalls_Page1.Text = "Стенки есть";
            this.chbHasWalls_Page1.UseVisualStyleBackColor = true;
            // 
            // tbShiftX
            // 
            this.tbShiftX.Location = new System.Drawing.Point(17, 39);
            this.tbShiftX.Name = "tbShiftX";
            this.tbShiftX.Size = new System.Drawing.Size(58, 23);
            this.tbShiftX.TabIndex = 93;
            this.tbShiftX.Text = "0";
            this.tbShiftX.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(144, 23);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(15, 16);
            this.label37.TabIndex = 98;
            this.label37.Text = "Z";
            // 
            // tbShiftZ
            // 
            this.tbShiftZ.Location = new System.Drawing.Point(147, 39);
            this.tbShiftZ.Name = "tbShiftZ";
            this.tbShiftZ.Size = new System.Drawing.Size(58, 23);
            this.tbShiftZ.TabIndex = 96;
            this.tbShiftZ.Text = "0";
            this.tbShiftZ.TextChanged += new System.EventHandler(this.TextBox_TextChangedFloat);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(14, 23);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(17, 16);
            this.label38.TabIndex = 94;
            this.label38.Text = "X";
            // 
            // chbWithShift
            // 
            this.chbWithShift.AutoSize = true;
            this.chbWithShift.Location = new System.Drawing.Point(25, 133);
            this.chbWithShift.Name = "chbWithShift";
            this.chbWithShift.Size = new System.Drawing.Size(180, 20);
            this.chbWithShift.TabIndex = 35;
            this.chbWithShift.Text = "Смещать при расчете";
            this.chbWithShift.UseVisualStyleBackColor = true;
            this.chbWithShift.CheckedChanged += new System.EventHandler(this.chbWithShift_CheckedChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(172, 10);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(413, 16);
            this.label39.TabIndex = 34;
            this.label39.Text = "Укажите путь к папке с файлами, содержащими молекулы:";
            // 
            // tbDirectoryPath
            // 
            this.tbDirectoryPath.Location = new System.Drawing.Point(175, 29);
            this.tbDirectoryPath.Name = "tbDirectoryPath";
            this.tbDirectoryPath.Size = new System.Drawing.Size(432, 23);
            this.tbDirectoryPath.TabIndex = 33;
            // 
            // btnChoosePath_Page1
            // 
            this.btnChoosePath_Page1.Enabled = false;
            this.btnChoosePath_Page1.Location = new System.Drawing.Point(624, 29);
            this.btnChoosePath_Page1.Name = "btnChoosePath_Page1";
            this.btnChoosePath_Page1.Size = new System.Drawing.Size(75, 23);
            this.btnChoosePath_Page1.TabIndex = 32;
            this.btnChoosePath_Page1.Text = "Выбрать";
            this.btnChoosePath_Page1.UseVisualStyleBackColor = true;
            this.btnChoosePath_Page1.Click += new System.EventHandler(this.btnChoosePath_Page1_Click);
            // 
            // tbPageDrop
            // 
            this.tbPageDrop.BackColor = System.Drawing.SystemColors.Control;
            this.tbPageDrop.Controls.Add(this.pictureBox6);
            this.tbPageDrop.Controls.Add(this.pictureBox5);
            this.tbPageDrop.Controls.Add(this.pictureBox4);
            this.tbPageDrop.Controls.Add(this.pictureBox3);
            this.tbPageDrop.Controls.Add(this.pictureBox2);
            this.tbPageDrop.Controls.Add(this.pictureBox1);
            this.tbPageDrop.Controls.Add(this.label78);
            this.tbPageDrop.Font = new System.Drawing.Font("Century Gothic", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPageDrop.Location = new System.Drawing.Point(4, 25);
            this.tbPageDrop.Name = "tbPageDrop";
            this.tbPageDrop.Size = new System.Drawing.Size(741, 577);
            this.tbPageDrop.TabIndex = 2;
            this.tbPageDrop.Text = "3D визуализация";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(348, 198);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(33, 36);
            this.pictureBox6.TabIndex = 22;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(348, 320);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(33, 36);
            this.pictureBox5.TabIndex = 21;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(617, 198);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(33, 36);
            this.pictureBox4.TabIndex = 20;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(92, 320);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 36);
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(617, 320);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 36);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(92, 198);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 36);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Century Gothic", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label78.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label78.Location = new System.Drawing.Point(38, 228);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(642, 80);
            this.label78.TabIndex = 16;
            this.label78.Text = "Under construction";
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // bgWorkerCenter
            // 
            this.bgWorkerCenter.WorkerReportsProgress = true;
            this.bgWorkerCenter.WorkerSupportsCancellation = true;
            this.bgWorkerCenter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerCenter_DoWork);
            this.bgWorkerCenter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerCenter_ProgressChanged);
            this.bgWorkerCenter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerCenter_RunWorkerCompleted);
            // 
            // VisualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tControlVisualize);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VisualControl";
            this.Size = new System.Drawing.Size(749, 606);
            this.tControlVisualize.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            this.gbShiftValues.ResumeLayout(false);
            this.gbShiftValues.PerformLayout();
            this.tbPageDrop.ResumeLayout(false);
            this.tbPageDrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        public System.Windows.Forms.TabControl tControlVisualize;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.TabPage tbPageDrop;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox tbTrimPerc;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.CheckBox chbTrim;
        private System.Windows.Forms.TextBox tbMovieOutName;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox cmbFormat_Page1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Button btnWriteMovieFile;
        private System.Windows.Forms.TextBox tbFilesToWrite;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox tbFileCount;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox gbShiftValues;
        private System.Windows.Forms.TextBox tbShiftY;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox chbHasWalls_Page1;
        private System.Windows.Forms.TextBox tbShiftX;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox tbShiftZ;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox chbWithShift;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox tbDirectoryPath;
        private System.Windows.Forms.Button btnChoosePath_Page1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnShowMovie;
        private System.Windows.Forms.ErrorProvider ep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTaskType;
        private System.ComponentModel.BackgroundWorker bgWorkerCenter;
    }
}
