namespace Talisman_Game_Tools
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupFolder = new System.Windows.Forms.GroupBox();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.groupSettings = new System.Windows.Forms.GroupBox();
            this.linkForum = new System.Windows.Forms.LinkLabel();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtSettings = new System.Windows.Forms.TextBox();
            this.groupTools = new System.Windows.Forms.GroupBox();
            this.btnPrivileges = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnTasks = new System.Windows.Forms.Button();
            this.btnNPCs = new System.Windows.Forms.Button();
            this.btnSuits = new System.Windows.Forms.Button();
            this.btnItems = new System.Windows.Forms.Button();
            this.groupFolder.SuspendLayout();
            this.groupSettings.SuspendLayout();
            this.groupTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFolder
            // 
            this.groupFolder.Controls.Add(this.btnLock);
            this.groupFolder.Controls.Add(this.btnFolder);
            this.groupFolder.Controls.Add(this.txtFolder);
            this.groupFolder.Location = new System.Drawing.Point(12, 12);
            this.groupFolder.Name = "groupFolder";
            this.groupFolder.Size = new System.Drawing.Size(382, 45);
            this.groupFolder.TabIndex = 0;
            this.groupFolder.TabStop = false;
            this.groupFolder.Text = "PASTA DO PROJETO:";
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Image = global::Talisman_Game_Tools.Properties.Resources.Lock;
            this.btnLock.Location = new System.Drawing.Point(346, 17);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(30, 22);
            this.btnLock.TabIndex = 2;
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolder.Location = new System.Drawing.Point(305, 17);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(35, 22);
            this.btnFolder.TabIndex = 1;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(6, 18);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(300, 20);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            // 
            // groupSettings
            // 
            this.groupSettings.Controls.Add(this.linkForum);
            this.groupSettings.Controls.Add(this.btnApply);
            this.groupSettings.Controls.Add(this.txtSettings);
            this.groupSettings.Location = new System.Drawing.Point(12, 63);
            this.groupSettings.Name = "groupSettings";
            this.groupSettings.Size = new System.Drawing.Size(382, 346);
            this.groupSettings.TabIndex = 1;
            this.groupSettings.TabStop = false;
            this.groupSettings.Text = "local_and_language.ini";
            // 
            // linkForum
            // 
            this.linkForum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkForum.AutoSize = true;
            this.linkForum.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkForum.Location = new System.Drawing.Point(6, 327);
            this.linkForum.Name = "linkForum";
            this.linkForum.Size = new System.Drawing.Size(135, 13);
            this.linkForum.TabIndex = 2;
            this.linkForum.TabStop = true;
            this.linkForum.Text = "www.aldeiatalisman.com.br";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(259, 317);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(117, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Aplicar";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtSettings
            // 
            this.txtSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSettings.Location = new System.Drawing.Point(6, 19);
            this.txtSettings.Multiline = true;
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSettings.Size = new System.Drawing.Size(370, 292);
            this.txtSettings.TabIndex = 0;
            this.txtSettings.Text = "Selecione uma pasta válida!\r\n\r\n- {Seu projeto}/\r\n  * game_client/\r\n  * game_serve" +
    "r/";
            this.txtSettings.TextChanged += new System.EventHandler(this.txtSettings_TextChanged);
            // 
            // groupTools
            // 
            this.groupTools.Controls.Add(this.btnPrivileges);
            this.groupTools.Controls.Add(this.button11);
            this.groupTools.Controls.Add(this.button10);
            this.groupTools.Controls.Add(this.button9);
            this.groupTools.Controls.Add(this.button8);
            this.groupTools.Controls.Add(this.button7);
            this.groupTools.Controls.Add(this.button6);
            this.groupTools.Controls.Add(this.button5);
            this.groupTools.Controls.Add(this.button4);
            this.groupTools.Controls.Add(this.btnTasks);
            this.groupTools.Controls.Add(this.btnNPCs);
            this.groupTools.Controls.Add(this.btnSuits);
            this.groupTools.Controls.Add(this.btnItems);
            this.groupTools.Location = new System.Drawing.Point(400, 12);
            this.groupTools.Name = "groupTools";
            this.groupTools.Size = new System.Drawing.Size(200, 397);
            this.groupTools.TabIndex = 2;
            this.groupTools.TabStop = false;
            this.groupTools.Text = "FERRAMENTAS:";
            // 
            // btnPrivileges
            // 
            this.btnPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrivileges.Location = new System.Drawing.Point(6, 367);
            this.btnPrivileges.Name = "btnPrivileges";
            this.btnPrivileges.Size = new System.Drawing.Size(188, 23);
            this.btnPrivileges.TabIndex = 12;
            this.btnPrivileges.Text = "Privileges";
            this.btnPrivileges.UseVisualStyleBackColor = true;
            this.btnPrivileges.Click += new System.EventHandler(this.btnPrivileges_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(6, 338);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(188, 23);
            this.button11.TabIndex = 11;
            this.button11.Text = "...";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(6, 309);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(188, 23);
            this.button10.TabIndex = 10;
            this.button10.Text = "...";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(6, 280);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(188, 23);
            this.button9.TabIndex = 9;
            this.button9.Text = "...";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(6, 251);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(188, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "...";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(6, 222);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(188, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(6, 193);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(188, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(6, 164);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(188, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(6, 135);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(188, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnTasks
            // 
            this.btnTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTasks.Location = new System.Drawing.Point(6, 106);
            this.btnTasks.Name = "btnTasks";
            this.btnTasks.Size = new System.Drawing.Size(188, 23);
            this.btnTasks.TabIndex = 3;
            this.btnTasks.Text = "Missões";
            this.btnTasks.UseVisualStyleBackColor = true;
            // 
            // btnNPCs
            // 
            this.btnNPCs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNPCs.Location = new System.Drawing.Point(6, 77);
            this.btnNPCs.Name = "btnNPCs";
            this.btnNPCs.Size = new System.Drawing.Size(188, 23);
            this.btnNPCs.TabIndex = 2;
            this.btnNPCs.Text = "NPC\'s";
            this.btnNPCs.UseVisualStyleBackColor = true;
            // 
            // btnSuits
            // 
            this.btnSuits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuits.Location = new System.Drawing.Point(6, 48);
            this.btnSuits.Name = "btnSuits";
            this.btnSuits.Size = new System.Drawing.Size(188, 23);
            this.btnSuits.TabIndex = 1;
            this.btnSuits.Text = "Trajes";
            this.btnSuits.UseVisualStyleBackColor = true;
            this.btnSuits.Click += new System.EventHandler(this.btnSuits_Click);
            // 
            // btnItems
            // 
            this.btnItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItems.Location = new System.Drawing.Point(6, 19);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(188, 23);
            this.btnItems.TabIndex = 0;
            this.btnItems.Text = "Items";
            this.btnItems.UseVisualStyleBackColor = true;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 420);
            this.Controls.Add(this.groupTools);
            this.Controls.Add(this.groupSettings);
            this.Controls.Add(this.groupFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Talisman Game Tools";
            this.groupFolder.ResumeLayout(false);
            this.groupFolder.PerformLayout();
            this.groupSettings.ResumeLayout(false);
            this.groupSettings.PerformLayout();
            this.groupTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.GroupBox groupSettings;
        private System.Windows.Forms.LinkLabel linkForum;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtSettings;
        private System.Windows.Forms.GroupBox groupTools;
        private System.Windows.Forms.Button btnPrivileges;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnTasks;
        private System.Windows.Forms.Button btnNPCs;
        private System.Windows.Forms.Button btnSuits;
        private System.Windows.Forms.Button btnItems;
        private System.Windows.Forms.Button btnLock;
    }
}

