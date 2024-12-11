using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace akademine_sistema
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "Server=localhost;Database=akademine_sistema;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrisijungiti_Click(object sender, EventArgs e)
        {
            string vardas = txtVardas.Text.Trim();
            string slaptazodis = txtSlaptazodis.Text.Trim();

            if (string.IsNullOrWhiteSpace(vardas) || string.IsNullOrWhiteSpace(slaptazodis))
            {
                MessageBox.Show("Prašome įvesti vartotojo vardą ir slaptažodį.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, role FROM users WHERE vardas = @vardas AND slaptazodis = @slaptazodis";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vardas", vardas);
                        cmd.Parameters.AddWithValue("@slaptazodis", slaptazodis);

                        int userId = 0;
                        string role = "";
                        bool userFound = false;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = Convert.ToInt32(reader["id"]);
                                role = reader["role"].ToString();
                                userFound = true;
                            }
                            else
                            {
                                MessageBox.Show("Vardas arba slaptažodis neteisingi.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        if (!userFound) return;

                        switch (role)
                        {
                            case "admin":
                                OpenAdminForm();
                                break;
                            case "destytojas":

                                OpenDestytojasForm(userId); 

                                break;
                            case "studentas":
                                OpenStudentasForm(userId);
                                break;
                            default:
                                MessageBox.Show("Nežinoma rolė.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Nepavyko prisijungti prie duomenų bazės: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenAdminForm()
        {
            Admin adminForm = new Admin();
            adminForm.Show();
            this.Hide();
        }

        private void OpenDestytojasForm(int destytojoId)
        {
            destytojas destytojasForm = new destytojas();
            destytojasForm.Show();
            this.Hide();
        }

        private void OpenStudentasForm(int studentId)
        {
            studentas studentasForm = new studentas(studentId);
            studentasForm.Show();
            this.Hide();
        }
    }
}
