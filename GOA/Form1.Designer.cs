namespace GOA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.org_name = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.block1_1 = new GOA.Block();
            this.addPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.CharAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.FuncAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
            this.StructAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
            this.OrgAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand5 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand4 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand4 = new System.Data.OleDb.OleDbCommand();
            this.BlockAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.dataSet1 = new GOA.DataSet1();
            this.OpenStruc = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // org_name
            // 
            this.org_name.Location = new System.Drawing.Point(254, 12);
            this.org_name.Multiline = true;
            this.org_name.Name = "org_name";
            this.org_name.Size = new System.Drawing.Size(631, 36);
            this.org_name.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.addPage);
            this.tabControl.Location = new System.Drawing.Point(12, 64);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1129, 554);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 5;
            this.tabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.block1_1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1121, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Оргструктура1";
            // 
            // block1_1
            // 
            this.block1_1.BackColor = System.Drawing.SystemColors.Control;
            this.block1_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.block1_1.Location = new System.Drawing.Point(447, 20);
            this.block1_1.Name = "block1_1";
            this.block1_1.Size = new System.Drawing.Size(190, 74);
            this.block1_1.TabIndex = 0;
            // 
            // addPage
            // 
            this.addPage.BackColor = System.Drawing.SystemColors.Control;
            this.addPage.Location = new System.Drawing.Point(4, 22);
            this.addPage.Name = "addPage";
            this.addPage.Size = new System.Drawing.Size(1121, 528);
            this.addPage.TabIndex = 1;
            this.addPage.Text = "+";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(909, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Анализировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1026, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\Системный_анализ\\GOA\\GOA\\bin\\Debu" +
    "g\\MyStructures.mdb";
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT        Характеристики.*\r\nFROM            Характеристики";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO `Характеристики` (`Структура`, `Информационная оценка`, `Число состоя" +
    "ний системы`, `Коэффициент централизации`, `Коэффициент децентрализации`) VALUES" +
    " (?, ?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Структура", System.Data.OleDb.OleDbType.Integer, 0, "Структура"),
            new System.Data.OleDb.OleDbParameter("Информационная_оценка", System.Data.OleDb.OleDbType.Single, 0, "Информационная оценка"),
            new System.Data.OleDb.OleDbParameter("Число_состояний_системы", System.Data.OleDb.OleDbType.Single, 0, "Число состояний системы"),
            new System.Data.OleDb.OleDbParameter("Коэффициент_централизации", System.Data.OleDb.OleDbType.Single, 0, "Коэффициент централизации"),
            new System.Data.OleDb.OleDbParameter("Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Single, 0, "Коэффициент децентрализации")});
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = resources.GetString("oleDbUpdateCommand1.CommandText");
            this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Структура", System.Data.OleDb.OleDbType.Integer, 0, "Структура"),
            new System.Data.OleDb.OleDbParameter("Информационная_оценка", System.Data.OleDb.OleDbType.Single, 0, "Информационная оценка"),
            new System.Data.OleDb.OleDbParameter("Число_состояний_системы", System.Data.OleDb.OleDbType.Single, 0, "Число состояний системы"),
            new System.Data.OleDb.OleDbParameter("Коэффициент_централизации", System.Data.OleDb.OleDbType.Single, 0, "Коэффициент централизации"),
            new System.Data.OleDb.OleDbParameter("Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Single, 0, "Коэффициент децентрализации"),
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Информационная_оценка", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Информационная оценка", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Информационная_оценка", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Информационная оценка", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Число_состояний_системы", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Число состояний системы", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Число_состояний_системы", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Число состояний системы", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Коэффициент_централизации", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Коэффициент централизации", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Коэффициент_централизации", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Коэффициент централизации", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Коэффициент децентрализации", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Коэффициент децентрализации", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = resources.GetString("oleDbDeleteCommand1.CommandText");
            this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Информационная_оценка", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Информационная оценка", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Информационная_оценка", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Информационная оценка", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Число_состояний_системы", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Число состояний системы", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Число_состояний_системы", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Число состояний системы", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Коэффициент_централизации", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Коэффициент централизации", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Коэффициент_централизации", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Коэффициент централизации", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Коэффициент децентрализации", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Коэффициент_децентрализации", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Коэффициент децентрализации", System.Data.DataRowVersion.Original, null)});
            // 
            // CharAdapter
            // 
            this.CharAdapter.DeleteCommand = this.oleDbDeleteCommand1;
            this.CharAdapter.InsertCommand = this.oleDbInsertCommand1;
            this.CharAdapter.SelectCommand = this.oleDbSelectCommand1;
            this.CharAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Характеристики", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код", "Код"),
                        new System.Data.Common.DataColumnMapping("Структура", "Структура"),
                        new System.Data.Common.DataColumnMapping("Информационная оценка", "Информационная оценка"),
                        new System.Data.Common.DataColumnMapping("Число состояний системы", "Число состояний системы"),
                        new System.Data.Common.DataColumnMapping("Коэффициент централизации", "Коэффициент централизации"),
                        new System.Data.Common.DataColumnMapping("Коэффициент децентрализации", "Коэффициент децентрализации")})});
            this.CharAdapter.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT        Функции.*\r\nFROM            Функции";
            this.oleDbSelectCommand2.Connection = this.oleDbConnection1;
            // 
            // oleDbInsertCommand2
            // 
            this.oleDbInsertCommand2.CommandText = "INSERT INTO `Функции` (`Родитель`, `Наименование`, `Блок`) VALUES (?, ?, ?)";
            this.oleDbInsertCommand2.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand2.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Родитель", System.Data.OleDb.OleDbType.Integer, 0, "Родитель"),
            new System.Data.OleDb.OleDbParameter("Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, "Наименование"),
            new System.Data.OleDb.OleDbParameter("Блок", System.Data.OleDb.OleDbType.Integer, 0, "Блок")});
            // 
            // oleDbUpdateCommand2
            // 
            this.oleDbUpdateCommand2.CommandText = resources.GetString("oleDbUpdateCommand2.CommandText");
            this.oleDbUpdateCommand2.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand2.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Родитель", System.Data.OleDb.OleDbType.Integer, 0, "Родитель"),
            new System.Data.OleDb.OleDbParameter("Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, "Наименование"),
            new System.Data.OleDb.OleDbParameter("Блок", System.Data.OleDb.OleDbType.Integer, 0, "Блок"),
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Наименование", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Блок", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Блок", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Блок", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Блок", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbDeleteCommand2
            // 
            this.oleDbDeleteCommand2.CommandText = resources.GetString("oleDbDeleteCommand2.CommandText");
            this.oleDbDeleteCommand2.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand2.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Наименование", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Блок", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Блок", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Блок", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Блок", System.Data.DataRowVersion.Original, null)});
            // 
            // FuncAdapter
            // 
            this.FuncAdapter.DeleteCommand = this.oleDbDeleteCommand2;
            this.FuncAdapter.InsertCommand = this.oleDbInsertCommand2;
            this.FuncAdapter.SelectCommand = this.oleDbSelectCommand2;
            this.FuncAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Функции", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код", "Код"),
                        new System.Data.Common.DataColumnMapping("Родитель", "Родитель"),
                        new System.Data.Common.DataColumnMapping("Наименование", "Наименование"),
                        new System.Data.Common.DataColumnMapping("Блок", "Блок")})});
            this.FuncAdapter.UpdateCommand = this.oleDbUpdateCommand2;
            // 
            // oleDbSelectCommand3
            // 
            this.oleDbSelectCommand3.CommandText = "SELECT        Структуры.*\r\nFROM            Структуры";
            this.oleDbSelectCommand3.Connection = this.oleDbConnection1;
            // 
            // StructAdapter
            // 
            this.StructAdapter.DeleteCommand = this.oleDbDeleteCommand;
            this.StructAdapter.InsertCommand = this.oleDbInsertCommand;
            this.StructAdapter.SelectCommand = this.oleDbSelectCommand3;
            this.StructAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Структуры", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код", "Код"),
                        new System.Data.Common.DataColumnMapping("Организация", "Организация")})});
            this.StructAdapter.UpdateCommand = this.oleDbUpdateCommand;
            // 
            // oleDbDeleteCommand
            // 
            this.oleDbDeleteCommand.CommandText = "DELETE FROM `Структуры` WHERE ((`Код` = ?) AND ((? = 1 AND `Организация` IS NULL)" +
    " OR (`Организация` = ?)))";
            this.oleDbDeleteCommand.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Организация", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Организация", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Организация", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Организация", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbInsertCommand
            // 
            this.oleDbInsertCommand.CommandText = "INSERT INTO `Структуры` (`Организация`) VALUES (?)";
            this.oleDbInsertCommand.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Организация", System.Data.OleDb.OleDbType.Integer, 0, "Организация")});
            // 
            // oleDbUpdateCommand
            // 
            this.oleDbUpdateCommand.CommandText = "UPDATE `Структуры` SET `Организация` = ? WHERE ((`Код` = ?) AND ((? = 1 AND `Орга" +
    "низация` IS NULL) OR (`Организация` = ?)))";
            this.oleDbUpdateCommand.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Организация", System.Data.OleDb.OleDbType.Integer, 0, "Организация"),
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Организация", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Организация", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Организация", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Организация", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbSelectCommand4
            // 
            this.oleDbSelectCommand4.CommandText = "SELECT        Организации.*\r\nFROM            Организации";
            this.oleDbSelectCommand4.Connection = this.oleDbConnection1;
            // 
            // oleDbInsertCommand3
            // 
            this.oleDbInsertCommand3.CommandText = "INSERT INTO `Организации` (`Наименование`) VALUES (?)";
            this.oleDbInsertCommand3.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand3.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, "Наименование")});
            // 
            // oleDbUpdateCommand3
            // 
            this.oleDbUpdateCommand3.CommandText = "UPDATE `Организации` SET `Наименование` = ? WHERE ((`Код` = ?) AND ((? = 1 AND `Н" +
    "аименование` IS NULL) OR (`Наименование` = ?)))";
            this.oleDbUpdateCommand3.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand3.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, "Наименование"),
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Наименование", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbDeleteCommand3
            // 
            this.oleDbDeleteCommand3.CommandText = "DELETE FROM `Организации` WHERE ((`Код` = ?) AND ((? = 1 AND `Наименование` IS NU" +
    "LL) OR (`Наименование` = ?)))";
            this.oleDbDeleteCommand3.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand3.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Наименование", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Наименование", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Наименование", System.Data.DataRowVersion.Original, null)});
            // 
            // OrgAdapter
            // 
            this.OrgAdapter.DeleteCommand = this.oleDbDeleteCommand3;
            this.OrgAdapter.InsertCommand = this.oleDbInsertCommand3;
            this.OrgAdapter.SelectCommand = this.oleDbSelectCommand4;
            this.OrgAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Организации", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код", "Код"),
                        new System.Data.Common.DataColumnMapping("Наименование", "Наименование")})});
            this.OrgAdapter.UpdateCommand = this.oleDbUpdateCommand3;
            // 
            // oleDbSelectCommand5
            // 
            this.oleDbSelectCommand5.CommandText = "SELECT        Блоки.*\r\nFROM            Блоки";
            this.oleDbSelectCommand5.Connection = this.oleDbConnection1;
            // 
            // oleDbInsertCommand4
            // 
            this.oleDbInsertCommand4.CommandText = "INSERT INTO `Блоки` (`Структура`, `Уровень`, `Номер на уровне`, `Имя`, `Родитель`" +
    ", `Тип`) VALUES (?, ?, ?, ?, ?, ?)";
            this.oleDbInsertCommand4.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand4.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Структура", System.Data.OleDb.OleDbType.Integer, 0, "Структура"),
            new System.Data.OleDb.OleDbParameter("Уровень", System.Data.OleDb.OleDbType.Integer, 0, "Уровень"),
            new System.Data.OleDb.OleDbParameter("Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, "Номер на уровне"),
            new System.Data.OleDb.OleDbParameter("Имя", System.Data.OleDb.OleDbType.VarWChar, 0, "Имя"),
            new System.Data.OleDb.OleDbParameter("Родитель", System.Data.OleDb.OleDbType.Integer, 0, "Родитель"),
            new System.Data.OleDb.OleDbParameter("Тип", System.Data.OleDb.OleDbType.VarWChar, 0, "Тип")});
            // 
            // oleDbUpdateCommand4
            // 
            this.oleDbUpdateCommand4.CommandText = resources.GetString("oleDbUpdateCommand4.CommandText");
            this.oleDbUpdateCommand4.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand4.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Структура", System.Data.OleDb.OleDbType.Integer, 0, "Структура"),
            new System.Data.OleDb.OleDbParameter("Уровень", System.Data.OleDb.OleDbType.Integer, 0, "Уровень"),
            new System.Data.OleDb.OleDbParameter("Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, "Номер на уровне"),
            new System.Data.OleDb.OleDbParameter("Имя", System.Data.OleDb.OleDbType.VarWChar, 0, "Имя"),
            new System.Data.OleDb.OleDbParameter("Родитель", System.Data.OleDb.OleDbType.Integer, 0, "Родитель"),
            new System.Data.OleDb.OleDbParameter("Тип", System.Data.OleDb.OleDbType.VarWChar, 0, "Тип"),
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Уровень", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Уровень", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Уровень", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Уровень", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Номер на уровне", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Номер на уровне", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Имя", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Имя", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Имя", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Имя", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Тип", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Тип", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Тип", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Тип", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbDeleteCommand4
            // 
            this.oleDbDeleteCommand4.CommandText = resources.GetString("oleDbDeleteCommand4.CommandText");
            this.oleDbDeleteCommand4.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand4.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_Код", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Код", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Структура", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Структура", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Уровень", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Уровень", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Уровень", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Уровень", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Номер на уровне", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Номер_на_уровне", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Номер на уровне", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Имя", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Имя", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Имя", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Имя", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Родитель", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Родитель", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("IsNull_Тип", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Тип", System.Data.DataRowVersion.Original, true, null),
            new System.Data.OleDb.OleDbParameter("Original_Тип", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Тип", System.Data.DataRowVersion.Original, null)});
            // 
            // BlockAdapter
            // 
            this.BlockAdapter.DeleteCommand = this.oleDbDeleteCommand4;
            this.BlockAdapter.InsertCommand = this.oleDbInsertCommand4;
            this.BlockAdapter.SelectCommand = this.oleDbSelectCommand5;
            this.BlockAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Блоки", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Код", "Код"),
                        new System.Data.Common.DataColumnMapping("Структура", "Структура"),
                        new System.Data.Common.DataColumnMapping("Уровень", "Уровень"),
                        new System.Data.Common.DataColumnMapping("Номер на уровне", "Номер на уровне"),
                        new System.Data.Common.DataColumnMapping("Имя", "Имя"),
                        new System.Data.Common.DataColumnMapping("Родитель", "Родитель"),
                        new System.Data.Common.DataColumnMapping("Тип", "Тип")})});
            this.BlockAdapter.UpdateCommand = this.oleDbUpdateCommand4;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // OpenStruc
            // 
            this.OpenStruc.Location = new System.Drawing.Point(956, 39);
            this.OpenStruc.Name = "OpenStruc";
            this.OpenStruc.Size = new System.Drawing.Size(111, 23);
            this.OpenStruc.TabIndex = 8;
            this.OpenStruc.Text = "Открыть";
            this.OpenStruc.UseVisualStyleBackColor = true;
            this.OpenStruc.Click += new System.EventHandler(this.OpenStruc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 631);
            this.Controls.Add(this.OpenStruc);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.org_name);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox org_name;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage addPage;
        private Block block1_1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Data.OleDb.OleDbConnection oleDbConnection1;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        private System.Data.OleDb.OleDbDataAdapter CharAdapter;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        private System.Data.OleDb.OleDbDataAdapter FuncAdapter;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
        private System.Data.OleDb.OleDbDataAdapter StructAdapter;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
        private System.Data.OleDb.OleDbDataAdapter OrgAdapter;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand5;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand4;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand4;
        private System.Data.OleDb.OleDbDataAdapter BlockAdapter;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Button OpenStruc;
    }
}

