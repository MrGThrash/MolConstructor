namespace MolConstructor
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TasksPanel = new System.Windows.Forms.Panel();
            this.btnShowSettings = new System.Windows.Forms.Button();
            this.btnShowAnalysis = new System.Windows.Forms.Button();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.btnShowVisualisation = new System.Windows.Forms.Button();
            this.btnShowConstruct = new System.Windows.Forms.Button();
            this.btnShowEdits = new System.Windows.Forms.Button();
            this.cnstrControl = new MolConstructor.ConstructControl();
            this.editControl = new MolConstructor.EditControl();
            this.visControl = new MolConstructor.UserControls.VisualControl();
            this.anlsControl = new MolConstructor.AnalysisControls();
            this.TasksPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TasksPanel
            // 
            resources.ApplyResources(this.TasksPanel, "TasksPanel");
            this.TasksPanel.BackColor = System.Drawing.SystemColors.Control;
            this.TasksPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TasksPanel.Controls.Add(this.btnShowSettings);
            this.TasksPanel.Controls.Add(this.btnShowAnalysis);
            this.TasksPanel.Controls.Add(this.btnShowInfo);
            this.TasksPanel.Controls.Add(this.btnShowVisualisation);
            this.TasksPanel.Controls.Add(this.btnShowConstruct);
            this.TasksPanel.Controls.Add(this.btnShowEdits);
            this.TasksPanel.Name = "TasksPanel";
            // 
            // btnShowSettings
            // 
            resources.ApplyResources(this.btnShowSettings, "btnShowSettings");
            this.btnShowSettings.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowSettings.FlatAppearance.BorderSize = 0;
            this.btnShowSettings.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowSettings.Name = "btnShowSettings";
            this.btnShowSettings.UseVisualStyleBackColor = false;
            this.btnShowSettings.Click += new System.EventHandler(this.btnShowSettings_Click);
            // 
            // btnShowAnalysis
            // 
            resources.ApplyResources(this.btnShowAnalysis, "btnShowAnalysis");
            this.btnShowAnalysis.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowAnalysis.FlatAppearance.BorderSize = 0;
            this.btnShowAnalysis.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAnalysis.Name = "btnShowAnalysis";
            this.btnShowAnalysis.UseVisualStyleBackColor = false;
            this.btnShowAnalysis.Click += new System.EventHandler(this.btnShowAnalysis_Click);
            // 
            // btnShowInfo
            // 
            resources.ApplyResources(this.btnShowInfo, "btnShowInfo");
            this.btnShowInfo.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowInfo.FlatAppearance.BorderSize = 0;
            this.btnShowInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.UseVisualStyleBackColor = false;
            this.btnShowInfo.Click += new System.EventHandler(this.btnShowInfo_Click);
            // 
            // btnShowVisualisation
            // 
            resources.ApplyResources(this.btnShowVisualisation, "btnShowVisualisation");
            this.btnShowVisualisation.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowVisualisation.FlatAppearance.BorderSize = 0;
            this.btnShowVisualisation.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowVisualisation.Name = "btnShowVisualisation";
            this.btnShowVisualisation.UseVisualStyleBackColor = false;
            this.btnShowVisualisation.Click += new System.EventHandler(this.btnShowVisualisation_Click);
            // 
            // btnShowConstruct
            // 
            resources.ApplyResources(this.btnShowConstruct, "btnShowConstruct");
            this.btnShowConstruct.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowConstruct.FlatAppearance.BorderSize = 0;
            this.btnShowConstruct.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowConstruct.Name = "btnShowConstruct";
            this.btnShowConstruct.UseVisualStyleBackColor = false;
            this.btnShowConstruct.Click += new System.EventHandler(this.btnShowConstruct_Click);
            // 
            // btnShowEdits
            // 
            resources.ApplyResources(this.btnShowEdits, "btnShowEdits");
            this.btnShowEdits.BackColor = System.Drawing.SystemColors.Control;
            this.btnShowEdits.FlatAppearance.BorderSize = 0;
            this.btnShowEdits.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowEdits.Name = "btnShowEdits";
            this.btnShowEdits.UseVisualStyleBackColor = false;
            this.btnShowEdits.Click += new System.EventHandler(this.btnShowEdits_Click);
            // 
            // cnstrControl
            // 
            resources.ApplyResources(this.cnstrControl, "cnstrControl");
            this.cnstrControl.Name = "cnstrControl";
            // 
            // editControl
            // 
            resources.ApplyResources(this.editControl, "editControl");
            this.editControl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.editControl.Name = "editControl";
            // 
            // visControl
            // 
            resources.ApplyResources(this.visControl, "visControl");
            this.visControl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.visControl.Name = "visControl";
            // 
            // anlsControl
            // 
            resources.ApplyResources(this.anlsControl, "anlsControl");
            this.anlsControl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.anlsControl.Name = "anlsControl";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cnstrControl);
            this.Controls.Add(this.TasksPanel);
            this.Controls.Add(this.anlsControl);
            this.Controls.Add(this.visControl);
            this.Controls.Add(this.editControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.TasksPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TasksPanel;
        private System.Windows.Forms.Button btnShowConstruct;
        private System.Windows.Forms.Button btnShowEdits;
        private System.Windows.Forms.Button btnShowVisualisation;
        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.Button btnShowAnalysis;
        private System.Windows.Forms.Button btnShowSettings;
        private ConstructControl cnstrControl;
        private EditControl editControl;
        private UserControls.VisualControl visControl;
        private AnalysisControls anlsControl;
    }
}

