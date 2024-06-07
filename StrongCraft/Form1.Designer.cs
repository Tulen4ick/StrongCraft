namespace StrongCraft
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MiniMap_timer = new System.Windows.Forms.Timer(this.components);
            this.ButtonGroup = new System.Windows.Forms.GroupBox();
            this.Extraction = new System.Windows.Forms.Button();
            this.CancelAction = new System.Windows.Forms.PictureBox();
            this.UpTownHall = new System.Windows.Forms.Button();
            this.StopUnit = new System.Windows.Forms.Button();
            this.MoveUnit = new System.Windows.Forms.Button();
            this.CreateArcher = new System.Windows.Forms.Button();
            this.CreateBarracks = new System.Windows.Forms.Button();
            this.CreateFarm = new System.Windows.Forms.Button();
            this.CreateTownHall = new System.Windows.Forms.Button();
            this.CreateFootMan = new System.Windows.Forms.Button();
            this.DeleteWalls = new System.Windows.Forms.Button();
            this.CreateWalls = new System.Windows.Forms.Button();
            this.Information = new System.Windows.Forms.GroupBox();
            this.Training = new System.Windows.Forms.PictureBox();
            this.progressHP = new System.Windows.Forms.PictureBox();
            this.HpOfBlock = new System.Windows.Forms.Label();
            this.InfoValue = new System.Windows.Forms.Label();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.LevelOfBlock = new System.Windows.Forms.Label();
            this.Lvl = new System.Windows.Forms.Label();
            this.NameOfBlock = new System.Windows.Forms.Label();
            this.Portrait = new System.Windows.Forms.PictureBox();
            this.Resources = new System.Windows.Forms.GroupBox();
            this.StoneAmount = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.FoodAmount = new System.Windows.Forms.Label();
            this.WoodAmount = new System.Windows.Forms.Label();
            this.GoldAmount = new System.Windows.Forms.Label();
            this.NecessaryResources = new System.Windows.Forms.GroupBox();
            this.NameOfButton = new System.Windows.Forms.Label();
            this.NessRes2 = new System.Windows.Forms.Label();
            this.NessRes1 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.InGameMessage = new System.Windows.Forms.Label();
            this.MessageTimer = new System.Windows.Forms.Timer(this.components);
            this.BarrackTimer = new System.Windows.Forms.Timer(this.components);
            this.GlobalTimer = new System.Windows.Forms.Timer(this.components);
            this.Moving_Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Moving_Timer2 = new System.Windows.Forms.Timer(this.components);
            this.Moving_Timer3 = new System.Windows.Forms.Timer(this.components);
            this.ActualTime = new System.Windows.Forms.Label();
            this.Enemy_moving1 = new System.Windows.Forms.Timer(this.components);
            this.Enemy_moving2 = new System.Windows.Forms.Timer(this.components);
            this.Enemy_moving3 = new System.Windows.Forms.Timer(this.components);
            this.Enemy_moving4 = new System.Windows.Forms.Timer(this.components);
            this.Attack_timer = new System.Windows.Forms.Timer(this.components);
            this.Spawn_Enemies = new System.Windows.Forms.Timer(this.components);
            this.ButtonGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CancelAction)).BeginInit();
            this.Information.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Training)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Portrait)).BeginInit();
            this.Resources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.NecessaryResources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // MiniMap_timer
            // 
            this.MiniMap_timer.Interval = 15;
            this.MiniMap_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ButtonGroup
            // 
            this.ButtonGroup.BackColor = System.Drawing.Color.Transparent;
            this.ButtonGroup.Controls.Add(this.Extraction);
            this.ButtonGroup.Controls.Add(this.CancelAction);
            this.ButtonGroup.Controls.Add(this.UpTownHall);
            this.ButtonGroup.Controls.Add(this.StopUnit);
            this.ButtonGroup.Controls.Add(this.MoveUnit);
            this.ButtonGroup.Controls.Add(this.CreateArcher);
            this.ButtonGroup.Controls.Add(this.CreateBarracks);
            this.ButtonGroup.Controls.Add(this.CreateFarm);
            this.ButtonGroup.Controls.Add(this.CreateTownHall);
            this.ButtonGroup.Controls.Add(this.CreateFootMan);
            this.ButtonGroup.Controls.Add(this.DeleteWalls);
            this.ButtonGroup.Controls.Add(this.CreateWalls);
            this.ButtonGroup.Location = new System.Drawing.Point(35, 664);
            this.ButtonGroup.Name = "ButtonGroup";
            this.ButtonGroup.Size = new System.Drawing.Size(377, 256);
            this.ButtonGroup.TabIndex = 0;
            this.ButtonGroup.TabStop = false;
            // 
            // Extraction
            // 
            this.Extraction.BackColor = System.Drawing.Color.Black;
            this.Extraction.Image = ((System.Drawing.Image)(resources.GetObject("Extraction.Image")));
            this.Extraction.Location = new System.Drawing.Point(140, 193);
            this.Extraction.Name = "Extraction";
            this.Extraction.Size = new System.Drawing.Size(61, 55);
            this.Extraction.TabIndex = 12;
            this.Extraction.UseVisualStyleBackColor = false;
            this.Extraction.Click += new System.EventHandler(this.Extraction_Click);
            this.Extraction.MouseEnter += new System.EventHandler(this.Extraction_MouseEnter);
            this.Extraction.MouseLeave += new System.EventHandler(this.Extraction_MouseLeave);
            // 
            // CancelAction
            // 
            this.CancelAction.BackColor = System.Drawing.Color.Black;
            this.CancelAction.Image = ((System.Drawing.Image)(resources.GetObject("CancelAction.Image")));
            this.CancelAction.Location = new System.Drawing.Point(310, 195);
            this.CancelAction.Name = "CancelAction";
            this.CancelAction.Size = new System.Drawing.Size(61, 55);
            this.CancelAction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CancelAction.TabIndex = 11;
            this.CancelAction.TabStop = false;
            this.CancelAction.Visible = false;
            this.CancelAction.Click += new System.EventHandler(this.CancelAction_Click);
            // 
            // UpTownHall
            // 
            this.UpTownHall.BackColor = System.Drawing.Color.Black;
            this.UpTownHall.Image = ((System.Drawing.Image)(resources.GetObject("UpTownHall.Image")));
            this.UpTownHall.Location = new System.Drawing.Point(73, 9);
            this.UpTownHall.Name = "UpTownHall";
            this.UpTownHall.Size = new System.Drawing.Size(61, 55);
            this.UpTownHall.TabIndex = 10;
            this.UpTownHall.UseVisualStyleBackColor = false;
            this.UpTownHall.Click += new System.EventHandler(this.UpTownHall_Click);
            this.UpTownHall.MouseEnter += new System.EventHandler(this.UpTownHall_MouseEnter);
            this.UpTownHall.MouseLeave += new System.EventHandler(this.UpTownHall_MouseLeave);
            // 
            // StopUnit
            // 
            this.StopUnit.BackColor = System.Drawing.Color.Black;
            this.StopUnit.Image = ((System.Drawing.Image)(resources.GetObject("StopUnit.Image")));
            this.StopUnit.Location = new System.Drawing.Point(73, 193);
            this.StopUnit.Name = "StopUnit";
            this.StopUnit.Size = new System.Drawing.Size(61, 55);
            this.StopUnit.TabIndex = 8;
            this.StopUnit.UseVisualStyleBackColor = false;
            this.StopUnit.Click += new System.EventHandler(this.StopUnit_Click);
            // 
            // MoveUnit
            // 
            this.MoveUnit.BackColor = System.Drawing.Color.Black;
            this.MoveUnit.Image = ((System.Drawing.Image)(resources.GetObject("MoveUnit.Image")));
            this.MoveUnit.Location = new System.Drawing.Point(6, 193);
            this.MoveUnit.Name = "MoveUnit";
            this.MoveUnit.Size = new System.Drawing.Size(61, 55);
            this.MoveUnit.TabIndex = 7;
            this.MoveUnit.UseVisualStyleBackColor = false;
            this.MoveUnit.Click += new System.EventHandler(this.MoveUnit_Click);
            // 
            // CreateArcher
            // 
            this.CreateArcher.BackColor = System.Drawing.Color.Black;
            this.CreateArcher.CausesValidation = false;
            this.CreateArcher.Image = ((System.Drawing.Image)(resources.GetObject("CreateArcher.Image")));
            this.CreateArcher.Location = new System.Drawing.Point(73, 131);
            this.CreateArcher.Name = "CreateArcher";
            this.CreateArcher.Size = new System.Drawing.Size(61, 55);
            this.CreateArcher.TabIndex = 6;
            this.CreateArcher.UseVisualStyleBackColor = false;
            this.CreateArcher.Click += new System.EventHandler(this.CreateArcher_Click);
            this.CreateArcher.MouseEnter += new System.EventHandler(this.CreateArcher_MouseEnter);
            this.CreateArcher.MouseLeave += new System.EventHandler(this.CreateArcher_MouseLeave);
            // 
            // CreateBarracks
            // 
            this.CreateBarracks.BackColor = System.Drawing.Color.Black;
            this.CreateBarracks.Image = ((System.Drawing.Image)(resources.GetObject("CreateBarracks.Image")));
            this.CreateBarracks.Location = new System.Drawing.Point(6, 70);
            this.CreateBarracks.Name = "CreateBarracks";
            this.CreateBarracks.Size = new System.Drawing.Size(61, 55);
            this.CreateBarracks.TabIndex = 5;
            this.CreateBarracks.UseVisualStyleBackColor = false;
            this.CreateBarracks.Click += new System.EventHandler(this.CreateBarracks_Click);
            this.CreateBarracks.MouseEnter += new System.EventHandler(this.CreateBarracks_MouseEnter);
            this.CreateBarracks.MouseLeave += new System.EventHandler(this.CreateBarracks_MouseLeave);
            // 
            // CreateFarm
            // 
            this.CreateFarm.BackColor = System.Drawing.Color.Black;
            this.CreateFarm.Image = ((System.Drawing.Image)(resources.GetObject("CreateFarm.Image")));
            this.CreateFarm.Location = new System.Drawing.Point(73, 70);
            this.CreateFarm.Name = "CreateFarm";
            this.CreateFarm.Size = new System.Drawing.Size(61, 55);
            this.CreateFarm.TabIndex = 4;
            this.CreateFarm.UseVisualStyleBackColor = false;
            this.CreateFarm.Click += new System.EventHandler(this.CreateFarm_Click);
            this.CreateFarm.MouseEnter += new System.EventHandler(this.CreateFarm_MouseEnter);
            this.CreateFarm.MouseLeave += new System.EventHandler(this.CreateFarm_MouseLeave);
            // 
            // CreateTownHall
            // 
            this.CreateTownHall.BackColor = System.Drawing.Color.Black;
            this.CreateTownHall.Image = ((System.Drawing.Image)(resources.GetObject("CreateTownHall.Image")));
            this.CreateTownHall.Location = new System.Drawing.Point(6, 9);
            this.CreateTownHall.Name = "CreateTownHall";
            this.CreateTownHall.Size = new System.Drawing.Size(61, 55);
            this.CreateTownHall.TabIndex = 3;
            this.CreateTownHall.UseVisualStyleBackColor = false;
            this.CreateTownHall.Click += new System.EventHandler(this.CreateTownHall_Click);
            this.CreateTownHall.MouseEnter += new System.EventHandler(this.CreateTownHall_MouseEnter);
            this.CreateTownHall.MouseLeave += new System.EventHandler(this.CreateTownHall_MouseLeave);
            // 
            // CreateFootMan
            // 
            this.CreateFootMan.BackColor = System.Drawing.Color.Black;
            this.CreateFootMan.CausesValidation = false;
            this.CreateFootMan.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.CreateFootMan.Image = ((System.Drawing.Image)(resources.GetObject("CreateFootMan.Image")));
            this.CreateFootMan.Location = new System.Drawing.Point(6, 131);
            this.CreateFootMan.Name = "CreateFootMan";
            this.CreateFootMan.Size = new System.Drawing.Size(61, 55);
            this.CreateFootMan.TabIndex = 2;
            this.CreateFootMan.UseVisualStyleBackColor = false;
            this.CreateFootMan.Click += new System.EventHandler(this.CreateFootMan_Click);
            this.CreateFootMan.MouseEnter += new System.EventHandler(this.CreateFootMan_MouseEnter);
            this.CreateFootMan.MouseLeave += new System.EventHandler(this.CreateFootMan_MouseLeave);
            // 
            // DeleteWalls
            // 
            this.DeleteWalls.BackColor = System.Drawing.Color.DarkRed;
            this.DeleteWalls.Image = ((System.Drawing.Image)(resources.GetObject("DeleteWalls.Image")));
            this.DeleteWalls.Location = new System.Drawing.Point(207, 70);
            this.DeleteWalls.Name = "DeleteWalls";
            this.DeleteWalls.Size = new System.Drawing.Size(61, 55);
            this.DeleteWalls.TabIndex = 1;
            this.DeleteWalls.UseVisualStyleBackColor = false;
            this.DeleteWalls.Click += new System.EventHandler(this.DeleteWalls_Click);
            this.DeleteWalls.MouseEnter += new System.EventHandler(this.DeleteWalls_MouseEnter);
            this.DeleteWalls.MouseLeave += new System.EventHandler(this.DeleteWalls_MouseLeave);
            // 
            // CreateWalls
            // 
            this.CreateWalls.BackColor = System.Drawing.Color.Black;
            this.CreateWalls.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CreateWalls.Image = ((System.Drawing.Image)(resources.GetObject("CreateWalls.Image")));
            this.CreateWalls.Location = new System.Drawing.Point(140, 70);
            this.CreateWalls.Name = "CreateWalls";
            this.CreateWalls.Size = new System.Drawing.Size(61, 55);
            this.CreateWalls.TabIndex = 0;
            this.CreateWalls.UseVisualStyleBackColor = false;
            this.CreateWalls.Click += new System.EventHandler(this.CreateWalls_Click);
            this.CreateWalls.Leave += new System.EventHandler(this.CreateWalls_Leave);
            this.CreateWalls.MouseEnter += new System.EventHandler(this.CreateWalls_MouseEnter);
            this.CreateWalls.MouseLeave += new System.EventHandler(this.CreateWalls_MouseLeave);
            // 
            // Information
            // 
            this.Information.BackColor = System.Drawing.Color.Transparent;
            this.Information.Controls.Add(this.Training);
            this.Information.Controls.Add(this.progressHP);
            this.Information.Controls.Add(this.HpOfBlock);
            this.Information.Controls.Add(this.InfoValue);
            this.Information.Controls.Add(this.InfoLabel);
            this.Information.Controls.Add(this.LevelOfBlock);
            this.Information.Controls.Add(this.Lvl);
            this.Information.Controls.Add(this.NameOfBlock);
            this.Information.Controls.Add(this.Portrait);
            this.Information.Location = new System.Drawing.Point(35, 392);
            this.Information.Name = "Information";
            this.Information.Size = new System.Drawing.Size(339, 227);
            this.Information.TabIndex = 1;
            this.Information.TabStop = false;
            // 
            // Training
            // 
            this.Training.Location = new System.Drawing.Point(241, 96);
            this.Training.Name = "Training";
            this.Training.Size = new System.Drawing.Size(92, 76);
            this.Training.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Training.TabIndex = 9;
            this.Training.TabStop = false;
            // 
            // progressHP
            // 
            this.progressHP.Location = new System.Drawing.Point(6, 93);
            this.progressHP.Name = "progressHP";
            this.progressHP.Size = new System.Drawing.Size(92, 10);
            this.progressHP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.progressHP.TabIndex = 8;
            this.progressHP.TabStop = false;
            // 
            // HpOfBlock
            // 
            this.HpOfBlock.AutoSize = true;
            this.HpOfBlock.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HpOfBlock.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.HpOfBlock.Location = new System.Drawing.Point(2, 106);
            this.HpOfBlock.Name = "HpOfBlock";
            this.HpOfBlock.Size = new System.Drawing.Size(105, 21);
            this.HpOfBlock.TabIndex = 7;
            this.HpOfBlock.Text = "Hp / MaxHp";
            // 
            // InfoValue
            // 
            this.InfoValue.AutoSize = true;
            this.InfoValue.Font = new System.Drawing.Font("Old English Text MT", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoValue.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.InfoValue.Location = new System.Drawing.Point(242, 175);
            this.InfoValue.Name = "InfoValue";
            this.InfoValue.Size = new System.Drawing.Size(282, 33);
            this.InfoValue.TabIndex = 5;
            this.InfoValue.Text = "Attack/Resources Info";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Old English Text MT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.InfoLabel.Location = new System.Drawing.Point(6, 175);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(166, 33);
            this.InfoLabel.TabIndex = 4;
            this.InfoLabel.Text = "Information";
            // 
            // LevelOfBlock
            // 
            this.LevelOfBlock.AutoSize = true;
            this.LevelOfBlock.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelOfBlock.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LevelOfBlock.Location = new System.Drawing.Point(203, 68);
            this.LevelOfBlock.Name = "LevelOfBlock";
            this.LevelOfBlock.Size = new System.Drawing.Size(96, 24);
            this.LevelOfBlock.TabIndex = 3;
            this.LevelOfBlock.Text = "NowLevel";
            // 
            // Lvl
            // 
            this.Lvl.AutoSize = true;
            this.Lvl.Font = new System.Drawing.Font("Old English Text MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lvl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Lvl.Location = new System.Drawing.Point(136, 68);
            this.Lvl.Name = "Lvl";
            this.Lvl.Size = new System.Drawing.Size(59, 24);
            this.Lvl.TabIndex = 2;
            this.Lvl.Text = "Level";
            // 
            // NameOfBlock
            // 
            this.NameOfBlock.AutoSize = true;
            this.NameOfBlock.Font = new System.Drawing.Font("Old English Text MT", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameOfBlock.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.NameOfBlock.Location = new System.Drawing.Point(124, 18);
            this.NameOfBlock.Name = "NameOfBlock";
            this.NameOfBlock.Size = new System.Drawing.Size(189, 35);
            this.NameOfBlock.TabIndex = 1;
            this.NameOfBlock.Text = "Name of block";
            // 
            // Portrait
            // 
            this.Portrait.Location = new System.Drawing.Point(6, 10);
            this.Portrait.Name = "Portrait";
            this.Portrait.Size = new System.Drawing.Size(92, 76);
            this.Portrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Portrait.TabIndex = 0;
            this.Portrait.TabStop = false;
            // 
            // Resources
            // 
            this.Resources.BackColor = System.Drawing.Color.Transparent;
            this.Resources.Controls.Add(this.StoneAmount);
            this.Resources.Controls.Add(this.pictureBox6);
            this.Resources.Controls.Add(this.pictureBox5);
            this.Resources.Controls.Add(this.pictureBox4);
            this.Resources.Controls.Add(this.pictureBox3);
            this.Resources.Controls.Add(this.FoodAmount);
            this.Resources.Controls.Add(this.WoodAmount);
            this.Resources.Controls.Add(this.GoldAmount);
            this.Resources.Location = new System.Drawing.Point(411, 49);
            this.Resources.Name = "Resources";
            this.Resources.Size = new System.Drawing.Size(121, 157);
            this.Resources.TabIndex = 2;
            this.Resources.TabStop = false;
            // 
            // StoneAmount
            // 
            this.StoneAmount.AutoSize = true;
            this.StoneAmount.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoneAmount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.StoneAmount.Location = new System.Drawing.Point(47, 126);
            this.StoneAmount.Name = "StoneAmount";
            this.StoneAmount.Size = new System.Drawing.Size(51, 21);
            this.StoneAmount.TabIndex = 7;
            this.StoneAmount.Text = "Stone";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(7, 120);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(27, 27);
            this.pictureBox6.TabIndex = 6;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(7, 86);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(27, 27);
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(7, 52);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(27, 27);
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(27, 27);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // FoodAmount
            // 
            this.FoodAmount.AutoSize = true;
            this.FoodAmount.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoodAmount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FoodAmount.Location = new System.Drawing.Point(47, 92);
            this.FoodAmount.Name = "FoodAmount";
            this.FoodAmount.Size = new System.Drawing.Size(46, 21);
            this.FoodAmount.TabIndex = 2;
            this.FoodAmount.Text = "Food";
            // 
            // WoodAmount
            // 
            this.WoodAmount.AutoSize = true;
            this.WoodAmount.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WoodAmount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.WoodAmount.Location = new System.Drawing.Point(44, 57);
            this.WoodAmount.Name = "WoodAmount";
            this.WoodAmount.Size = new System.Drawing.Size(49, 21);
            this.WoodAmount.TabIndex = 1;
            this.WoodAmount.Text = "Wood";
            // 
            // GoldAmount
            // 
            this.GoldAmount.AutoSize = true;
            this.GoldAmount.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoldAmount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.GoldAmount.Location = new System.Drawing.Point(44, 24);
            this.GoldAmount.Name = "GoldAmount";
            this.GoldAmount.Size = new System.Drawing.Size(43, 21);
            this.GoldAmount.TabIndex = 0;
            this.GoldAmount.Text = "Gold";
            // 
            // NecessaryResources
            // 
            this.NecessaryResources.BackColor = System.Drawing.Color.Transparent;
            this.NecessaryResources.Controls.Add(this.NameOfButton);
            this.NecessaryResources.Controls.Add(this.NessRes2);
            this.NecessaryResources.Controls.Add(this.NessRes1);
            this.NecessaryResources.Controls.Add(this.pictureBox8);
            this.NecessaryResources.Controls.Add(this.pictureBox7);
            this.NecessaryResources.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.NecessaryResources.Location = new System.Drawing.Point(418, 874);
            this.NecessaryResources.Name = "NecessaryResources";
            this.NecessaryResources.Size = new System.Drawing.Size(552, 46);
            this.NecessaryResources.TabIndex = 3;
            this.NecessaryResources.TabStop = false;
            // 
            // NameOfButton
            // 
            this.NameOfButton.AutoSize = true;
            this.NameOfButton.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameOfButton.Location = new System.Drawing.Point(338, 11);
            this.NameOfButton.Name = "NameOfButton";
            this.NameOfButton.Size = new System.Drawing.Size(121, 21);
            this.NameOfButton.TabIndex = 7;
            this.NameOfButton.Text = "Name of Button";
            // 
            // NessRes2
            // 
            this.NessRes2.AutoSize = true;
            this.NessRes2.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NessRes2.Location = new System.Drawing.Point(192, 11);
            this.NessRes2.Name = "NessRes2";
            this.NessRes2.Size = new System.Drawing.Size(125, 21);
            this.NessRes2.TabIndex = 6;
            this.NessRes2.Text = "Second Resource";
            // 
            // NessRes1
            // 
            this.NessRes1.AutoSize = true;
            this.NessRes1.Font = new System.Drawing.Font("Old English Text MT", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NessRes1.Location = new System.Drawing.Point(39, 11);
            this.NessRes1.Name = "NessRes1";
            this.NessRes1.Size = new System.Drawing.Size(114, 21);
            this.NessRes1.TabIndex = 5;
            this.NessRes1.Text = "First Resource";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Location = new System.Drawing.Point(159, 11);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(27, 27);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 4;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Location = new System.Drawing.Point(6, 11);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(27, 27);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 0;
            this.pictureBox7.TabStop = false;
            // 
            // InGameMessage
            // 
            this.InGameMessage.AutoSize = true;
            this.InGameMessage.BackColor = System.Drawing.Color.Transparent;
            this.InGameMessage.Font = new System.Drawing.Font("Old English Text MT", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InGameMessage.ForeColor = System.Drawing.Color.Gold;
            this.InGameMessage.Location = new System.Drawing.Point(37, 938);
            this.InGameMessage.Name = "InGameMessage";
            this.InGameMessage.Size = new System.Drawing.Size(94, 27);
            this.InGameMessage.TabIndex = 4;
            this.InGameMessage.Text = "Message";
            this.InGameMessage.Visible = false;
            // 
            // MessageTimer
            // 
            this.MessageTimer.Interval = 3000;
            this.MessageTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // BarrackTimer
            // 
            this.BarrackTimer.Interval = 50;
            this.BarrackTimer.Tick += new System.EventHandler(this.BarrackTimer_Tick);
            // 
            // GlobalTimer
            // 
            this.GlobalTimer.Interval = 1000;
            this.GlobalTimer.Tick += new System.EventHandler(this.GlobalTimer_Tick);
            // 
            // Moving_Timer1
            // 
            this.Moving_Timer1.Interval = 67;
            this.Moving_Timer1.Tick += new System.EventHandler(this.Moving_Timer1_Tick);
            // 
            // Moving_Timer2
            // 
            this.Moving_Timer2.Interval = 67;
            this.Moving_Timer2.Tick += new System.EventHandler(this.Moving_Timer2_Tick);
            // 
            // Moving_Timer3
            // 
            this.Moving_Timer3.Interval = 67;
            this.Moving_Timer3.Tick += new System.EventHandler(this.Moving_Timer3_Tick);
            // 
            // ActualTime
            // 
            this.ActualTime.AutoSize = true;
            this.ActualTime.BackColor = System.Drawing.Color.Transparent;
            this.ActualTime.Font = new System.Drawing.Font("Old English Text MT", 19.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActualTime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ActualTime.Location = new System.Drawing.Point(411, 228);
            this.ActualTime.Name = "ActualTime";
            this.ActualTime.Size = new System.Drawing.Size(110, 39);
            this.ActualTime.TabIndex = 5;
            this.ActualTime.Text = "00 : 00";
            // 
            // Enemy_moving1
            // 
            this.Enemy_moving1.Tick += new System.EventHandler(this.Enemy_moving1_Tick);
            // 
            // Enemy_moving2
            // 
            this.Enemy_moving2.Tick += new System.EventHandler(this.Enemy_moving2_Tick);
            // 
            // Enemy_moving3
            // 
            this.Enemy_moving3.Tick += new System.EventHandler(this.Enemy_moving3_Tick);
            // 
            // Enemy_moving4
            // 
            this.Enemy_moving4.Tick += new System.EventHandler(this.Enemy_moving4_Tick);
            // 
            // Attack_timer
            // 
            this.Attack_timer.Interval = 700;
            this.Attack_timer.Tick += new System.EventHandler(this.Attack_timer_Tick);
            // 
            // Spawn_Enemies
            // 
            this.Spawn_Enemies.Interval = 60000;
            this.Spawn_Enemies.Tick += new System.EventHandler(this.Spawn_Enemies_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1362, 977);
            this.Controls.Add(this.ActualTime);
            this.Controls.Add(this.InGameMessage);
            this.Controls.Add(this.NecessaryResources);
            this.Controls.Add(this.Resources);
            this.Controls.Add(this.Information);
            this.Controls.Add(this.ButtonGroup);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "StrongCraft";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ButtonGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CancelAction)).EndInit();
            this.Information.ResumeLayout(false);
            this.Information.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Training)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Portrait)).EndInit();
            this.Resources.ResumeLayout(false);
            this.Resources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.NecessaryResources.ResumeLayout(false);
            this.NecessaryResources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer MiniMap_timer;
        private System.Windows.Forms.GroupBox ButtonGroup;
        private System.Windows.Forms.Button CreateWalls;
        private System.Windows.Forms.Button DeleteWalls;
        private System.Windows.Forms.Button CreateFootMan;
        private System.Windows.Forms.Button CreateBarracks;
        private System.Windows.Forms.Button CreateFarm;
        private System.Windows.Forms.Button CreateTownHall;
        private System.Windows.Forms.Button StopUnit;
        private System.Windows.Forms.Button MoveUnit;
        private System.Windows.Forms.Button CreateArcher;
        private System.Windows.Forms.Button UpTownHall;
        private System.Windows.Forms.GroupBox Information;
        private System.Windows.Forms.Label LevelOfBlock;
        private System.Windows.Forms.Label Lvl;
        private System.Windows.Forms.Label NameOfBlock;
        private System.Windows.Forms.PictureBox Portrait;
        private System.Windows.Forms.Label InfoValue;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Label HpOfBlock;
        private System.Windows.Forms.GroupBox Resources;
        private System.Windows.Forms.Label GoldAmount;
        private System.Windows.Forms.Label FoodAmount;
        private System.Windows.Forms.Label WoodAmount;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label StoneAmount;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.GroupBox NecessaryResources;
        private System.Windows.Forms.Label NessRes2;
        private System.Windows.Forms.Label NessRes1;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox progressHP;
        private System.Windows.Forms.PictureBox CancelAction;
        private System.Windows.Forms.Label InGameMessage;
        private System.Windows.Forms.Timer MessageTimer;
        private System.Windows.Forms.Timer BarrackTimer;
        private System.Windows.Forms.PictureBox Training;
        private System.Windows.Forms.Label NameOfButton;
        private System.Windows.Forms.Timer GlobalTimer;
        private System.Windows.Forms.Timer Moving_Timer1;
        private System.Windows.Forms.Timer Moving_Timer2;
        private System.Windows.Forms.Timer Moving_Timer3;
        private System.Windows.Forms.Label ActualTime;
        private System.Windows.Forms.Button Extraction;
        private System.Windows.Forms.Timer Enemy_moving1;
        private System.Windows.Forms.Timer Enemy_moving2;
        private System.Windows.Forms.Timer Enemy_moving3;
        private System.Windows.Forms.Timer Enemy_moving4;
        private System.Windows.Forms.Timer Attack_timer;
        private System.Windows.Forms.Timer Spawn_Enemies;
    }
}

