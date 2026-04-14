using SQLite;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace PizzaLend
{
    public partial class PizzaTime : Form
    {
        public PizzaTime()
        {
            InitializeComponent();
            lblLoginError.Visible = false;
            lblRegisterError.Visible = false;
            tabOrder.Enabled = false;
            tabPay.Enabled = false;
        }

        private void PizzaTime_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Variables.filePath))
            {
                var db = new SQLiteConnection(Variables.filePath);
                db.CreateTable<DBusers>();
                db.Close();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string name = tBxRegisterName.Text;
            string email = tBxRegisterEmail.Text;
            string password = tBxRegisterPassword.Text;

            if (string.IsNullOrEmpty(name)) tBxRegisterName.BackColor = Color.Red;
            else tBxRegisterName.BackColor = Color.White;

            if (string.IsNullOrEmpty(email)) tBxRegisterEmail.BackColor = Color.Red;
            else tBxRegisterEmail.BackColor = Color.White;

            if (string.IsNullOrEmpty(password)) tBxRegisterPassword.BackColor = Color.Red;
            else tBxRegisterPassword.BackColor = Color.White;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

            DBusers newUser = new DBusers(name, email, password);

            var db = new SQLiteConnection(Variables.filePath);

            var users = db.Table<DBusers>().ToList();

            foreach (var user in users)
            {
                if (user.Email == newUser.Email)
                {
                    lblRegisterError.Visible = true;
                    return;
                }
            }

            lblRegisterError.Visible = false;
            db.Insert(newUser);
            db.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tBxEmail.Text;
            string password = tBxPassword.Text;

            if (string.IsNullOrEmpty(email)) tBxEmail.BackColor = Color.Red;
            else tBxEmail.BackColor = Color.White;

            if (string.IsNullOrEmpty(password)) tBxPassword.BackColor = Color.Red;
            else tBxPassword.BackColor = Color.White;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

            var db = new SQLiteConnection(Variables.filePath);
            var users = db.Table<DBusers>().Where(u => u.Email == email && u.Password == password).ToList();

            if (users.Count == 0) return;
            else
            {
                tabOrder.Enabled = true;
                tabPay.Enabled = true;
                tabControl.SelectTab(2);
            }
        }

        private void btnAddPizza_Click(object sender, EventArgs e)
        {
            string typeOfPizza = comboBoxPizzas.Text;
            int count = (int)numericUpDownCount.Value;

            if (string.IsNullOrEmpty(typeOfPizza) || count == 0) return;

            List<string> addities = new List<string>();

            foreach (object it in checkedListBoxAddities.CheckedItems)
                addities.Add(it?.ToString() ?? string.Empty);

            Pizza newPizza = new Pizza(typeOfPizza, count, addities);
            comboBoxOrder.Items.Add(newPizza.GetFullPizza());
            Variables.pizzas.Add(newPizza);
        }

        private void checkedListBoxAddities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOrderPizza_Click(object sender, EventArgs e)
        {
            if (Variables.pizzas.Count == 0)
                MessageBox.Show($"Order is empty.");

            string message = string.Empty;

            foreach (Pizza pizza in Variables.pizzas)
                message += pizza.GetFullPizza();

            tabControl.SelectTab(3);
            MessageBox.Show($"Accept of order: \n {message}");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOrderPay_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButtonCard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCard.Checked) Variables.paymentMethod = "card";
            else if (radioButtonCash.Checked) Variables.paymentMethod = "cash";
        }
    }
}
